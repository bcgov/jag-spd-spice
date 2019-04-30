using Gov.Jag.Spice.Interfaces;
using Gov.Jag.Spice.Interfaces.Models;
using Gov.Jag.Spice.CarlaSync;
using Hangfire.Console;
using Hangfire.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SpdSync.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace SpdSync
{
    public class WorkerUpdater
    {
        public ILogger _logger { get; }

        private static readonly HttpClient Client = new HttpClient();
        private readonly string SharePointDocumentTitle = "spd_worker";
        private readonly string SharePointFolderName = "SPD Worker Files";
        
        private IConfiguration Configuration { get; }
        private IDynamicsClient _dynamics;

        public WorkerUpdater(IConfiguration Configuration, ILoggerFactory loggerFactory)
        {
            this.Configuration = Configuration;
            _logger = loggerFactory.CreateLogger(typeof(WorkerUpdater));
            _dynamics = DynamicsUtil.SetupDynamics(Configuration);            
        }

        /// <summary>
        /// Hangfire job to send an export to SPD.
        /// </summary>
        public async Task ReceiveImportJob(PerformContext hangfireContext)
        {
            hangfireContext.WriteLine("Starting Sharepoint Checker Job.");
            _logger.LogError("Starting Sharepoint Checker Job.");

            
        }

        public async Task UpdateSecurityClearance(PerformContext hangfireContext, WorkerScreeningResponse spdResponse, string id)
        {
            var filter = "adoxio_workerjobnumber eq '" + id + "'";
            List<string> expand = new List<string> { "adoxio_WorkerId" };
            MicrosoftDynamicsCRMadoxioPersonalhistorysummary response = null;
            try
            {
                response = _dynamics.Personalhistorysummaries.Get(filter: filter).Value.FirstOrDefault();
            }
            catch (OdataerrorException odee)
            {
                hangfireContext.WriteLine("Unable to get personal history summary.");
                hangfireContext.WriteLine("Request:");
                hangfireContext.WriteLine(odee.Request.Content);
                hangfireContext.WriteLine("Response:");
                hangfireContext.WriteLine(odee.Response.Content);

                _logger.LogError("Unable to get personal history summary.");
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Request.Content);
                throw odee;
            }

            MicrosoftDynamicsCRMadoxioPersonalhistorysummary patchPHS = new MicrosoftDynamicsCRMadoxioPersonalhistorysummary
            {
                AdoxioSecuritystatus = (int)Enum.Parse(typeof(SecurityStatusPicklist), spdResponse.Result, true),
                AdoxioCompletedon = spdResponse.DateProcessed
            };

            try
            {
                await _dynamics.Personalhistorysummaries.UpdateAsync(response.AdoxioPersonalhistorysummaryid, patchPHS);
            }
            catch (OdataerrorException odee)
            {
                hangfireContext.WriteLine("Unable to patch personal history summary.");
                hangfireContext.WriteLine("Request:");
                hangfireContext.WriteLine(odee.Request.Content);
                hangfireContext.WriteLine("Response:");
                hangfireContext.WriteLine(odee.Response.Content);

                _logger.LogError("Unable to patch personal history summary.");
                _logger.LogError("Request:");
                _logger.LogError(odee.Request.Content);
                _logger.LogError("Response:");
                _logger.LogError(odee.Request.Content);
                throw odee;
            }
        }
    }

    public class FileSystemItem
    {
        public string id { get; set; }
        public string name { get; set; }
        public string documenttype { get; set; }
        public int size { get; set; }
        public string serverrelativeurl { get; set; }
        public DateTime timecreated { get; set; }
        public DateTime timelastmodified { get; set; }
    }
}
