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
    /// CreateActivitiesListResponse
    /// </summary>
    public partial class MicrosoftDynamicsCRMCreateActivitiesListResponse
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMCreateActivitiesListResponse class.
        /// </summary>
        public MicrosoftDynamicsCRMCreateActivitiesListResponse()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMCreateActivitiesListResponse class.
        /// </summary>
        public MicrosoftDynamicsCRMCreateActivitiesListResponse(string bulkOperationId = default(string))
        {
            BulkOperationId = bulkOperationId;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "BulkOperationId")]
        public string BulkOperationId { get; set; }

    }
}
