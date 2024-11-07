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
        private readonly ILogger<ConfigurationController> _logger;
        private readonly IDynamicsClient _dynamicsClient;
        private readonly IConfiguration _configuration;

        public ConfigurationController(ILogger<ConfigurationController> logger, IDynamicsClient dynamicsClient, IConfiguration configuration)
        {
            _logger = logger;
            _dynamicsClient = dynamicsClient;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetConfiguration()
        {
            try
            {
                // Log to check if the section exists
                _logger.LogInformation("Configuration section exists: " + _configuration.GetSection("Configuration").Exists());

                // Retrieve the entire Configuration section from appsettings.json
                var configurationData = _configuration.GetSection("Configuration").Get<ConfigurationRoot>();

                if (configurationData == null)
                {
                    _logger.LogWarning("Configuration information is not configured.");
                    return NotFound("Configuration information not found.");
                }

                _logger.LogInformation("Full Configuration Information: {@ConfigurationData}", configurationData);

                return new JsonResult(configurationData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve configuration information.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

public class ConfigurationRoot
{
    public OutageInfo OutageInfo { get; set; }
}
public class OutageInfo
{
    public bool IsOutage { get; set; }
    public string Content { get; set; }
    public DateTime OutageStartDate { get; set; }
    public DateTime OutageEndDate { get; set; }
}