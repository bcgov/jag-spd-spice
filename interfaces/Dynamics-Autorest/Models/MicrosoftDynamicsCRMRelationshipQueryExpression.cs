// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Gov.Jag.Spice.Interfaces.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// RelationshipQueryExpression
    /// </summary>
    public partial class MicrosoftDynamicsCRMRelationshipQueryExpression
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMRelationshipQueryExpression class.
        /// </summary>
        public MicrosoftDynamicsCRMRelationshipQueryExpression()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMRelationshipQueryExpression class.
        /// </summary>
        public MicrosoftDynamicsCRMRelationshipQueryExpression(MicrosoftDynamicsCRMMetadataFilterExpression criteria = default(MicrosoftDynamicsCRMMetadataFilterExpression), MicrosoftDynamicsCRMMetadataPropertiesExpression properties = default(MicrosoftDynamicsCRMMetadataPropertiesExpression))
        {
            Criteria = criteria;
            Properties = properties;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Criteria")]
        public MicrosoftDynamicsCRMMetadataFilterExpression Criteria { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Properties")]
        public MicrosoftDynamicsCRMMetadataPropertiesExpression Properties { get; set; }

    }
}
