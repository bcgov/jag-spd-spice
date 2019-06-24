using Gov.Jag.Spice.Interfaces;
using Gov.Jag.Spice.Interfaces.Models;
using Hangfire.Console;
using Hangfire.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SpdSync;
using SpdSync.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Gov.Jag.Spice.CarlaSync
{
    public class SpdUtils
    {
        public ILogger _logger { get; }

        private IConfiguration Configuration { get; }
        private IDynamicsClient _dynamics;
        private static readonly HttpClient Client = new HttpClient();

        public SpdUtils(IConfiguration Configuration, ILoggerFactory loggerFactory)
        {
            this.Configuration = Configuration;
            _logger = loggerFactory.CreateLogger(typeof(SpdUtils));
            _dynamics = DynamicsSetupUtil.SetupDynamics(Configuration);

        }


        private string AddZeroPadding(string input, int maxLength = 8)
        {
            while (input.Length < maxLength)
            {
                input = "0" + input;
            }
            return input;
        }


        private bool SendSPDEmail(string attachmentContent, string attachmentName)
        {
            var emailSentSuccessfully = false;
            var datePart = DateTime.Now.ToString().Replace('/', '-').Replace(':', '_');
            var email = Configuration["SPD_EXPORT_EMAIL"];
            string body = $@"";

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))    // using UTF-8 encoding by default
            using (var mailClient = new SmtpClient(Configuration["SMTP_HOST"]))
            using (var message = new MailMessage("no-reply@gov.bc.ca", email))
            {
                writer.WriteLine(attachmentContent);
                writer.Flush();
                stream.Position = 0;     // read from the start of what was written

                message.Subject = $"{attachmentName}";
                message.Body = body;
                message.IsBodyHtml = true;

                message.Attachments.Add(new Attachment(stream, attachmentName, "text/csv"));

                try
                {
                    mailClient.Send(message);
                    emailSentSuccessfully = true;
                }
                catch (Exception)
                {
                    emailSentSuccessfully = false;
                }

            }
            return emailSentSuccessfully;
        }

        private bool SendSPDNoResultsEmail(string batchNumber)
        {
            var emailSentSuccessfully = false;
            var datePart = DateTime.Now.ToString().Replace('/', '-').Replace(':', '_');
            var email = Configuration["SPD_EXPORT_EMAIL"];
            string body = "";

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))    // using UTF-8 encoding by default
            using (var mailClient = new SmtpClient(Configuration["SMTP_HOST"]))
            using (var message = new MailMessage("no-reply@gov.bc.ca", email))
            {
                writer.WriteLine($"No data exists on this batch request({ batchNumber})");
                writer.Flush();
                stream.Position = 0;     // read from the start of what was written

                message.Subject = $"No data exists on this batch request({ batchNumber})";
                message.Body = body;
                message.IsBodyHtml = true;


                try
                {
                    mailClient.Send(message);
                    emailSentSuccessfully = true;
                }
                catch (Exception)
                {
                    emailSentSuccessfully = false;
                }

            }
            return emailSentSuccessfully;
        }

        private List<KeyValuePair<string, string>> GetExportHeadersWorker()
        {
            var result = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("AdoxioLcrbworkerjobid", "LCRB ASSOCIATE JOB ID"),
                // 9-12-18 - BC Registries number is no longer required for individuals (only business records, which are not yet part of the export)
                // new KeyValuePair<string, string>("AdoxioBcregistriesnumber", "Bcregistriesnumber"),
                new KeyValuePair<string, string>("AdoxioSelfdisclosure", "SELF-DISCLOSURE YN"),
                new KeyValuePair<string, string>("AdoxioLegalsurname", "SURNAME"),
                new KeyValuePair<string, string>("AdoxioLegalfirstname", "FIRST NAME"),
                new KeyValuePair<string, string>("AdoxioLegalmiddlename", "SECOND NAME"),
                new KeyValuePair<string, string>("AdoxioBirthdate", "BIRTHDATE"),
                new KeyValuePair<string, string>("AdoxioGendermf", "GENDER"),
                new KeyValuePair<string, string>("AdoxioBirthplacecity", "BIRTHPLACE"),
                new KeyValuePair<string, string>("AdoxioDriverslicence", "DRIVERS LICENCE"),
                new KeyValuePair<string, string>("AdoxioBcidentificationcardnumber", "BC ID CARD NUMBER"),
                new KeyValuePair<string, string>("AdoxioContactphone", "CONTACT PHONE"),
                new KeyValuePair<string, string>("AdoxioPersonalemailaddress", "EMAIL ADDRESS"),
                new KeyValuePair<string, string>("AdoxioAddressline1", "STREET"),
                new KeyValuePair<string, string>("AdoxioAddresscity", "CITY"),
                new KeyValuePair<string, string>("AdoxioAddressprovstate", "PROVINCE"),
                new KeyValuePair<string, string>("AdoxioAddresscountry", "COUNTRY"),
                new KeyValuePair<string, string>("AdoxioAddresspostalcode", "POSTAL CODE"),
                new KeyValuePair<string, string>("AdoxioAlias1surname", "ALIAS 1 SURNAME"),
                new KeyValuePair<string, string>("AdoxioAlias1firstname", "ALIAS 1 FIRST NAME"),
                new KeyValuePair<string, string>("AdoxioAlias1middlename", "ALIAS 1 SECOND NAME"),
                new KeyValuePair<string, string>("AdoxioAlias2surname", "ALIAS 2 SURNAME"),
                new KeyValuePair<string, string>("AdoxioAlias2firstname", "ALIAS 2 FIRST NAME"),
                new KeyValuePair<string, string>("AdoxioAlias2middlename", "ALIAS 2 MIDDLE NAME"),
                new KeyValuePair<string, string>("AdoxioAlias3surname", "ALIAS 3 SURNAME"),
                new KeyValuePair<string, string>("AdoxioAlias3firstname", "ALIAS 3 FIRST NAME"),
                new KeyValuePair<string, string>("AdoxioAlias3middlename", "ALIAS 3 MIDDLE NAME"),
                new KeyValuePair<string, string>("AdoxioAlias4surname", "ALIAS 4 SURNAME"),
                new KeyValuePair<string, string>("AdoxioAlias4firstname", "ALIAS 4 FIRST NAME"),
                new KeyValuePair<string, string>("AdoxioAlias4middlename", "ALIAS 4 MIDDLE NAME"),
                new KeyValuePair<string, string>("AdoxioAlias5surname", "ALIAS 5 SURNAME"),
                new KeyValuePair<string, string>("AdoxioAlias5firstname", "ALIAS 5 FIRST NAME"),
                new KeyValuePair<string, string>("AdoxioAlias5middlename", "ALIAS 5 MIDDLE NAME"),
                new KeyValuePair<string, string>("AdoxioPreviousstreetaddress1", "PREV STREET 1"),
                new KeyValuePair<string, string>("AdoxioPreviouscity1", "PREV CITY 1"),
                new KeyValuePair<string, string>("AdoxioPreviousprovstate1", "PREV PROVINCE 1"),
                new KeyValuePair<string, string>("AdoxioPreviouscountry1", "PREV COUNTRY 1"),
                new KeyValuePair<string, string>("AdoxioPreviouspostalcode1", "PREV POSTCODE1"),
                new KeyValuePair<string, string>("AdoxioPreviousstreetaddress2", "PREV STREET 2"),
                new KeyValuePair<string, string>("AdoxioPreviouscity2", "PREV CITY 2"),
                new KeyValuePair<string, string>("AdoxioPreviousprovstate2", "PREV PROVINCE 2"),
                new KeyValuePair<string, string>("AdoxioPreviouscountry2", "PREV COUNTRY 2"),
                new KeyValuePair<string, string>("AdoxioPreviouspostalcode2", "PREV POSTCODE2"),
                new KeyValuePair<string, string>("AdoxioPreviousstreetaddress3", "PREV STREET 3"),
                new KeyValuePair<string, string>("AdoxioPreviouscity3", "PREV CITY 3"),
                new KeyValuePair<string, string>("AdoxioPreviousprovstate3", "PREV PROVINCE 3"),
                new KeyValuePair<string, string>("AdoxioPreviouscountry3", "PREV COUNTRY 3"),
                new KeyValuePair<string, string>("AdoxioPreviouspostalcode3", "PREV POSTCODE3"),
                new KeyValuePair<string, string>("AdoxioPreviousstreetaddress4", "PREV STREET 4"),
                new KeyValuePair<string, string>("AdoxioPreviouscity4", "PREV CITY 4"),
                new KeyValuePair<string, string>("AdoxioPreviousprovstate4", "PREV PROVINCE 4"),
                new KeyValuePair<string, string>("AdoxioPreviouscountry4", "PREV COUNTRY 4"),
                new KeyValuePair<string, string>("AdoxioPreviouspostalcode4", "PREV POSTCODE4"),
                new KeyValuePair<string, string>("AdoxioPreviousstreetaddress5", "PREV STREET 5"),
                new KeyValuePair<string, string>("AdoxioPreviouscity5", "PREV CITY 5"),
                new KeyValuePair<string, string>("AdoxioPreviousprovstate5", "PREV PROVINCE 5"),
                new KeyValuePair<string, string>("AdoxioPreviouscountry5", "PREV COUNTRY 5"),
                new KeyValuePair<string, string>("AdoxioPreviouspostalcode5", "PREV POSTCODE5"),
                new KeyValuePair<string, string>("AdoxioPreviousstreetaddress6", "PREV STREET 6"),
                new KeyValuePair<string, string>("AdoxioPreviouscity6", "PREV CITY 6"),
                new KeyValuePair<string, string>("AdoxioPreviousprovstate6", "PREV PROVINCE 6"),
                new KeyValuePair<string, string>("AdoxioPreviouscountry6", "PREV COUNTRY 6"),
                new KeyValuePair<string, string>("AdoxioPreviouspostalcode6", "PREV POSTCODE6"),
                new KeyValuePair<string, string>("AdoxioPreviousstreetaddress7", "PREV STREET 7"),
                new KeyValuePair<string, string>("AdoxioPreviouscity7", "PREV CITY 7"),
                new KeyValuePair<string, string>("AdoxioPreviousprovstate7", "PREV PROVINCE 7"),
                new KeyValuePair<string, string>("AdoxioPreviouscountry7", "PREV COUNTRY 7"),
                new KeyValuePair<string, string>("AdoxioPreviouspostalcode7", "PREV POSTCODE7"),
                new KeyValuePair<string, string>("AdoxioPreviousstreetaddress8", "PREV STREET 8"),
                new KeyValuePair<string, string>("AdoxioPreviouscity8", "PREV CITY 8"),
                new KeyValuePair<string, string>("AdoxioPreviousprovstate8", "PREV PROVINCE 8"),
                new KeyValuePair<string, string>("AdoxioPreviouscountry8", "PREV COUNTRY 8"),
                new KeyValuePair<string, string>("AdoxioPreviouspostalcode8", "PREV POSTCODE8"),
                new KeyValuePair<string, string>("AdoxioPreviousstreetaddress9", "PREV STREET 9"),
                new KeyValuePair<string, string>("AdoxioPreviouscity9", "PREV CITY 9"),
                new KeyValuePair<string, string>("AdoxioPreviousprovstate9", "PREV PROVINCE 9"),
                new KeyValuePair<string, string>("AdoxioPreviouscountry9", "PREV COUNTRY 9"),
                new KeyValuePair<string, string>("AdoxioPreviouspostalcode9", "PREV POSTCODE9"),
                new KeyValuePair<string, string>("AdoxioPreviousstreetaddressx", "PREV STREET x"),
                new KeyValuePair<string, string>("AdoxioPreviouscityx", "PREV CITY x"),
                new KeyValuePair<string, string>("AdoxioPreviousprovstatex", "PREV PROVINCE x"),
                new KeyValuePair<string, string>("AdoxioPreviouspostalcodex", "PREV POSTCODE x"),
                new KeyValuePair<string, string>("AdoxioPreviouscountryx", "PREV COUNTRY x"),
            };
            return result;
        }

        private Dictionary<string, string> GetProviceCodeMap(){
            var map = new Dictionary<string, string>{
                { "AB",	"Alberta	Alberta" },
                { "BC",	"British Columbia" },
                { "MB",	"Manitoba" },
                { "NB",	"New Brunswick" },
                { "NL",	"Newfoundland and Labrador" },
                { "NS",	"Nova Scotia" },
                { "NT",	"Northwest Territories" },
                { "NU",	"Nunavut" },
                { "ON",	"Ontario" },
                { "PE",	"Prince Edward Island" },
                { "QC",	"Quebec" },
                { "SK",	"Saskatchewan" },
                { "YT",	"Yukon" }
            };
            return map;
        }
        private Dictionary<string, string> GetCountryCodeMap()
        {
            var map = new Dictionary<string, string> {
                {"AD", "Andorra"},
                {"AE", "United Arab Emirates"},
                {"AF", "Afghanistan"},
                {"AG", "Antigua and Barbuda"},
                {"AI", "Anguilla"},
                {"AL", "Albania"},
                {"AM", "Armenia"},
                {"AN", "Antilles - Netherlands"},
                {"AO", "Angola"},
                {"AQ", "Antarctica"},
                {"AR", "Argentina"},
                {"AS", "American Samoa"},
                {"AT", "Austria"},
                {"AU", "Australia"},
                {"AW", "Aruba"},
                {"AX", "Aland Islands"},
                {"AZ", "Azerbaijan"},
                {"BA", "Bosnia and Herzegovina"},
                {"BB", "Barbados"},
                {"BD", "Bangladesh"},
                {"BE", "Belgium"},
                {"BF", "Burkina Faso"},
                {"BG", "Bulgaria"},
                {"BH", "Bahrain"},
                {"BI", "Burundi"},
                {"BJ", "Benin"},
                {"BM", "Bermuda"},
                {"BN", "Brunei Darussalam"},
                {"BO", "Bolivia"},
                {"BR", "Brazil"},
                {"BS", "Bahamas"},
                {"BT", "Bhutan"},
                {"BV", "Bouvet Island"},
                {"BW", "Botswana"},
                {"BZ", "Belize"},
                {"CA", "Canada"},
                {"CC", "Cocos (Keeling) Islands"},
                {"CD", "Democratic Republic of the Congo"},
                {"CF", "Central African Republic"},
                {"CG", "Congo"},
                {"CH", "Switzerland"},
                {"CI", "Cote D'Ivoire (Ivory Coast)"},
                {"CK", "Cook Islands"},
                {"CL", "Chile"},
                {"CM", "Cameroon"},
                {"CN", "China"},
                {"CO", "Colombia"},
                {"CR", "Costa Rica"},
                {"CS", "Serbia and Montenegro"},
                {"CU", "Cuba"},
                {"CV", "Cape Verde"},
                {"CX", "Christmas Island"},
                {"CY", "Cyprus"},
                {"CZ", "Czech Republic"},
                {"DE", "Germany"},
                {"DJ", "Djibouti"},
                {"DK", "Denmark"},
                {"DM", "Dominica"},
                {"DO", "Dominican Republic"},
                {"DZ", "Algeria"},
                {"EC", "Ecuador"},
                {"EE", "Estonia"},
                {"EG", "Egypt"},
                {"EH", "Western Sahara"},
                {"ER", "Eritrea"},
                {"ES", "Spain"},
                {"ET", "Ethiopia"},
                {"FI", "Finland"},
                {"FJ", "Fiji"},
                {"FK", "Falkland Islands (Malvinas)"},
                {"FM", "Federated States of Micronesia"},
                {"FO", "Faroe Islands"},
                {"FR", "France"},
                {"FX", "France, Metropolitan"},
                {"GA", "Gabon"},
                {"GB", "Great Britain (UK)"},
                {"GD", "Grenada"},
                {"GE", "Georgia"},
                {"GF", "French Guiana"},
                {"GH", "Ghana"},
                {"GI", "Gibraltar"},
                {"GL", "Greenland"},
                {"GM", "Gambia"},
                {"GN", "Guinea"},
                {"GP", "Guadeloupe"},
                {"GQ", "Equatorial Guinea"},
                {"GR", "Greece"},
                {"GS", "S. Georgia and S. Sandwich Islands"},
                {"GT", "Guatemala"},
                {"GU", "Guam"},
                {"GW", "Guinea-Bissau"},
                {"GY", "Guyana"},
                {"HK", "Hong Kong"},
                {"HM", "Heard Island and McDonald Islands"},
                {"HN", "Honduras"},
                {"HR", "Croatia (Hrvatska)"},
                {"HT", "Haiti"},
                {"HU", "Hungary"},
                {"ID", "Indonesia"},
                {"IE", "Ireland"},
                {"IL", "Israel"},
                {"IN", "India"},
                {"IO", "British Indian Ocean Territory"},
                {"IQ", "Iraq"},
                {"IR", "Iran"},
                {"IT", "Italy"},
                {"JM", "Jamaica"},
                {"JO", "Jordan"},
                {"JP", "Japan"},
                {"KE", "Kenya"},
                {"KG", "Kyrgyzstan"},
                {"KH", "Cambodia"},
                {"KI", "Kiribati"},
                {"KM", "Comoros"},
                {"KN", "Saint Kitts and Nevis"},
                {"KP", "Korea (North)"},
                {"KR", "Korea (South)"},
                {"KW", "Kuwait"},
                {"KY", "Cayman Islands"},
                {"KZ", "Kazakhstan"},
                {"LA", "Laos"},
                {"LB", "Lebanon"},
                {"LC", "Saint Lucia"},
                {"LI", "Liechtenstein"},
                {"LK", "Sri Lanka"},
                {"LR", "Liberia"},
                {"LS", "Lesotho"},
                {"LT", "Lithuania"},
                {"LU", "Luxembourg"},
                {"LV", "Latvia"},
                {"LY", "Libya"},
                {"MA", "Morocco"},
                {"MC", "Monaco"},
                {"MD", "Moldova"},
                {"MG", "Madagascar"},
                {"MH", "Marshall Islands"},
                {"MK", "Macedonia"},
                {"ML", "Mali"},
                {"MM", "Myanmar"},
                {"MN", "Mongolia"},
                {"MO", "Macao"},
                {"MP", "Northern Mariana Islands"},
                {"MQ", "Martinique"},
                {"MR", "Mauritania"},
                {"MS", "Montserrat"},
                {"MT", "Malta"},
                {"MU", "Mauritius"},
                {"MV", "Maldives"},
                {"MW", "Malawi"},
                {"MX", "Mexico"},
                {"MY", "Malaysia"},
                {"MZ", "Mozambique"},
                {"NA", "Namibia"},
                {"NC", "New Caledonia"},
                {"NE", "Niger"},
                {"NF", "Norfolk Island"},
                {"NG", "Nigeria"},
                {"NI", "Nicaragua"},
                {"NL", "Netherlands"},
                {"NO", "Norway"},
                {"NP", "Nepal"},
                {"NR", "Nauru"},
                {"NU", "Niue"},
                {"NZ", "New Zealand (Aotearoa)"},
                {"OM", "Oman"},
                {"PA", "Panama"},
                {"PE", "Peru"},
                {"PF", "French Polynesia"},
                {"PG", "Papua New Guinea"},
                {"PH", "Philippines"},
                {"PK", "Pakistan"},
                {"PL", "Poland"},
                {"PM", "Saint Pierre and Miquelon"},
                {"PN", "Pitcairn"},
                {"PR", "Puerto Rico"},
                {"PS", "Palestinian Territory"},
                {"PT", "Portugal"},
                {"PW", "Palau"},
                {"PY", "Paraguay"},
                {"QA", "Qatar"},
                {"RE", "Reunion"},
                {"RO", "Romania"},
                {"RU", "Russian Federation"},
                {"RW", "Rwanda"},
                {"SA", "Saudi Arabia"},
                {"SB", "Solomon Islands"},
                {"SC", "Seychelles"},
                {"SD", "Sudan"},
                {"SE", "Sweden"},
                {"SG", "Singapore"},
                {"SH", "Saint Helena"},
                {"SI", "Slovenia"},
                {"SJ", "Svalbard and Jan Mayen"},
                {"SK", "Slovakia"},
                {"SL", "Sierra Leone"},
                {"SM", "San Marino"},
                {"SN", "Senegal"},
                {"SO", "Somalia"},
                {"SR", "Suriname"},
                {"ST", "Sao Tome and Principe"},
                {"SU", "USSR (former)"},
                {"SV", "El Salvador"},
                {"SY", "Syria"},
                {"SZ", "Swaziland"},
                {"TC", "Turks and Caicos Islands"},
                {"TD", "Chad"},
                {"TG", "Togo"},
                {"TH", "Thailand"},
                {"TJ", "Tajikistan"},
                {"TK", "Tokelau"},
                {"TL", "Timor-Leste"},
                {"TM", "Turkmenistan"},
                {"TN", "Tunisia"},
                {"TO", "Tonga"},
                {"TP", "East Timor"},
                {"TR", "Turkey"},
                {"TT", "Trinidad and Tobago"},
                {"TV", "Tuvalu"},
                {"TW", "Taiwan"},
                {"TZ", "Tanzania"},
                {"UA", "Ukraine"},
                {"UG", "Uganda"},
                {"UK", "United Kingdom"},
                {"UM", "United States Minor Outlying Islands"},
                {"US", "United States"},
                {"UY", "Uruguay"},
                {"UZ", "Uzbekistan"},
                {"VA", "Vatican City State"},
                {"VC", "Saint Vincent and the Grenadines"},
                {"VE", "Venezuela"},
                {"VG", "Virgin Islands (British)"},
                {"VI", "Virgin Islands (U.S.)"},
                {"VN", "Viet Nam"},
                {"VU", "Vanuatu"},
                {"WF", "Wallis and Futuna"},
                {"WS", "Samoa"},
                {"YE", "Yemen"},
                {"YT", "Mayotte"},
                {"YU", "Yugoslavia (former)"},
                {"ZA", "South Africa"},
                {"ZM", "Zambia"},
                {"ZR", "Zaire (former)"},
                {"ZW", "Zimbabwe"},
            };
            return map;
        }

    }
}
