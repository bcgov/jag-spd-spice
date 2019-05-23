using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Gov.Jag.Spice.Public.Controllers
{
    [Route("[controller]")]
    public class LogoutController : Controller
    {
        private readonly IConfiguration Configuration;

        public LogoutController(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();

            string logoutPath = string.IsNullOrEmpty(Configuration["SITEMINDER_LOGOUT_URL"]) ? "/" : Configuration["SITEMINDER_LOGOUT_URL"];
            return Redirect(logoutPath + $"?returl={Configuration["BASE_URI"]}{Configuration["BASE_PATH"]}&retnow=1");
        }
    }
}
