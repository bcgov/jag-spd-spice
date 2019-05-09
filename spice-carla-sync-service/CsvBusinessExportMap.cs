using System;
using CsvHelper.Configuration;
using SpiceCarlaSync.models;

namespace SpiceCarlaSync
{
    public sealed class CsvBusinessExportMap : ClassMap<CsvBusinessExport>
    {
        public CsvBusinessExportMap()
        {
            Map(m => m.JobId).Name("LCRB BUSINESS JOB ID");
            Map(m => m.OrganizationName).Name("ORGANIZATION NAME");
            Map(m => m.EstablishmentParcelId).Name("PARCEL ID NUMBER");
            Map(m => m.BusinessNumber).Name("BC CORP REG NUMBER");
            Map(m => m.BusinessAddressStreet1).Name("BUSINESS STREET ADDRESS");
            Map(m => m.BusinessCity).Name("BUSINESS CITY");
            Map(m => m.BusinessStateProvince).Name("BUSINESS PROVINCE");
            Map(m => m.BusinessCountry).Name("BUSINESS COUNTRY");
            Map(m => m.EstablishmentAddressStreet1).Name("BUSINESS POSTAL CODE");
            Map(m => m.EstablishmentCity).Name("LOCATION STREET ADDRESS");
            Map(m => m.EstablishmentStateProvince).Name("LOCATION CITY");
            Map(m => m.EstablishmentPostal).Name("LOCATION PROVINCE");
            Map(m => m.EstablishmentCountry).Name("LOCATION COUNTRY");
            Map(m => m.ContactPhone).Name("CONTACT PERSON PHONE NUMBER");
            Map(m => m.ContactPersonSurname).Name("CONTACT PERSON SURNAME");
            Map(m => m.ContactPersonFirstname).Name("CONTACT PERSON FIRSTNAME");
            Map(m => m.ContactEmail).Name("CONTACT PERSON EMAIL ADDRESS");
        }
    }
}
