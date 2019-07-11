namespace Gov.Jag.Spice.Interfaces.Models
{
    using Newtonsoft.Json;

    public partial class MicrosoftDynamicsCRMspiceAccountcaseassignment
    {
        [JsonProperty(PropertyName = "spice_ContactCaseAssignmentId@odata.bind")]
        public string SpiceContactCaseAssignmentIdODataBind { get; set; }

        [JsonProperty(PropertyName = "spice_BusinessId@odata.bind")]
        public string SpiceBusinessIdODataBind { get; set; }
    }
}
