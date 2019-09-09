﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Hangfire;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using Gov.Jag.Spice.Interfaces;
using Gov.Jag.Spice.Interfaces.SharePoint;
using SpiceCarlaSync.models;

namespace Gov.Jag.Spice.CarlaSync.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationScreeningsController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private FileManager _sharepoint;
        private IDynamicsClient _dynamicsClient;

        public ApplicationScreeningsController(IConfiguration configuration, ILoggerFactory loggerFactory, FileManager sharepoint, IServiceProvider serviceProvider)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger(typeof(ApplicationScreeningsController));
            _sharepoint = sharepoint;
            _dynamicsClient = (IDynamicsClient)serviceProvider.GetService(typeof(IDynamicsClient));
        }

        /// <summary>
        /// Receive an incomplete application screening from the CARLA system
        /// </summary>
        /// <returns>OK if successful</returns>
        [HttpPost("receive")]
        public ActionResult ReceiveApplicationScreenings([FromBody] List<IncompleteApplicationScreening> requests)
        {
            // Process the updates received from the SPICE system.
            CarlaUtils _carlaUtils = new CarlaUtils(Configuration, _loggerFactory, _sharepoint);
            _carlaUtils.ReceiveApplicationImportJob(requests);
            if (!string.IsNullOrEmpty(Configuration["DYNAMICS_ODATA_URI"]))
            {
                DynamicsUtils dynamicsUtils = new DynamicsUtils(Configuration, _loggerFactory, _dynamicsClient);
                dynamicsUtils.ImportApplicationRequests(requests);
            }

            _logger.LogInformation("Started receive Application Screenings import job");
            return Ok();
        }


        /// <summary>
        /// Send a completed application screening to the CARLA system for test purposes.  Normally this would occur from a polling process.
        /// </summary>
        /// <returns></returns>
        // [HttpPost("send/{applicationId}")]
        // public ActionResult SendCompletedApplicationScreening(string applicationId)
        // {
        //     result.Result = result.Result.ToUpper();
        //     if (result.Result != "PASS" && result.Result != "FAIL")
        //     {
        //         return BadRequest();
        //     }
        //     result.RecordIdentifier = applicationId;

        //     List<CompletedApplicationScreening> payload = new List<CompletedApplicationScreening>()
        //     {
        //         result
        //     };

        //     //Send the result to CARLA
        //     BackgroundJob.Enqueue(() => new CarlaUtils(Configuration, _loggerFactory, _sharepoint).ProcessResults(null));
        //     _logger.LogInformation($"Started send Application Screening result for job: {result.RecordIdentifier}");
        //     return Ok();
        // }


    }
}
