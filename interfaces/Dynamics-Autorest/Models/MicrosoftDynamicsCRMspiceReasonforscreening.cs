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
    /// Microsoft.Dynamics.CRM.spice_reasonforscreening
    /// </summary>
    public partial class MicrosoftDynamicsCRMspiceReasonforscreening
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMspiceReasonforscreening class.
        /// </summary>
        public MicrosoftDynamicsCRMspiceReasonforscreening()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMspiceReasonforscreening class.
        /// </summary>
        public MicrosoftDynamicsCRMspiceReasonforscreening(string _owningteamValue = default(string), string _owneridValue = default(string), string _modifiedonbehalfbyValue = default(string), System.DateTimeOffset? overriddencreatedon = default(System.DateTimeOffset?), string _createdbyValue = default(string), string _createdonbehalfbyValue = default(string), int? statuscode = default(int?), string _modifiedbyValue = default(string), int? importsequencenumber = default(int?), int? statecode = default(int?), int? utcconversiontimezonecode = default(int?), System.DateTimeOffset? modifiedon = default(System.DateTimeOffset?), string spiceReasonforscreeningid = default(string), System.DateTimeOffset? createdon = default(System.DateTimeOffset?), string spiceName = default(string), string versionnumber = default(string), int? timezoneruleversionnumber = default(int?), string _owningbusinessunitValue = default(string), string _owninguserValue = default(string), MicrosoftDynamicsCRMsystemuser createdby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser createdonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser owninguser = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMteam owningteam = default(MicrosoftDynamicsCRMteam), MicrosoftDynamicsCRMprincipal ownerid = default(MicrosoftDynamicsCRMprincipal), MicrosoftDynamicsCRMbusinessunit owningbusinessunit = default(MicrosoftDynamicsCRMbusinessunit), IList<MicrosoftDynamicsCRMsyncerror> spiceReasonforscreeningSyncErrors = default(IList<MicrosoftDynamicsCRMsyncerror>), IList<MicrosoftDynamicsCRMduplicaterecord> spiceReasonforscreeningDuplicateMatchingRecord = default(IList<MicrosoftDynamicsCRMduplicaterecord>), IList<MicrosoftDynamicsCRMduplicaterecord> spiceReasonforscreeningDuplicateBaseRecord = default(IList<MicrosoftDynamicsCRMduplicaterecord>), IList<MicrosoftDynamicsCRMasyncoperation> spiceReasonforscreeningAsyncOperations = default(IList<MicrosoftDynamicsCRMasyncoperation>), IList<MicrosoftDynamicsCRMmailboxtrackingfolder> spiceReasonforscreeningMailboxTrackingFolders = default(IList<MicrosoftDynamicsCRMmailboxtrackingfolder>), IList<MicrosoftDynamicsCRMprocesssession> spiceReasonforscreeningProcessSession = default(IList<MicrosoftDynamicsCRMprocesssession>), IList<MicrosoftDynamicsCRMbulkdeletefailure> spiceReasonforscreeningBulkDeleteFailures = default(IList<MicrosoftDynamicsCRMbulkdeletefailure>), IList<MicrosoftDynamicsCRMprincipalobjectattributeaccess> spiceReasonforscreeningPrincipalObjectAttributeAccesses = default(IList<MicrosoftDynamicsCRMprincipalobjectattributeaccess>), IList<MicrosoftDynamicsCRMincident> spiceReasonforscreeningIncident = default(IList<MicrosoftDynamicsCRMincident>))
        {
            this._owningteamValue = _owningteamValue;
            this._owneridValue = _owneridValue;
            this._modifiedonbehalfbyValue = _modifiedonbehalfbyValue;
            Overriddencreatedon = overriddencreatedon;
            this._createdbyValue = _createdbyValue;
            this._createdonbehalfbyValue = _createdonbehalfbyValue;
            Statuscode = statuscode;
            this._modifiedbyValue = _modifiedbyValue;
            Importsequencenumber = importsequencenumber;
            Statecode = statecode;
            Utcconversiontimezonecode = utcconversiontimezonecode;
            Modifiedon = modifiedon;
            SpiceReasonforscreeningid = spiceReasonforscreeningid;
            Createdon = createdon;
            SpiceName = spiceName;
            Versionnumber = versionnumber;
            Timezoneruleversionnumber = timezoneruleversionnumber;
            this._owningbusinessunitValue = _owningbusinessunitValue;
            this._owninguserValue = _owninguserValue;
            Createdby = createdby;
            Createdonbehalfby = createdonbehalfby;
            Modifiedby = modifiedby;
            Modifiedonbehalfby = modifiedonbehalfby;
            Owninguser = owninguser;
            Owningteam = owningteam;
            Ownerid = ownerid;
            Owningbusinessunit = owningbusinessunit;
            SpiceReasonforscreeningSyncErrors = spiceReasonforscreeningSyncErrors;
            SpiceReasonforscreeningDuplicateMatchingRecord = spiceReasonforscreeningDuplicateMatchingRecord;
            SpiceReasonforscreeningDuplicateBaseRecord = spiceReasonforscreeningDuplicateBaseRecord;
            SpiceReasonforscreeningAsyncOperations = spiceReasonforscreeningAsyncOperations;
            SpiceReasonforscreeningMailboxTrackingFolders = spiceReasonforscreeningMailboxTrackingFolders;
            SpiceReasonforscreeningProcessSession = spiceReasonforscreeningProcessSession;
            SpiceReasonforscreeningBulkDeleteFailures = spiceReasonforscreeningBulkDeleteFailures;
            SpiceReasonforscreeningPrincipalObjectAttributeAccesses = spiceReasonforscreeningPrincipalObjectAttributeAccesses;
            SpiceReasonforscreeningIncident = spiceReasonforscreeningIncident;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_owningteam_value")]
        public string _owningteamValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_ownerid_value")]
        public string _owneridValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedonbehalfby_value")]
        public string _modifiedonbehalfbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "overriddencreatedon")]
        public System.DateTimeOffset? Overriddencreatedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_createdby_value")]
        public string _createdbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_createdonbehalfby_value")]
        public string _createdonbehalfbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "statuscode")]
        public int? Statuscode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedby_value")]
        public string _modifiedbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "importsequencenumber")]
        public int? Importsequencenumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "statecode")]
        public int? Statecode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "utcconversiontimezonecode")]
        public int? Utcconversiontimezonecode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedon")]
        public System.DateTimeOffset? Modifiedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_reasonforscreeningid")]
        public string SpiceReasonforscreeningid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdon")]
        public System.DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_name")]
        public string SpiceName { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public string Versionnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "timezoneruleversionnumber")]
        public int? Timezoneruleversionnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_owningbusinessunit_value")]
        public string _owningbusinessunitValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_owninguser_value")]
        public string _owninguserValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdby")]
        public MicrosoftDynamicsCRMsystemuser Createdby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdonbehalfby")]
        public MicrosoftDynamicsCRMsystemuser Createdonbehalfby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedby")]
        public MicrosoftDynamicsCRMsystemuser Modifiedby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedonbehalfby")]
        public MicrosoftDynamicsCRMsystemuser Modifiedonbehalfby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "owninguser")]
        public MicrosoftDynamicsCRMsystemuser Owninguser { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "owningteam")]
        public MicrosoftDynamicsCRMteam Owningteam { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ownerid")]
        public MicrosoftDynamicsCRMprincipal Ownerid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "owningbusinessunit")]
        public MicrosoftDynamicsCRMbusinessunit Owningbusinessunit { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_reasonforscreening_SyncErrors")]
        public IList<MicrosoftDynamicsCRMsyncerror> SpiceReasonforscreeningSyncErrors { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_reasonforscreening_DuplicateMatchingRecord")]
        public IList<MicrosoftDynamicsCRMduplicaterecord> SpiceReasonforscreeningDuplicateMatchingRecord { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_reasonforscreening_DuplicateBaseRecord")]
        public IList<MicrosoftDynamicsCRMduplicaterecord> SpiceReasonforscreeningDuplicateBaseRecord { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_reasonforscreening_AsyncOperations")]
        public IList<MicrosoftDynamicsCRMasyncoperation> SpiceReasonforscreeningAsyncOperations { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_reasonforscreening_MailboxTrackingFolders")]
        public IList<MicrosoftDynamicsCRMmailboxtrackingfolder> SpiceReasonforscreeningMailboxTrackingFolders { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_reasonforscreening_ProcessSession")]
        public IList<MicrosoftDynamicsCRMprocesssession> SpiceReasonforscreeningProcessSession { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_reasonforscreening_BulkDeleteFailures")]
        public IList<MicrosoftDynamicsCRMbulkdeletefailure> SpiceReasonforscreeningBulkDeleteFailures { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_reasonforscreening_PrincipalObjectAttributeAccesses")]
        public IList<MicrosoftDynamicsCRMprincipalobjectattributeaccess> SpiceReasonforscreeningPrincipalObjectAttributeAccesses { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_reasonforscreening_incident")]
        public IList<MicrosoftDynamicsCRMincident> SpiceReasonforscreeningIncident { get; set; }

    }
}
