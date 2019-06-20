using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Gov.Jag.Spice.Public.Authentication;
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
            var user = new ViewModels.User(HttpContext.User);

            return new JsonResult(user);
        }
    }
}
