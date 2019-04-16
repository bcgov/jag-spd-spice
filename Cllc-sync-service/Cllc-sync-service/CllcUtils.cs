using System;
using System.Net.Http;
using Hangfire.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace cllcsyncservice
{
    public class CllcUtils
    {
        public ILogger _logger { get; }

        private IConfiguration Configuration { get; }
        //private IDynamicsClient _dynamics;
        private static readonly HttpClient Client = new HttpClient();

        public CllcUtils(IConfiguration Configuration, ILoggerFactory loggerFactory)
        {
            this.Configuration = Configuration;
            _logger = loggerFactory.CreateLogger(typeof(CllcUtils));
        }

        /// <summary>
        /// Hangfire job to send an export to SPD.
        /// </summary>
        public void SendExportJob(PerformContext hangfireContext)
        {

        }

        /// <summary>
        /// Hangfire job to receive an import from SPD.
        /// </summary>
        public void ReceiveImportJob(PerformContext hangfireContext)
        {
        }
    }
}
