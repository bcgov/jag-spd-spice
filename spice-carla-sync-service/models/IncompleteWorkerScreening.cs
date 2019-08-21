using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SpiceCarlaSync.models;

namespace SpiceCarlaSync.models
{
    public class IncompleteWorkerScreening
    {
        public string RecordIdentifier { get; set; }
        public string Name { get; set; }

        public DateTimeOffset? BirthDate { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public GeneralYesNo SelfDisclosure { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public AdoxioGenderCode Gender { get; set; }
        public string Birthplace { get; set; }
        public string BCIdCardNumber { get; set; }
        public string DriversLicence { get; set; }
        public Contact Contact { get; set; }
        
        public Address Address { get; set; }
        public List<Alias> Aliases { get; set; }

        public List<Address> PreviousAddresses { get; set; }
    }
}
