// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Gov.Jag.Spice.Interfaces.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Collection of spice_company
    /// </summary>
    /// <remarks>
    /// Microsoft.Dynamics.CRM.spice_companyCollection
    /// </remarks>
    public partial class MicrosoftDynamicsCRMspiceCompanyCollection
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMspiceCompanyCollection class.
        /// </summary>
        public MicrosoftDynamicsCRMspiceCompanyCollection()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMspiceCompanyCollection class.
        /// </summary>
        public MicrosoftDynamicsCRMspiceCompanyCollection(IList<MicrosoftDynamicsCRMspiceCompany> value = default(IList<MicrosoftDynamicsCRMspiceCompany>))
        {
            Value = value;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public IList<MicrosoftDynamicsCRMspiceCompany> Value { get; set; }

    }
}
