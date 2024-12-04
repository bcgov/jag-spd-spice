using Gov.Jag.Spice.Interfaces;
using Gov.Jag.Spice.Interfaces.Models;
using Gov.Jag.Spice.Public.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.Spice.Public.Utils
{
    public static class DynamicsUtility
    {
        public static async Task<IEnumerable<MicrosoftDynamicsCRMspiceGovministry>> GetMinistriesAsync(IDynamicsClient dynamicsClient)
        {
            var entities = (await dynamicsClient.Govministries.GetAsync()).Value;
            if (!entities.Any())
            {
                throw new DynamicsEntityNotFoundException(nameof(MicrosoftDynamicsCRMspiceGovministry));
            }

            return entities;
        }

        public static async Task<IEnumerable<MicrosoftDynamicsCRMspiceMinistry>> GetProgramAreasAsync(IDynamicsClient dynamicsClient, string orgCode)
        {
            string filter = $"spice_orgcode eq '{Escape(orgCode)}'";
            var entities = (await dynamicsClient.Ministries.GetAsync(filter: filter)).Value;
            if (!entities.Any())
            {
                throw new DynamicsEntityNotFoundException(nameof(MicrosoftDynamicsCRMspiceMinistry));
            }

            return entities;
        }

        public static async Task<IEnumerable<MicrosoftDynamicsCRMspiceServices>> GetActiveScreeningTypesAsync(IDynamicsClient dynamicsClient)
        {
            const string filter = "statecode eq 0";
            var entities = (await dynamicsClient.Serviceses.GetAsync(filter: filter)).Value;
            if (!entities.Any())
            {
                throw new DynamicsEntityNotFoundException(nameof(MicrosoftDynamicsCRMspiceServices));
            }

            return entities;
        }

        public static async Task<IEnumerable<MicrosoftDynamicsCRMspiceReasonforscreening>> GetScreeningReasonsAsync(IDynamicsClient dynamicsClient)
        {
            var entities = (await dynamicsClient.Reasonforscreenings.GetAsync()).Value;
            if (!entities.Any())
            {
                throw new DynamicsEntityNotFoundException(nameof(MicrosoftDynamicsCRMspiceReasonforscreening));
            }

            return entities;
        }

        public static async Task<MicrosoftDynamicsCRMspiceReasonforscreening> GetScreeningReasonAsync(IDynamicsClient dynamicsClient, string reasonId)
        {
            string filter = $"spice_reasonforscreeningid eq {Escape(reasonId)}";
            var entities = (await dynamicsClient.Reasonforscreenings.GetAsync(filter: filter)).Value;
            if (!entities.Any())
            {
                throw new DynamicsEntityNotFoundException(nameof(MicrosoftDynamicsCRMspiceReasonforscreening), filter);
            }

            return entities.FirstOrDefault();
        }

        public static async Task<MicrosoftDynamicsCRMcontact> GetCandidateAsync(IDynamicsClient dynamicsClient, Candidate candidate)
        {
            /* SPDCSS-1174 - Middle name is optional. If it's not included, we need to use a different filter - just setting it to an empty string doesn't work. */
            string filter = string.Empty;
            if (string.IsNullOrEmpty(candidate.MiddleName))
            {
                filter = $"fullname eq '{Escape(candidate.FirstName)} {Escape(candidate.LastName)}' and middlename eq null and spice_dateofbirth eq {candidate.DateOfBirth:o}";
            }
            else
            {
                filter = $"fullname eq '{Escape(candidate.FirstName)} {Escape(candidate.LastName)}' and middlename eq '{Escape(candidate.MiddleName)}' and spice_dateofbirth eq {candidate.DateOfBirth:o}";
            }

            var entities = (await dynamicsClient.Contacts.GetAsync(filter: filter)).Value;
            return entities.FirstOrDefault();
        }

        public static async Task UpdateCandidateEmailAsync(IDynamicsClient dynamicsClient, MicrosoftDynamicsCRMcontact candidate)
        {
            try
            {
                var entity = new MicrosoftDynamicsCRMcontact
                {
                    Emailaddress1 = candidate.Emailaddress1,
                };
                await dynamicsClient.Contacts.UpdateAsync(candidate.Contactid, entity);
            }
            catch (OdataerrorException ex)
            {
                throw new DynamicsEntityNotFoundException(nameof(MicrosoftDynamicsCRMspiceGovministry));
            }
        }

        public static async Task<MicrosoftDynamicsCRMcontact> GetSubmitterAsync(IDynamicsClient dynamicsClient, User user)
        {
            string filter = $"spice_portalcontactidentifier eq '{Escape(user.Id)}'";
            var entities = (await dynamicsClient.Contacts.GetAsync(filter: filter)).Value;
            return entities.FirstOrDefault();
        }

        public static async Task<MicrosoftDynamicsCRMspiceMinistryemployee> GetContactAsync(IDynamicsClient dynamicsClient, Contact contact)
        {
            /* SPDCSS-1195: Changed spice_name to spice_firstname. */ 
            string filter = $"spice_firstname eq '{Escape(contact.FirstName)}' and spice_lastname eq '{Escape(contact.LastName)}' and spice_email eq '{Escape(contact.Email)}'";
            var entity = (await dynamicsClient.Ministryemployees.GetAsync(filter: filter)).Value;
            return entity.FirstOrDefault();
        }

        public static async Task<MicrosoftDynamicsCRMcontact> CreateCandidateAsync(IDynamicsClient dynamicsClient, Candidate candidate)
        {
            var entity = new MicrosoftDynamicsCRMcontact
            {
                Fullname = $"{candidate.FirstName} {candidate.LastName}",
                Firstname = candidate.FirstName,
                Middlename = candidate.MiddleName,
                Lastname = candidate.LastName,
                SpiceDateofbirth = candidate.DateOfBirth,
                Emailaddress1 = candidate.Email,
            };

            entity = await dynamicsClient.Contacts.CreateAsync(entity);
            return entity;
        }

        public static async Task<MicrosoftDynamicsCRMcontact> CreateSubmitterAsync(IDynamicsClient dynamicsClient, User user)
        {
            var entity = new MicrosoftDynamicsCRMcontact
            {
                Fullname = $"{user.GivenName} {user.Surname}",
                Firstname = user.GivenName,
                Lastname = user.Surname,
                Emailaddress1 = user.Email,
                SpicePortalcontactidentifier = user.Id,
            };

            entity = await dynamicsClient.Contacts.CreateAsync(entity);
            return entity;
        }

        public static async Task<MicrosoftDynamicsCRMspiceMinistryemployee> CreateContactAsync(IDynamicsClient dynamicsClient, Contact contact, string programAreaId)
        {
            string programArea = dynamicsClient.GetEntityURI("spice_ministries", programAreaId);

            var entity = new MicrosoftDynamicsCRMspiceMinistryemployee
            {
                SpiceFirstName = contact.FirstName,
                SpiceLastname = contact.LastName,
                SpiceEmail = contact.Email,
                SpiceMinistryIdODataBind = programArea,
            };

            entity = await dynamicsClient.Ministryemployees.CreateAsync(entity);
            return entity;
        }

        public static async Task<MicrosoftDynamicsCRMincident> CreateScreeningRequestAsync(IDynamicsClient dynamicsClient, ScreeningRequest screeningRequest, string candidateId, string submitterId, string contactId, int? applicantType, int? cannabisApplicantType)
        {
            string candidate = dynamicsClient.GetEntityURI("contacts", candidateId);
            string submitter = dynamicsClient.GetEntityURI("spice_ministryemployees", submitterId);
            string programArea = dynamicsClient.GetEntityURI("spice_ministries", screeningRequest.ProgramArea);
            string screeningType = dynamicsClient.GetEntityURI("spice_serviceses", screeningRequest.ScreeningType);
            string reason = dynamicsClient.GetEntityURI("spice_reasonforscreenings", screeningRequest.Reason);
            string contact = dynamicsClient.GetEntityURI("spice_ministryemployees", contactId);

            var entity = new MicrosoftDynamicsCRMincident
            {
                CustomerIdODataBind = candidate,
                RequesterHiringManagerODataBind = submitter,
                SpiceClientIdODataBind = programArea,
                SpiceServiceIdODataBind = screeningType,
                SpiceReasonForScreeningIdODataBind = reason,
                SpiceOtherscreeningreason = screeningRequest.OtherReason,
                SpiceReturnToIdODataBind = contact,
                SpiceApplicanttype = applicantType,
                SpiceCannabisapplicanttype = cannabisApplicantType,
                SpiceJobtitle = screeningRequest.Candidate.Position,
            };

            entity = await dynamicsClient.Incidents.CreateAsync(entity);
            return entity;
        }

        private static string Escape(string s) => s.Replace("'", "''");
    }
}
