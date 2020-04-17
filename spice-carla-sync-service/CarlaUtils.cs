using Gov.Jag.Spice.Interfaces.SharePoint;
using Gov.Lclb.Cllb.Interfaces;
using Gov.Lclb.Cllb.Interfaces.Models;
using Hangfire.Console;
using Hangfire.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Rest;
using SpiceCarlaSync.models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Gov.Jag.Spice.CarlaSync
{
    public class CarlaUtils
    {
        public ILogger _logger { get; }

        private IConfiguration Configuration { get; }
        public CarlaClient CarlaClient;
        public CarlaSharepoint _carlaSharepoint;

        public CarlaUtils(IConfiguration Configuration, ILoggerFactory loggerFactory, FileManager sharepoint)
        {
            this.Configuration = Configuration;
            _logger = loggerFactory.CreateLogger(typeof(CarlaUtils));
            CarlaClient = SetupCarlaClient();
            if(sharepoint != null)
            {
                _carlaSharepoint = new CarlaSharepoint(Configuration, loggerFactory, sharepoint, CarlaClient);
            }
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
        public async Task ReceiveWorkerImportJob(PerformContext hangfireContext, List<IncompleteWorkerScreening> requests)
        {
            hangfireContext.WriteLine("Starting SPICE Import Job.");
            _logger.LogError("Starting SPICE Import Job.");

            var (sent, filepath) = await _carlaSharepoint.SendWorkerRequestsToSharePoint(requests);
            if (sent)
            {
                foreach (var request in requests)
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

        public async Task ReceiveApplicationImportJob(PerformContext hangfireContext, List<IncompleteApplicationScreening> requests)
        {
            _logger.LogError("Starting SPICE Import Job.");

            var (sent, businessFilepath, associatesFilepath) = await _carlaSharepoint.SendApplicationRequestsToSharePoint( requests);
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
                        "<li>Associates: <a href='" + fullAssociatesFilepath + "'>here</a></li></ul>" +
                        $"<p>Current LCRB Assignee: <a href='mailto:{request.AssignedPerson.Email}'>{request.AssignedPerson.FirstName} {request.AssignedPerson.LastName}</a></p>");
                }
            }


            _logger.LogError("Done.");
        }

        private bool SendSPDEmail(List<Attachment> attachments, string subject, string body)
        {
            if (string.IsNullOrEmpty(Configuration["SPD_EXPORT_EMAIL"]))
            {
                return false;
            }
            var emailSentSuccessfully = false;
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
            await _carlaSharepoint.ProcessResultsFolders();
        }

        public async Task<bool> SendApplicationScreeningResult(List<CompletedApplicationScreening> responses)
        {
            try
            {
                var result = await CarlaClient.ApplicationScreenings.POSTWithHttpMessagesAsync(responses);
                return result.Response.StatusCode.ToString() == "OK";
            }
            catch (HttpOperationException ex)
            {
                _logger.LogError(ex, "Failed to send application results to carla");
                _logger.LogError(ex.Response.Content);
                return false;
            }
        }

        public async Task<bool> SendWorkerScreeningResult(List<CompletedWorkerScreening> responses)
        {
            try
            {
                var result = await CarlaClient.WorkerScreenings.POSTWithHttpMessagesAsync(responses);
                return result.Response.StatusCode.ToString() == "OK";
            }
            catch (HttpOperationException ex)
            {
                _logger.LogError(ex, "Failed to send worker results to carla");
                _logger.LogError(ex.Response.Content);
                return false;
            }
        }
    }
}
