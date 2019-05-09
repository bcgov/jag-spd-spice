using System;
namespace SpiceCarlaSync.models
{
    public class CsvBusinessExport
    {
        public string OrganizationName { get; set; }
        public string JobId { get; set; }
        public string BusinessNumber { get; set; }
        public string BusinessAddressStreet1 { get; set; }
        public string BusinessCity { get; set; }
        public string BusinessStateProvince { get; set; }
        public string BusinessCountry { get; set; }
        public string BusinessPostal { get; set; }
        public string EstablishmentParcelId { get; set; }
        public string EstablishmentAddressStreet1 { get; set; }
        public string EstablishmentCity { get; set; }
        public string EstablishmentStateProvince { get; set; }
        public string EstablishmentCountry { get; set; }
        public string EstablishmentPostal { get; set; }
        public string ContactPersonSurname { get; set; }
        public string ContactPersonFirstname { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
    }
}
