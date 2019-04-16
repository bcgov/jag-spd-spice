using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cllcsyncservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CllcController : Controller
    {

        private readonly IConfiguration Configuration;
        private readonly string accessToken;
        private readonly string baseUri;
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;

        public CllcController(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            accessToken = Configuration["ACCESS_TOKEN"];
            baseUri = Configuration["BASE_URI"];
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger(typeof(CllcController));
        }

        /// <summary>
        /// GET api/cllc/send
        /// Send export to CLLC.
        /// </summary>
        /// <returns>OK if successful</returns>
        [HttpGet("send")]
        //[ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Send()
        {
            BackgroundJob.Enqueue(() => new CllcUtils(Configuration, _loggerFactory).SendExportJob(null));
            _logger.LogInformation("Started send export job");
            return Ok();
        }

        /// <summary>
        /// GET api/spd/receive
        /// Start a receive import job
        /// </summary>
        /// <returns>OK if successful</returns>
        [HttpGet("receive")]
        public ActionResult Receive()
        {
            // check the file drop for a file, and then process it.
            BackgroundJob.Enqueue(() => new CllcUtils(Configuration, _loggerFactory).ReceiveImportJob(null));
            _logger.LogInformation("Started receive import job");
            return Ok();

        }

        /// <summary>
        /// GET api/apd/receive
        /// Start a receive import job
        /// </summary>
        /// <returns>OK if successful</returns>
        /*[HttpGet("update-worker")]
        public async System.Threading.Tasks.Task<ActionResult> UpdateWorkerAsync()
        {
            // check the file drop for a file, and then process it.
            await new WorkerUpdater(Configuration, _loggerFactory, SpdUtils.SetupSharepoint(Configuration)).ReceiveImportJob(null);
            _logger.LogInformation("Started receive import job");
            return Ok();

        }*/
    }
}
