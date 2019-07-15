using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace DynamicsAutorest.ModelExtensions
{
    public partial class MicrosoftDynamicsCRMcontact
    {
        [JsonProperty(PropertyName = "spice_dateofbirth")]
        //[JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd")]
        public DateTimeOffset SpiceDateofbirth { get; set; }
    }

    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
