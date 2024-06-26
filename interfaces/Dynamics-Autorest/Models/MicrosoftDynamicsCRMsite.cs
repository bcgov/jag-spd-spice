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
    /// Microsoft.Dynamics.CRM.site
    /// </summary>
    public partial class MicrosoftDynamicsCRMsite
    {
        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMsite class.
        /// </summary>
        public MicrosoftDynamicsCRMsite()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMsite class.
        /// </summary>
        public MicrosoftDynamicsCRMsite(string address1Telephone2 = default(string), string address1Postofficebox = default(string), string address2Line2 = default(string), string address1Fax = default(string), string address1Country = default(string), int? address2Addresstypecode = default(int?), int? timezonecode = default(int?), string address1Postalcode = default(string), string address2Line1 = default(string), string address2Name = default(string), string address2Stateorprovince = default(string), string address1Telephone3 = default(string), string emailaddress = default(string), string _createdonbehalfbyValue = default(string), System.DateTimeOffset? overriddencreatedon = default(System.DateTimeOffset?), int? address1Utcoffset = default(int?), string address1City = default(string), string address2Country = default(string), string address2Line3 = default(string), string address2City = default(string), int? address1Addresstypecode = default(int?), string address1Line1 = default(string), string address2Telephone3 = default(string), string address2Telephone2 = default(string), int? timezoneruleversionnumber = default(int?), string address1Line2 = default(string), decimal? address1Latitude = default(decimal?), string address1Line3 = default(string), string name = default(string), string _modifiedbyValue = default(string), string address1County = default(string), string versionnumber = default(string), string siteid = default(string), int? address2Shippingmethodcode = default(int?), string address2Postalcode = default(string), string address2Upszone = default(string), int? importsequencenumber = default(int?), string address1Stateorprovince = default(string), System.DateTimeOffset? createdon = default(System.DateTimeOffset?), decimal? address2Latitude = default(decimal?), string address1Upszone = default(string), string address2County = default(string), int? address1Shippingmethodcode = default(int?), string address2Postofficebox = default(string), string address2Telephone1 = default(string), string _modifiedonbehalfbyValue = default(string), string _createdbyValue = default(string), System.DateTimeOffset? modifiedon = default(System.DateTimeOffset?), string address1Telephone1 = default(string), decimal? address1Longitude = default(decimal?), string address1Addressid = default(string), string address1Name = default(string), string _organizationidValue = default(string), string address2Addressid = default(string), int? utcconversiontimezonecode = default(int?), string address2Fax = default(string), int? address2Utcoffset = default(int?), decimal? address2Longitude = default(decimal?), MicrosoftDynamicsCRMsystemuser createdby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser createdonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMorganization organizationidOrganization = default(MicrosoftDynamicsCRMorganization), IList<MicrosoftDynamicsCRMactivitypointer> siteActivityPointers = default(IList<MicrosoftDynamicsCRMactivitypointer>), IList<MicrosoftDynamicsCRMsyncerror> siteSyncErrors = default(IList<MicrosoftDynamicsCRMsyncerror>), IList<MicrosoftDynamicsCRMasyncoperation> siteAsyncOperations = default(IList<MicrosoftDynamicsCRMasyncoperation>), IList<MicrosoftDynamicsCRMmailboxtrackingfolder> siteMailboxTrackingFolders = default(IList<MicrosoftDynamicsCRMmailboxtrackingfolder>), IList<MicrosoftDynamicsCRMprocesssession> siteProcessSessions = default(IList<MicrosoftDynamicsCRMprocesssession>), IList<MicrosoftDynamicsCRMbulkdeletefailure> siteBulkDeleteFailures = default(IList<MicrosoftDynamicsCRMbulkdeletefailure>), IList<MicrosoftDynamicsCRMprincipalobjectattributeaccess> sitePrincipalObjectAttributeAccesses = default(IList<MicrosoftDynamicsCRMprincipalobjectattributeaccess>), IList<MicrosoftDynamicsCRMappointment> siteAppointments = default(IList<MicrosoftDynamicsCRMappointment>), IList<MicrosoftDynamicsCRMemail> siteEmails = default(IList<MicrosoftDynamicsCRMemail>), IList<MicrosoftDynamicsCRMfax> siteFaxes = default(IList<MicrosoftDynamicsCRMfax>), IList<MicrosoftDynamicsCRMletter> siteLetters = default(IList<MicrosoftDynamicsCRMletter>), IList<MicrosoftDynamicsCRMphonecall> sitePhoneCalls = default(IList<MicrosoftDynamicsCRMphonecall>), IList<MicrosoftDynamicsCRMtask> siteTasks = default(IList<MicrosoftDynamicsCRMtask>), IList<MicrosoftDynamicsCRMrecurringappointmentmaster> siteRecurringAppointmentMasters = default(IList<MicrosoftDynamicsCRMrecurringappointmentmaster>), IList<MicrosoftDynamicsCRMsocialactivity> siteSocialActivities = default(IList<MicrosoftDynamicsCRMsocialactivity>), IList<MicrosoftDynamicsCRMserviceappointment> siteServiceAppointments = default(IList<MicrosoftDynamicsCRMserviceappointment>), IList<MicrosoftDynamicsCRMequipment> siteEquipment = default(IList<MicrosoftDynamicsCRMequipment>), IList<MicrosoftDynamicsCRMresource> siteResources = default(IList<MicrosoftDynamicsCRMresource>), IList<MicrosoftDynamicsCRMsystemuser> siteSystemUsers = default(IList<MicrosoftDynamicsCRMsystemuser>), IList<MicrosoftDynamicsCRMserviceappointment> siteServiceAppointments1 = default(IList<MicrosoftDynamicsCRMserviceappointment>), IList<MicrosoftDynamicsCRMopportunityclose> siteOpportunityCloses = default(IList<MicrosoftDynamicsCRMopportunityclose>), IList<MicrosoftDynamicsCRMorderclose> siteOrderCloses = default(IList<MicrosoftDynamicsCRMorderclose>), IList<MicrosoftDynamicsCRMquoteclose> siteQuoteCloses = default(IList<MicrosoftDynamicsCRMquoteclose>), IList<MicrosoftDynamicsCRMspiceRequiredchecks> siteSpiceRequiredcheckses = default(IList<MicrosoftDynamicsCRMspiceRequiredchecks>))
        {
            Address1Telephone2 = address1Telephone2;
            Address1Postofficebox = address1Postofficebox;
            Address2Line2 = address2Line2;
            Address1Fax = address1Fax;
            Address1Country = address1Country;
            Address2Addresstypecode = address2Addresstypecode;
            Timezonecode = timezonecode;
            Address1Postalcode = address1Postalcode;
            Address2Line1 = address2Line1;
            Address2Name = address2Name;
            Address2Stateorprovince = address2Stateorprovince;
            Address1Telephone3 = address1Telephone3;
            Emailaddress = emailaddress;
            this._createdonbehalfbyValue = _createdonbehalfbyValue;
            Overriddencreatedon = overriddencreatedon;
            Address1Utcoffset = address1Utcoffset;
            Address1City = address1City;
            Address2Country = address2Country;
            Address2Line3 = address2Line3;
            Address2City = address2City;
            Address1Addresstypecode = address1Addresstypecode;
            Address1Line1 = address1Line1;
            Address2Telephone3 = address2Telephone3;
            Address2Telephone2 = address2Telephone2;
            Timezoneruleversionnumber = timezoneruleversionnumber;
            Address1Line2 = address1Line2;
            Address1Latitude = address1Latitude;
            Address1Line3 = address1Line3;
            Name = name;
            this._modifiedbyValue = _modifiedbyValue;
            Address1County = address1County;
            Versionnumber = versionnumber;
            Siteid = siteid;
            Address2Shippingmethodcode = address2Shippingmethodcode;
            Address2Postalcode = address2Postalcode;
            Address2Upszone = address2Upszone;
            Importsequencenumber = importsequencenumber;
            Address1Stateorprovince = address1Stateorprovince;
            Createdon = createdon;
            Address2Latitude = address2Latitude;
            Address1Upszone = address1Upszone;
            Address2County = address2County;
            Address1Shippingmethodcode = address1Shippingmethodcode;
            Address2Postofficebox = address2Postofficebox;
            Address2Telephone1 = address2Telephone1;
            this._modifiedonbehalfbyValue = _modifiedonbehalfbyValue;
            this._createdbyValue = _createdbyValue;
            Modifiedon = modifiedon;
            Address1Telephone1 = address1Telephone1;
            Address1Longitude = address1Longitude;
            Address1Addressid = address1Addressid;
            Address1Name = address1Name;
            this._organizationidValue = _organizationidValue;
            Address2Addressid = address2Addressid;
            Utcconversiontimezonecode = utcconversiontimezonecode;
            Address2Fax = address2Fax;
            Address2Utcoffset = address2Utcoffset;
            Address2Longitude = address2Longitude;
            Createdby = createdby;
            Createdonbehalfby = createdonbehalfby;
            Modifiedby = modifiedby;
            Modifiedonbehalfby = modifiedonbehalfby;
            OrganizationidOrganization = organizationidOrganization;
            SiteActivityPointers = siteActivityPointers;
            SiteSyncErrors = siteSyncErrors;
            SiteAsyncOperations = siteAsyncOperations;
            SiteMailboxTrackingFolders = siteMailboxTrackingFolders;
            SiteProcessSessions = siteProcessSessions;
            SiteBulkDeleteFailures = siteBulkDeleteFailures;
            SitePrincipalObjectAttributeAccesses = sitePrincipalObjectAttributeAccesses;
            SiteAppointments = siteAppointments;
            SiteEmails = siteEmails;
            SiteFaxes = siteFaxes;
            SiteLetters = siteLetters;
            SitePhoneCalls = sitePhoneCalls;
            SiteTasks = siteTasks;
            SiteRecurringAppointmentMasters = siteRecurringAppointmentMasters;
            SiteSocialActivities = siteSocialActivities;
            SiteServiceAppointments = siteServiceAppointments;
            SiteEquipment = siteEquipment;
            SiteResources = siteResources;
            SiteSystemUsers = siteSystemUsers;
            SiteServiceAppointments1 = siteServiceAppointments1;
            SiteOpportunityCloses = siteOpportunityCloses;
            SiteOrderCloses = siteOrderCloses;
            SiteQuoteCloses = siteQuoteCloses;
            SiteSpiceRequiredcheckses = siteSpiceRequiredcheckses;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_telephone2")]
        public string Address1Telephone2 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_postofficebox")]
        public string Address1Postofficebox { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_line2")]
        public string Address2Line2 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_fax")]
        public string Address1Fax { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_country")]
        public string Address1Country { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_addresstypecode")]
        public int? Address2Addresstypecode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "timezonecode")]
        public int? Timezonecode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_postalcode")]
        public string Address1Postalcode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_line1")]
        public string Address2Line1 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_name")]
        public string Address2Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_stateorprovince")]
        public string Address2Stateorprovince { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_telephone3")]
        public string Address1Telephone3 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "emailaddress")]
        public string Emailaddress { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_createdonbehalfby_value")]
        public string _createdonbehalfbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "overriddencreatedon")]
        public System.DateTimeOffset? Overriddencreatedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_utcoffset")]
        public int? Address1Utcoffset { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_city")]
        public string Address1City { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_country")]
        public string Address2Country { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_line3")]
        public string Address2Line3 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_city")]
        public string Address2City { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_addresstypecode")]
        public int? Address1Addresstypecode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_line1")]
        public string Address1Line1 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_telephone3")]
        public string Address2Telephone3 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_telephone2")]
        public string Address2Telephone2 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "timezoneruleversionnumber")]
        public int? Timezoneruleversionnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_line2")]
        public string Address1Line2 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_latitude")]
        public decimal? Address1Latitude { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_line3")]
        public string Address1Line3 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedby_value")]
        public string _modifiedbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_county")]
        public string Address1County { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public string Versionnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "siteid")]
        public string Siteid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_shippingmethodcode")]
        public int? Address2Shippingmethodcode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_postalcode")]
        public string Address2Postalcode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_upszone")]
        public string Address2Upszone { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "importsequencenumber")]
        public int? Importsequencenumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_stateorprovince")]
        public string Address1Stateorprovince { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdon")]
        public System.DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_latitude")]
        public decimal? Address2Latitude { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_upszone")]
        public string Address1Upszone { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_county")]
        public string Address2County { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_shippingmethodcode")]
        public int? Address1Shippingmethodcode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_postofficebox")]
        public string Address2Postofficebox { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_telephone1")]
        public string Address2Telephone1 { get; set; }

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
        [JsonProperty(PropertyName = "modifiedon")]
        public System.DateTimeOffset? Modifiedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_telephone1")]
        public string Address1Telephone1 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_longitude")]
        public decimal? Address1Longitude { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_addressid")]
        public string Address1Addressid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address1_name")]
        public string Address1Name { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_organizationid_value")]
        public string _organizationidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_addressid")]
        public string Address2Addressid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "utcconversiontimezonecode")]
        public int? Utcconversiontimezonecode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_fax")]
        public string Address2Fax { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_utcoffset")]
        public int? Address2Utcoffset { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "address2_longitude")]
        public decimal? Address2Longitude { get; set; }

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
        [JsonProperty(PropertyName = "organizationid_organization")]
        public MicrosoftDynamicsCRMorganization OrganizationidOrganization { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_ActivityPointers")]
        public IList<MicrosoftDynamicsCRMactivitypointer> SiteActivityPointers { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Site_SyncErrors")]
        public IList<MicrosoftDynamicsCRMsyncerror> SiteSyncErrors { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Site_AsyncOperations")]
        public IList<MicrosoftDynamicsCRMasyncoperation> SiteAsyncOperations { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_MailboxTrackingFolders")]
        public IList<MicrosoftDynamicsCRMmailboxtrackingfolder> SiteMailboxTrackingFolders { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Site_ProcessSessions")]
        public IList<MicrosoftDynamicsCRMprocesssession> SiteProcessSessions { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Site_BulkDeleteFailures")]
        public IList<MicrosoftDynamicsCRMbulkdeletefailure> SiteBulkDeleteFailures { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_PrincipalObjectAttributeAccesses")]
        public IList<MicrosoftDynamicsCRMprincipalobjectattributeaccess> SitePrincipalObjectAttributeAccesses { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_Appointments")]
        public IList<MicrosoftDynamicsCRMappointment> SiteAppointments { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_Emails")]
        public IList<MicrosoftDynamicsCRMemail> SiteEmails { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_Faxes")]
        public IList<MicrosoftDynamicsCRMfax> SiteFaxes { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_Letters")]
        public IList<MicrosoftDynamicsCRMletter> SiteLetters { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_PhoneCalls")]
        public IList<MicrosoftDynamicsCRMphonecall> SitePhoneCalls { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_Tasks")]
        public IList<MicrosoftDynamicsCRMtask> SiteTasks { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_RecurringAppointmentMasters")]
        public IList<MicrosoftDynamicsCRMrecurringappointmentmaster> SiteRecurringAppointmentMasters { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_SocialActivities")]
        public IList<MicrosoftDynamicsCRMsocialactivity> SiteSocialActivities { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_ServiceAppointments")]
        public IList<MicrosoftDynamicsCRMserviceappointment> SiteServiceAppointments { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_equipment")]
        public IList<MicrosoftDynamicsCRMequipment> SiteEquipment { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_resources")]
        public IList<MicrosoftDynamicsCRMresource> SiteResources { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_system_users")]
        public IList<MicrosoftDynamicsCRMsystemuser> SiteSystemUsers { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_service_appointments")]
        public IList<MicrosoftDynamicsCRMserviceappointment> SiteServiceAppointments1 { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_OpportunityCloses")]
        public IList<MicrosoftDynamicsCRMopportunityclose> SiteOpportunityCloses { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_OrderCloses")]
        public IList<MicrosoftDynamicsCRMorderclose> SiteOrderCloses { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_QuoteCloses")]
        public IList<MicrosoftDynamicsCRMquoteclose> SiteQuoteCloses { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "site_spice_requiredcheckses")]
        public IList<MicrosoftDynamicsCRMspiceRequiredchecks> SiteSpiceRequiredcheckses { get; set; }

    }
}
