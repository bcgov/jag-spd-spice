using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
    }
}
