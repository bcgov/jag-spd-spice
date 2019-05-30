using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SpdSync;
using System.Collections.Generic;
using SpdSync.models;
using Newtonsoft.Json;
using System;
using Gov.Jag.Spice.Interfaces;

namespace Gov.Jag.Spice.CarlaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerScreeningsController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private SharePointFileManager _sharepoint;

        public WorkerScreeningsController (IConfiguration configuration, ILoggerFactory loggerFactory, SharePointFileManager sharepoint)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger(typeof(WorkerScreeningsController));
            _sharepoint = sharepoint;
        }

        /// <summary>
        /// POST api/spice/receive
        /// Receive a response from the SPICE system
        /// </summary>
        /// <returns>OK if successful</returns>
        [HttpPost("receive")]
        public ActionResult ReceiveWorkerScreenings([FromBody] List<WorkerScreeningRequest> requests)
        {
            _logger.LogError("Received worker screening data");
            string jsonString = JsonConvert.SerializeObject(requests);
            _logger.LogError(jsonString);

            // Process the updates received from the SPICE system.
            BackgroundJob.Enqueue(() => new CarlaUtils(Configuration, _loggerFactory, _sharepoint).ImportWorkerRequestsToSMTP(null, requests));
            _logger.LogInformation("Started receive worker screening job");
            return Ok();
        }       

        /// <summary>
        /// Send a worker record to SPICE for test purposes.  Normally this would occur from a polling process.
        /// </summary>
        /// <returns></returns>
        [HttpPost("send/{workerId}")]
        public ActionResult SendWorkerScreeningResults([FromBody] Gov.Lclb.Cllb.Interfaces.Models.WorkerScreeningResponse result, string workerId )
        {
            result.Result = result.Result.ToUpper();
            if (result.Result != "PASS" && result.Result != "FAIL")
            {
                return BadRequest();
            }
            result.RecordIdentifier = workerId;
            result.DateProcessed = DateTimeOffset.Now;            

            List<Gov.Lclb.Cllb.Interfaces.Models.WorkerScreeningResponse> payload = new List<Gov.Lclb.Cllb.Interfaces.Models.WorkerScreeningResponse>()
            {
                result
            };

            //Send the result to CARLA
            BackgroundJob.Enqueue(() => new CarlaUtils(Configuration, _loggerFactory, _sharepoint).SendWorkerScreeningResult(payload));
            _logger.LogInformation($"Started send Worker Screening result for job: {result.RecordIdentifier}");
            return Ok();
        }


    }
}
