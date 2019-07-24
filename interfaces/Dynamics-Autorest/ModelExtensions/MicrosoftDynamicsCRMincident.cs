using System;
using Newtonsoft.Json;

namespace Gov.Jag.Spice.Interfaces.Models
{
    public partial class MicrosoftDynamicsCRMincident
    {
        [JsonProperty(PropertyName = "customerid_contact@odata.bind")]
        public string CustomerIdODataBind { get; set; }

        [JsonProperty(PropertyName = "spice_ApplyingPersonId@odata.bind")]
        public string ApplyingPersonIdODataBind { get; set; }

        [JsonProperty(PropertyName = "spice_Client@odata.bind")]
        public string SpiceClientIdODataBind { get; set; }

        [JsonProperty(PropertyName = "spice_ServiceId@odata.bind")]
        public string SpiceServiceIdODataBind { get; set; }

        [JsonProperty(PropertyName = "spice_ReasonforScreeningId@odata.bind")]
        public string SpiceReasonForScreeningIdODataBind { get; set; }

        [JsonProperty(PropertyName = "spice_ReturnResultto@odata.bind")]
        public string SpiceReturnToIdODataBind { get; set; }

        [JsonProperty(PropertyName = "customerid_account@odata.bind")]
        public string CustomerIdAccountOdataBind { get; set; }

        [JsonProperty(PropertyName = "parentcaseid@odata.bind")]
        public string ParentCaseIdOdataBind { get; set; }
    }
}
