using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Gov.Jag.Spice.Public.Authentication;
using Gov.Jag.Spice.Public.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Gov.Jag.Spice.Public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningRequestController : ControllerBase
    {
        private readonly string _dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();

        // POST: api/ScreeningRequest
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ScreeningRequest screeningRequest, CancellationToken cancellationToken)
        {
            // validate that the client ministry and program area specified in the screening request match the values provided by SiteMinder
            var principal = HttpContext.User;
            string siteMinderClientMinistry = principal.FindFirstValue(SiteMinderClaimTypes.COMPANY);
            string siteMinderProgramArea = principal.FindFirstValue(SiteMinderClaimTypes.DEPARTMENT);

            var ministries = await GetMinistryScreeningTypeData(cancellationToken);

            var ministry = ministries.Find(m => m.Name == siteMinderClientMinistry);
            if (ministry == null)
            {
                return Unauthorized();
            }

            var programArea = ministry.ProgramAreas.Find(a => a.Name == siteMinderProgramArea);
            if (programArea == null)
            {
                return Unauthorized();
            }

            if (screeningRequest.ClientMinistry != ministry.Value || screeningRequest.ProgramArea != programArea.Value)
            {
                return BadRequest();
            }

            // TODO
            return new JsonResult(new { requestId = 42 });
        }

        // GET: api/ScreeningRequest/MinistryScreeningTypes
        [Route("MinistryScreeningTypes")]
        [HttpGet]
        public async Task<IActionResult> GetMinistryScreeningTypes(CancellationToken cancellationToken)
        {
            var data = await GetMinistryScreeningTypeData(cancellationToken);
            return new JsonResult(data);
        }

        // GET: api/ScreeningRequest/ScreeningReasons
        [Route("ScreeningReasons")]
        [HttpGet]
        public async Task<IActionResult> GetScreeningReasons(CancellationToken cancellationToken)
        {
            var data = await GetScreeningReasonData(cancellationToken);
            return new JsonResult(data);
        }

        private async Task<List<Ministry>> GetMinistryScreeningTypeData(CancellationToken cancellationToken)
        {
            string text = await System.IO.File.ReadAllTextAsync(Path.Combine(_dataDirectory, "ministry-screening-types.json"), cancellationToken);
            return JsonConvert.DeserializeObject<List<Ministry>>(text);
        }

        private async Task<List<ScreeningReason>> GetScreeningReasonData(CancellationToken cancellationToken)
        {
            string text = await System.IO.File.ReadAllTextAsync(Path.Combine(_dataDirectory, "screening-reasons.json"), cancellationToken);
            return JsonConvert.DeserializeObject<List<string>>(text).Select(r => new ScreeningReason(r)).ToList();
        }
    }
}
