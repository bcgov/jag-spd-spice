using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SpiceCarlaSync.models;

namespace SpdSync.models
{
    public class Contact
    {
        public string ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public GeneralYesNo SelfDisclosure { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public AdoxioGenderCode Gender { get; set; }
        public string DriversLicenceNumber { get; set; }
        public string BCIdCardNumber { get; set; }

        public Address Address { get; set; }
    }
}
