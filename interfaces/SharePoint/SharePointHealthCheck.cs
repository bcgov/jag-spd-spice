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
        private readonly ISharePointFileManager _sharePointFileManager;

        public SharePointHealthCheck(ISharePointFileManager sharePointFileManager)
        {
            _sharePointFileManager = sharePointFileManager;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default(CancellationToken))
        {
            // Try and get the Account document library
            bool healthCheckResultHealthy;
            try
            {
                healthCheckResultHealthy = await _sharePointFileManager.TestStatus();
            }
            catch (Exception)
            {
                healthCheckResultHealthy = false;
            }

            if (healthCheckResultHealthy)
            {
                return HealthCheckResult.Healthy("SharePoint is healthy.");
            }
            else
            {
                return HealthCheckResult.Unhealthy("SharePoint is unhealthy.");
            }
        }
    }
}
