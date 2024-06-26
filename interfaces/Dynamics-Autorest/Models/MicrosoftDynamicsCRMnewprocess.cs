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
    /// Microsoft.Dynamics.CRM.newprocess
    /// </summary>
    public partial class MicrosoftDynamicsCRMnewprocess
    {
        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMnewprocess
        /// class.
        /// </summary>
        public MicrosoftDynamicsCRMnewprocess()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMnewprocess
        /// class.
        /// </summary>
        public MicrosoftDynamicsCRMnewprocess(int? statuscode = default(int?), System.DateTimeOffset? createdon = default(System.DateTimeOffset?), string _modifiedonbehalfbyValue = default(string), string _createdbyValue = default(string), string _organizationidValue = default(string), System.DateTimeOffset? completedon = default(System.DateTimeOffset?), string _modifiedbyValue = default(string), string _transactioncurrencyidValue = default(string), System.DateTimeOffset? activestagestartedon = default(System.DateTimeOffset?), string name = default(string), System.DateTimeOffset? modifiedon = default(System.DateTimeOffset?), string _createdonbehalfbyValue = default(string), int? duration = default(int?), System.DateTimeOffset? overriddencreatedon = default(System.DateTimeOffset?), decimal? exchangerate = default(decimal?), int? importsequencenumber = default(int?), int? statecode = default(int?), string traversedpath = default(string), string versionnumber = default(string), string _knowledgearticleidValue = default(string), string _processidValue = default(string), string _activestageidValue = default(string), string businessprocessflowinstanceid = default(string), MicrosoftDynamicsCRMsystemuser createdbyname = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedbyname = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMknowledgearticle knowledgearticleid = default(MicrosoftDynamicsCRMknowledgearticle), IList<MicrosoftDynamicsCRMprocesssession> newProcessProcessSessions = default(IList<MicrosoftDynamicsCRMprocesssession>), MicrosoftDynamicsCRMprocessstage activestageid = default(MicrosoftDynamicsCRMprocessstage), MicrosoftDynamicsCRMsystemuser createdonbehalfbyname = default(MicrosoftDynamicsCRMsystemuser), IList<MicrosoftDynamicsCRMworkflowlog> workflowlogsNewprocess = default(IList<MicrosoftDynamicsCRMworkflowlog>), MicrosoftDynamicsCRMworkflow processid = default(MicrosoftDynamicsCRMworkflow), MicrosoftDynamicsCRMtransactioncurrency transactioncurrencyid = default(MicrosoftDynamicsCRMtransactioncurrency), MicrosoftDynamicsCRMorganization organizationid = default(MicrosoftDynamicsCRMorganization), MicrosoftDynamicsCRMsystemuser modifiedonbehalfbyname = default(MicrosoftDynamicsCRMsystemuser), IList<MicrosoftDynamicsCRMsyncerror> newProcessSyncErrors = default(IList<MicrosoftDynamicsCRMsyncerror>))
        {
            Statuscode = statuscode;
            Createdon = createdon;
            this._modifiedonbehalfbyValue = _modifiedonbehalfbyValue;
            this._createdbyValue = _createdbyValue;
            this._organizationidValue = _organizationidValue;
            Completedon = completedon;
            this._modifiedbyValue = _modifiedbyValue;
            this._transactioncurrencyidValue = _transactioncurrencyidValue;
            Activestagestartedon = activestagestartedon;
            Name = name;
            Modifiedon = modifiedon;
            this._createdonbehalfbyValue = _createdonbehalfbyValue;
            Duration = duration;
            Overriddencreatedon = overriddencreatedon;
            Exchangerate = exchangerate;
            Importsequencenumber = importsequencenumber;
            Statecode = statecode;
            Traversedpath = traversedpath;
            Versionnumber = versionnumber;
            this._knowledgearticleidValue = _knowledgearticleidValue;
            this._processidValue = _processidValue;
            this._activestageidValue = _activestageidValue;
            Businessprocessflowinstanceid = businessprocessflowinstanceid;
            Createdbyname = createdbyname;
            Modifiedbyname = modifiedbyname;
            Knowledgearticleid = knowledgearticleid;
            NewProcessProcessSessions = newProcessProcessSessions;
            Activestageid = activestageid;
            Createdonbehalfbyname = createdonbehalfbyname;
            WorkflowlogsNewprocess = workflowlogsNewprocess;
            Processid = processid;
            Transactioncurrencyid = transactioncurrencyid;
            Organizationid = organizationid;
            Modifiedonbehalfbyname = modifiedonbehalfbyname;
            NewProcessSyncErrors = newProcessSyncErrors;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "statuscode")]
        public int? Statuscode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdon")]
        public System.DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedonbehalfby_value")]
        public string _modifiedonbehalfbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_createdby_value")]
        public string _createdbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_organizationid_value")]
        public string _organizationidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "completedon")]
        public System.DateTimeOffset? Completedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedby_value")]
        public string _modifiedbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_transactioncurrencyid_value")]
        public string _transactioncurrencyidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "activestagestartedon")]
        public System.DateTimeOffset? Activestagestartedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedon")]
        public System.DateTimeOffset? Modifiedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_createdonbehalfby_value")]
        public string _createdonbehalfbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "duration")]
        public int? Duration { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "overriddencreatedon")]
        public System.DateTimeOffset? Overriddencreatedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "exchangerate")]
        public decimal? Exchangerate { get; set; }

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
        [JsonProperty(PropertyName = "traversedpath")]
        public string Traversedpath { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public string Versionnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_knowledgearticleid_value")]
        public string _knowledgearticleidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_processid_value")]
        public string _processidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_activestageid_value")]
        public string _activestageidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "businessprocessflowinstanceid")]
        public string Businessprocessflowinstanceid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdbyname")]
        public MicrosoftDynamicsCRMsystemuser Createdbyname { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedbyname")]
        public MicrosoftDynamicsCRMsystemuser Modifiedbyname { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "knowledgearticleid")]
        public MicrosoftDynamicsCRMknowledgearticle Knowledgearticleid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "NewProcess_ProcessSessions")]
        public IList<MicrosoftDynamicsCRMprocesssession> NewProcessProcessSessions { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "activestageid")]
        public MicrosoftDynamicsCRMprocessstage Activestageid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdonbehalfbyname")]
        public MicrosoftDynamicsCRMsystemuser Createdonbehalfbyname { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "workflowlogs_newprocess")]
        public IList<MicrosoftDynamicsCRMworkflowlog> WorkflowlogsNewprocess { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "processid")]
        public MicrosoftDynamicsCRMworkflow Processid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "transactioncurrencyid")]
        public MicrosoftDynamicsCRMtransactioncurrency Transactioncurrencyid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "organizationid")]
        public MicrosoftDynamicsCRMorganization Organizationid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedonbehalfbyname")]
        public MicrosoftDynamicsCRMsystemuser Modifiedonbehalfbyname { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "NewProcess_SyncErrors")]
        public IList<MicrosoftDynamicsCRMsyncerror> NewProcessSyncErrors { get; set; }

    }
}
