using CsvHelper;
using Gov.Jag.Spice.Interfaces;
using Gov.Jag.Spice.Interfaces.Models;
using Gov.Jag.Spice.Interfaces.SharePoint;
using Gov.Lclb.Cllb.Interfaces;
using Gov.Lclb.Cllb.Interfaces.Models;
using Hangfire.Console;
using Hangfire.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Rest;
using SpdSync.models;
using SpiceCarlaSync;
using SpiceCarlaSync.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Gov.Jag.Spice.CarlaSync
{
    public class CarlaUtils
    {
        const string DOCUMENT_LIBRARY = "SPD Applications";
        const string REQUESTS_PATH = "Requests";
        const string RESULTS_PATH = "Results";
        const string APPLICATIONS_PATH = "businesses";
        const string ASSOCIATES_PATH = "associates";
        const string WORKERS_PATH = "workers";

        const int MAX_WORKER_ALIAS = 5; // maximum number of aliases to send in the CSV export.
        const int MAX_WORKER_PREVIOUS_ADDRESS = 10;

        public ILogger _logger { get; }

        private IConfiguration Configuration { get; }
        private IDynamicsClient _dynamics;
        public ICarlaClient CarlaClient;
        public CarlaSharepoint _carlaSharepoint;

        public CarlaUtils(IConfiguration Configuration, ILoggerFactory loggerFactory, FileManager sharepoint)
        {
            this.Configuration = Configuration;
            _logger = loggerFactory.CreateLogger(typeof(CarlaUtils));
            if(!string.IsNullOrEmpty(Configuration["DYNAMICS_ODATA_URI"]))
            {
                _dynamics = DynamicsSetupUtil.SetupDynamics(Configuration);
            }
            else
            {
                _dynamics = null;
            }
            CarlaClient = SetupCarlaClient();
            _carlaSharepoint = new CarlaSharepoint(Configuration, loggerFactory, sharepoint, CarlaClient);
        }

        public CarlaClient SetupCarlaClient()
        {
            string carlaURI = Configuration["CARLA_URI"];
            string token = Configuration["CARLA_JWT_TOKEN"];

            // create JWT credentials
            TokenCredentials credentials = new TokenCredentials(token);

            return new CarlaClient(new Uri(carlaURI), credentials);
        }

        /// <summary>
        /// Hangfire job to receive an import from SPICE.
        /// </summary>
        public async Task ReceiveWorkerImportJob(PerformContext hangfireContext, List<WorkerScreeningRequest> requests)
        {
            hangfireContext.WriteLine("Starting SPICE Import Job.");
            _logger.LogError("Starting SPICE Import Job.");

            var (sent, filepath) = await _carlaSharepoint.SendWorkerRequestsToSharePoint(hangfireContext, requests);
            foreach (var request in requests)
            {
                if (sent)
                {
                    string fullFilepath = Configuration["SHAREPOINT_NATIVE_BASE_URI"] + filepath;
                    SendSPDEmail(
                        new List<Attachment>(),
                        $"New Worker Security Screening requested for {request.Contact.SpdJobId}",
                        "<p>LCRB has sent a request for a worker screening for " + request.Contact.SpdJobId + ".<p>" +
                        "<p>A CSV file has been placed in Sharepoint <a href='" + fullFilepath + "'>here</a></p>");
                }
            }

            hangfireContext.WriteLine("Done.");
            _logger.LogError("Done.");
        }

        /// <summary>
        /// Hangfire job to receive an import from SPICE.
        /// </summary>
        public async Task ReceiveApplicationImportJob(PerformContext hangfireContext, List<ApplicationScreeningRequest> requests)
        {
            hangfireContext.WriteLine("Starting SPICE Application Screening Import Job.");
            _logger.LogError("Starting SPICE Import Job.");

            var (sent, businessFilepath, associatesFilepath) = await _carlaSharepoint.SendApplicationRequestsToSharePoint(hangfireContext, requests);
            foreach (var request in requests)
            {
                if (sent)
                {
                    string fullBusinessFilepath = Configuration["SHAREPOINT_NATIVE_BASE_URI"] + businessFilepath;
                    string fullAssociatesFilepath = Configuration["SHAREPOINT_NATIVE_BASE_URI"] + associatesFilepath;
                    SendSPDEmail(
                        new List<Attachment>(),
                        $"New {request.ApplicantType} Security Screening requested for {request.RecordIdentifier}",
                        "<p>LCRB has sent a request for an " + request.ApplicantType + " security screening for Application " + request.RecordIdentifier + ".</p>" +
                        "<p>CSV files have been placed in SharePoint for:</p><ul>" + 
                        "<li>Business: <a href='" + fullBusinessFilepath + "'>here</a></li>" +
                        "<li>Associates: <a href='" + fullAssociatesFilepath + "'>here</a></li></ul>");
                }
            }


            hangfireContext.WriteLine("Done.");
            _logger.LogError("Done.");
        }

        /// <summary>
        /// Import responses to Dynamics.
        /// </summary>
        /// <returns></returns>
        private void ImportWorkerRequestsToDynamics(PerformContext hangfireContext, List<WorkerScreeningRequest> requests)
        {
            if(_dynamics == null)
            {
                hangfireContext.WriteLine("Dynamics not configured properly");
                return;
            }
            foreach (WorkerScreeningRequest workerRequest in requests)
            {
                // add data to dynamics.
                // create a Contact which will be bound to the customer id field.
                MicrosoftDynamicsCRMcontact contact = null; 

                if (workerRequest.Contact != null)
                {
                    
                    contact = _dynamics.GetContactByExternalId(workerRequest.Contact.ContactId);

                    if (contact == null)
                    {                        
                        contact = new MicrosoftDynamicsCRMcontact();
                    }

                    contact.Firstname = workerRequest.Contact.FirstName;
                    contact.Lastname = workerRequest.Contact.LastName;
                    contact.SpiceDateofbirth = workerRequest.Contact.BirthDate;
                    contact.Emailaddress1 = workerRequest.Contact.Email;
                    if (workerRequest.Contact.Address != null)
                    {
                        contact.Address1Line1 = workerRequest.Contact.Address.AddressStreet1;
                        contact.Address1Line2 = workerRequest.Contact.Address.AddressStreet2;
                        contact.Address1City = workerRequest.Contact.Address.City;
                        contact.Address1Stateorprovince = workerRequest.Contact.Address.StateProvince;
                        contact.Address1Postalcode = workerRequest.Contact.Address.Postal;
                        contact.Address1Country = workerRequest.Contact.Address.Country;
                    }

                    contact.SpiceBcidcardnumber = workerRequest.BCIdCardNumber;
                    contact.SpiceDriverslicensenumber = int.Parse(workerRequest.DriversLicence);
                    //contact.Externaluseridentifier = workerRequest.RecordIdentifier;
                    contact.Gendercode = (int?)workerRequest.Gender;

                    if (contact.Contactid == null) // new record
                    {
                        contact = _dynamics.Contacts.Create(contact);
                    }
                    else
                    {
                        _dynamics.Contacts.Update(contact.Contactid, contact);
                    }

                }
                                
                    
                MicrosoftDynamicsCRMincident incident = new MicrosoftDynamicsCRMincident();

                incident.SpiceApplicanttype = 525840001; // Cannabis  
                incident.SpiceCannabisapplicanttype = 525840002; // Worker

                // Screenings are Incidents in Dynamics.

                _dynamics.Incidents.Create(incident);
                                
            }
        }

        /// <summary>
        /// Import responses to Dynamics.
        /// </summary>
        /// <returns></returns>
        public void ImportWorkerRequestsToSMTP(PerformContext hangfireContext, List<WorkerScreeningRequest> requests)
        {

            List<CsvWorkerExport> export = new List<CsvWorkerExport>();

            foreach (WorkerScreeningRequest workerRequest in requests)
            {
                CsvWorkerExport csvWorkerExport = new CsvWorkerExport()
                {
                    Lcrbworkerjobid = workerRequest.RecordIdentifier,
                    Birthdate = $"{workerRequest.BirthDate:yyyy-MM-dd}",
                    Birthplacecity = workerRequest.Birthplace,
                    Driverslicence = workerRequest.DriversLicence,
                    Bcidentificationcardnumber = workerRequest.BCIdCardNumber,
                };
                //Selfdisclosure = workerRequest.SelfDisclosure,
                //Gendermf = workerRequest.Gender,

                if (workerRequest.Contact != null)
                {
                    csvWorkerExport.Legalsurname = workerRequest.Contact.LastName;
                    csvWorkerExport.Legalfirstname = workerRequest.Contact.FirstName;
                    csvWorkerExport.Legalmiddlename = workerRequest.Contact.MiddleName;
                    csvWorkerExport.Contactphone = workerRequest.Contact.PhoneNumber;
                    csvWorkerExport.Personalemailaddress = workerRequest.Contact.Email;
                }

                if (workerRequest.Address != null)
                {
                    csvWorkerExport.Addressline1 = workerRequest.Address.AddressStreet1;
                    csvWorkerExport.Addresscity = workerRequest.Address.City;
                    csvWorkerExport.Addressprovstate = workerRequest.Address.StateProvince;
                    csvWorkerExport.Addresscountry = workerRequest.Address.Country;
                    csvWorkerExport.Addresspostalcode = workerRequest.Address.Postal;
                }

                /* Flatten up the aliases */
                var aliasId = 1;
                foreach (var alias in workerRequest.Aliases)
                {
                    csvWorkerExport[$"Alias{aliasId}surname"] = alias.Surname;
                    csvWorkerExport[$"Alias{aliasId}middlename"] = alias.SecondName;
                    csvWorkerExport[$"Alias{aliasId}firstname"] = alias.GivenName;
                    aliasId++;

                    if (aliasId > MAX_WORKER_ALIAS)
                    {
                        break;
                    }
                }

                /* Flatten up the previous addresses */
                var addressId = 1;
                foreach (var address in workerRequest.PreviousAddresses)
                {
                    string addressIdString = addressId.ToString();
                    if (addressId == 10)
                    {
                        addressIdString = "x";
                    }
                    csvWorkerExport[$"Previousstreetaddress{addressIdString}"] = address.AddressStreet1;
                    csvWorkerExport[$"Previouscity{addressIdString}"] = address.City;
                    csvWorkerExport[$"Previousprovstate{addressIdString}"] = address.StateProvince;
                    csvWorkerExport[$"Previouscountry{addressIdString}"] = address.Country;
                    csvWorkerExport[$"Previouspostalcode{addressIdString}"] = address.Postal;
                    addressId++;
                    
                    if (addressId > MAX_WORKER_PREVIOUS_ADDRESS)
                    {
                        break;
                    }
                }

                export.Add(csvWorkerExport);
            }

            Attachment attachment = Attachment.CreateAttachmentFromString(CreateWorkerCSV(export), "Worker_ScreeningRequest.csv", Encoding.UTF8, "text/csv");
            bool sentEmail = SendSPDEmail(new List<Attachment>() { attachment }, "New Cannabis Worker Screening Request Received", "");

            if (sentEmail)
            {
                _logger.LogError($"Sent email to {Configuration["SPD_EXPORT_EMAIL"]}.");
            }
            else
            {
                _logger.LogError($"Unable to send email to {Configuration["SPD_EXPORT_EMAIL"]}.");
            }
        }

        private string CreateWorkerCSV(List<CsvWorkerExport> workers)
        {

            // convert the list to a CSV document.
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (var csv = new CsvWriter(sw))
            {
                csv.WriteRecords(workers);
            }

            sw.Flush();
            sw.Close();
            string csvData = sb.ToString();

            return csvData;
        }

        private string CreateAssociateCSV(List<CsvAssociateExport> associates)
        {
            // convert the list to a CSV document.
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (var csv = new CsvWriter(sw))
            {
                csv.Configuration.RegisterClassMap<CsvAssociateExportMap>();
                csv.WriteRecords(associates);
            }

            sw.Flush();
            sw.Close();
            string csvData = sb.ToString();

            return csvData;
        }

        private string CreateBusinessCSV(List<CsvBusinessExport> businesses)
        {
            // convert the list to a CSV document.
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            using (var csv = new CsvWriter(sw))
            {
                csv.Configuration.RegisterClassMap<CsvBusinessExportMap>();
                csv.WriteRecords(businesses);
            }

            sw.Flush();
            sw.Close();
            string csvData = sb.ToString();

            return csvData;
        }

        private bool SendSPDEmail(List<Attachment> attachments, string subject, string body)
        {
            if (string.IsNullOrEmpty(Configuration["SPD_EXPORT_EMAIL"]))
            {
                return false;
            }
            var emailSentSuccessfully = false;
            var datePart = DateTime.Now.ToString().Replace('/', '-').Replace(':', '_');
            var email = Configuration["SPD_EXPORT_EMAIL"];

            using (var mailClient = new SmtpClient(Configuration["SMTP_HOST"]))
            using (var message = new MailMessage("no-reply@gov.bc.ca", email))
            {
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = body;

                foreach (var attachment in attachments)
                {
                    message.Attachments.Add(attachment);
                }

                try
                {
                    mailClient.Send(message);
                    emailSentSuccessfully = true;
                }
                catch (Exception)
                {
                    emailSentSuccessfully = false;
                }

            }
            return emailSentSuccessfully;
        }

        public async Task ProcessResults(PerformContext hangfireContext)
        {
            await _carlaSharepoint.ProcessResultsFolders(hangfireContext);
        }

        public async Task<bool> SendApplicationScreeningResult(List<ApplicationScreeningResponse> responses)
        {
            var result = await CarlaClient.ReceiveApplicationScreeningResultWithHttpMessagesAsync(responses);

            return result.Response.StatusCode.ToString() == "Ok";
        }

        public async Task<bool> SendWorkerScreeningResult(List<Gov.Lclb.Cllb.Interfaces.Models.WorkerScreeningResponse> responses)
        {
            var result = await CarlaClient.ReceiveWorkerScreeningResultsWithHttpMessagesAsync(responses);

            return result.Response.StatusCode.ToString() == "Ok";
        }

        /// <summary>
        /// Hangfire job to receive an import from SPICE.
        /// </summary>
        public void SendResultsJob(PerformContext hangfireContext)
        {
            hangfireContext.WriteLine("Starting SPICE Application Send Results Job.");
            _logger.LogError("Starting SPICE Send Results Job.");

            // TODO - send results.

            hangfireContext.WriteLine("Done.");
            _logger.LogError("Done.");
        }
    }
}
