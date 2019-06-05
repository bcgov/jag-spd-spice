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
            Map(m => m.LCRBBusinessJobId).Name("LCRB BUSINESS JOB ID").Index(0);
            Map(m => m.Lcrbworkerjobid).Name("LCRB ASSOCIATE JOB ID").Index(1);
            Map(m => m.Selfdisclosure).Name("SELF DECLARATION Y/N").Index(2);
            Map(m => m.Legalsurname).Name("LAST").Index(3);
            Map(m => m.Legalfirstname).Name("FIRST").Index(4);
            Map(m => m.Legalmiddlename).Name("MIDDLE").Index(5);
            Map(m => m.Birthdate).Name("DOB").Index(6);
            Map(m => m.Gendermf).Name("GENDER").Index(7);
            Map(m => m.Birthplacecity).Name("BIRTH PLACE").Index(8);
            Map(m => m.Driverslicence).Name("DRIVERS LICENSE").Index(9);
            Map(m => m.Bcidentificationcardnumber).Name("BCID").Index(10);
            Map(m => m.Contactphone).Name("PHONE NUMBER").Index(11);
            Map(m => m.Personalemailaddress).Name("EMAIL").Index(12);
            Map(m => m.Addressline1).Name("STREET ADDRESS").Index(13);
            Map(m => m.Addresscity).Name("CITY").Index(14);
            Map(m => m.Addressprovstate).Name("PROVINCE").Index(15);
            Map(m => m.Addresscountry).Name("COUNTRY").Index(16);
            Map(m => m.Addresspostalcode).Name("POSTAL").Index(17);
            Map(m => m.Alias1surname).Name("ALIAS LAST 1").Index(18);
            Map(m => m.Alias1firstname).Name("ALIAS FIRST 1").Index(19);
            Map(m => m.Alias1middlename).Name("ALIAS MIDDLE 1").Index(20);
            Map(m => m.Alias1surname).Name("ALIAS LAST 2").Index(21);
            Map(m => m.Alias1firstname).Name("ALIAS FIRST 2").Index(22);
            Map(m => m.Alias1middlename).Name("ALIAS MIDDLE 2").Index(23);
            Map(m => m.Alias1surname).Name("ALIAS LAST 3").Index(24);
            Map(m => m.Alias1firstname).Name("ALIAS FIRST 3").Index(25);
            Map(m => m.Alias1middlename).Name("ALIAS MIDDLE 3").Index(26);
            Map(m => m.Alias1surname).Name("ALIAS LAST 4").Index(27);
            Map(m => m.Alias1firstname).Name("ALIAS FIRST 4").Index(28);
            Map(m => m.Alias1middlename).Name("ALIAS MIDDLE 4").Index(29);
            Map(m => m.Alias1surname).Name("ALIAS LAST 5").Index(30);
            Map(m => m.Alias1firstname).Name("ALIAS FIRST 5").Index(31);
            Map(m => m.Alias1middlename).Name("ALIAS MIDDLE 5").Index(32);
            Map(m => m.Previousstreetaddress1).Name("PREV STREET 1").Index(33);
            Map(m => m.Previouscity1).Name("PREV CITY 1").Index(34);
            Map(m => m.Previousprovstate1).Name("PREV PROVINCE 1").Index(35);
            Map(m => m.Previouscountry1).Name("PREV COUNTRY 1").Index(36);
            Map(m => m.Previouspostalcode1).Name("PREV POSTAL 1").Index(37);
            Map(m => m.Previousstreetaddress2).Name("PREV STREET 2").Index(38);
            Map(m => m.Previouscity2).Name("PREV CITY 2").Index(39);
            Map(m => m.Previousprovstate2).Name("PREV PROVINCE 2").Index(40);
            Map(m => m.Previouscountry2).Name("PREV COUNTRY 2").Index(41);
            Map(m => m.Previouspostalcode2).Name("PREV POSTAL 2").Index(42);
            Map(m => m.Previousstreetaddress3).Name("PREV STREET 3").Index(43);
            Map(m => m.Previouscity3).Name("PREV CITY 3").Index(44);
            Map(m => m.Previousprovstate3).Name("PREV PROVINCE 3").Index(45);
            Map(m => m.Previouscountry3).Name("PREV COUNTRY 3").Index(46);
            Map(m => m.Previouspostalcode3).Name("PREV POSTAL 3").Index(47);
            Map(m => m.Previousstreetaddress4).Name("PREV STREET 4").Index(48);
            Map(m => m.Previouscity4).Name("PREV CITY 4").Index(49);
            Map(m => m.Previousprovstate4).Name("PREV PROVINCE 4").Index(50);
            Map(m => m.Previouscountry4).Name("PREV COUNTRY 4").Index(51);
            Map(m => m.Previouspostalcode4).Name("PREV POSTAL 4").Index(52);
            Map(m => m.Previousstreetaddress5).Name("PREV STREET 5").Index(53);
            Map(m => m.Previouscity5).Name("PREV CITY 5").Index(54);
            Map(m => m.Previousprovstate5).Name("PREV PROVINCE 5").Index(55);
            Map(m => m.Previouscountry5).Name("PREV COUNTRY 5").Index(56);
            Map(m => m.Previouspostalcode5).Name("PREV POSTAL 5").Index(57);
            Map(m => m.Previousstreetaddress6).Name("PREV STREET 6").Index(58);
            Map(m => m.Previouscity6).Name("PREV CITY 6").Index(59);
            Map(m => m.Previousprovstate6).Name("PREV PROVINCE 6").Index(60);
            Map(m => m.Previouscountry6).Name("PREV COUNTRY 6").Index(61);
            Map(m => m.Previouspostalcode6).Name("PREV POSTAL 6").Index(62);
            Map(m => m.Previousstreetaddress7).Name("PREV STREET 7").Index(63);
            Map(m => m.Previouscity7).Name("PREV CITY 7").Index(64);
            Map(m => m.Previousprovstate7).Name("PREV PROVINCE 7").Index(65);
            Map(m => m.Previouscountry7).Name("PREV COUNTRY 7").Index(66);
            Map(m => m.Previouspostalcode7).Name("PREV POSTAL 7").Index(67);
            Map(m => m.Previousstreetaddress8).Name("PREV STREET 8").Index(68);
            Map(m => m.Previouscity8).Name("PREV CITY 8").Index(69);
            Map(m => m.Previousprovstate8).Name("PREV PROVINCE 8").Index(70);
            Map(m => m.Previouscountry8).Name("PREV COUNTRY 8").Index(71);
            Map(m => m.Previouspostalcode8).Name("PREV POSTAL 8").Index(72);
            Map(m => m.Previousstreetaddress9).Name("PREV STREET 9").Index(73);
            Map(m => m.Previouscity9).Name("PREV CITY 9").Index(74);
            Map(m => m.Previousprovstate9).Name("PREV PROVINCE 9").Index(75);
            Map(m => m.Previouscountry9).Name("PREV COUNTRY 9").Index(76);
            Map(m => m.Previouspostalcode9).Name("PREV POSTAL 9").Index(77);
            Map(m => m.Previousstreetaddressx).Name("PREV STREET x").Index(78);
            Map(m => m.Previouscityx).Name("PREV CITY x").Index(79);
            Map(m => m.Previousprovstatex).Name("PREV PROVINCE x").Index(80);
            Map(m => m.Previouspostalcodex).Name("PREV POSTAL x").Index(81);
            Map(m => m.Previouscountryx).Name("PREV COUNTRY x").Index(82);
        }
    }
}
