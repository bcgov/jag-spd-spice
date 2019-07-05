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
using Gov.Jag.Spice.Interfaces.SharePoint;

namespace Gov.Jag.Spice.Public.Controllers
{
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {        
        private const string ScreeningDocumentListTitle = "Screening";
        private const string ScreeningDocumentUrlTitle = "incident";

        private readonly ILogger<FileController> _logger;
        private readonly IConfiguration Configuration;
        private readonly FileManager _sharePointFileManager;
        private readonly IDynamicsClient _dynamicsClient;

        public FileController(ILogger<FileController> logger, IConfiguration configuration, IDynamicsClient dynamicsClient, FileManager fileManager)
        {
            _logger = logger;
            Configuration = configuration;
            _sharePointFileManager = fileManager;
            _dynamicsClient = dynamicsClient;
        }



        private static string GetScreeningFolderName(MicrosoftDynamicsCRMincident screening)
        {
            string screeningIdCleaned = screening.Incidentid.ToUpper().Replace("-", "");
            string folderName = $"{screening.Title}_{screeningIdCleaned}";
            return folderName;
        }

        [HttpPost("upload/{screeningId}")]
        // allow large uploads
        [DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile([FromRoute] string screeningId, [FromForm] IFormFile file)
        {
            if (_sharePointFileManager == null)
            {
                _logger.LogWarning("Cannot upload file {FileName} for screening {ScreeningId} because no connection to SharePoint exists", file.Name, screeningId);
                return new JsonResult(file.FileName);
            }

            if (string.IsNullOrEmpty(screeningId))
            {
                _logger.LogWarning("Cannot upload file without a screeningId");
                return BadRequest();
            }

            await CreateDocumentLibraryIfMissing(ScreeningDocumentListTitle, ScreeningDocumentUrlTitle);

            bool hasAccess = await CanAccessScreening(screeningId);
            if (!hasAccess)
            {
                _logger.LogWarning("Cannot access screening {ScreeningId}", screeningId);
                return new NotFoundResult();
            }

            try
            {
                // Update modifiedon to current time
                await UpdateScreeningModifiedOnDate(screeningId);
            }
            catch (OdataerrorException ex)
            {
                _logger.LogError(ex, string.Join(Environment.NewLine, "Failed to update last modification date for screening {ScreeningId}", "Request: {Request}", "Response: {Response}"), screeningId, ex.Request.Content, ex.Response.Content);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            // Sanitize file name
            var illegalInFileName = new Regex(@"[#%*<>?{}~¿""]");
            string fileName = illegalInFileName.Replace(file.FileName, "");
            illegalInFileName = new Regex(@"[&:/\\|]");
            fileName = illegalInFileName.Replace(fileName, "-");

            fileName = FileUtility.CombineNameDocumentType(fileName, "WebUpload");
            string folderName = await GetFolderName(screeningId);
            try
            {
                await _sharePointFileManager.AddFile(ScreeningDocumentUrlTitle, folderName, fileName, file.OpenReadStream(), file.ContentType);
                _logger.LogInformation("Successfully uploaded file {FileName} to folder {FolderName} on SharePoint", fileName, folderName);
            }
            catch (SharePointRestException ex)
            {
                _logger.LogError(ex, "Failed to upload file {FileName} to folder {FolderName} on SharePoint {ResponseContent}", ex.Response.Content);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return new JsonResult(fileName);
        }

        private async Task<bool> CanAccessScreening(string screeningId)
        {
            var screening = await _dynamicsClient.GetScreeningById(Guid.Parse(screeningId));
            return screening != null;
        }

        private async Task<string> GetFolderName(string screeningId)
        {
            var screening = await _dynamicsClient.GetScreeningById(Guid.Parse(screeningId));
            return GetScreeningFolderName(screening);
        }

        private async Task UpdateScreeningModifiedOnDate(string screeningId)
        {
            var patchScreening = new MicrosoftDynamicsCRMincident();
            await _dynamicsClient.Incidents.UpdateAsync(screeningId, patchScreening);
        }

        private async Task CreateDocumentLibraryIfMissing(string listTitle, string folderName)
        {
            bool exists = await _sharePointFileManager.DocumentLibraryExists(listTitle);
            if (!exists)
            {
                await _sharePointFileManager.CreateDocumentLibrary(listTitle, folderName);
            }
        }
    }
}
