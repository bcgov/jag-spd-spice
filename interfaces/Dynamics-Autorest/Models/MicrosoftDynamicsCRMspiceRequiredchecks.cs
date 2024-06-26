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
    /// Microsoft.Dynamics.CRM.spice_requiredchecks
    /// </summary>
    public partial class MicrosoftDynamicsCRMspiceRequiredchecks
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMspiceRequiredchecks class.
        /// </summary>
        public MicrosoftDynamicsCRMspiceRequiredchecks()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMspiceRequiredchecks class.
        /// </summary>
        public MicrosoftDynamicsCRMspiceRequiredchecks(int? spiceScreeningstatus = default(int?), string _spiceCompletedbyuserValue = default(string), System.DateTimeOffset? overriddencreatedon = default(System.DateTimeOffset?), int? importsequencenumber = default(int?), int? spiceResults = default(int?), int? spiceScreeningresult = default(int?), string _spiceCompletedbyValue = default(string), int? spiceCheckseries = default(int?), MicrosoftDynamicsCRMinteractionforemail regardingobjectidNewInteractionforemailSpiceRequiredchecks = default(MicrosoftDynamicsCRMinteractionforemail), MicrosoftDynamicsCRMknowledgebaserecord regardingobjectidKnowledgebaserecordSpiceRequiredchecks = default(MicrosoftDynamicsCRMknowledgebaserecord), MicrosoftDynamicsCRMlead regardingobjectidLeadSpiceRequiredchecks = default(MicrosoftDynamicsCRMlead), MicrosoftDynamicsCRMbookableresourcebooking regardingobjectidBookableresourcebookingSpiceRequiredchecks = default(MicrosoftDynamicsCRMbookableresourcebooking), MicrosoftDynamicsCRMbookableresourcebookingheader regardingobjectidBookableresourcebookingheaderSpiceRequiredchecks = default(MicrosoftDynamicsCRMbookableresourcebookingheader), MicrosoftDynamicsCRMbulkoperation regardingobjectidBulkoperationSpiceRequiredchecks = default(MicrosoftDynamicsCRMbulkoperation), MicrosoftDynamicsCRMcampaign regardingobjectidCampaignSpiceRequiredchecks = default(MicrosoftDynamicsCRMcampaign), MicrosoftDynamicsCRMcampaignactivity regardingobjectidCampaignactivitySpiceRequiredchecks = default(MicrosoftDynamicsCRMcampaignactivity), MicrosoftDynamicsCRMcontract regardingobjectidContractSpiceRequiredchecks = default(MicrosoftDynamicsCRMcontract), MicrosoftDynamicsCRMentitlement regardingobjectidEntitlementSpiceRequiredchecks = default(MicrosoftDynamicsCRMentitlement), MicrosoftDynamicsCRMentitlementtemplate regardingobjectidEntitlementtemplateSpiceRequiredchecks = default(MicrosoftDynamicsCRMentitlementtemplate), MicrosoftDynamicsCRMincident regardingobjectidIncidentSpiceRequiredchecks = default(MicrosoftDynamicsCRMincident), MicrosoftDynamicsCRMsite regardingobjectidSiteSpiceRequiredchecks = default(MicrosoftDynamicsCRMsite), MicrosoftDynamicsCRMservice serviceidSpiceRequiredchecks = default(MicrosoftDynamicsCRMservice), MicrosoftDynamicsCRMinvoice regardingobjectidInvoiceSpiceRequiredchecks = default(MicrosoftDynamicsCRMinvoice), MicrosoftDynamicsCRMopportunity regardingobjectidOpportunitySpiceRequiredchecks = default(MicrosoftDynamicsCRMopportunity), MicrosoftDynamicsCRMquote regardingobjectidQuoteSpiceRequiredchecks = default(MicrosoftDynamicsCRMquote), MicrosoftDynamicsCRMsalesorder regardingobjectidSalesorderSpiceRequiredchecks = default(MicrosoftDynamicsCRMsalesorder), MicrosoftDynamicsCRMmsdynPostalbum regardingobjectidMsdynPostalbumSpiceRequiredchecks = default(MicrosoftDynamicsCRMmsdynPostalbum), MicrosoftDynamicsCRMspiceExportrequest regardingobjectidSpiceExportrequestSpiceRequiredchecks = default(MicrosoftDynamicsCRMspiceExportrequest), MicrosoftDynamicsCRMaccount regardingobjectidAccountSpiceRequiredchecks = default(MicrosoftDynamicsCRMaccount), MicrosoftDynamicsCRMsystemuser createdbySpiceRequiredchecks = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMcontact regardingobjectidContactSpiceRequiredchecks = default(MicrosoftDynamicsCRMcontact), MicrosoftDynamicsCRMmailbox sendermailboxidSpiceRequiredchecks = default(MicrosoftDynamicsCRMmailbox), MicrosoftDynamicsCRMtransactioncurrency transactioncurrencyidSpiceRequiredchecks = default(MicrosoftDynamicsCRMtransactioncurrency), MicrosoftDynamicsCRMprincipal owneridSpiceRequiredchecks = default(MicrosoftDynamicsCRMprincipal), MicrosoftDynamicsCRMsystemuser owninguserSpiceRequiredchecks = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsla slaActivitypointerSlaSpiceRequiredchecks = default(MicrosoftDynamicsCRMsla), MicrosoftDynamicsCRMbusinessunit owningbusinessunitSpiceRequiredchecks = default(MicrosoftDynamicsCRMbusinessunit), MicrosoftDynamicsCRMknowledgearticle regardingobjectidKnowledgearticleSpiceRequiredchecks = default(MicrosoftDynamicsCRMknowledgearticle), MicrosoftDynamicsCRMsystemuser modifiedonbehalfbySpiceRequiredchecks = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser createdonbehalfbySpiceRequiredchecks = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedbySpiceRequiredchecks = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMteam owningteamSpiceRequiredchecks = default(MicrosoftDynamicsCRMteam), MicrosoftDynamicsCRMsla slainvokedidActivitypointerSlaSpiceRequiredchecks = default(MicrosoftDynamicsCRMsla), MicrosoftDynamicsCRMactivitypointer activityidSpiceRequiredchecks = default(MicrosoftDynamicsCRMactivitypointer), IList<MicrosoftDynamicsCRMactivityparty> spiceRequiredchecksActivityParties = default(IList<MicrosoftDynamicsCRMactivityparty>), IList<MicrosoftDynamicsCRMcampaignresponse> campaignResponseSpiceRequiredcheckses = default(IList<MicrosoftDynamicsCRMcampaignresponse>), IList<MicrosoftDynamicsCRMactioncard> spiceRequiredchecksActionCards = default(IList<MicrosoftDynamicsCRMactioncard>), IList<MicrosoftDynamicsCRMsyncerror> spiceRequiredchecksSyncErrors = default(IList<MicrosoftDynamicsCRMsyncerror>), IList<MicrosoftDynamicsCRMduplicaterecord> spiceRequiredchecksDuplicateMatchingRecord = default(IList<MicrosoftDynamicsCRMduplicaterecord>), IList<MicrosoftDynamicsCRMduplicaterecord> spiceRequiredchecksDuplicateBaseRecord = default(IList<MicrosoftDynamicsCRMduplicaterecord>), IList<MicrosoftDynamicsCRMasyncoperation> spiceRequiredchecksAsyncOperations = default(IList<MicrosoftDynamicsCRMasyncoperation>), IList<MicrosoftDynamicsCRMmailboxtrackingfolder> spiceRequiredchecksMailboxTrackingFolders = default(IList<MicrosoftDynamicsCRMmailboxtrackingfolder>), IList<MicrosoftDynamicsCRMprocesssession> spiceRequiredchecksProcessSession = default(IList<MicrosoftDynamicsCRMprocesssession>), IList<MicrosoftDynamicsCRMbulkdeletefailure> spiceRequiredchecksBulkDeleteFailures = default(IList<MicrosoftDynamicsCRMbulkdeletefailure>), IList<MicrosoftDynamicsCRMprincipalobjectattributeaccess> spiceRequiredchecksPrincipalObjectAttributeAccesses = default(IList<MicrosoftDynamicsCRMprincipalobjectattributeaccess>), IList<MicrosoftDynamicsCRMconnection> spiceRequiredchecksConnections1 = default(IList<MicrosoftDynamicsCRMconnection>), IList<MicrosoftDynamicsCRMconnection> spiceRequiredchecksConnections2 = default(IList<MicrosoftDynamicsCRMconnection>), IList<MicrosoftDynamicsCRMqueueitem> spiceRequiredchecksQueueItems = default(IList<MicrosoftDynamicsCRMqueueitem>), IList<MicrosoftDynamicsCRMannotation> spiceRequiredchecksAnnotations = default(IList<MicrosoftDynamicsCRMannotation>), IList<MicrosoftDynamicsCRMfeedback> spiceRequiredchecksFeedback = default(IList<MicrosoftDynamicsCRMfeedback>), MicrosoftDynamicsCRMsystemuser spiceCompletedBySpiceRequiredchecks = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser spiceCompletedbyuserSpiceRequiredchecks = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMspiceCompany regardingobjectidSpiceCompanySpiceRequiredchecks = default(MicrosoftDynamicsCRMspiceCompany))
        {
            SpiceScreeningstatus = spiceScreeningstatus;
            this._spiceCompletedbyuserValue = _spiceCompletedbyuserValue;
            Overriddencreatedon = overriddencreatedon;
            Importsequencenumber = importsequencenumber;
            SpiceResults = spiceResults;
            SpiceScreeningresult = spiceScreeningresult;
            this._spiceCompletedbyValue = _spiceCompletedbyValue;
            SpiceCheckseries = spiceCheckseries;
            RegardingobjectidNewInteractionforemailSpiceRequiredchecks = regardingobjectidNewInteractionforemailSpiceRequiredchecks;
            RegardingobjectidKnowledgebaserecordSpiceRequiredchecks = regardingobjectidKnowledgebaserecordSpiceRequiredchecks;
            RegardingobjectidLeadSpiceRequiredchecks = regardingobjectidLeadSpiceRequiredchecks;
            RegardingobjectidBookableresourcebookingSpiceRequiredchecks = regardingobjectidBookableresourcebookingSpiceRequiredchecks;
            RegardingobjectidBookableresourcebookingheaderSpiceRequiredchecks = regardingobjectidBookableresourcebookingheaderSpiceRequiredchecks;
            RegardingobjectidBulkoperationSpiceRequiredchecks = regardingobjectidBulkoperationSpiceRequiredchecks;
            RegardingobjectidCampaignSpiceRequiredchecks = regardingobjectidCampaignSpiceRequiredchecks;
            RegardingobjectidCampaignactivitySpiceRequiredchecks = regardingobjectidCampaignactivitySpiceRequiredchecks;
            RegardingobjectidContractSpiceRequiredchecks = regardingobjectidContractSpiceRequiredchecks;
            RegardingobjectidEntitlementSpiceRequiredchecks = regardingobjectidEntitlementSpiceRequiredchecks;
            RegardingobjectidEntitlementtemplateSpiceRequiredchecks = regardingobjectidEntitlementtemplateSpiceRequiredchecks;
            RegardingobjectidIncidentSpiceRequiredchecks = regardingobjectidIncidentSpiceRequiredchecks;
            RegardingobjectidSiteSpiceRequiredchecks = regardingobjectidSiteSpiceRequiredchecks;
            ServiceidSpiceRequiredchecks = serviceidSpiceRequiredchecks;
            RegardingobjectidInvoiceSpiceRequiredchecks = regardingobjectidInvoiceSpiceRequiredchecks;
            RegardingobjectidOpportunitySpiceRequiredchecks = regardingobjectidOpportunitySpiceRequiredchecks;
            RegardingobjectidQuoteSpiceRequiredchecks = regardingobjectidQuoteSpiceRequiredchecks;
            RegardingobjectidSalesorderSpiceRequiredchecks = regardingobjectidSalesorderSpiceRequiredchecks;
            RegardingobjectidMsdynPostalbumSpiceRequiredchecks = regardingobjectidMsdynPostalbumSpiceRequiredchecks;
            RegardingobjectidSpiceExportrequestSpiceRequiredchecks = regardingobjectidSpiceExportrequestSpiceRequiredchecks;
            RegardingobjectidAccountSpiceRequiredchecks = regardingobjectidAccountSpiceRequiredchecks;
            CreatedbySpiceRequiredchecks = createdbySpiceRequiredchecks;
            RegardingobjectidContactSpiceRequiredchecks = regardingobjectidContactSpiceRequiredchecks;
            SendermailboxidSpiceRequiredchecks = sendermailboxidSpiceRequiredchecks;
            TransactioncurrencyidSpiceRequiredchecks = transactioncurrencyidSpiceRequiredchecks;
            OwneridSpiceRequiredchecks = owneridSpiceRequiredchecks;
            OwninguserSpiceRequiredchecks = owninguserSpiceRequiredchecks;
            SlaActivitypointerSlaSpiceRequiredchecks = slaActivitypointerSlaSpiceRequiredchecks;
            OwningbusinessunitSpiceRequiredchecks = owningbusinessunitSpiceRequiredchecks;
            RegardingobjectidKnowledgearticleSpiceRequiredchecks = regardingobjectidKnowledgearticleSpiceRequiredchecks;
            ModifiedonbehalfbySpiceRequiredchecks = modifiedonbehalfbySpiceRequiredchecks;
            CreatedonbehalfbySpiceRequiredchecks = createdonbehalfbySpiceRequiredchecks;
            ModifiedbySpiceRequiredchecks = modifiedbySpiceRequiredchecks;
            OwningteamSpiceRequiredchecks = owningteamSpiceRequiredchecks;
            SlainvokedidActivitypointerSlaSpiceRequiredchecks = slainvokedidActivitypointerSlaSpiceRequiredchecks;
            ActivityidSpiceRequiredchecks = activityidSpiceRequiredchecks;
            SpiceRequiredchecksActivityParties = spiceRequiredchecksActivityParties;
            CampaignResponseSpiceRequiredcheckses = campaignResponseSpiceRequiredcheckses;
            SpiceRequiredchecksActionCards = spiceRequiredchecksActionCards;
            SpiceRequiredchecksSyncErrors = spiceRequiredchecksSyncErrors;
            SpiceRequiredchecksDuplicateMatchingRecord = spiceRequiredchecksDuplicateMatchingRecord;
            SpiceRequiredchecksDuplicateBaseRecord = spiceRequiredchecksDuplicateBaseRecord;
            SpiceRequiredchecksAsyncOperations = spiceRequiredchecksAsyncOperations;
            SpiceRequiredchecksMailboxTrackingFolders = spiceRequiredchecksMailboxTrackingFolders;
            SpiceRequiredchecksProcessSession = spiceRequiredchecksProcessSession;
            SpiceRequiredchecksBulkDeleteFailures = spiceRequiredchecksBulkDeleteFailures;
            SpiceRequiredchecksPrincipalObjectAttributeAccesses = spiceRequiredchecksPrincipalObjectAttributeAccesses;
            SpiceRequiredchecksConnections1 = spiceRequiredchecksConnections1;
            SpiceRequiredchecksConnections2 = spiceRequiredchecksConnections2;
            SpiceRequiredchecksQueueItems = spiceRequiredchecksQueueItems;
            SpiceRequiredchecksAnnotations = spiceRequiredchecksAnnotations;
            SpiceRequiredchecksFeedback = spiceRequiredchecksFeedback;
            SpiceCompletedBySpiceRequiredchecks = spiceCompletedBySpiceRequiredchecks;
            SpiceCompletedbyuserSpiceRequiredchecks = spiceCompletedbyuserSpiceRequiredchecks;
            RegardingobjectidSpiceCompanySpiceRequiredchecks = regardingobjectidSpiceCompanySpiceRequiredchecks;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_screeningstatus")]
        public int? SpiceScreeningstatus { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_spice_completedbyuser_value")]
        public string _spiceCompletedbyuserValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "overriddencreatedon")]
        public System.DateTimeOffset? Overriddencreatedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "importsequencenumber")]
        public int? Importsequencenumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_results")]
        public int? SpiceResults { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_screeningresult")]
        public int? SpiceScreeningresult { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_spice_completedby_value")]
        public string _spiceCompletedbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_checkseries")]
        public int? SpiceCheckseries { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_new_interactionforemail_spice_requiredchecks")]
        public MicrosoftDynamicsCRMinteractionforemail RegardingobjectidNewInteractionforemailSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_knowledgebaserecord_spice_requiredchecks")]
        public MicrosoftDynamicsCRMknowledgebaserecord RegardingobjectidKnowledgebaserecordSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_lead_spice_requiredchecks")]
        public MicrosoftDynamicsCRMlead RegardingobjectidLeadSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_bookableresourcebooking_spice_requiredchecks")]
        public MicrosoftDynamicsCRMbookableresourcebooking RegardingobjectidBookableresourcebookingSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_bookableresourcebookingheader_spice_requiredchecks")]
        public MicrosoftDynamicsCRMbookableresourcebookingheader RegardingobjectidBookableresourcebookingheaderSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_bulkoperation_spice_requiredchecks")]
        public MicrosoftDynamicsCRMbulkoperation RegardingobjectidBulkoperationSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_campaign_spice_requiredchecks")]
        public MicrosoftDynamicsCRMcampaign RegardingobjectidCampaignSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_campaignactivity_spice_requiredchecks")]
        public MicrosoftDynamicsCRMcampaignactivity RegardingobjectidCampaignactivitySpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_contract_spice_requiredchecks")]
        public MicrosoftDynamicsCRMcontract RegardingobjectidContractSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_entitlement_spice_requiredchecks")]
        public MicrosoftDynamicsCRMentitlement RegardingobjectidEntitlementSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_entitlementtemplate_spice_requiredchecks")]
        public MicrosoftDynamicsCRMentitlementtemplate RegardingobjectidEntitlementtemplateSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_incident_spice_requiredchecks")]
        public MicrosoftDynamicsCRMincident RegardingobjectidIncidentSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_site_spice_requiredchecks")]
        public MicrosoftDynamicsCRMsite RegardingobjectidSiteSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "serviceid_spice_requiredchecks")]
        public MicrosoftDynamicsCRMservice ServiceidSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_invoice_spice_requiredchecks")]
        public MicrosoftDynamicsCRMinvoice RegardingobjectidInvoiceSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_opportunity_spice_requiredchecks")]
        public MicrosoftDynamicsCRMopportunity RegardingobjectidOpportunitySpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_quote_spice_requiredchecks")]
        public MicrosoftDynamicsCRMquote RegardingobjectidQuoteSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_salesorder_spice_requiredchecks")]
        public MicrosoftDynamicsCRMsalesorder RegardingobjectidSalesorderSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_msdyn_postalbum_spice_requiredchecks")]
        public MicrosoftDynamicsCRMmsdynPostalbum RegardingobjectidMsdynPostalbumSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_spice_exportrequest_spice_requiredchecks")]
        public MicrosoftDynamicsCRMspiceExportrequest RegardingobjectidSpiceExportrequestSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_account_spice_requiredchecks")]
        public MicrosoftDynamicsCRMaccount RegardingobjectidAccountSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdby_spice_requiredchecks")]
        public MicrosoftDynamicsCRMsystemuser CreatedbySpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_contact_spice_requiredchecks")]
        public MicrosoftDynamicsCRMcontact RegardingobjectidContactSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "sendermailboxid_spice_requiredchecks")]
        public MicrosoftDynamicsCRMmailbox SendermailboxidSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "transactioncurrencyid_spice_requiredchecks")]
        public MicrosoftDynamicsCRMtransactioncurrency TransactioncurrencyidSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ownerid_spice_requiredchecks")]
        public MicrosoftDynamicsCRMprincipal OwneridSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "owninguser_spice_requiredchecks")]
        public MicrosoftDynamicsCRMsystemuser OwninguserSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "sla_activitypointer_sla_spice_requiredchecks")]
        public MicrosoftDynamicsCRMsla SlaActivitypointerSlaSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "owningbusinessunit_spice_requiredchecks")]
        public MicrosoftDynamicsCRMbusinessunit OwningbusinessunitSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_knowledgearticle_spice_requiredchecks")]
        public MicrosoftDynamicsCRMknowledgearticle RegardingobjectidKnowledgearticleSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedonbehalfby_spice_requiredchecks")]
        public MicrosoftDynamicsCRMsystemuser ModifiedonbehalfbySpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdonbehalfby_spice_requiredchecks")]
        public MicrosoftDynamicsCRMsystemuser CreatedonbehalfbySpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedby_spice_requiredchecks")]
        public MicrosoftDynamicsCRMsystemuser ModifiedbySpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "owningteam_spice_requiredchecks")]
        public MicrosoftDynamicsCRMteam OwningteamSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "slainvokedid_activitypointer_sla_spice_requiredchecks")]
        public MicrosoftDynamicsCRMsla SlainvokedidActivitypointerSlaSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "activityid_spice_requiredchecks")]
        public MicrosoftDynamicsCRMactivitypointer ActivityidSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_requiredchecks_activity_parties")]
        public IList<MicrosoftDynamicsCRMactivityparty> SpiceRequiredchecksActivityParties { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "CampaignResponse_spice_requiredcheckses")]
        public IList<MicrosoftDynamicsCRMcampaignresponse> CampaignResponseSpiceRequiredcheckses { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_requiredchecks_ActionCards")]
        public IList<MicrosoftDynamicsCRMactioncard> SpiceRequiredchecksActionCards { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_requiredchecks_SyncErrors")]
        public IList<MicrosoftDynamicsCRMsyncerror> SpiceRequiredchecksSyncErrors { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_requiredchecks_DuplicateMatchingRecord")]
        public IList<MicrosoftDynamicsCRMduplicaterecord> SpiceRequiredchecksDuplicateMatchingRecord { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_requiredchecks_DuplicateBaseRecord")]
        public IList<MicrosoftDynamicsCRMduplicaterecord> SpiceRequiredchecksDuplicateBaseRecord { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_requiredchecks_AsyncOperations")]
        public IList<MicrosoftDynamicsCRMasyncoperation> SpiceRequiredchecksAsyncOperations { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_requiredchecks_MailboxTrackingFolders")]
        public IList<MicrosoftDynamicsCRMmailboxtrackingfolder> SpiceRequiredchecksMailboxTrackingFolders { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_requiredchecks_ProcessSession")]
        public IList<MicrosoftDynamicsCRMprocesssession> SpiceRequiredchecksProcessSession { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_requiredchecks_BulkDeleteFailures")]
        public IList<MicrosoftDynamicsCRMbulkdeletefailure> SpiceRequiredchecksBulkDeleteFailures { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_requiredchecks_PrincipalObjectAttributeAccesses")]
        public IList<MicrosoftDynamicsCRMprincipalobjectattributeaccess> SpiceRequiredchecksPrincipalObjectAttributeAccesses { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_requiredchecks_connections1")]
        public IList<MicrosoftDynamicsCRMconnection> SpiceRequiredchecksConnections1 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_requiredchecks_connections2")]
        public IList<MicrosoftDynamicsCRMconnection> SpiceRequiredchecksConnections2 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_requiredchecks_QueueItems")]
        public IList<MicrosoftDynamicsCRMqueueitem> SpiceRequiredchecksQueueItems { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_requiredchecks_Annotations")]
        public IList<MicrosoftDynamicsCRMannotation> SpiceRequiredchecksAnnotations { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_requiredchecks_Feedback")]
        public IList<MicrosoftDynamicsCRMfeedback> SpiceRequiredchecksFeedback { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_completedBy_spice_requiredchecks")]
        public MicrosoftDynamicsCRMsystemuser SpiceCompletedBySpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "spice_completedbyuser_spice_requiredchecks")]
        public MicrosoftDynamicsCRMsystemuser SpiceCompletedbyuserSpiceRequiredchecks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "regardingobjectid_spice_company_spice_requiredchecks")]
        public MicrosoftDynamicsCRMspiceCompany RegardingobjectidSpiceCompanySpiceRequiredchecks { get; set; }

    }
}
