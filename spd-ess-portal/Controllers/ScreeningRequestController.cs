using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gov.Jag.Spice.Interfaces;
using Gov.Jag.Spice.Public.Utils;
using Gov.Jag.Spice.Public.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Gov.Jag.Spice.Public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningRequestController : ControllerBase
    {
        private readonly ILogger<ScreeningRequestController> _logger;
        private readonly IDynamicsClient _dynamicsClient;

        public ScreeningRequestController(ILogger<ScreeningRequestController> logger, IDynamicsClient dynamicsClient)
        {
            _logger = logger;
            _dynamicsClient = dynamicsClient;
        }

        // POST: api/ScreeningRequest
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ScreeningRequest screeningRequest)
        {
            try
            {
                var user = new User(HttpContext.User);

                var (ministry, programArea, screeningType) =
                    await ScreeningRequest.GetMinistryScreeningTypeAsync(
                        _dynamicsClient,
                        user.OrgCode,
                        screeningRequest.ScreeningType
                    );

                // validate screening request
                try
                {
                    bool validationResult = await screeningRequest.Validate(_dynamicsClient, ministry, programArea, screeningType);
                    if (validationResult == false)
                    {
                        _logger.LogWarning(
                            string.Join(
                                Environment.NewLine,
                                "Validation failed for screening request {@ScreeningRequest}",
                                "{@ministry}",
                                "{@programArea}",
                                "{@screeningType}"
                            ),
                            screeningRequest,
                            ministry,
                            programArea,
                            screeningType
                        );

                        return BadRequest();
                    }
                }
                catch (DynamicsEntityNotFoundException ex)
                {
                    _logger.LogError(
                        ex, 
                        string.Join(
                            Environment.NewLine,
                            "Validation failed for screening request {@ScreeningRequest}",
                            "{@ministry}",
                            "{@programArea}",
                            "{@screeningType}"
                        ),
                        screeningRequest,
                        ministry,
                        programArea,
                        screeningType
                    );

                    return BadRequest();
                }

                // validate user
                if (string.IsNullOrWhiteSpace(user.Email))
                {
                    _logger.LogWarning("Validation failed for user {@user} submitting screening request {@ScreeningRequest}", user, screeningRequest);
                    return BadRequest();
                }

                // submit request to dynamics and return its new screeningId
                string screeningId = await screeningRequest.Submit(_dynamicsClient, _logger, user, screeningType, programArea.Value);

                return new JsonResult(new { screeningId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to submit screening request {@ScreeningRequest}", screeningRequest);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/ScreeningRequest/MinistryScreeningTypes
        [Route("MinistryScreeningTypes")]
        [HttpGet]
        public async Task<IActionResult> GetMinistryScreeningTypes()
        {
            try
            {
                var user = new User(HttpContext.User);
                var data = await ScreeningRequest.GetMinistryScreeningTypesAsync(_dynamicsClient, user.OrgCode);
                _logger.LogInformation("Successfully retrieved ministry screening types {@Ministries}", data);
                return new JsonResult(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve ministry screening types");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // GET: api/ScreeningRequest/ScreeningReasons
        [Route("ScreeningReasons")]
        [HttpGet]
        public async Task<IActionResult> GetScreeningReasons()
        {
            try
            {
                var data = await ScreeningRequest.GetScreeningReasonsAsync(_dynamicsClient);
                _logger.LogInformation("Successfully retrieved screening reasons {@ScreeningReasons}", data);
                return new JsonResult(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve screening reasons");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
