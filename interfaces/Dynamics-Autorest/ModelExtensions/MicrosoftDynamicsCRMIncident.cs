using System;
using Newtonsoft.Json;

namespace Gov.Jag.Spice.Interfaces.Models
{
    public partial class MicrosoftDynamicsCRMincident
    {
        [JsonProperty(PropertyName = "customerid_account@odata.bind")]
        public string CustomerIdAccountOdataBind { get; set; }
        [JsonProperty(PropertyName = "customerid_contact@odata.bind")]
        public string CustomerIdContactOdataBind { get; set; }
        [JsonProperty(PropertyName = "parentcaseid@odata.bind")]
        public string ParentCaseIdOdataBind { get; set; }
    }
}
