using Gov.Jag.Spice.Interfaces;
using Gov.Jag.Spice.Interfaces.SharePoint;
using Gov.Lclb.Cllb.Interfaces;
using Hangfire;
using Hangfire.Console;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Splunk;
using Splunk.Configurations;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

[assembly: ApiController]
namespace Gov.Jag.Spice.CarlaSync
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc(config =>
            {
                if (!string.IsNullOrEmpty(Configuration["JWT_TOKEN_KEY"]))
                {
                    var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                    config.Filters.Add(new AuthorizeFilter(policy));
                }
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Other ConfigureServices() code...

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "JAG SPICE to CARLA Transfer Service", Version = "v1" });
                c.DescribeAllEnumsAsStrings();
                c.SchemaFilter<EnumTypeSchemaFilter>();
            });

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddDefaultTokenProviders();

            if (!string.IsNullOrEmpty(Configuration["JWT_TOKEN_KEY"]))
            {
                // Configure JWT authentication
                services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(o =>
                {
                    o.SaveToken = true;
                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        RequireExpirationTime = false,
                        ValidIssuer = Configuration["JWT_VALID_ISSUER"],
                        ValidAudience = Configuration["JWT_VALID_AUDIENCE"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT_TOKEN_KEY"]))
                    };
                });
            }

            // Setup Dynamics
            if (!string.IsNullOrEmpty(Configuration["DYNAMICS_ODATA_URI"]))
            {
                services.AddTransient(serviceProvider =>
                {
                    IDynamicsClient client = DynamicsSetupUtil.SetupDynamics(Configuration);
                    return client;
                });
            }

            if (!string.IsNullOrEmpty(Configuration["SHAREPOINT_ODATA_URI"]))
            {
                SetupSharePoint(services);
            }

            services.AddHangfire(config =>
            {
                // Change this line if you wish to have Hangfire use persistent storage.
                config.UseMemoryStorage();
                // enable console logs for jobs
                
                config.UseConsole();
            });

            // health checks. 
            services.AddHealthChecks(checks =>
            {
                checks.AddValueTaskCheck("HTTP Endpoint", () => new
                    ValueTask<IHealthCheckResult>(HealthCheckResult.Healthy("Ok")));
            });
        }

        private void SetupSharePoint(IServiceCollection services)
        {            
            services.AddTransient(_ => new FileManager(Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            bool startHangfire = true;
#if DEBUG
            // do not start Hangfire if we are running tests.        
            foreach (var assem in Assembly.GetEntryAssembly().GetReferencedAssemblies())
            {
                if (assem.FullName.ToLowerInvariant().StartsWith("xunit"))
                {
                    startHangfire = false;
                    break;
                }
            }
#endif

            if (startHangfire)
            {
                // enable Hangfire, using the default authentication model (local connections only)
                app.UseHangfireServer();

                GlobalJobFilters.Filters.Add(new ProlongExpirationTimeAttribute());

                DashboardOptions dashboardOptions = new DashboardOptions
                {
                    AppPath = null
                };

                app.UseHangfireDashboard("/hangfire", dashboardOptions);
            }

            if (!string.IsNullOrEmpty(Configuration["ENABLE_HANGFIRE_JOBS"]))
            {
                SetupHangfireJobs(app, loggerFactory);
            }

            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JAG SPICE to CARLA Transfer Service");
            });

            // enable Splunk logger
            if (!string.IsNullOrEmpty(Configuration["SPLUNK_COLLECTOR_URL"]))
            {
                var splunkLoggerConfiguration = GetSplunkLoggerConfiguration(app);

                //Append Http Json logger
                loggerFactory.AddHECJsonSplunkLogger(splunkLoggerConfiguration);
            }

        }

        SplunkLoggerConfiguration GetSplunkLoggerConfiguration(IApplicationBuilder app)
        {
            SplunkLoggerConfiguration result = null;
            string splunkCollectorUrl = Configuration["SPLUNK_COLLECTOR_URL"];
            if (!string.IsNullOrEmpty(splunkCollectorUrl))
            {
                string splunkToken = Configuration["SPLUNK_TOKEN"];
                if (!string.IsNullOrEmpty(splunkToken))
                {
                    result = new SplunkLoggerConfiguration()
                    {
                        HecConfiguration = new HECConfiguration()
                        {
                            BatchIntervalInMilliseconds = 5000,
                            BatchSizeCount = 10,
                            ChannelIdType = HECConfiguration.ChannelIdOption.None,
                            DefaultTimeoutInMilliseconds = 10000,

                            SplunkCollectorUrl = splunkCollectorUrl,
                            Token = splunkToken,
                            UseAuthTokenAsQueryString = false
                        }
                    };
                }
            }
            return result;
        }

        /// <summary>
        /// Setup the Hangfire jobs.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="loggerFactory"></param>
        private void SetupHangfireJobs(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            ILogger log = loggerFactory.CreateLogger(typeof(Startup));
            log.LogInformation("Starting setup of Hangfire jobs ...");

            try
            {
                using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    log.LogInformation("Creating Hangfire job for Send Results jobs ...");
                    RecurringJob.AddOrUpdate(() => new CarlaUtils(Configuration, loggerFactory, serviceScope.ServiceProvider.GetRequiredService<FileManager>()).ProcessResults(null), "*/5 * * * *"); // Run every 5 minutes
                    // Process Results in Dynamics
                    // IDynamicsClient dynamics = DynamicsSetupUtil.SetupDynamics(Configuration);
                    // RecurringJob.AddOrUpdate(() => new DynamicsUtils(Configuration, loggerFactory, dynamics).ProcessBusinessResults(null), Cron.MinuteInterval(5)); // Run every 5 minutes
                    log.LogInformation("Hangfire Send Results jobs created.");
                }
            }
            catch (Exception e)
            {
                StringBuilder msg = new StringBuilder();
                msg.AppendLine("Failed to setup Hangfire job.");
                log.LogCritical(new EventId(-1, "Hangfire job setup failed"), e, msg.ToString());
            }
        }
    }
}
