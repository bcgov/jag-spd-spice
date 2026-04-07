using System;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Gov.Jag.Spice.Interfaces.SharePoint;


/// <summary>
/// This class is only for backwards compatibility with existing legacy code so that the ISharePointFileManager
/// interface can be used without modification.
/// </summary>
public partial class CloudSharePointFileManager : ISharePointFileManager
{
    public async Task<List<FolderItem>> GetFoldersInDocumentLibraryAfterDate(
        string listTitle,
        DateTime afterDate
    )
    {
        throw new NotImplementedException();
    }

    public async Task<List<FolderItem>> FindFolderOne(string entityName, string folderGuidSegment)
    {
        throw new NotImplementedException();
    }

    public async Task<List<FolderItem>> FindFolderTwo(
        string parentRelativePath,
        string rawFolderName
    )
    {
        throw new NotImplementedException();
    }

    public async Task<List<FolderItem>> FindFolderThree(
        string parentRelativePath,
        string rawFolderNameSegment,
        string rawFolderGuidSegment
    )
    {
        throw new NotImplementedException();
    }

    public async Task<FolderItem> CreateFolder2(string relativeUrl)
    {
        throw new NotImplementedException();
    }

    public async Task<string> UploadFile2(
        string serverRelativeUrl,
        string fileName,
        byte[] fileData,
        string contentType
    )
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Tests the status of the SharePoint service to determine if it's accessible.
    /// This method attempts to get the site information to verify connectivity.
    /// </summary>
    /// <returns>True if SharePoint is accessible and healthy, false otherwise</returns>
    public async Task<bool> TestStatus()
    {
        bool result = false;

        try
        {
            _logger.LogDebug(
                "[CloudSharePointFileManager] TestStatus - Testing SharePoint connection to site '{SiteUrl}'",
                SiteUrl
            );

            // Ensure we have a valid access token
            await EnsureValidAccessTokenAsync();

            // Ensure the site ID is resolved
            await EnsureSiteIdResolvedAsync();

            // Try to get site information as a health check
            string requestUri = $"{GraphApiEndpoint}sites/{SiteId}";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            var response = await _Client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                result = true;
                _logger.LogInformation(
                    "[CloudSharePointFileManager] TestStatus - SharePoint is healthy (status: {StatusCode})",
                    response.StatusCode
                );
            }
            else
            {
                _logger.LogWarning(
                    "[CloudSharePointFileManager] TestStatus - SharePoint returned non-OK status: {StatusCode}",
                    response.StatusCode
                );
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "[CloudSharePointFileManager] TestStatus - Exception occurred while testing SharePoint: {ErrorMessage}",
                ex.Message
            );
            result = false;
        }

        return result;
    }
}


