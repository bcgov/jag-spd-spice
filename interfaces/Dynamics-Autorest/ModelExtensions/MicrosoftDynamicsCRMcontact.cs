using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Gov.Jag.Spice.Interfaces.Models
{
    public partial class MicrosoftDynamicsCRMcontact
    {
        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_dateofbirth")]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public System.DateTimeOffset? SpiceDateofbirth { get; set; }
    }
    
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}


