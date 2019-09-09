namespace Gov.Jag.Spice.Interfaces.Models
{
    using Newtonsoft.Json;

    public partial class MicrosoftDynamicsCRMspicePreviousaddresses
    {
        [JsonProperty(PropertyName = "spice_ContactId@odata.bind")]
        public string SpiceContactIdODataBind { get; set; }
    }
}
