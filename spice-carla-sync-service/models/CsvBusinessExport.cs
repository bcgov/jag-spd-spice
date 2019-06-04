using System;
using SpdSync.models;

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

        public static CsvBusinessExport CreateFromRequest(ApplicationScreeningRequest request)
        {
            return new CsvBusinessExport()
            {
                OrganizationName = request.ApplicantName,
                JobId = request.RecordIdentifier,
                BusinessNumber = request.BusinessNumber,
                BusinessAddressStreet1 = request.BusinessAddress.AddressStreet1,
                BusinessCity = request.BusinessAddress.City,
                BusinessStateProvince = request.BusinessAddress.StateProvince,
                BusinessCountry = request.BusinessAddress.Country,
                BusinessPostal = request.BusinessAddress.Postal,
                EstablishmentParcelId = request.Establishment.ParcelId,
                EstablishmentAddressStreet1 = request.Establishment.Address.AddressStreet1,
                EstablishmentCity = request.Establishment.Address.City,
                EstablishmentStateProvince = request.Establishment.Address.StateProvince,
                EstablishmentCountry = request.Establishment.Address.Country,
                EstablishmentPostal = request.Establishment.Address.Postal,
                ContactPersonSurname = request.ContactPerson.LastName,
                ContactPersonFirstname = request.ContactPerson.FirstName,
                ContactPhone = request.ContactPerson.PhoneNumber,
                ContactEmail = request.ContactPerson.Email
            };
        }
    }
}
