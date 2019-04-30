namespace Gov.Jag.Spice.Interfaces
{
    using Microsoft.Rest;
    using Microsoft.Rest.Serialization;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;

    /// <summary>
    /// Auto Generated
    /// </summary>
    public partial class DynamicsClient : ServiceClient<DynamicsClient>, IDynamicsClient
    {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        public System.Uri NativeBaseUri { get; set; }

        public string GetEntityURI(string entityType, string id)
        {
            string result = "";
            result = NativeBaseUri + entityType + "(" + id.Trim() + ")";
            return result;
        }

        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="odee">the source exception</param>
        /// <param name="errorMessage">The error message to present if no entity was created, or null if no error should be shown.</param>
        /// <returns>The ID of a new record, or null of no record was created</returns>
        public string GetCreatedRecord(OdataerrorException odee, string errorMessage)
        {
            string result = null;
            if (odee.Response.StatusCode == System.Net.HttpStatusCode.NoContent && odee.Response.Headers.ContainsKey("OData-EntityId") && odee.Response.Headers["OData-EntityId"] != null)
            {

                string temp = odee.Response.Headers["OData-EntityId"].FirstOrDefault();
                int guidStart = temp.LastIndexOf("(");
                int guidEnd = temp.LastIndexOf(")");
                result = temp.Substring(guidStart + 1, guidEnd - (guidStart + 1));
                
            }
            else
            {
                if (errorMessage != null)
                {
                    Console.WriteLine(errorMessage);
                    Console.WriteLine(odee.Message);
                    Console.WriteLine(odee.Request.Content);
                    Console.WriteLine(odee.Response.Content);
                }                
            }
            return result;
        }

    }

    

   
}
