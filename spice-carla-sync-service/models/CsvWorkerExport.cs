using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpiceCarlaSync.models
{
    public class CsvWorkerExport
    {
            public string Lcrbworkerjobid { get; set; }

            // 9-12-18 - BC Registries number is no longer required for individuals (only business records, which are not yet part of the export)
            // new KeyValuePair<string, string>("AdoxioBcregistriesnumber", "Bcregistriesnumber"),
            public string Selfdisclosure { get; set; }
            public string Legalsurname { get; set; }
            public string Legalfirstname { get; set; }
            public string Legalmiddlename { get; set; }
            public DateTimeOffset? Birthdate { get; set; }
            public string Gendermf { get; set; }
            public string Birthplacecity { get; set; }
            public string Driverslicence { get; set; }
            public string Bcidentificationcardnumber { get; set; }
            public string Contactphone { get; set; }
            public string Personalemailaddress { get; set; }
            public string Addressline1 { get; set; }
            public string Addresscity { get; set; }
            public string Addressprovstate { get; set; }
            public string Addresscountry { get; set; }
            public string Addresspostalcode { get; set; }
            public string Alias1surname { get; set; }
            public string Alias1firstname { get; set; }
            public string Alias1middlename { get; set; }
            public string Alias2surname { get; set; }
            public string Alias2firstname { get; set; }
            public string Alias2middlename { get; set; }
            public string Alias3surname { get; set; }
            public string Alias3firstname { get; set; }
            public string Alias3middlename { get; set; }
            public string Alias4surname { get; set; }
            public string Alias4firstname { get; set; }
            public string Alias4middlename { get; set; }
            public string Alias5surname { get; set; }
            public string Alias5firstname { get; set; }
            public string Alias5middlename { get; set; }
            public string Previousstreetaddress1 { get; set; }
            public string Previouscity1 { get; set; }
            public string Previousprovstate1 { get; set; }
            public string Previouscountry1 { get; set; }
            public string Previouspostalcode1 { get; set; }
            public string Previousstreetaddress2 { get; set; }
            public string Previouscity2 { get; set; }
            public string Previousprovstate2 { get; set; }
            public string Previouscountry2 { get; set; }
            public string Previouspostalcode2 { get; set; }
            public string Previousstreetaddress3 { get; set; }
            public string Previouscity3 { get; set; }
            public string Previousprovstate3 { get; set; }
            public string Previouscountry3 { get; set; }
            public string Previouspostalcode3 { get; set; }
            public string Previousstreetaddress4 { get; set; }
            public string Previouscity4 { get; set; }
            public string Previousprovstate4 { get; set; }
            public string Previouscountry4 { get; set; }
            public string Previouspostalcode4 { get; set; }
            public string Previousstreetaddress5 { get; set; }
            public string Previouscity5 { get; set; }
            public string Previousprovstate5 { get; set; }
            public string Previouscountry5 { get; set; }
            public string Previouspostalcode5 { get; set; }
            public string Previousstreetaddress6 { get; set; }
            public string Previouscity6 { get; set; }
            public string Previousprovstate6 { get; set; }
            public string Previouscountry6 { get; set; }
            public string Previouspostalcode6 { get; set; }
            public string Previousstreetaddress7 { get; set; }
            public string Previouscity7 { get; set; }
            public string Previousprovstate7 { get; set; }
            public string Previouscountry7 { get; set; }
            public string Previouspostalcode7 { get; set; }
            public string Previousstreetaddress8 { get; set; }
            public string Previouscity8 { get; set; }
            public string Previousprovstate8 { get; set; }
            public string Previouscountry8 { get; set; }
            public string Previouspostalcode8 { get; set; }
            public string Previousstreetaddress9 { get; set; }
            public string Previouscity9 { get; set; }
            public string Previousprovstate9 { get; set; }
            public string Previouscountry9 { get; set; }
            public string Previouspostalcode9 { get; set; }
            public string Previousstreetaddressx { get; set; }
            public string Previouscityx { get; set; }
            public string Previousprovstatex { get; set; }
            public string Previouspostalcodex { get; set; }
            public string Previouscountryx { get; set; }
    }
}
