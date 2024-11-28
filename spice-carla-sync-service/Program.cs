using System;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Gov.Jag.Spice.CarlaSync
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            CreateWebHostBuilder(args)
                .Build()
                .Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.ClearProviders();
                logging.AddConsole(x =>
                {
                    x.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
                    x.IncludeScopes = true;
                });
                logging.SetMinimumLevel(LogLevel.Debug);
                logging.AddDebug();
                logging.AddEventSourceLogger();
            })
            .UseSerilog()
            .UseStartup<Startup>()
            .UseKestrel(options =>
             {
                 options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(5);
                 options.Limits.MaxRequestBodySize = 512 * 1024 * 1024; // allow large transfers
                                                                        // for macOS local dev but don't have env
                                                                        // options.ListenLocalhost(5001, o => {
                                                                        //     o.Protocols = HttpProtocols.Http2;
                                                                        // });
             });

        }
           

    }
}
