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

        public string Department { get; set; }

        public string OrgCode { get; set; }

        public string Company { get; set; }

        public User(ClaimsPrincipal principal)
        {
            Id = principal.FindFirstValue(ClaimTypes.Upn);
            DisplayName = principal.FindFirstValue(SiteMinderClaimTypes.NAME);
            Department = principal.FindFirstValue(SiteMinderClaimTypes.DEPARTMENT);
            OrgCode = principal.FindFirstValue(SiteMinderClaimTypes.ORG_CODE);
            Company = principal.FindFirstValue(SiteMinderClaimTypes.COMPANY);
        }
    }
}
