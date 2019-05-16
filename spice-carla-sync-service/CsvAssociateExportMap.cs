using System;
using CsvHelper;
using CsvHelper.Configuration;
using SpiceCarlaSync.models;

namespace SpiceCarlaSync
{
    public sealed class CsvAssociateExportMap : ClassMap<CsvAssociateExport>
    {
        public CsvAssociateExportMap()
        {
            Map(m => m.Lcrbworkerjobid).Name("LCRB ASSOCIATE JOB ID");
            Map(m => m.LCRBBusinessJobId).Name("LCRB BUSINESS JOB ID");
            Map(m => m.Selfdisclosure).Name("SELF DECLARATION Y/N");
            Map(m => m.Legalsurname).Name("LAST");
            Map(m => m.Legalfirstname).Name("FIRST");
            Map(m => m.Legalmiddlename).Name("MIDDLE");
            Map(m => m.Birthdate).Name("DOB");
            Map(m => m.Gendermf).Name("GENDER");
            Map(m => m.Birthplacecity).Name("BIRTH PLACE");
            Map(m => m.Driverslicence).Name("DRIVERS LICENSE");
            Map(m => m.Bcidentificationcardnumber).Name("BCID");
            Map(m => m.Contactphone).Name("PHONE NUMBER");
            Map(m => m.Personalemailaddress).Name("EMAIL");
            Map(m => m.Addressline1).Name("STREET ADDRESS");
            Map(m => m.Addresscity).Name("CITY");
            Map(m => m.Addressprovstate).Name("PROVINCE");
            Map(m => m.Addresscountry).Name("COUNTRY");
            Map(m => m.Addresspostalcode).Name("POSTAL");
            Map(m => m.Alias1surname).Name("ALIAS LAST 1");
            Map(m => m.Alias1firstname).Name("ALIAS FIRST 1");
            Map(m => m.Alias1middlename).Name("ALIAS MIDDLE 1");
            Map(m => m.Alias1surname).Name("ALIAS LAST 2");
            Map(m => m.Alias1firstname).Name("ALIAS FIRST 2");
            Map(m => m.Alias1middlename).Name("ALIAS MIDDLE 2");
            Map(m => m.Alias1surname).Name("ALIAS LAST 3");
            Map(m => m.Alias1firstname).Name("ALIAS FIRST 3");
            Map(m => m.Alias1middlename).Name("ALIAS MIDDLE 3");
            Map(m => m.Alias1surname).Name("ALIAS LAST 4");
            Map(m => m.Alias1firstname).Name("ALIAS FIRST 4");
            Map(m => m.Alias1middlename).Name("ALIAS MIDDLE 4");
            Map(m => m.Alias1surname).Name("ALIAS LAST 5");
            Map(m => m.Alias1firstname).Name("ALIAS FIRST 5");
            Map(m => m.Alias1middlename).Name("ALIAS MIDDLE 5");
            Map(m => m.Previousstreetaddress1).Name("PREV STREET 1");
            Map(m => m.Previouscity1).Name("PREV CITY 1");
            Map(m => m.Previousprovstate1).Name("PREV PROVINCE 1");
            Map(m => m.Previouscountry1).Name("PREV COUNTRY 1");
            Map(m => m.Previouspostalcode1).Name("PREV POSTAL 1");
            Map(m => m.Previousstreetaddress2).Name("PREV STREET 2");
            Map(m => m.Previouscity2).Name("PREV CITY 2");
            Map(m => m.Previousprovstate2).Name("PREV PROVINCE 2");
            Map(m => m.Previouscountry2).Name("PREV COUNTRY 2");
            Map(m => m.Previouspostalcode2).Name("PREV POSTAL 2");
            Map(m => m.Previousstreetaddress3).Name("PREV STREET 3");
            Map(m => m.Previouscity3).Name("PREV CITY 3");
            Map(m => m.Previousprovstate3).Name("PREV PROVINCE 3");
            Map(m => m.Previouscountry3).Name("PREV COUNTRY 3");
            Map(m => m.Previouspostalcode3).Name("PREV POSTAL 3");
            Map(m => m.Previousstreetaddress4).Name("PREV STREET 4");
            Map(m => m.Previouscity4).Name("PREV CITY 4");
            Map(m => m.Previousprovstate4).Name("PREV PROVINCE 4");
            Map(m => m.Previouscountry4).Name("PREV COUNTRY 4");
            Map(m => m.Previouspostalcode4).Name("PREV POSTAL 4");
            Map(m => m.Previousstreetaddress5).Name("PREV STREET 5");
            Map(m => m.Previouscity5).Name("PREV CITY 5");
            Map(m => m.Previousprovstate5).Name("PREV PROVINCE 5");
            Map(m => m.Previouscountry5).Name("PREV COUNTRY 5");
            Map(m => m.Previouspostalcode5).Name("PREV POSTAL 5");
            Map(m => m.Previousstreetaddress6).Name("PREV STREET 6");
            Map(m => m.Previouscity6).Name("PREV CITY 6");
            Map(m => m.Previousprovstate6).Name("PREV PROVINCE 6");
            Map(m => m.Previouscountry6).Name("PREV COUNTRY 6");
            Map(m => m.Previouspostalcode6).Name("PREV POSTAL 6");
            Map(m => m.Previousstreetaddress7).Name("PREV STREET 7");
            Map(m => m.Previouscity7).Name("PREV CITY 7");
            Map(m => m.Previousprovstate7).Name("PREV PROVINCE 7");
            Map(m => m.Previouscountry7).Name("PREV COUNTRY 7");
            Map(m => m.Previouspostalcode7).Name("PREV POSTAL 7");
            Map(m => m.Previousstreetaddress8).Name("PREV STREET 8");
            Map(m => m.Previouscity8).Name("PREV CITY 8");
            Map(m => m.Previousprovstate8).Name("PREV PROVINCE 8");
            Map(m => m.Previouscountry8).Name("PREV COUNTRY 8");
            Map(m => m.Previouspostalcode8).Name("PREV POSTAL 8");
            Map(m => m.Previousstreetaddress9).Name("PREV STREET 9");
            Map(m => m.Previouscity9).Name("PREV CITY 9");
            Map(m => m.Previousprovstate9).Name("PREV PROVINCE 9");
            Map(m => m.Previouscountry9).Name("PREV COUNTRY 9");
            Map(m => m.Previouspostalcode9).Name("PREV POSTAL 9");
            Map(m => m.Previousstreetaddressx).Name("PREV STREET x");
            Map(m => m.Previouscityx).Name("PREV CITY x");
            Map(m => m.Previousprovstatex).Name("PREV PROVINCE x");
            Map(m => m.Previouspostalcodex).Name("PREV POSTAL x");
            Map(m => m.Previouscountryx).Name("PREV COUNTRY x");
        }
    }
}
