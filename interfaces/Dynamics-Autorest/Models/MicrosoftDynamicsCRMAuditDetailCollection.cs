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
    /// AuditDetailCollection
    /// </summary>
    public partial class MicrosoftDynamicsCRMAuditDetailCollection
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMAuditDetailCollection class.
        /// </summary>
        public MicrosoftDynamicsCRMAuditDetailCollection()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMAuditDetailCollection class.
        /// </summary>
        public MicrosoftDynamicsCRMAuditDetailCollection(IList<MicrosoftDynamicsCRMAuditDetail> auditDetails = default(IList<MicrosoftDynamicsCRMAuditDetail>), bool? moreRecords = default(bool?), string pagingCookie = default(string), int? totalRecordCount = default(int?))
        {
            AuditDetails = auditDetails;
            MoreRecords = moreRecords;
            PagingCookie = pagingCookie;
            TotalRecordCount = totalRecordCount;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "AuditDetails")]
        public IList<MicrosoftDynamicsCRMAuditDetail> AuditDetails { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "MoreRecords")]
        public bool? MoreRecords { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "PagingCookie")]
        public string PagingCookie { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "TotalRecordCount")]
        public int? TotalRecordCount { get; set; }

    }
}
