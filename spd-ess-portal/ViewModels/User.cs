using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Gov.Jag.Spice.Public.Authentication;

namespace Gov.Jag.Spice.Public.ViewModels
{
    public class User
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string GivenName { get; set; }

        public string Surname { get; set; }

        public string Ministry { get; set; }

        public string OrgCode { get; set; }

        public string ProgramArea { get; set; }

        public string Email { get; set; }

        public User(ClaimsPrincipal principal)
        {
            Id = principal.FindFirstValue(ClaimTypes.Upn);
            DisplayName = principal.FindFirstValue(SiteMinderClaimTypes.NAME);
            GivenName = principal.FindFirstValue(SiteMinderClaimTypes.GIVEN_NAME);
            Surname = principal.FindFirstValue(SiteMinderClaimTypes.SURNAME);
            Ministry = principal.FindFirstValue(SiteMinderClaimTypes.COMPANY);
            OrgCode = principal.FindFirstValue(SiteMinderClaimTypes.ORGCODE);
            ProgramArea = principal.FindFirstValue(SiteMinderClaimTypes.DEPARTMENT);
            Email = principal.FindFirstValue(SiteMinderClaimTypes.EMAIL);
        }
    }
}
