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

        public static CsvBusinessExport CreateFromRequest(IncompleteApplicationScreening request)
        {
            return new CsvBusinessExport()
            {
                OrganizationName = request.ApplicantName?.Replace(",", ""),
                JobId = request.RecordIdentifier?.Replace(",", ""),
                BusinessNumber = request.BusinessNumber?.Replace(",", ""),
                BusinessAddressStreet1 = request.BusinessAddress.AddressStreet1?.Replace(",", ""),
                BusinessCity = request.BusinessAddress.City?.Replace(",", ""),
                BusinessStateProvince = request.BusinessAddress.StateProvince?.Replace(",", ""),
                BusinessCountry = request.BusinessAddress.Country?.Replace(",", ""),
                BusinessPostal = request.BusinessAddress.Postal?.Replace(",", ""),
                EstablishmentParcelId = request.Establishment.ParcelId?.Replace(",", ""),
                EstablishmentAddressStreet1 = request.Establishment.Address.AddressStreet1?.Replace(",", ""),
                EstablishmentCity = request.Establishment.Address.City?.Replace(",", ""),
                EstablishmentStateProvince = request.Establishment.Address.StateProvince?.Replace(",", ""),
                EstablishmentCountry = request.Establishment.Address.Country?.Replace(",", ""),
                EstablishmentPostal = request.Establishment.Address.Postal?.Replace(",", ""),
                ContactPersonSurname = request.ContactPerson.LastName?.Replace(",", ""),
                ContactPersonFirstname = request.ContactPerson.FirstName?.Replace(",", ""),
                ContactPhone = request.ContactPerson.PhoneNumber?.Replace(",", ""),
                ContactEmail = request.ContactPerson.Email?.Replace(",", "")
            };
        }
    }
}
