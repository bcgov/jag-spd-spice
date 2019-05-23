using Gov.Jag.Spice.Public.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gov.Jag.Spice.Public.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        private readonly IConfiguration Configuration;

        private readonly IHostingEnvironment _env;

        public LoginController(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            _env = env;        
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> Login(string path)
        {
            if (!_env.IsProduction() && "headers".Equals(path, StringComparison.OrdinalIgnoreCase))
            {
                return Content(string.Join(Environment.NewLine, Request.Headers.Select(header => $"{header.Key}={string.Join(",", header.Value.ToArray())}")), "text/plain", Encoding.UTF8);
            }

            if (ControllerContext.HttpContext.User == null || !ControllerContext.HttpContext.User.Identity.IsAuthenticated) return Unauthorized();

            return await Task.FromResult(LocalRedirect($"{Configuration["BASE_PATH"]}/{path}"));
        }

        [HttpGet]
        [Route("token/{userName}")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginDevelopment(string userName)
        {
            if (_env.IsProduction() || string.IsNullOrWhiteSpace(userName)) return Unauthorized();

            HttpContext.Session.Clear();

            var secToken = await CreateDevTokenForIdir(userName);

            SiteMinderAuthenticationToken.AddToResponse(secToken, Response);

            return LocalRedirect($"{Configuration["BASE_PATH"]}/login");
        }

        private static async Task<SiteMinderAuthenticationToken> CreateDevTokenForIdir(string userName)
        {
            var guidBytes = Guid.Empty.ToByteArray();
            guidBytes[guidBytes.Length - 1] += (byte)userName.GetHashCode();
            return await Task.FromResult(new SiteMinderAuthenticationToken
            {
                smgov_userdisplayname = "Doe, John PSSG:EX",
                smgov_userguid = new Guid(guidBytes).ToString(),
                sm_universalid = userName,
                sm_user = userName,
                smgov_givenname = "John",
                smgov_sn = "Doe",
                smgov_company = "Ministry of Public Safety and Solicitor General",
                smgov_orgcode = "PSSG",
                smgov_department = "Corrections Branch",
            });
        }
    }
}
