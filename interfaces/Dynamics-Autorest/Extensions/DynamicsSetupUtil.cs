using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Rest;
using Newtonsoft.Json;

namespace Gov.Jag.Spice.Interfaces
{
    public static class DynamicsSetupUtil
    {
        /// <summary>
        /// Retrieves <see cref="ServiceClientCredentials"/> for the Dynamics client using Dataverse (cloud) authentication.
        /// </summary>
        /// <param name="Configuration">The application configuration.</param>
        /// <returns>ServiceClientCredentials, or null if required configuration is missing.</returns>
        public static ServiceClientCredentials GetDataverseTokenCredentials(IConfiguration Configuration)
        {
            string aadTenantId = Configuration["DYNAMICS_AAD_TENANT_ID"]; // Cloud AAD Tenant ID
            string serverAppIdUri = Configuration["DYNAMICS_SERVER_APP_ID_URI"]; // Cloud Server App ID URI
            string appRegistrationClientKey = Configuration["DYNAMICS_APP_REG_CLIENT_KEY"]; // Cloud App Registration Client Key
            string appRegistrationClientId = Configuration["DYNAMICS_APP_REG_CLIENT_ID"]; // Cloud App Registration Client Id

            if (
                string.IsNullOrEmpty(appRegistrationClientId)
                || string.IsNullOrEmpty(appRegistrationClientKey)
                || string.IsNullOrEmpty(serverAppIdUri)
                || string.IsNullOrEmpty(aadTenantId)
            )
            {
                return null;
            }

            var tokenUrl = $"https://login.microsoftonline.com/{aadTenantId}/oauth2/v2.0/token";
            using var httpClient = new HttpClient();
            var pairs = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "client_id", appRegistrationClientId },
                { "client_secret", appRegistrationClientKey },
                { "scope", $"{serverAppIdUri}/.default" },
            };

            var content = new FormUrlEncodedContent(pairs);
            var response = httpClient.PostAsync(tokenUrl, content).Result;
            var resultContent = response.Content.ReadAsStringAsync().Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Dataverse token request failed: {resultContent}");
            }

            var result = JsonConvert.DeserializeObject<Dictionary<string, object>>(resultContent);
            string token = result["access_token"].ToString();

            return new TokenCredentials(token);
        }

        /// <summary>
        /// Retrieves <see cref="ServiceClientCredentials"/> for the Dynamics client using ADFS 2016 (on-premise) authentication.
        /// </summary>
        /// <param name="Configuration">The application configuration.</param>
        /// <returns>ServiceClientCredentials, or null if required configuration is missing.</returns>
        public static ServiceClientCredentials GetOnPremTokenCredentials(IConfiguration Configuration)
        {
            string adfsOauth2Uri = Configuration["ADFS_OAUTH2_URI"]; // ADFS OAUTH2 URI - usually /adfs/oauth2/token on STS
            string applicationGroupResource = Configuration["DYNAMICS_APP_GROUP_RESOURCE"]; // ADFS 2016 Application Group resource (URI)
            string applicationGroupClientId = Configuration["DYNAMICS_APP_GROUP_CLIENT_ID"]; // ADFS 2016 Application Group Client ID
            string applicationGroupSecret = Configuration["DYNAMICS_APP_GROUP_SECRET"]; // ADFS 2016 Application Group Secret
            string serviceAccountUsername = Configuration["DYNAMICS_USERNAME"]; // Service account username
            string serviceAccountPassword = Configuration["DYNAMICS_PASSWORD"]; // Service account password
            string bypassStsCertValidation = Configuration["BYPASS_STS_CERT_VALIDATION"]; // Bypass STS certificate validation (true/false)

            if (
                string.IsNullOrEmpty(adfsOauth2Uri)
                || string.IsNullOrEmpty(applicationGroupResource)
                || string.IsNullOrEmpty(applicationGroupClientId)
                || string.IsNullOrEmpty(applicationGroupSecret)
                || string.IsNullOrEmpty(serviceAccountUsername)
                || string.IsNullOrEmpty(serviceAccountPassword)
            )
            {
                return null;
            }

            HttpClient stsClient;
            if (!string.IsNullOrEmpty(bypassStsCertValidation) && bypassStsCertValidation.ToLower() == "true")
            {
                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback = (
                    httpRequestMessage,
                    cert,
                    cetChain,
                    policyErrors
                ) =>
                {
                    return true;
                };
                stsClient = new HttpClient(handler);
            }
            else
            {
                stsClient = new HttpClient();
            }

            stsClient.DefaultRequestHeaders.Add("client-request-id", Guid.NewGuid().ToString());
            stsClient.DefaultRequestHeaders.Add("return-client-request-id", "true");
            stsClient.DefaultRequestHeaders.Add("Accept", "application/json");

            // Construct the body of the request
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("resource", applicationGroupResource),
                new KeyValuePair<string, string>("client_id", applicationGroupClientId),
                new KeyValuePair<string, string>("client_secret", applicationGroupSecret),
                new KeyValuePair<string, string>("username", serviceAccountUsername),
                new KeyValuePair<string, string>("password", serviceAccountPassword),
                new KeyValuePair<string, string>("scope", "openid"),
                new KeyValuePair<string, string>("response_mode", "form_post"),
                new KeyValuePair<string, string>("grant_type", "password"),
            };

            var content = new FormUrlEncodedContent(pairs);
            var _httpResponse = stsClient.PostAsync(adfsOauth2Uri, content).GetAwaiter().GetResult();
            var _responseContent = _httpResponse.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            try
            {
                Dictionary<string, string> result = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                    _responseContent
                );
                string token = result["access_token"];
                return new TokenCredentials(token);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + " " + _responseContent);
            }
        }

        /// <summary>
        /// Retrieves <see cref="ServiceClientCredentials"/> for the Dynamics client using BASIC authentication.
        /// </summary>
        /// <param name="Configuration">The application configuration.</param>
        /// <returns>ServiceClientCredentials, or null if required configuration is missing.</returns>
        public static ServiceClientCredentials GetBasicTokenCredentials(IConfiguration Configuration)
        {
            // API Gateway to NTLM user.  This is used in v8 environments.  Note that the SSG Username and password are not the same as the NTLM user.
            string ssgUsername = Configuration["SSG_USERNAME"]; // BASIC authentication username
            string ssgPassword = Configuration["SSG_PASSWORD"]; // BASIC authentication password

            if (string.IsNullOrEmpty(ssgUsername) || string.IsNullOrEmpty(ssgPassword))
            {
                return null;
            }

            return new BasicAuthenticationCredentials() { UserName = ssgUsername, Password = ssgPassword };
        }

        /// <summary>
        /// Retrieves <see cref="ServiceClientCredentials"/> for the Dynamics client.
        /// Tries Dataverse (cloud), then ADFS on-premise, then BASIC authentication in order.
        /// </summary>
        /// <param name="Configuration">The application configuration.</param>
        /// <returns>ServiceClientCredentials</returns>
        public static ServiceClientCredentials GetServiceClientCredentials(IConfiguration Configuration)
        {
            ServiceClientCredentials serviceClientCredentials =
                GetDataverseTokenCredentials(Configuration)
                ?? GetOnPremTokenCredentials(Configuration)
                ?? GetBasicTokenCredentials(Configuration);

            if (serviceClientCredentials == null)
            {
                throw new InvalidOperationException("No configured connection to Dynamics.");
            }

            return serviceClientCredentials;
        }

        /// <summary>
        /// Setup a Dynamics client.
        /// </summary>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static IDynamicsClient SetupDynamics(IConfiguration Configuration)
        {
            string dynamicsOdataUri = Configuration["DYNAMICS_ODATA_URI"]; // Dynamics ODATA endpoint

            if (string.IsNullOrEmpty(dynamicsOdataUri))
            {
                throw new ArgumentException(
                    "Configuration setting DYNAMICS_ODATA_URI is blank.",
                    nameof(Configuration)
                );
            }

            ServiceClientCredentials serviceClientCredentials = GetServiceClientCredentials(Configuration);

            IDynamicsClient client = new DynamicsClient(new Uri(dynamicsOdataUri), serviceClientCredentials);

            // Set the native client URI. This is required if you have a reverse proxy or IFD in place and the native
            // URI is different from your access URI.
            if (string.IsNullOrEmpty(Configuration["DYNAMICS_NATIVE_ODATA_URI"]))
            {
                client.NativeBaseUri = new Uri(Configuration["DYNAMICS_ODATA_URI"]);
            }
            else
            {
                client.NativeBaseUri = new Uri(Configuration["DYNAMICS_NATIVE_ODATA_URI"]);
            }

            return client;
        }
    }
}
