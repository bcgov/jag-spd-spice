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
    /// Microsoft.Dynamics.CRM.productpricelevel
    /// </summary>
    public partial class MicrosoftDynamicsCRMproductpricelevel
    {
        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMproductpricelevel class.
        /// </summary>
        public MicrosoftDynamicsCRMproductpricelevel()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// MicrosoftDynamicsCRMproductpricelevel class.
        /// </summary>
        public MicrosoftDynamicsCRMproductpricelevel(string productnumber = default(string), string _transactioncurrencyidValue = default(string), System.DateTimeOffset? overriddencreatedon = default(System.DateTimeOffset?), string _productidValue = default(string), decimal? amountBase = default(decimal?), decimal? roundingoptionamount = default(decimal?), string versionnumber = default(string), string productpricelevelid = default(string), string stageid = default(string), string _discounttypeidValue = default(string), string traversedpath = default(string), int? importsequencenumber = default(int?), int? utcconversiontimezonecode = default(int?), string _createdbyValue = default(string), string _modifiedbyValue = default(string), decimal? roundingoptionamountBase = default(decimal?), decimal? exchangerate = default(decimal?), System.DateTimeOffset? createdon = default(System.DateTimeOffset?), decimal? percentage = default(decimal?), string _pricelevelidValue = default(string), int? roundingoptioncode = default(int?), string _uomidValue = default(string), int? timezoneruleversionnumber = default(int?), string _createdonbehalfbyValue = default(string), string organizationid = default(string), System.DateTimeOffset? modifiedon = default(System.DateTimeOffset?), string processid = default(string), int? roundingpolicycode = default(int?), int? quantitysellingcode = default(int?), decimal? amount = default(decimal?), string _uomscheduleidValue = default(string), string _modifiedonbehalfbyValue = default(string), int? pricingmethodcode = default(int?), MicrosoftDynamicsCRMsystemuser createdby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser createdonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMsystemuser modifiedonbehalfby = default(MicrosoftDynamicsCRMsystemuser), IList<MicrosoftDynamicsCRMsyncerror> productPriceLevelSyncErrors = default(IList<MicrosoftDynamicsCRMsyncerror>), IList<MicrosoftDynamicsCRMasyncoperation> productPriceLevelAsyncOperations = default(IList<MicrosoftDynamicsCRMasyncoperation>), IList<MicrosoftDynamicsCRMmailboxtrackingfolder> productpricelevelMailboxTrackingFolders = default(IList<MicrosoftDynamicsCRMmailboxtrackingfolder>), IList<MicrosoftDynamicsCRMprocesssession> productPriceLevelProcessSessions = default(IList<MicrosoftDynamicsCRMprocesssession>), IList<MicrosoftDynamicsCRMbulkdeletefailure> productPriceLevelBulkDeleteFailures = default(IList<MicrosoftDynamicsCRMbulkdeletefailure>), IList<MicrosoftDynamicsCRMprincipalobjectattributeaccess> productpricelevelPrincipalObjectAttributeAccesses = default(IList<MicrosoftDynamicsCRMprincipalobjectattributeaccess>), MicrosoftDynamicsCRMprocessstage stageidProcessstage = default(MicrosoftDynamicsCRMprocessstage), MicrosoftDynamicsCRMtransactioncurrency transactioncurrencyid = default(MicrosoftDynamicsCRMtransactioncurrency), MicrosoftDynamicsCRMpricelevel pricelevelid = default(MicrosoftDynamicsCRMpricelevel), MicrosoftDynamicsCRMproduct productid = default(MicrosoftDynamicsCRMproduct), MicrosoftDynamicsCRMuom uomid = default(MicrosoftDynamicsCRMuom), MicrosoftDynamicsCRMuomschedule uomscheduleid = default(MicrosoftDynamicsCRMuomschedule), MicrosoftDynamicsCRMdiscounttype discounttypeid = default(MicrosoftDynamicsCRMdiscounttype))
        {
            Productnumber = productnumber;
            this._transactioncurrencyidValue = _transactioncurrencyidValue;
            Overriddencreatedon = overriddencreatedon;
            this._productidValue = _productidValue;
            AmountBase = amountBase;
            Roundingoptionamount = roundingoptionamount;
            Versionnumber = versionnumber;
            Productpricelevelid = productpricelevelid;
            Stageid = stageid;
            this._discounttypeidValue = _discounttypeidValue;
            Traversedpath = traversedpath;
            Importsequencenumber = importsequencenumber;
            Utcconversiontimezonecode = utcconversiontimezonecode;
            this._createdbyValue = _createdbyValue;
            this._modifiedbyValue = _modifiedbyValue;
            RoundingoptionamountBase = roundingoptionamountBase;
            Exchangerate = exchangerate;
            Createdon = createdon;
            Percentage = percentage;
            this._pricelevelidValue = _pricelevelidValue;
            Roundingoptioncode = roundingoptioncode;
            this._uomidValue = _uomidValue;
            Timezoneruleversionnumber = timezoneruleversionnumber;
            this._createdonbehalfbyValue = _createdonbehalfbyValue;
            Organizationid = organizationid;
            Modifiedon = modifiedon;
            Processid = processid;
            Roundingpolicycode = roundingpolicycode;
            Quantitysellingcode = quantitysellingcode;
            Amount = amount;
            this._uomscheduleidValue = _uomscheduleidValue;
            this._modifiedonbehalfbyValue = _modifiedonbehalfbyValue;
            Pricingmethodcode = pricingmethodcode;
            Createdby = createdby;
            Createdonbehalfby = createdonbehalfby;
            Modifiedby = modifiedby;
            Modifiedonbehalfby = modifiedonbehalfby;
            ProductPriceLevelSyncErrors = productPriceLevelSyncErrors;
            ProductPriceLevelAsyncOperations = productPriceLevelAsyncOperations;
            ProductpricelevelMailboxTrackingFolders = productpricelevelMailboxTrackingFolders;
            ProductPriceLevelProcessSessions = productPriceLevelProcessSessions;
            ProductPriceLevelBulkDeleteFailures = productPriceLevelBulkDeleteFailures;
            ProductpricelevelPrincipalObjectAttributeAccesses = productpricelevelPrincipalObjectAttributeAccesses;
            StageidProcessstage = stageidProcessstage;
            Transactioncurrencyid = transactioncurrencyid;
            Pricelevelid = pricelevelid;
            Productid = productid;
            Uomid = uomid;
            Uomscheduleid = uomscheduleid;
            Discounttypeid = discounttypeid;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "productnumber")]
        public string Productnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_transactioncurrencyid_value")]
        public string _transactioncurrencyidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "overriddencreatedon")]
        public System.DateTimeOffset? Overriddencreatedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_productid_value")]
        public string _productidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "amount_base")]
        public decimal? AmountBase { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "roundingoptionamount")]
        public decimal? Roundingoptionamount { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public string Versionnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "productpricelevelid")]
        public string Productpricelevelid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "stageid")]
        public string Stageid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_discounttypeid_value")]
        public string _discounttypeidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "traversedpath")]
        public string Traversedpath { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "importsequencenumber")]
        public int? Importsequencenumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "utcconversiontimezonecode")]
        public int? Utcconversiontimezonecode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_createdby_value")]
        public string _createdbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedby_value")]
        public string _modifiedbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "roundingoptionamount_base")]
        public decimal? RoundingoptionamountBase { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "exchangerate")]
        public decimal? Exchangerate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdon")]
        public System.DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "percentage")]
        public decimal? Percentage { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_pricelevelid_value")]
        public string _pricelevelidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "roundingoptioncode")]
        public int? Roundingoptioncode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_uomid_value")]
        public string _uomidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "timezoneruleversionnumber")]
        public int? Timezoneruleversionnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_createdonbehalfby_value")]
        public string _createdonbehalfbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "organizationid")]
        public string Organizationid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedon")]
        public System.DateTimeOffset? Modifiedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "processid")]
        public string Processid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "roundingpolicycode")]
        public int? Roundingpolicycode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "quantitysellingcode")]
        public int? Quantitysellingcode { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_uomscheduleid_value")]
        public string _uomscheduleidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedonbehalfby_value")]
        public string _modifiedonbehalfbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "pricingmethodcode")]
        public int? Pricingmethodcode { get; set; }

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
        [JsonProperty(PropertyName = "ProductPriceLevel_SyncErrors")]
        public IList<MicrosoftDynamicsCRMsyncerror> ProductPriceLevelSyncErrors { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ProductPriceLevel_AsyncOperations")]
        public IList<MicrosoftDynamicsCRMasyncoperation> ProductPriceLevelAsyncOperations { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "productpricelevel_MailboxTrackingFolders")]
        public IList<MicrosoftDynamicsCRMmailboxtrackingfolder> ProductpricelevelMailboxTrackingFolders { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ProductPriceLevel_ProcessSessions")]
        public IList<MicrosoftDynamicsCRMprocesssession> ProductPriceLevelProcessSessions { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ProductPriceLevel_BulkDeleteFailures")]
        public IList<MicrosoftDynamicsCRMbulkdeletefailure> ProductPriceLevelBulkDeleteFailures { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "productpricelevel_PrincipalObjectAttributeAccesses")]
        public IList<MicrosoftDynamicsCRMprincipalobjectattributeaccess> ProductpricelevelPrincipalObjectAttributeAccesses { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "stageid_processstage")]
        public MicrosoftDynamicsCRMprocessstage StageidProcessstage { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "transactioncurrencyid")]
        public MicrosoftDynamicsCRMtransactioncurrency Transactioncurrencyid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "pricelevelid")]
        public MicrosoftDynamicsCRMpricelevel Pricelevelid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "productid")]
        public MicrosoftDynamicsCRMproduct Productid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "uomid")]
        public MicrosoftDynamicsCRMuom Uomid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "uomscheduleid")]
        public MicrosoftDynamicsCRMuomschedule Uomscheduleid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "discounttypeid")]
        public MicrosoftDynamicsCRMdiscounttype Discounttypeid { get; set; }

    }
}
