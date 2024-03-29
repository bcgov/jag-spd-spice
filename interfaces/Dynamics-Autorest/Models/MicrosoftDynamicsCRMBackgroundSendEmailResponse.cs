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
    /// BackgroundSendEmailResponse
    /// </summary>
    public partial class MicrosoftDynamicsCRMBackgroundSendEmailResponse
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMBackgroundSendEmailResponse class.
        /// </summary>
        public MicrosoftDynamicsCRMBackgroundSendEmailResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMBackgroundSendEmailResponse class.
        /// </summary>
        public MicrosoftDynamicsCRMBackgroundSendEmailResponse(IList<bool?> hasAttachments = default(IList<bool?>), IList<MicrosoftDynamicsCRMemail> entityCollection = default(IList<MicrosoftDynamicsCRMemail>))
        {
            HasAttachments = hasAttachments;
            EntityCollection = entityCollection;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "HasAttachments")]
        public IList<bool?> HasAttachments { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "EntityCollection")]
        public IList<MicrosoftDynamicsCRMemail> EntityCollection { get; set; }

    }
}
