using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gov.Jag.Spice.Public.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gov.Jag.Spice.Public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreeningRequestController : ControllerBase
    {
        // POST: api/ScreeningRequest
        [HttpPost]
        public IActionResult Post([FromBody] ScreeningRequest screeningRequest)
        {
            // TODO
            return new JsonResult("Success");
        }
    }
}
