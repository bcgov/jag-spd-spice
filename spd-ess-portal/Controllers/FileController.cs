using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly FileManager _sharePointFileManager;
        private readonly IDynamicsClient _dynamicsClient;

        public FileController(ILogger<FileController> logger, IDynamicsClient dynamicsClient, FileManager fileManager)
        {
            _logger = logger;
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
            if (string.IsNullOrEmpty(screeningId))
            {
                _logger.LogWarning("Cannot upload file without a screeningId");
                return BadRequest();
            }

            try
            {
                bool documentLibraryExists = await _sharePointFileManager.DocumentLibraryExists(ScreeningDocumentListTitle);
                if (!documentLibraryExists)
                {
                    await _sharePointFileManager.CreateDocumentLibrary(ScreeningDocumentListTitle, ScreeningDocumentUrlTitle);
                    _logger.LogInformation("Successfully created document library {ScreeningDocumentListTitle} on SharePoint", ScreeningDocumentListTitle);
                }
                else
                {
                    _logger.LogInformation("Successfully retrieved document library {ScreeningDocumentListTitle} on SharePoint", ScreeningDocumentListTitle);
                }

                // update screening modification time to current time
                await UpdateScreeningModifiedOnDate(screeningId);
                _logger.LogInformation("Successfully updated last modification date for screening {ScreeningId}", screeningId);

                // retrieve folder name
                var screening = await _dynamicsClient.GetScreeningById(Guid.Parse(screeningId));
                string folderName = GetScreeningFolderName(screening);
                _logger.LogInformation("Successfully retrieved SharePoint folder name {FolderName} for screening {ScreeningId}", folderName, screeningId);

                // format file name
                string fileName = SanitizeFileName(file.FileName);
                fileName = FileUtility.CombineNameDocumentType(fileName, "WebUpload");

                // upload to SharePoint
                await _sharePointFileManager.AddFile(ScreeningDocumentUrlTitle, folderName, fileName, file.OpenReadStream(), file.ContentType);
                _logger.LogInformation("Successfully uploaded file {FileName} to folder {FolderName} on SharePoint", fileName, folderName);

                return new JsonResult(fileName);
            }
            catch (SharePointRestException ex)
            {
                _logger.LogError(ex,
                    string.Join(Environment.NewLine, "Failed to upload file {FileName} for screening {ScreeningId}", "Request: {@Request}", "Response: {@Response}"),
                    file.Name,
                    screeningId,
                    ex.Request,
                    ex.Response);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (OdataerrorException ex)
            {
                _logger.LogError(ex,
                    string.Join(Environment.NewLine, "Failed to upload file {FileName} for screening {ScreeningId}", "{@ErrorBody}"),
                    file.Name,
                    screeningId,
                    ex.Body);

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload file {FileName} for screening {ScreeningId}", file.Name, screeningId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private static string SanitizeFileName(string fileName)
        {
            fileName = new Regex(@"[#%*<>?{}~¿""]").Replace(fileName, "");
            fileName = new Regex(@"[&:/\\|]").Replace(fileName, "-");
            return fileName;
        }

        private async Task UpdateScreeningModifiedOnDate(string screeningId)
        {
            var patchScreening = new MicrosoftDynamicsCRMincident();
            await _dynamicsClient.Incidents.UpdateAsync(screeningId, patchScreening);
        }
    }
}
