using System;
using CsvHelper.Configuration;
using SpiceCarlaSync.models;

namespace SpiceCarlaSync
{
    public sealed class CsvBusinessExportMap : ClassMap<CsvBusinessExport>
    {
        public CsvBusinessExportMap()
        {
            Map(m => m.OrganizationName).Name("ORGANIZATION NAME").Index(0);
            Map(m => m.JobId).Name("LCRB BUSINESS JOB ID").Index(1);
            Map(m => m.EstablishmentParcelId).Name("PARCEL ID NUMBER").Index(2);
            Map(m => m.BusinessNumber).Name("BC CORP REG NUMBER").Index(3);
            Map(m => m.BusinessAddressStreet1).Name("BUSINESS STREET ADDRESS").Index(4);
            Map(m => m.BusinessCity).Name("BUSINESS CITY").Index(5);
            Map(m => m.BusinessStateProvince).Name("BUSINESS PROVINCE").Index(6);
            Map(m => m.BusinessCountry).Name("BUSINESS COUNTRY").Index(7);
            Map(m => m.BusinessPostal).Name("BUSINESS POSTAL CODE").Index(8);
            Map(m => m.EstablishmentAddressStreet1).Name("LOCATION STREET ADDRESS").Index(9);
            Map(m => m.EstablishmentCity).Name("LOCATION CITY").Index(10);
            Map(m => m.EstablishmentStateProvince).Name("LOCATION PROVINCE").Index(11);
            Map(m => m.EstablishmentCountry).Name("LOCATION COUNTRY").Index(12);
            Map(m => m.EstablishmentPostal).Name("LOCATION POSTAL CODE").Index(13);
            Map(m => m.ContactPhone).Name("CONTACT PERSON PHONE NUMBER").Index(14);
            Map(m => m.ContactPersonSurname).Name("CONTACT PERSON SURNAME").Index(15);
            Map(m => m.ContactPersonFirstname).Name("CONTACT PERSON FIRSTNAME").Index(16);
            Map(m => m.ContactEmail).Name("CONTACT PERSON EMAIL ADDRESS").Index(17);
        }
    }
}
