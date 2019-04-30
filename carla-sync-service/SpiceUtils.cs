using Gov.Jag.Spice.Interfaces;
using Gov.Jag.Spice.Interfaces.Models;
using Hangfire.Console;
using Hangfire.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SpdSync;
using SpdSync.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gov.Jag.Spice.CarlaSync
{
    public class SpiceUtils
    {
        public ILogger _logger { get; }

        private IConfiguration Configuration { get; }
        private IDynamicsClient _dynamics;

        public SpiceUtils(IConfiguration Configuration, ILoggerFactory loggerFactory)
        {
            this.Configuration = Configuration;
            _logger = loggerFactory.CreateLogger(typeof(SpdUtils));
            _dynamics = DynamicsUtil.SetupDynamics(Configuration);
        }

        /// <summary>
        /// Hangfire job to receive an import from SPICE.
        /// </summary>
        public void ReceiveWorkerImportJob(PerformContext hangfireContext, List<WorkerScreeningRequest> requests)
        {
            hangfireContext.WriteLine("Starting SPICE Import Job.");
            _logger.LogError("Starting SPICE Import Job.");

            ImportWorkerRequests(hangfireContext, requests);

            hangfireContext.WriteLine("Done.");
            _logger.LogError("Done.");
        }

        /// <summary>
        /// Hangfire job to receive an import from SPICE.
        /// </summary>
        public void ReceiveApplicationImportJob(PerformContext hangfireContext, List<ApplicationScreeningRequest> requests)
        {
            hangfireContext.WriteLine("Starting SPICE Application Screening Import Job.");
            _logger.LogError("Starting SPICE Import Job.");

            ImportApplicationRequests(hangfireContext, requests);

            hangfireContext.WriteLine("Done.");
            _logger.LogError("Done.");
        }

        /// <summary>
        /// Import responses to Dynamics.
        /// </summary>
        /// <returns></returns>
        private void ImportApplicationRequests(PerformContext hangfireContext, List<ApplicationScreeningRequest> requests)
        {
            foreach (ApplicationScreeningRequest WorkerRequest in requests)
            {

                // add data to dynamics

            }
        }

        /// <summary>
        /// Import responses to Dynamics.
        /// </summary>
        /// <returns></returns>
        private void ImportWorkerRequests(PerformContext hangfireContext, List<WorkerScreeningRequest> requests)
        {
            foreach (WorkerScreeningRequest workerResponse in requests)
            {
                // add data to dynamics.
                
            }
        }
    }
}
