namespace Gov.Jag.Spice.Interfaces.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public enum SecurityStatusPicklist
    {
        PASS = 845280000,
        FAIL = 845280001,
        WITHDRAWN = 845280003
    }

    public partial class MicrosoftDynamicsCRMadoxioWorker
    {
        
        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_adoxio_contactid_value@odata.bind")]
        public string ContactIdAccountODataBind { get; set; }

        
    }
}
