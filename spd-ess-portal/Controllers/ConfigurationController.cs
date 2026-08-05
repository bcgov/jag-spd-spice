using System;
using System.Threading.Tasks;
using Gov.Jag.Spice.Interfaces;
using Gov.Jag.Spice.Public.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
                    NotificationBannerMessage = configuration.GetValue<string>("NOTIFICATION_BANNER_MESSAGE"),
                    NotificationBannerStartDate = configuration.GetValue<string>("NOTIFICATION_BANNER_STARTDATE"),
                    NotificationBannerEndDate = configuration.GetValue<string>("NOTIFICATION_BANNER_ENDDATE"),
                    NotificationBannerSeverity = Enum.TryParse<NotificationBannerSeverity>(
                        configuration.GetValue<string>("NOTIFICATION_BANNER_SEVERITY"),
                        ignoreCase: true,
                        out var severity
                    )
                        ? severity
                        : NotificationBannerSeverity.Warning,
                    NotificationBannerIsOutage = configuration.GetValue<bool>("NOTIFICATION_BANNER_IS_OUTAGE"),
                };

                if (
                    string.IsNullOrEmpty(config.NotificationBannerMessage)
                    || string.IsNullOrEmpty(config.NotificationBannerStartDate)
                    || string.IsNullOrEmpty(config.NotificationBannerEndDate)
                )
                {
                    return Ok();
                }

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
