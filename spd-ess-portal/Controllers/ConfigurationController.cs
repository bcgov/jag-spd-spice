using System;
using System.Threading.Tasks;
using Gov.Jag.Spice.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Gov.Jag.Spice.Public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly ILogger<ConfigurationController> logger;
        private readonly IConfiguration configuration;

        public ConfigurationController(ILogger<ConfigurationController> logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetConfiguration()
        {
            try
            {
                var config = new Configuration
                {
                    OutageMessage = configuration.GetValue<string>("CONFIGURATION_OUTAGEINFORMATION_MESSAGE"),
                    OutageStartDate = configuration.GetValue<string>("CONFIGURATION_OUTAGEINFORMATION_STARTDATE"),
                    OutageEndDate = configuration.GetValue<string>("CONFIGURATION_OUTAGEINFORMATION_ENDDATE")
                };

                if (string.IsNullOrEmpty(config.OutageMessage) || string.IsNullOrEmpty(config.OutageStartDate) || string.IsNullOrEmpty(config.OutageEndDate))
                {
                    return Ok();
                };

                return Ok(config);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve configuration information.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

public class Configuration
{
    public string OutageMessage { get; set; }
    public string OutageStartDate { get; set; }
    public string OutageEndDate { get; set; }
};