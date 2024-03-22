﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Hangfire;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using Gov.Jag.Spice.Interfaces;
using Gov.Jag.Spice.Interfaces.SharePoint;
using SpiceCarlaSync.models;
using Gov.Lclb.Cllb.Interfaces.Models;

namespace Gov.Jag.Spice.CarlaSync.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkerScreeningsController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private FileManager _sharepoint;
        private IDynamicsClient _dynamicsClient;
        private CarlaUtils _carlaUtils;

        public WorkerScreeningsController (IConfiguration configuration, ILoggerFactory loggerFactory, FileManager sharepoint, IServiceProvider serviceProvider)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger(typeof(WorkerScreeningsController));
            _sharepoint = sharepoint;
            _dynamicsClient = (IDynamicsClient)serviceProvider.GetService(typeof(IDynamicsClient));
            _carlaUtils = new CarlaUtils(Configuration, _loggerFactory, _sharepoint);
        }

        /// <summary>
        /// Receive an incomplete worker screening from the CARLA system
        /// </summary>
        /// <returns>OK if successful</returns>
        [HttpPost("receive")]
        public ActionResult ReceiveWorkerScreenings([FromBody] List<IncompleteWorkerScreening> requests)
        {
            // Process the updates received from the SPICE system.
            BackgroundJob.Enqueue(() => new CarlaUtils(Configuration, _loggerFactory, _sharepoint).ReceiveWorkerImportJob(null, requests));
            if (!string.IsNullOrEmpty(Configuration["DYNAMICS_ODATA_URI"]))
            {
                BackgroundJob.Enqueue(() => new DynamicsUtils(Configuration, _loggerFactory, _dynamicsClient).ImportWorkerRequests(null, requests));
            }

            _logger.LogInformation("Started receive worker screening import job");
            return Ok();
        }       

        /*/// <summary>
        /// Send a completed worker screening to the CARLA system for test purposes.  Normally this would occur from a polling process.
        /// </summary>
        /// <returns></returns>
        [HttpPost("send")]
        public ActionResult SendWorkerScreeningResults([FromBody] CompletedWorkerScreening result)
        {
            List<CompletedWorkerScreening> payload = new List<CompletedWorkerScreening>()
            {
                result
            };

            //Send the result to CARLA
            BackgroundJob.Enqueue(() => new CarlaUtils(Configuration, _loggerFactory, _sharepoint).SendWorkerScreeningResult(payload));
            _logger.LogInformation($"Started send Worker Screening result for job: {result.RecordIdentifier}");
            return Ok();
        }*/
    }
}
