using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace cllcsyncservice.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration Configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // <summary>
        /// Returns a security token.
        /// </summary>
        /// <param name="secret"></param>
        /// <returns>A new JWT</returns>
        [HttpGet("token")]
        [AllowAnonymous]
        public string GetToken(string secret)
        {
            string result = "Invalid secret.";
            string configuredSecret = Configuration["JWT_TOKEN_KEY"];
            if (configuredSecret.Equals(secret))
            {
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT_TOKEN_KEY"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                    Configuration["JWT_VALID_ISSUER"],
                    Configuration["JWT_VALID_ISSUER"],
                    expires: DateTime.UtcNow.AddYears(5),
                    signingCredentials: creds
                    );
                result = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken) + " Expires:" + jwtSecurityToken.ValidTo.ToShortTimeString();
            }

            return result;
        }

    }
}
