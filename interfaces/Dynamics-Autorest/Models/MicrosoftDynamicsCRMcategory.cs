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
    /// Microsoft.Dynamics.CRM.category
    /// </summary>
    public partial class MicrosoftDynamicsCRMcategory
    {
        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMcategory
        /// class.
        /// </summary>
        public MicrosoftDynamicsCRMcategory()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the MicrosoftDynamicsCRMcategory
        /// class.
        /// </summary>
        public MicrosoftDynamicsCRMcategory(string _parentcategoryidValue = default(string), string _modifiedbyValue = default(string), string _createdonbehalfbyValue = default(string), System.DateTimeOffset? modifiedon = default(System.DateTimeOffset?), System.DateTimeOffset? createdon = default(System.DateTimeOffset?), string title = default(string), string description = default(string), System.DateTimeOffset? overriddencreatedon = default(System.DateTimeOffset?), string _owneridValue = default(string), string categorynumber = default(string), string _modifiedonbehalfbyValue = default(string), string _owningteamValue = default(string), int? sequencenumber = default(int?), string _createdbyValue = default(string), string _transactioncurrencyidValue = default(string), decimal? exchangerate = default(decimal?), string _owningbusinessunitValue = default(string), string versionnumber = default(string), int? importsequencenumber = default(int?), string _owninguserValue = default(string), string categoryid = default(string), MicrosoftDynamicsCRMcategory parentcategoryid = default(MicrosoftDynamicsCRMcategory), IList<MicrosoftDynamicsCRMcategory> categoryParentCategory = default(IList<MicrosoftDynamicsCRMcategory>), IList<MicrosoftDynamicsCRMsyncerror> categorySyncErrors = default(IList<MicrosoftDynamicsCRMsyncerror>), MicrosoftDynamicsCRMsystemuser lkCategoryModifiedonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMprincipal ownerid = default(MicrosoftDynamicsCRMprincipal), MicrosoftDynamicsCRMsystemuser lkCategoryCreatedonbehalfby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMtransactioncurrency transactioncurrencyid = default(MicrosoftDynamicsCRMtransactioncurrency), MicrosoftDynamicsCRMsystemuser lkCategoryModifiedby = default(MicrosoftDynamicsCRMsystemuser), IList<MicrosoftDynamicsCRMknowledgearticle> knowledgearticleCategory = default(IList<MicrosoftDynamicsCRMknowledgearticle>), MicrosoftDynamicsCRMsystemuser lkCategoryCreatedby = default(MicrosoftDynamicsCRMsystemuser), MicrosoftDynamicsCRMbusinessunit owningbusinessunit = default(MicrosoftDynamicsCRMbusinessunit))
        {
            this._parentcategoryidValue = _parentcategoryidValue;
            this._modifiedbyValue = _modifiedbyValue;
            this._createdonbehalfbyValue = _createdonbehalfbyValue;
            Modifiedon = modifiedon;
            Createdon = createdon;
            Title = title;
            Description = description;
            Overriddencreatedon = overriddencreatedon;
            this._owneridValue = _owneridValue;
            Categorynumber = categorynumber;
            this._modifiedonbehalfbyValue = _modifiedonbehalfbyValue;
            this._owningteamValue = _owningteamValue;
            Sequencenumber = sequencenumber;
            this._createdbyValue = _createdbyValue;
            this._transactioncurrencyidValue = _transactioncurrencyidValue;
            Exchangerate = exchangerate;
            this._owningbusinessunitValue = _owningbusinessunitValue;
            Versionnumber = versionnumber;
            Importsequencenumber = importsequencenumber;
            this._owninguserValue = _owninguserValue;
            Categoryid = categoryid;
            Parentcategoryid = parentcategoryid;
            CategoryParentCategory = categoryParentCategory;
            CategorySyncErrors = categorySyncErrors;
            LkCategoryModifiedonbehalfby = lkCategoryModifiedonbehalfby;
            Ownerid = ownerid;
            LkCategoryCreatedonbehalfby = lkCategoryCreatedonbehalfby;
            Transactioncurrencyid = transactioncurrencyid;
            LkCategoryModifiedby = lkCategoryModifiedby;
            KnowledgearticleCategory = knowledgearticleCategory;
            LkCategoryCreatedby = lkCategoryCreatedby;
            Owningbusinessunit = owningbusinessunit;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_parentcategoryid_value")]
        public string _parentcategoryidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedby_value")]
        public string _modifiedbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_createdonbehalfby_value")]
        public string _createdonbehalfbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "modifiedon")]
        public System.DateTimeOffset? Modifiedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "createdon")]
        public System.DateTimeOffset? Createdon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "overriddencreatedon")]
        public System.DateTimeOffset? Overriddencreatedon { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_ownerid_value")]
        public string _owneridValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "categorynumber")]
        public string Categorynumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_modifiedonbehalfby_value")]
        public string _modifiedonbehalfbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_owningteam_value")]
        public string _owningteamValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "sequencenumber")]
        public int? Sequencenumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_createdby_value")]
        public string _createdbyValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_transactioncurrencyid_value")]
        public string _transactioncurrencyidValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "exchangerate")]
        public decimal? Exchangerate { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_owningbusinessunit_value")]
        public string _owningbusinessunitValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "versionnumber")]
        public string Versionnumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "importsequencenumber")]
        public int? Importsequencenumber { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "_owninguser_value")]
        public string _owninguserValue { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "categoryid")]
        public string Categoryid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "parentcategoryid")]
        public MicrosoftDynamicsCRMcategory Parentcategoryid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "category_parent_category")]
        public IList<MicrosoftDynamicsCRMcategory> CategoryParentCategory { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "Category_SyncErrors")]
        public IList<MicrosoftDynamicsCRMsyncerror> CategorySyncErrors { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "lk_category_modifiedonbehalfby")]
        public MicrosoftDynamicsCRMsystemuser LkCategoryModifiedonbehalfby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "ownerid")]
        public MicrosoftDynamicsCRMprincipal Ownerid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "lk_category_createdonbehalfby")]
        public MicrosoftDynamicsCRMsystemuser LkCategoryCreatedonbehalfby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "transactioncurrencyid")]
        public MicrosoftDynamicsCRMtransactioncurrency Transactioncurrencyid { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "lk_category_modifiedby")]
        public MicrosoftDynamicsCRMsystemuser LkCategoryModifiedby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "knowledgearticle_category")]
        public IList<MicrosoftDynamicsCRMknowledgearticle> KnowledgearticleCategory { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "lk_category_createdby")]
        public MicrosoftDynamicsCRMsystemuser LkCategoryCreatedby { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "owningbusinessunit")]
        public MicrosoftDynamicsCRMbusinessunit Owningbusinessunit { get; set; }

    }
}
