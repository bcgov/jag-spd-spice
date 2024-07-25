using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Gov.Jag.Spice.Interfaces.SharePoint
{
    public class SharePointHealthCheck : IHealthCheck
    {
        private readonly IConfiguration _configuration;

        public SharePointHealthCheck(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default(CancellationToken))
        {
            FileManager sharePoint = new FileManager(_configuration);
            // Try and get the Account document library
            bool healthCheckResultHealthy;
            try
            {
                healthCheckResultHealthy = sharePoint.TestStatus().GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                healthCheckResultHealthy = false;
            }

            if (healthCheckResultHealthy)
            {
                return Task.FromResult(HealthCheckResult.Healthy("SharePoint is healthy."));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("SharePoint is unhealthy."));
            }
        }
    }
}
