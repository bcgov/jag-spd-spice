namespace Gov.Jag.Spice.Interfaces.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class MicrosoftDynamicsCRMadoxioTiedhouseconnection
    {
        
        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "adoxio_AccountId@odata.bind")]
        public string AccountODataBind { get; set; }

    }
}
