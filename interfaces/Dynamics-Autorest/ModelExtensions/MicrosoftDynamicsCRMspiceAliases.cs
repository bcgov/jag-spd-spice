namespace Gov.Jag.Spice.Interfaces.Models
{
    using Newtonsoft.Json;

    public partial class MicrosoftDynamicsCRMspiceAliases
    {
        [JsonProperty(PropertyName = "spice_aliascontact@odata.bind")]
        public string SpiceAliascontactODataBind { get; set; }
    }
}
