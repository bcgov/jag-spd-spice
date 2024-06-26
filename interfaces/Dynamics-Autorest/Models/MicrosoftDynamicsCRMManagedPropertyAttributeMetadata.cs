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
    /// ManagedPropertyAttributeMetadata
    /// </summary>
    public partial class MicrosoftDynamicsCRMManagedPropertyAttributeMetadata
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMManagedPropertyAttributeMetadata class.
        /// </summary>
        public MicrosoftDynamicsCRMManagedPropertyAttributeMetadata()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMManagedPropertyAttributeMetadata class.
        /// </summary>
        /// <param name="valueAttributeTypeCode">Possible values include:
        /// 'Boolean', 'Customer', 'DateTime', 'Decimal', 'Double', 'Integer',
        /// 'Lookup', 'Memo', 'Money', 'Owner', 'PartyList', 'Picklist',
        /// 'State', 'Status', 'String', 'Uniqueidentifier', 'CalendarRules',
        /// 'Virtual', 'BigInt', 'ManagedProperty', 'EntityName'</param>
        public MicrosoftDynamicsCRMManagedPropertyAttributeMetadata(string managedPropertyLogicalName = default(string), int? parentComponentType = default(int?), string parentAttributeName = default(string), string valueAttributeTypeCode = default(string))
        {
            ManagedPropertyLogicalName = managedPropertyLogicalName;
            ParentComponentType = parentComponentType;
            ParentAttributeName = parentAttributeName;
            ValueAttributeTypeCode = valueAttributeTypeCode;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ManagedPropertyLogicalName")]
        public string ManagedPropertyLogicalName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ParentComponentType")]
        public int? ParentComponentType { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ParentAttributeName")]
        public string ParentAttributeName { get; set; }

        /// <summary>
        /// Gets or sets possible values include: 'Boolean', 'Customer',
        /// 'DateTime', 'Decimal', 'Double', 'Integer', 'Lookup', 'Memo',
        /// 'Money', 'Owner', 'PartyList', 'Picklist', 'State', 'Status',
        /// 'String', 'Uniqueidentifier', 'CalendarRules', 'Virtual', 'BigInt',
        /// 'ManagedProperty', 'EntityName'
        /// </summary>
        [JsonProperty(PropertyName = "ValueAttributeTypeCode")]
        public string ValueAttributeTypeCode { get; set; }

    }
}
