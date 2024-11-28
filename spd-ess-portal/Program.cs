using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Gov.Jag.Spice.Public
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            host.Run();
        }

        private static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;

                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })

            .UseSerilog((hostingContext, loggerConfiguration) => {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext();
                    string splunkCollectorUrl = hostingContext.Configuration["SPLUNK_COLLECTOR_URL"];
                    string splunkToken = hostingContext.Configuration["SPLUNK_TOKEN"];

                    if (!string.IsNullOrEmpty(splunkCollectorUrl) && !string.IsNullOrEmpty(splunkToken))
                    {
                        loggerConfiguration
                            .WriteTo.EventCollector(splunkCollectorUrl, splunkToken, batchIntervalInSeconds: 5, batchSizeLimit: 10);
                    }

            
            }).ConfigureWebHostDefaults( webBuilder =>
            {
                    webBuilder.UseStartup<Startup>();
             });


    }


}
