// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Gov.Jag.Spice.Interfaces
{
    using Microsoft.Rest;
    using Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for Accounts.
    /// </summary>
    public static partial class AccountsExtensions
    {
            /// <summary>
            /// Get entities from accounts
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='top'>
            /// </param>
            /// <param name='skip'>
            /// </param>
            /// <param name='search'>
            /// </param>
            /// <param name='filter'>
            /// </param>
            /// <param name='count'>
            /// </param>
            /// <param name='orderby'>
            /// Order items by property values
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            public static MicrosoftDynamicsCRMaccountCollection Get(this IAccounts operations, int? top = default(int?), int? skip = default(int?), string search = default(string), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.GetAsync(top, skip, search, filter, count, orderby, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get entities from accounts
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='top'>
            /// </param>
            /// <param name='skip'>
            /// </param>
            /// <param name='search'>
            /// </param>
            /// <param name='filter'>
            /// </param>
            /// <param name='count'>
            /// </param>
            /// <param name='orderby'>
            /// Order items by property values
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<MicrosoftDynamicsCRMaccountCollection> GetAsync(this IAccounts operations, int? top = default(int?), int? skip = default(int?), string search = default(string), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetWithHttpMessagesAsync(top, skip, search, filter, count, orderby, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Get entities from accounts
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='top'>
            /// </param>
            /// <param name='skip'>
            /// </param>
            /// <param name='search'>
            /// </param>
            /// <param name='filter'>
            /// </param>
            /// <param name='count'>
            /// </param>
            /// <param name='orderby'>
            /// Order items by property values
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            /// <param name='customHeaders'>
            /// Headers that will be added to request.
            /// </param>
            public static HttpOperationResponse<MicrosoftDynamicsCRMaccountCollection> GetWithHttpMessages(this IAccounts operations, int? top = default(int?), int? skip = default(int?), string search = default(string), string filter = default(string), bool? count = default(bool?), IList<string> orderby = default(IList<string>), IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), Dictionary<string, List<string>> customHeaders = null)
            {
                return operations.GetWithHttpMessagesAsync(top, skip, search, filter, count, orderby, select, expand, customHeaders, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Add new entity to accounts
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='body'>
            /// New entity
            /// </param>
            /// <param name='prefer'>
            /// Required in order for the service to return a JSON representation of the
            /// object.
            /// </param>
            public static MicrosoftDynamicsCRMaccount Create(this IAccounts operations, MicrosoftDynamicsCRMaccount body, string prefer = "return=representation")
            {
                return operations.CreateAsync(body, prefer).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Add new entity to accounts
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='body'>
            /// New entity
            /// </param>
            /// <param name='prefer'>
            /// Required in order for the service to return a JSON representation of the
            /// object.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<MicrosoftDynamicsCRMaccount> CreateAsync(this IAccounts operations, MicrosoftDynamicsCRMaccount body, string prefer = "return=representation", CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.CreateWithHttpMessagesAsync(body, prefer, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Add new entity to accounts
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='body'>
            /// New entity
            /// </param>
            /// <param name='prefer'>
            /// Required in order for the service to return a JSON representation of the
            /// object.
            /// </param>
            /// <param name='customHeaders'>
            /// Headers that will be added to request.
            /// </param>
            public static HttpOperationResponse<MicrosoftDynamicsCRMaccount> CreateWithHttpMessages(this IAccounts operations, MicrosoftDynamicsCRMaccount body, string prefer = "return=representation", Dictionary<string, List<string>> customHeaders = null)
            {
                return operations.CreateWithHttpMessagesAsync(body, prefer, customHeaders, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get entity from accounts by key
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='accountid'>
            /// key: accountid of account
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            public static MicrosoftDynamicsCRMaccount GetByKey(this IAccounts operations, string accountid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>))
            {
                return operations.GetByKeyAsync(accountid, select, expand).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Get entity from accounts by key
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='accountid'>
            /// key: accountid of account
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<MicrosoftDynamicsCRMaccount> GetByKeyAsync(this IAccounts operations, string accountid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.GetByKeyWithHttpMessagesAsync(accountid, select, expand, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Get entity from accounts by key
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='accountid'>
            /// key: accountid of account
            /// </param>
            /// <param name='select'>
            /// Select properties to be returned
            /// </param>
            /// <param name='expand'>
            /// Expand related entities
            /// </param>
            /// <param name='customHeaders'>
            /// Headers that will be added to request.
            /// </param>
            public static HttpOperationResponse<MicrosoftDynamicsCRMaccount> GetByKeyWithHttpMessages(this IAccounts operations, string accountid, IList<string> select = default(IList<string>), IList<string> expand = default(IList<string>), Dictionary<string, List<string>> customHeaders = null)
            {
                return operations.GetByKeyWithHttpMessagesAsync(accountid, select, expand, customHeaders, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Update entity in accounts
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='accountid'>
            /// key: accountid of account
            /// </param>
            /// <param name='body'>
            /// New property values
            /// </param>
            public static void Update(this IAccounts operations, string accountid, MicrosoftDynamicsCRMaccount body)
            {
                operations.UpdateAsync(accountid, body).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Update entity in accounts
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='accountid'>
            /// key: accountid of account
            /// </param>
            /// <param name='body'>
            /// New property values
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task UpdateAsync(this IAccounts operations, string accountid, MicrosoftDynamicsCRMaccount body, CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.UpdateWithHttpMessagesAsync(accountid, body, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Update entity in accounts
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='accountid'>
            /// key: accountid of account
            /// </param>
            /// <param name='body'>
            /// New property values
            /// </param>
            /// <param name='customHeaders'>
            /// Headers that will be added to request.
            /// </param>
            public static HttpOperationResponse UpdateWithHttpMessages(this IAccounts operations, string accountid, MicrosoftDynamicsCRMaccount body, Dictionary<string, List<string>> customHeaders = null)
            {
                return operations.UpdateWithHttpMessagesAsync(accountid, body, customHeaders, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Delete entity from accounts
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='accountid'>
            /// key: accountid of account
            /// </param>
            /// <param name='ifMatch'>
            /// ETag
            /// </param>
            public static void Delete(this IAccounts operations, string accountid, string ifMatch = default(string))
            {
                operations.DeleteAsync(accountid, ifMatch).GetAwaiter().GetResult();
            }

            /// <summary>
            /// Delete entity from accounts
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='accountid'>
            /// key: accountid of account
            /// </param>
            /// <param name='ifMatch'>
            /// ETag
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task DeleteAsync(this IAccounts operations, string accountid, string ifMatch = default(string), CancellationToken cancellationToken = default(CancellationToken))
            {
                (await operations.DeleteWithHttpMessagesAsync(accountid, ifMatch, null, cancellationToken).ConfigureAwait(false)).Dispose();
            }

            /// <summary>
            /// Delete entity from accounts
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='accountid'>
            /// key: accountid of account
            /// </param>
            /// <param name='ifMatch'>
            /// ETag
            /// </param>
            /// <param name='customHeaders'>
            /// Headers that will be added to request.
            /// </param>
            public static HttpOperationResponse DeleteWithHttpMessages(this IAccounts operations, string accountid, string ifMatch = default(string), Dictionary<string, List<string>> customHeaders = null)
            {
                return operations.DeleteWithHttpMessagesAsync(accountid, ifMatch, customHeaders, CancellationToken.None).ConfigureAwait(false).GetAwaiter().GetResult();
            }

    }
}
