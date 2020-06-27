using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Gov.Jag.Spice.Public.Utils;

// ReSharper disable InconsistentNaming
namespace Gov.Jag.Spice.Public.Authentication
{
    public static class SiteMinderClaimTypes
    {
        public const string NAME = "sm.name";
        public const string GIVEN_NAME = "sm.given_name";
        public const string SURNAME = "sm.surname";
        public const string DEPARTMENT = "sm.department";
        public const string ORGCODE = "sm.orgcode";
        public const string COMPANY = "sm.company";
        public const string EMAIL = "sm.email";
    }

    public class SiteMinderAuthenticationToken
    {
        public string smgov_userguid;
        public string sm_universalid;
        public string sm_user;
        public string smgov_userdisplayname;
        public string smgov_givenname;
        public string smgov_sn;
        public string smgov_department;
        public string smgov_orgcode;
        public string smgov_company;
        public string smgov_email;

        private static readonly SiteMinderAuthenticationToken Anonymous = new SiteMinderAuthenticationToken();
        public const string SM_TOKEN_NAME = "sm.token";

        public static SiteMinderAuthenticationToken CreateFromFwdHeaders(HttpRequest req)
        {
            return new SiteMinderAuthenticationToken
            {
                smgov_userguid = req.Headers["smgov_userguid"].ToString(),
                sm_universalid = req.Headers["sm_universalid"].ToString(),
                sm_user = req.Headers["sm_user"].ToString(),
                smgov_userdisplayname = req.Headers["smgov_userdisplayname"].ToString(),
                smgov_givenname = req.Headers["smgov_givenname"].ToString(),
                smgov_sn = req.Headers["smgov_sn"].ToString(),
                smgov_department = req.Headers["smgov_department"].ToString(),
                smgov_orgcode = req.Headers["smgov_orgcode"].ToString(),
                smgov_company = req.Headers["smgov_company"].ToString(),
                smgov_email = req.Headers["smgov_email"].ToString(),
            };
        }

        public static SiteMinderAuthenticationToken CreateForDev(HttpRequest req)
        {
            string str = req.Cookies[SM_TOKEN_NAME]?.Base64Decode();
            if (string.IsNullOrEmpty(str)) str = req.Headers[SM_TOKEN_NAME].ToString().Base64Decode();
            if (string.IsNullOrWhiteSpace(str)) return Anonymous;

            var dict = str.Split(';').Select(p => p.Split('=')).ToDictionary(v => v[0], v => v[1]);
            return new SiteMinderAuthenticationToken
            {
                smgov_userguid = dict["smgov_userguid"],
                sm_universalid = dict["sm_universalid"],
                sm_user = dict["sm_user"],
                smgov_userdisplayname = dict["smgov_userdisplayname"],
                smgov_givenname = dict["smgov_givenname"],
                smgov_sn = dict["smgov_sn"],
                smgov_department = dict["smgov_department"],
                smgov_orgcode = dict["smgov_orgcode"],
                smgov_company = dict["smgov_company"],
                smgov_email = dict["smgov_email"],
            };
        }

        public static void AddToResponse(SiteMinderAuthenticationToken token, HttpResponse res)
        {
            res.Cookies.Append(SM_TOKEN_NAME, token.ToString().Base64Encode(), new CookieOptions
            {
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.Now.AddSeconds(30),
                HttpOnly = true
            });
        }

        public bool IsAnonymous()
        {
            return
              string.IsNullOrEmpty(sm_universalid)
              || string.IsNullOrEmpty(smgov_userdisplayname)
              || string.IsNullOrEmpty(smgov_userguid);
        }

        public override string ToString()
        {
            return 
                $"smgov_userguid={smgov_userguid};" +
                $"sm_universalid={sm_universalid};" +
                $"smgov_userdisplayname={smgov_userdisplayname};" +
                $"sm_user={sm_user};" +
                $"smgov_givenname={smgov_givenname};" +
                $"smgov_sn={smgov_sn};" +
                $"smgov_department={smgov_department};" +
                $"smgov_orgcode={smgov_orgcode};" +
                $"smgov_company={smgov_company};" +
                $"smgov_email={smgov_email}";
        }
    }
}
