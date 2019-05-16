using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gov.Jag.Spice.Public.Models;
using Gov.Jag.Spice.Public.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gov.Jag.Spice.Public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/User/Current
        [HttpGet]
        [Route("Current")]
        public IActionResult Current()
        {
            // TODO
            var user = new User();

            return new JsonResult(user);
        }
    }
}
