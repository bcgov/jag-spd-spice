using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gov.Jag.Spice.Interfaces;
using Gov.Jag.Spice.Interfaces.Models;
using Gov.Jag.Spice.Public.Models.Extensions;
using Gov.Jag.Spice.Public.Utils;
using Microsoft.Extensions.Logging;

namespace Gov.Jag.Spice.Public.ViewModels
{
    public class ScreeningRequest
    {
        public string ClientMinistry { get; set; }
        public string ProgramArea { get; set; }
        public string ScreeningType { get; set; }
        public string Reason { get; set; }
        public string OtherReason { get; set; }

        public Candidate Candidate { get; set; }

        public Contact Contact { get; set; }

        public async Task<bool> Validate(IDynamicsClient dynamicsClient, Ministry dynamicsMinistry, ProgramArea dynamicsProgramArea, ScreeningType dynamicsScreeningType)
        {
            // validate nested data exists
            if (Candidate == null || Contact == null)
            {
                return false;
            }

            // validate required properties
            var requiredProperties = new string[]
            {
                ClientMinistry,
                ProgramArea,
                ScreeningType,
                Reason,
                Candidate.FirstName,
                Candidate.LastName,
                Candidate.Email,
                Candidate.Position,
                Contact.FirstName,
                Contact.LastName,
                Contact.Email,
            };

            if (requiredProperties.Any(string.IsNullOrWhiteSpace))
            {
                return false;
            }

            // validate range for candidate date of birth
            if (Candidate.DateOfBirth < DateTime.Today.AddYears(-100) || Candidate.DateOfBirth > DateTime.Today.AddYears(-10))
            {
                return false;
            }

            // validate submitted screening type matches an existing screening type in Dynamics which is valid for the user's org code
            if (dynamicsMinistry == null || dynamicsProgramArea == null || dynamicsScreeningType == null)
            {
                return false;
            }

            // validate submitted ministry and program area match the associated entities in Dynamics for the submitted screening type
            if (ClientMinistry != dynamicsMinistry.Value || ProgramArea != dynamicsProgramArea.Value)
            {
                return false;
            }

            // validate screening reason matches one of the possible screening reasons
            var screeningReason = await DynamicsUtility.GetScreeningReasonAsync(dynamicsClient, Reason);
            if (screeningReason == null)
            {
                return false;
            }

            return true;
        }

        public async Task<string> Submit(IDynamicsClient dynamicsClient, ILogger logger, User user, ScreeningType screeningType, string programAreaId)
        {
            var candidate = await ObtainCandidate(dynamicsClient, logger);
            var submitter = await ObtainSubmitter(dynamicsClient, logger, user, programAreaId);
            var contact = await ObtainContact(dynamicsClient, logger);

            try
            {
                var screeningRequest = await DynamicsUtility.CreateScreeningRequestAsync(
                    dynamicsClient, 
                    this,
                    candidate.Contactid,
                    submitter.SpiceMinistryemployeeid,                                       
                    contact.SpiceMinistryemployeeid,
                    screeningType.ApplicantType,
                    screeningType.CannabisApplicantType
                );

                logger.LogInformation("Successfully created screening request {ScreeningId} from view model {@ScreeningRequest}", screeningRequest.Incidentid, this);

                return screeningRequest.Incidentid;
            }
            catch (OdataerrorException ex)
            {
                logger.LogError(ex, string.Join(Environment.NewLine, "Failed to create screening request incident", "{@ErrorBody}"), ex.Body);
                throw;
            }
        }

        public static async Task<IEnumerable<Ministry>> GetMinistryScreeningTypesAsync(IDynamicsClient dynamicsClient, string orgCode)
        {
            var getMinistries = DynamicsUtility.GetMinistriesAsync(dynamicsClient);
            var getProgramAreas = DynamicsUtility.GetProgramAreasAsync(dynamicsClient, orgCode);
            var getScreeningTypes = DynamicsUtility.GetActiveScreeningTypesAsync(dynamicsClient);

            var screeningTypes = (await getScreeningTypes).ToList();
            var programAreas = (await getProgramAreas).ToList();
            var ministries = (await getMinistries).ToList();

            foreach (var programArea in programAreas)
            {
                programArea.SpiceSpiceMinistrySpiceServices =
                    screeningTypes.Where(t => t._spiceMinistryserviceidValue == programArea.SpiceMinistryid).ToList();
            }

            foreach (var ministry in ministries)
            {
                ministry.SpiceGovministrySpiceMinistry =
                    programAreas.Where(a => a._spiceGovministryidValue == ministry.SpiceGovministryid).ToList();
            }

            return ministries.Select(m => m.ToViewModel()).Where(m => m.ProgramAreas.Any());
        }

        public static async Task<(Ministry, ProgramArea, ScreeningType)> GetMinistryScreeningTypeAsync(
            IDynamicsClient dynamicsClient, string orgCode, string screeningTypeId)
        {
            var getMinistries = DynamicsUtility.GetMinistriesAsync(dynamicsClient);
            var getProgramAreas = DynamicsUtility.GetProgramAreasAsync(dynamicsClient, orgCode);
            var getScreeningTypes = DynamicsUtility.GetActiveScreeningTypesAsync(dynamicsClient);

            var screeningType = (await getScreeningTypes).SingleOrDefault(s => s.SpiceServicesid == screeningTypeId);
            var programArea = (await getProgramAreas).SingleOrDefault(p => p.SpiceMinistryid == screeningType?._spiceMinistryserviceidValue);
            var ministry = (await getMinistries).SingleOrDefault(m => m.SpiceGovministryid == programArea?._spiceGovministryidValue);

            return (ministry?.ToViewModel(), programArea?.ToViewModel(), screeningType?.ToViewModel());
        }

        public static async Task<IEnumerable<ScreeningReason>> GetScreeningReasonsAsync(IDynamicsClient dynamicsClient)
        {
            return (await DynamicsUtility.GetScreeningReasonsAsync(dynamicsClient)).Select(a => a.ToViewModel());
        }

        private async Task<MicrosoftDynamicsCRMcontact> ObtainCandidate(IDynamicsClient dynamicsClient, ILogger logger)
        {
            var candidate = await DynamicsUtility.GetCandidateAsync(dynamicsClient, Candidate);
            if (candidate == null)
            {
                try
                {
                    candidate = await DynamicsUtility.CreateCandidateAsync(dynamicsClient, Candidate);
                    logger.LogInformation("Successfully created candidate {CandidateId} from view model {@Candidate}", candidate.Contactid, Candidate);
                }
                catch (OdataerrorException ex)
                {
                    logger.LogError(ex, string.Join(Environment.NewLine, "Failed to create candidate from view model {@Candidate}", "{@ErrorBody}"), Candidate, ex.Body);
                    throw;
                }
            }
            else
            {
                logger.LogInformation("Successfully retrieved existing candidate {CandidateId} from view model {@Candidate}", candidate.Contactid, Candidate);
            }

            return candidate;
        }

        private async Task<MicrosoftDynamicsCRMspiceMinistryemployee> ObtainSubmitter(IDynamicsClient dynamicsClient, ILogger logger, User user, string programAreaId)
        {
            // Submitter is now a ministry employee
            Contact contact = new Contact() { Email = user.Email, FirstName = user.GivenName, LastName = user.Surname };

            var submitter = await DynamicsUtility.GetContactAsync(dynamicsClient, contact);
            if (submitter == null)
            {
                try
                {
                    submitter = await DynamicsUtility.CreateContactAsync(dynamicsClient, contact, programAreaId);
                    logger.LogInformation("Successfully created contact {ContactId} from view model {@Contact}", submitter.SpiceMinistryemployeeid,  contact);
                }
                catch (OdataerrorException ex)
                {
                    logger.LogError(ex, string.Join(Environment.NewLine, "Failed to create contact from view model {@Contact}", "{@ErrorBody}"), contact, ex.Body);
                    throw;
                }
            }
            else
            {
                logger.LogInformation("Successfully retrieved existing contact {ContactId} from view model {@Contact}", submitter.SpiceMinistryemployeeid, contact);
            }

            return submitter;


        }

        private async Task<MicrosoftDynamicsCRMspiceMinistryemployee> ObtainContact(IDynamicsClient dynamicsClient, ILogger logger)
        {
            var contact = await DynamicsUtility.GetContactAsync(dynamicsClient, Contact);
            if (contact == null)
            {
                try
                {
                    contact = await DynamicsUtility.CreateContactAsync(dynamicsClient, Contact, ProgramArea);
                    logger.LogInformation("Successfully created contact {ContactId} from view model {@Contact}", contact.SpiceMinistryemployeeid, Contact);
                }
                catch (OdataerrorException ex)
                {
                    logger.LogError(ex, string.Join(Environment.NewLine, "Failed to create contact from view model {@Contact}", "{@ErrorBody}"), Contact, ex.Body);
                    throw;
                }
            }
            else
            {
                logger.LogInformation("Successfully retrieved existing contact {ContactId} from view model {@Contact}", contact.SpiceMinistryemployeeid, Contact);
            }

            return contact;
        }
    }
}
