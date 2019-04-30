﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using SpdSync;
using System.Collections.Generic;
using SpdSync.models;

namespace Gov.Jag.Spice.CarlaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarlaController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;

        public CarlaController(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger(typeof(CarlaController));
        }

        /// <summary>
        /// POST api/spice/receive
        /// Receive a response from the SPICE system
        /// </summary>
        /// <returns>OK if successful</returns>
        [HttpPost("receive")]
        public ActionResult Receive([FromBody] List<WorkerScreeningResponse> responses)
        {
            // Process the updates received from the SPICE system.
            BackgroundJob.Enqueue(() => new SpiceUtils(Configuration, _loggerFactory).ReceiveImportJob(null, responses));
            _logger.LogInformation("Started receive import job");
            return Ok();
        }

        /// <summary>
        /// Send a worker record to SPICE for test purposes.  Normally this would occur from a polling process.
        /// </summary>
        /// <returns></returns>
        [HttpPost("sendWorkerScreening/{workerId}")]
        public ActionResult SendWorker(string workerId )
        {
            // Process the updates received from the SPICE system.
            //BackgroundJob.Enqueue(() => new SpiceUtils(Configuration, _loggerFactory).ReceiveImportJob(null, responses));
            _logger.LogInformation("Started SendWorkerScreening job");
            return Ok();
        }

        /// <summary>
        /// Send a application screening process to SPICE for test purposes.  Normally this would occur from a polling process.
        /// </summary>
        /// <returns></returns>
        [HttpPost("sendApplicationScreening/{applicationId}")]
        public ActionResult SendApplicationScreening(string applicationId)
        {
            // Process the updates received from the SPICE system.
            //BackgroundJob.Enqueue(() => new SpiceUtils(Configuration, _loggerFactory).ReceiveImportJob(null, responses));
            _logger.LogInformation("Started SendApplicationScreening job");
            return Ok();
        }




    }
}
