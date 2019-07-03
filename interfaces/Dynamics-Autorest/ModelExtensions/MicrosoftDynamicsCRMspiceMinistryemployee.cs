using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gov.Jag.Spice.Interfaces.Models
{
    public partial class MicrosoftDynamicsCRMspiceMinistryemployee
    {
        [JsonProperty(PropertyName = "spice_MinistryId@odata.bind")]
        public string SpiceMinistryIdODataBind { get; set; }
    }
}
