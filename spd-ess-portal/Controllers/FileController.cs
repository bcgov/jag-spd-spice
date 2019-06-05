using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Gov.Jag.Spice.Interfaces;
using Gov.Jag.Spice.Interfaces.Models;
using Gov.Jag.Spice.Public.Utils;

namespace Gov.Jag.Spice.Public.Controllers
{
    [Route("api/[controller]")]
    public class FileController : Controller
    {
        private readonly IConfiguration Configuration;
        private readonly SharePointFileManager _sharePointFileManager;
        private readonly ILogger _logger;
        private readonly IDynamicsClient _dynamicsClient;

        public FileController(/*SharePointFileManager sharePointFileManager, */IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ILoggerFactory loggerFactory/*, IDynamicsClient dynamicsClient */)
        {
            Configuration = configuration;
            //_sharePointFileManager = sharePointFileManager;
            //_dynamicsClient = dynamicsClient;
            _logger = loggerFactory.CreateLogger<FileController>();
        }

        private static string GetAccountFolderName(MicrosoftDynamicsCRMaccount account)
        {
            string accountIdCleaned = account.Accountid.ToString().ToUpper().Replace("-", "");
            string folderName = $"{account.Accountid}_{accountIdCleaned}";
            return folderName;
        }

        private static string GetContactFolderName(MicrosoftDynamicsCRMcontact contact)
        {
            string applicationIdCleaned = contact.Contactid.ToString().ToUpper().Replace("-", "");
            string folderName = $"contact_{applicationIdCleaned}";
            return folderName;
        }

        /// <summary>
        /// Get a document location by reference
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <returns></returns>
        private string GetDocumentLocationReferenceByRelativeURL(string relativeUrl)
        {
            string result = null;
            string sanitized = relativeUrl.Replace("'", "''");
            // first see if one exists.
            var locations = _dynamicsClient.Sharepointdocumentlocations.Get(filter: "relativeurl eq '" + sanitized + "'");

            var location = locations.Value.FirstOrDefault();

            if (location == null)
            {
                MicrosoftDynamicsCRMsharepointdocumentlocation newRecord = new MicrosoftDynamicsCRMsharepointdocumentlocation()
                {
                    Relativeurl = relativeUrl
                };
                // create a new document location.
                try
                {
                    location = _dynamicsClient.Sharepointdocumentlocations.Create(newRecord);
                }
                catch (OdataerrorException odee)
                {
                    _logger.LogError("Error creating document location");
                    _logger.LogError("Request:");
                    _logger.LogError(odee.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(odee.Response.Content);
                }
            }

            if (location != null)
            {
                result = location.Sharepointdocumentlocationid;
            }

            return result;
        }

        [HttpPost("upload/{requestId}")]
        // allow large uploads
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile([FromRoute] string requestId, [FromForm]IFormFile file)
        {
            ViewModels.FileSystemItem result = null;
            
            if (string.IsNullOrEmpty(requestId))
            {
                return BadRequest();
            }

            //await CreateDocumentLibraryIfMissing(GetDocumentListTitle(entityName), GetDocumentTemplateUrlPart(entityName));

            //var hasAccess = await CanAccessEntity(entityName, entityId);
            //if (!hasAccess)
            //{
            //    return new NotFoundResult();
            //}

            //// Update modifiedon to current time
            //UpdateEntityModifiedOnDate(entityName, entityId, true);

            // Sanitize file name
            Regex illegalInFileName = new Regex(@"[#%*<>?{}~¿""]");
            string fileName = illegalInFileName.Replace(file.FileName, "");
            illegalInFileName = new Regex(@"[&:/\\|]");
            fileName = illegalInFileName.Replace(fileName, "-");

            //fileName = FileUtility.CombineNameDocumentType(fileName, documentType);
            //string folderName = await GetFolderName(entityName, entityId, _dynamicsClient);
            try
            {
                //await _sharePointFileManager.AddFile(GetDocumentTemplateUrlPart(entityName), folderName, fileName, file.OpenReadStream(), file.ContentType);
            }
            catch (SharePointRestException ex)
            {
                _logger.LogError("Error uploading file to SharePoint");
                _logger.LogError(ex.Response.Content);
                _logger.LogError(ex.Message);
                return new NotFoundResult();
            }
            return Json(fileName);
        }

        private async Task<bool> CanAccessEntity(string entityName, string entityId)
        {
            var result = false;
            var id = Guid.Parse(entityId);
            switch (entityName.ToLower())
            {
                case "account":
                    var account = await _dynamicsClient.GetAccountById(id);
                    result = account != null;
                    break;
                case "contact":
                    var contact = await _dynamicsClient.GetContactById(id);
                    result = contact != null;
                    break;
            }
            return result;
        }
        private async Task<bool> CanAccessEntityFile(string entityName, string entityId, string documentType, string serverRelativeUrl)
        {
            var result = await CanAccessEntity(entityName, entityId);
            //get list of files for entity
            var files = await getFileDetailsListInFolder(entityId, entityName, documentType);
            //confirm the serverRelativeUrl is in one of the files
            var hasFile = files.Any(f => f.serverrelativeurl == serverRelativeUrl);
            return result && hasFile;
        }

        private static async Task<string> GetFolderName(string entityName, string entityId, IDynamicsClient _dynamicsClient)
        {
            var folderName = "";
            switch (entityName.ToLower())
            {
                case "account":
                    var account = await _dynamicsClient.GetAccountById(Guid.Parse(entityId));
                    folderName = GetAccountFolderName(account);
                    break;
                case "contact":
                    var contact = await _dynamicsClient.GetContactById(Guid.Parse(entityId));
                    folderName = GetContactFolderName(contact);
                    break;
            }
            return folderName;
        }

        private void UpdateEntityModifiedOnDate(string entityName, string entityId, bool setUploadedFromPortal = false)
        {
            switch (entityName.ToLower())
            {
                case "contact":
                    var patchContact = new MicrosoftDynamicsCRMcontact();
                    try
                    {
                        _dynamicsClient.Contacts.Update(entityId, patchContact);
                    }
                    catch (OdataerrorException odee)
                    {
                        _logger.LogError("Error updating Contact");
                        _logger.LogError("Request:");
                        _logger.LogError(odee.Request.Content);
                        _logger.LogError("Response:");
                        _logger.LogError(odee.Response.Content);
                        // fail if we can't create.
                        throw (odee);
                    }
                    break;
            }
        }

        [HttpGet("{entityId}/download-file/{entityName}/{fileName}")]
        public async Task<IActionResult> DownloadFile(string entityId, string entityName, [FromQuery]string serverRelativeUrl, [FromQuery]string documentType)
        {
            // get the file.
            if (string.IsNullOrEmpty(serverRelativeUrl) || string.IsNullOrEmpty(documentType) || string.IsNullOrEmpty(entityId) || string.IsNullOrEmpty(entityName))
            {
                return BadRequest();
            }

            var hasAccess = await CanAccessEntityFile(entityName, entityId, documentType, serverRelativeUrl);
            if (!hasAccess)
            {
                return new NotFoundResult();
            }

            byte[] fileContents = await _sharePointFileManager.DownloadFile(serverRelativeUrl);
            return new FileContentResult(fileContents, "application/octet-stream");

        }

        /// <summary>
        /// Get the file details list in folder associated to the application folder and document type
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="documentType"></param>
        /// <returns></returns>
        [HttpGet("{entityId}/attachments/{entityName}/{documentType}")]
        public async Task<IActionResult> GetFileDetailsListInFolder([FromRoute] string entityId, [FromRoute] string entityName, [FromRoute] string documentType)
        {
            if (string.IsNullOrEmpty(entityId) || string.IsNullOrEmpty(entityName) || string.IsNullOrEmpty(documentType))
            {
                return BadRequest();
            }

            List<ViewModels.FileSystemItem> fileSystemItemVMList = await getFileDetailsListInFolder(entityId, entityName, documentType);

            var hasAccess = await CanAccessEntity(entityName, entityId);
            if (!hasAccess)
            {
                return new NotFoundResult();
            }

            return Json(fileSystemItemVMList);
        }

        private async Task<List<ViewModels.FileSystemItem>> getFileDetailsListInFolder(string entityId, string entityName, string documentType)
        {
            List<ViewModels.FileSystemItem> fileSystemItemVMList = new List<ViewModels.FileSystemItem>();

            if (string.IsNullOrEmpty(entityId) || string.IsNullOrEmpty(entityName) || string.IsNullOrEmpty(documentType))
            {
                return fileSystemItemVMList;
            }

            try
            {
                await CreateDocumentLibraryIfMissing(GetDocumentListTitle(entityName), GetDocumentTemplateUrlPart(entityName));

                string folderName = await GetFolderName(entityName, entityId, _dynamicsClient); ;
                // Get the file details list in folder
                List<SharePointFileManager.FileDetailsList> fileDetailsList = null;
                try
                {
                    fileDetailsList = await _sharePointFileManager.GetFileDetailsListInFolder(GetDocumentTemplateUrlPart(entityName), folderName, documentType);
                }
                catch (SharePointRestException spre)
                {
                    _logger.LogError("Error getting SharePoint File List");
                    _logger.LogError("Request URI:");
                    _logger.LogError(spre.Request.RequestUri.ToString());
                    _logger.LogError("Response:");
                    _logger.LogError(spre.Response.Content);
                    throw new Exception("Unable to get Sharepoint File List.");
                }

                if (fileDetailsList != null)
                {
                    foreach (SharePointFileManager.FileDetailsList fileDetails in fileDetailsList)
                    {
                        ViewModels.FileSystemItem fileSystemItemVM = new ViewModels.FileSystemItem()
                        {
                            // remove the document type text from file name
                            name = fileDetails.Name.Substring(fileDetails.Name.IndexOf("__") + 2),
                            // convert size from bytes (original) to KB
                            size = int.Parse(fileDetails.Length),
                            serverrelativeurl = fileDetails.ServerRelativeUrl,
                            timelastmodified = DateTime.Parse(fileDetails.TimeLastModified),
                            documenttype = fileDetails.DocumentType
                        };

                        fileSystemItemVMList.Add(fileSystemItemVM);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError("Error getting SharePoint File List");
                
                _logger.LogError(e.Message);
            }

            return fileSystemItemVMList;
        }

        private static string GetDocumentListTitle(string entityName)
        {
            var listTitle = "";
            switch (entityName.ToLower())
            {
                case "account":
                    listTitle = SharePointFileManager.DefaultDocumentListTitle;
                    break;
                case "contact":
                    listTitle = SharePointFileManager.ContactDocumentListTitle;
                    break;
            }
            return listTitle;
        }

        private static string GetDocumentTemplateUrlPart(string entityName)
        {
            var listTitle = "";
            switch (entityName.ToLower())
            {
                case "account":
                    listTitle = SharePointFileManager.DefaultDocumentListTitle;
                    break;
                case "contact":
                    listTitle = SharePointFileManager.ContactDocumentListTitle;
                    break;
            }
            return listTitle;
        }

        private async Task CreateDocumentLibraryIfMissing(string listTitle, string documentTemplateUrl = null)
        {
            var exists = await _sharePointFileManager.DocumentLibraryExists(listTitle);
            if (!exists)
            {
                await _sharePointFileManager.CreateDocumentLibrary(listTitle, documentTemplateUrl);
            }
        }
    }
}
