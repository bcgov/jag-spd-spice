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
    public class TestController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private IDynamicsClient _dynamicsClient;

        public TestController (IConfiguration configuration, ILoggerFactory loggerFactory, IDynamicsClient dynamicsClient)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger(typeof(WorkerScreeningsController));
            _dynamicsClient = dynamicsClient;
        }

        /// <summary>
        /// Test Dynamics connection.
        /// </summary>
        /// <returns>OK if successful</returns>
        [HttpGet()]
        [AllowAnonymous]
        public ActionResult Test()
        {
            _logger.LogError("Testing Dynamics");

            var contacts = _dynamicsClient.Contacts.Get();

            return Json(contacts);

        }       

        


    }
}
