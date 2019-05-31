using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Gov.Jag.Spice.Public.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Gov.Jag.Spice.Public.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration Configuration;

        public UserController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET: api/User/Current
        [HttpGet]
        [Route("Current")]
        public IActionResult Current()
        {
            var principal = HttpContext.User;
            var user = new ViewModels.User
            {
                Id = principal.FindFirstValue(ClaimTypes.Upn),
                DisplayName = principal.FindFirstValue(SiteMinderClaimTypes.NAME),
                GivenName = principal.FindFirstValue(SiteMinderClaimTypes.GIVEN_NAME),
                LastName = principal.FindFirstValue(SiteMinderClaimTypes.LAST_NAME),
                Department = principal.FindFirstValue(SiteMinderClaimTypes.DEPARTMENT),
                OrgCode = principal.FindFirstValue(SiteMinderClaimTypes.ORG_CODE),
                Company = principal.FindFirstValue(SiteMinderClaimTypes.COMPANY),
            };

            return new JsonResult(user);
        }
    }
}
