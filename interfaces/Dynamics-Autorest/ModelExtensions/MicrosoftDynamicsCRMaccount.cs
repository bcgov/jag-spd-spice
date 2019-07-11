using System;
using Newtonsoft.Json;

namespace Gov.Jag.Spice.Interfaces.Models
{
    public partial class MicrosoftDynamicsCRMaccount
    {
        [JsonProperty(PropertyName = "spice_CompanyId@odata.bind")]
        public string SpiceCompanyIdOdataBind { get; set; }

        [JsonProperty(PropertyName = "primarycontactid@odata.bind")]
        public string PrimaryContactIdOdataBind { get; set; }
    }
}
