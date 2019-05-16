using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public IActionResult GetMinistryScreeningTypes()
        {
            using (var streamReader = System.IO.File.OpenText(Path.Combine(_dataDirectory, "ministry-screening-types.json")))
            {
                var serializer = new JsonSerializer();
                var data = serializer.Deserialize<List<Ministry>>(new JsonTextReader(streamReader));
                return new JsonResult(data);
            }
        }

        // GET: api/ScreeningRequest/ScreeningReasons
        [Route("ScreeningReasons")]
        [HttpGet]
        public IActionResult GetScreeningReasons()
        {
            using (var streamReader = System.IO.File.OpenText(Path.Combine(_dataDirectory, "screening-reasons.json")))
            {
                var serializer = new JsonSerializer();
                var data = serializer.Deserialize<List<ScreeningReason>>(new JsonTextReader(streamReader));
                return new JsonResult(data);
            }
        }
    }
}
