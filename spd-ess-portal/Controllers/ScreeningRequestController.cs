using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        public IActionResult Post([FromBody] ScreeningRequest screeningRequest)
        {
            // TODO
            return new JsonResult("Success");
        }

        // GET: api/ScreeningRequest/MinistryScreeningTypes
        [Route("MinistryScreeningTypes")]
        [HttpGet]
        public async Task<IActionResult> GetMinistryScreeningTypes(CancellationToken cancellationToken)
        {
            string text = await System.IO.File.ReadAllTextAsync(Path.Combine(_dataDirectory, "ministry-screening-types.json"), cancellationToken);
            var data = JsonConvert.DeserializeObject<List<Ministry>>(text);
            return new JsonResult(data);
        }

        // GET: api/ScreeningRequest/ScreeningReasons
        [Route("ScreeningReasons")]
        [HttpGet]
        public async Task<IActionResult> GetScreeningReasons(CancellationToken cancellationToken)
        {
            string text = await System.IO.File.ReadAllTextAsync(Path.Combine(_dataDirectory, "screening-reasons.json"), cancellationToken);
            var data = JsonConvert.DeserializeObject<List<ScreeningReason>>(text);
            return new JsonResult(data);
        }
    }
}
