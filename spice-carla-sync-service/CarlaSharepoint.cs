//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using CsvHelper;
//using Gov.Jag.Spice.Interfaces;
//using Hangfire.Console;
//using Hangfire.Server;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using SpdSync;
//using SpdSync.models;
//using SpiceCarlaSync.models;
//using static Gov.Jag.Spice.Interfaces.SharePointFileManager;

//namespace Gov.Lclb.Cllb.Interfaces
//{
//    public class FileSystemItem
//    {
//        public string id { get; set; }
//        public string name { get; set; }
//        public string documenttype { get; set; }
//        public int size { get; set; }
//        public string serverrelativeurl { get; set; }
//        public DateTime timecreated { get; set; }
//        public DateTime timelastmodified { get; set; }
//    }

//    public class CarlaSharepoint
//    {
//        const string DOCUMENT_LIBRARY = "SPD Applications";
//        const string REQUESTS_PATH = "Requests";
//        const string RESULTS_PATH = "Results";
//        const string BUSINESSES_PATH = "Businesses";
//        const string ASSOCIATES_PATH = "Associates";

//        public ILogger _logger { get; }
//        private IConfiguration _configuration { get; }
//        public SharePointFileManager _sharepoint;

//        public CarlaSharepoint(IConfiguration Configuration, ILoggerFactory loggerFactory, SharePointFileManager sharepoint)
//        {
//            _configuration = Configuration;
//            _sharepoint = sharepoint;
//            _logger = loggerFactory.CreateLogger(typeof(CarlaSharepoint));
//            //SetupSharepointFolders();
//        }

//        /// <summary>
//        /// Import requests to LCRB SharePoint
//        /// </summary>
//        /// <returns></returns>
//        public async Task SendApplicationRequestsToSharePoint(PerformContext hangfireContext, List<ApplicationScreeningRequest> requests)
//        {
//            int suffix = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
//            foreach (var request in requests)
//            {
//                List<CsvAssociateExport> associateExports = Jag.Spice.CarlaSync.CarlaUtils.CreateBaseAssociatesExport(request);
//                List<CsvBusinessExport> businessExports = Jag.Spice.CarlaSync.CarlaUtils.CreateBusinessExport(request);

//                using (var mem = new MemoryStream())
//                using (var writer = new StreamWriter(mem))
//                using (var csvWriter = new CsvWriter(writer))
//                {
//                    csvWriter.Configuration.Delimiter = ";";
//                    csvWriter.Configuration.HasHeaderRecord = true;
//                    csvWriter.Configuration.AutoMap<CsvAssociateExport>();

//                    csvWriter.WriteHeader<CsvAssociateExport>();
//                    csvWriter.WriteRecords(associateExports);

//                    writer.Flush();
//                    mem.Position = 0;

//                    try
//                    {
//                        hangfireContext.WriteLine("Uploading business associates CSV.");
//                        _logger.LogInformation("Uploading business associates CSV.");
//                        var upload = await _sharepoint.UploadFile($"{request.RecordIdentifier}_associates_{suffix}.csv", DOCUMENT_LIBRARY, REQUESTS_PATH + "/" + ASSOCIATES_PATH, mem, "text/csv");
//                    }
//                    catch (Exception ex)
//                    {
//                        _logger.LogError("Error: " + ex.Message);
//                    }
//                }

//                using (var mem = new MemoryStream())
//                using (var writer = new StreamWriter(mem))
//                using (var csvWriter = new CsvWriter(writer))
//                {
//                    csvWriter.Configuration.Delimiter = ";";
//                    csvWriter.Configuration.HasHeaderRecord = true;
//                    csvWriter.Configuration.AutoMap<CsvBusinessExport>();

//                    csvWriter.WriteHeader<CsvBusinessExport>();
//                    csvWriter.WriteRecords(businessExports);

//                    writer.Flush();
//                    mem.Position = 0;

//                    try
//                    {
//                        hangfireContext.WriteLine("Uploading business application CSV.");
//                        _logger.LogInformation("Uploading business application CSV.");
//                        var upload = await _sharepoint.UploadFile($"{request.RecordIdentifier}_business_{suffix}.csv", DOCUMENT_LIBRARY, REQUESTS_PATH + "/" + BUSINESSES_PATH, mem, "text/csv");
//                    }
//                    catch (Exception ex)
//                    {
//                        _logger.LogError("Error: " + ex.Message);
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// Sets up the sharepoint folders if they don't already exist
//        /// </summary>
//        public async Task<bool> SetupSharepointFolders()
//        {
//            try
//            {
//                var documentLibraryExists = await _sharepoint.DocumentLibraryExists(DOCUMENT_LIBRARY);
//                if (!documentLibraryExists)
//                {
//                    _logger.LogInformation("Creating document library.");
//                    await _sharepoint.CreateDocumentLibrary(DOCUMENT_LIBRARY);
//                }

//                var reqFoldersExist = await _sharepoint.FolderExists(DOCUMENT_LIBRARY, REQUESTS_PATH);
//                if (!reqFoldersExist)
//                {
//                    _logger.LogInformation("Creating request document folders.");
//                    await _sharepoint.CreateFolder(DOCUMENT_LIBRARY, REQUESTS_PATH);
//                    await _sharepoint.CreateFolder(DOCUMENT_LIBRARY, REQUESTS_PATH + "/" + BUSINESSES_PATH);
//                    await _sharepoint.CreateFolder(DOCUMENT_LIBRARY, REQUESTS_PATH + "/" + ASSOCIATES_PATH);
//                }

//                var resFoldersExist = await _sharepoint.FolderExists(DOCUMENT_LIBRARY, RESULTS_PATH);
//                if (!resFoldersExist)
//                {
//                    _logger.LogInformation("Creating result document folders.");
//                    await _sharepoint.CreateFolder(DOCUMENT_LIBRARY, RESULTS_PATH);
//                    await _sharepoint.CreateFolder(DOCUMENT_LIBRARY, RESULTS_PATH + "/" + BUSINESSES_PATH);
//                    await _sharepoint.CreateFolder(DOCUMENT_LIBRARY, RESULTS_PATH + "/" + ASSOCIATES_PATH);
//                }

//                return true;
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError("Error: " + ex.Message);
//                return false;
//            }
//        }

//        /// <summary>
//        /// Processes the results folders and sends new results to Carla for updating
//        /// </summary>
//        private async Task ProcessResultsFolders(PerformContext hangfireContext)
//        {
//            List<FileSystemItem> businessFiles = await getFileDetailsListInFolder(hangfireContext, DOCUMENT_LIBRARY, RESULTS_PATH + "/" + BUSINESSES_PATH);
//            List<FileSystemItem> associatesFiles = await getFileDetailsListInFolder(hangfireContext, DOCUMENT_LIBRARY, RESULTS_PATH + "/" + ASSOCIATES_PATH);

//            //// Look for files with unprocessed name
//            //hangfireContext.WriteLine("Looking for unprocessed files.");
//            //_logger.LogError("Looking for unprocessed files.");
//            //var unprocessedFiles = businessFiles.Where(f => !f.name.StartsWith("processed_")).ToList();
//            //foreach (var file in unprocessedFiles)
//            //{
//                //// Skip if file is not .csv
//                //if (Path.GetExtension(file.name).ToLower() != ".csv")
//                //{
//                //    continue;
//                //}

//                //string jobNumber = file.name.Split("_")[0];

//                //// Get the matching associates file
//                //var associatesFile = businessFiles.Where(f => f.name.StartsWith(jobNumber)).ToList();

//                //// Download file
//                //hangfireContext.WriteLine("File found. Downloading file.");
//                //_logger.LogError("File found. Downloading file.");
//                //byte[] fileContents = await _sharepoint.DownloadFile(file.serverrelativeurl);

//                //// Update worker
//                //hangfireContext.WriteLine("Updating worker.");
//                //_logger.LogError("Updating worker.");
//                //string data = System.Text.Encoding.Default.GetString(fileContents);
//                //List<WorkerScreeningResponse> parsedData = WorkerResponseParser.ParseWorkerResponse(data, _logger);

//                //foreach (var spdResponse in parsedData)
//                //{
//                //    try
//                //    {
//                //        await UpdateSecurityClearance(hangfireContext, spdResponse, spdResponse.RecordIdentifier);
//                //    }
//                //    catch (SharePointRestException spre)
//                //    {
//                //        hangfireContext.WriteLine("Unable to update security clearance status due to SharePoint.");
//                //        hangfireContext.WriteLine("Request:");
//                //        hangfireContext.WriteLine(spre.Request.Content);
//                //        hangfireContext.WriteLine("Response:");
//                //        hangfireContext.WriteLine(spre.Response.Content);

//                //        _logger.LogError("Unable to update security clearance status due to SharePoint.");
//                //        _logger.LogError("Request:");
//                //        _logger.LogError(spre.Request.Content);
//                //        _logger.LogError("Response:");
//                //        _logger.LogError(spre.Response.Content);
//                //        continue;
//                //    }
//                //    catch (Exception e)
//                //    {
//                //        hangfireContext.WriteLine("Unable to update security clearance status.");
//                //        hangfireContext.WriteLine("Message:");
//                //        hangfireContext.WriteLine(e.Message);

//                //        _logger.LogError("Unable to update security clearance status.");
//                //        _logger.LogError("Message:");
//                //        _logger.LogError(e.Message);
//                //        continue;
//                //    }
//                //}

//                //// Rename file
//                //hangfireContext.WriteLine("Finished processing file.");
//                //_logger.LogError($"Finished processing file {file.serverrelativeurl}");
//                //_logger.LogError($"{parsedData.Count} records updated.");

//                //string newserverrelativeurl = "";
//                //int index = file.serverrelativeurl.LastIndexOf("/");
//                //if (index > 0)
//                //{
//                //    newserverrelativeurl = file.serverrelativeurl.Substring(0, index);

//                //    // tag cases where the files were empty.
//                //    if (parsedData.Count == 0)
//                //    {
//                //        newserverrelativeurl += "/" + "processed_empty_" + file.name;
//                //    }
//                //    else
//                //    {
//                //        newserverrelativeurl += "/" + "processed_" + file.name;
//                //    }
//                //}

//                //try
//                //{
//                //    await _sharePointFileManager.RenameFile(file.serverrelativeurl, newserverrelativeurl);
//                //}
//                //catch (SharePointRestException spre)
//                //{
//                //    hangfireContext.WriteLine("Unable to rename file.");
//                //    hangfireContext.WriteLine("Request:");
//                //    hangfireContext.WriteLine(spre.Request.Content);
//                //    hangfireContext.WriteLine("Response:");
//                //    hangfireContext.WriteLine(spre.Response.Content);

//                //    _logger.LogError("Unable to rename file.");
//                //    _logger.LogError("Message:");
//                //    _logger.LogError(spre.Message);
//                //    throw spre;
//                //}
//            //}
//        }

//        private async Task<List<FileSystemItem>> getFileDetailsListInFolder(PerformContext hangfireContext, string libraryPath, string folderPath)
//        {
//            List<FileSystemItem> fileSystemItemVMList = new List<FileSystemItem>();

//            // Get the file details list in folder
//            List<FileDetailsList> fileDetailsList = null;
//            try
//            {
//                fileDetailsList = await _sharepoint.GetFileDetailsListInFolder(libraryPath, folderPath, "");
//            }
//            catch (SharePointRestException spre)
//            {
//                hangfireContext.WriteLine("Unable to get Sharepoint File List.");
//                hangfireContext.WriteLine("Request:");
//                hangfireContext.WriteLine(spre.Request.Content);
//                hangfireContext.WriteLine("Response:");
//                hangfireContext.WriteLine(spre.Response.Content);

//                _logger.LogError("Unable to get Sharepoint File List.");
//                _logger.LogError("Request:");
//                _logger.LogError(spre.Request.Content);
//                _logger.LogError("Response:");
//                _logger.LogError(spre.Request.Content);
//                throw spre;
//            }

//            if (fileDetailsList != null)
//            {
//                foreach (FileDetailsList fileDetails in fileDetailsList)
//                {
//                    FileSystemItem fileSystemItemVM = new FileSystemItem()
//                    {
//                        // remove the document type text from file name
//                        name = fileDetails.Name,
//                        // convert size from bytes (original) to KB
//                        size = int.Parse(fileDetails.Length),
//                        serverrelativeurl = fileDetails.ServerRelativeUrl,
//                        timelastmodified = DateTime.Parse(fileDetails.TimeLastModified),
//                        documenttype = fileDetails.DocumentType
//                    };

//                    fileSystemItemVMList.Add(fileSystemItemVM);
//                }
//            }

//            return fileSystemItemVMList;
//        }
//    }
//}
