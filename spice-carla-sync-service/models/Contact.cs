using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SpiceCarlaSync.models;

namespace SpiceCarlaSync.models
{
    public class Contact
    {
        public string SpdJobId { get; set; }
        public string ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string DriversLicenceNumber { get; set; }
        public string DriverLicenceJurisdiction { get; set; }
        public string BCIdCardNumber { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public string Birthplace { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public GeneralYesNo? SelfDisclosure { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public AdoxioGenderCode? Gender { get; set; }

        public Address Address { get; set; }
        public List<Alias> Aliases { get; set; }
        public List<Address> PreviousAddresses { get; set; }
    }
}
