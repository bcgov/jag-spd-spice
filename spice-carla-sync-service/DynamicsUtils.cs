using System;
using System.Collections.Generic;
using Gov.Jag.Spice.CarlaSync.models;
using Gov.Jag.Spice.Interfaces;
using Gov.Jag.Spice.Interfaces.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using SpdSync.models;
using SpiceCarlaSync.models;

namespace Gov.Jag.Spice.CarlaSync
{
    public class DynamicsUtils
    {
        private IDynamicsClient _dynamicsClient;
        private IConfiguration Configuration { get; }

        public DynamicsUtils(IConfiguration configuration, IDynamicsClient dynamicsClient)
        {
            Configuration = configuration;
            _dynamicsClient = dynamicsClient;
        }

        /// <summary>
        /// Import requests to Dynamics.
        /// </summary>
        /// <returns></returns>
        public void ImportApplicationRequests(List<ApplicationScreeningRequest> requests)
        {
            foreach (ApplicationScreeningRequest applicationRequest in requests)
            {
                /* Create 
                 * - company
                 *  - account (includes contact person)
                 *   - account screening
                 *   - associates
                 *    - associate screening                
                 * 
                 */                


                // TODO Look up if company exists already
                MicrosoftDynamicsCRMspiceCompany company = _dynamicsClient.Companies.Create(new MicrosoftDynamicsCRMspiceCompany()
                {
                    SpiceName = applicationRequest.ApplicantAccount.Name,
                    //SpiceBusinesstypes = applicationRequest.ApplicantAccount.
                    //SpiceAddress = NOT USED IN CARLA
                    SpiceStreet = applicationRequest.BusinessAddress.AddressStreet1,
                    SpiceCity = applicationRequest.BusinessAddress.City,
                    SpiceProvince = applicationRequest.BusinessAddress.StateProvince,
                    SpiceCountry = applicationRequest.BusinessAddress.Country,
                    SpicePostalcode = applicationRequest.BusinessAddress.Postal
                });

                // TODO Look up if dupe first
                // Contact person
                MicrosoftDynamicsCRMcontact contactPerson = _dynamicsClient.Contacts.Create(new MicrosoftDynamicsCRMcontact()
                {
                    Firstname = applicationRequest.ContactPerson.FirstName,
                    Middlename = applicationRequest.ContactPerson.MiddleName,
                    Lastname = applicationRequest.ContactPerson.LastName,
                    Emailaddress1 = applicationRequest.ContactPerson.Email,
                    Telephone1 = applicationRequest.ContactPerson.PhoneNumber
                });

                MicrosoftDynamicsCRMaccount account = _dynamicsClient.Accounts.Create(new MicrosoftDynamicsCRMaccount()
                {
                    Name = applicationRequest.ApplicantAccount.Name,
                    Address1Line1 = applicationRequest.BusinessAddress.AddressStreet1,
                    Address1City = applicationRequest.BusinessAddress.City,
                    Address1Country = applicationRequest.BusinessAddress.Country,
                    Address1Postalcode = applicationRequest.BusinessAddress.Postal,
                    SpiceLcrbjobid = applicationRequest.RecordIdentifier,
                    SpiceParcelidnumber = applicationRequest.Establishment.ParcelId,
                    SpiceBccorpregnumber = applicationRequest.ApplicantAccount.BCIncorporationNumber,
                    SpiceCompanyIdOdataBind = _dynamicsClient.GetEntityURI("spice_companies", company.SpiceCompanyid),
                    PrimaryContactIdOdataBind = _dynamicsClient.GetEntityURI("contacts", contactPerson.Contactid)
                });

                string accountEntityUri = _dynamicsClient.GetEntityURI("accounts", account.Accountid);

                MicrosoftDynamicsCRMincident incident = _dynamicsClient.Incidents.Create(new MicrosoftDynamicsCRMincident()
                {
                    SpiceCannabisapplicanttype = (int)CannabisApplicantType.Business,
                    SpiceApplicanttype = (int)SpiceApplicantType.Cannabis,
                    Prioritycode = (int)PriorityCode.Normal,
                    CustomerIdAccountOdataBind = accountEntityUri
                });

                foreach (var associate in applicationRequest.Associates)
                {
                    CreateAssociate(accountEntityUri, incident.Incidentid, associate);
                }

            }
        }

        public void CreateAssociate(string accountEntityUri, string screeningId, LegalEntity associateEntity)
        {
            if (associateEntity.IsIndividual)
            {
                // TODO
                // make unique
                // spdjobid?
                // Drivers licence expecting int
                // gender and self disclosure enumes
                // previous addresses
                // aliases
                // title
                // positions
                // tied house
                // interest percentage
                // appointment date
                // number voting shares
                // birthdate
                MicrosoftDynamicsCRMcontact associate = _dynamicsClient.Contacts.Create(new MicrosoftDynamicsCRMcontact()
                {
                    Firstname = associateEntity.Contact.FirstName,
                    Middlename = associateEntity.Contact.MiddleName,
                    Lastname = associateEntity.Contact.LastName,
                    Emailaddress1 = associateEntity.Contact.Email,
                    Telephone1 = associateEntity.Contact.PhoneNumber,
                    //SpiceDriverslicensenumber = associateEntity.Contact.DriversLicenceNumber,
                    SpiceBcidcardnumber = associateEntity.Contact.BCIdCardNumber,
                    //Birthdate = associateEntity.Contact.BirthDate,
                    SpiceBirthplace = associateEntity.Contact.Birthplace,
                    //SpiceSelfdisclosed = associateEntity.Contact.SelfDisclosure,
                    //Gendercode = associateEntity.Contact.Gender,
                    Address1Line1 = associateEntity.Contact.Address.AddressStreet1,
                    Address1Line2 = associateEntity.Contact.Address.AddressStreet2,
                    Address1Line3 = associateEntity.Contact.Address.AddressStreet3,
                    Address1City = associateEntity.Contact.Address.City,
                    Address1Postalcode = associateEntity.Contact.Address.Postal,
                    Address1Stateorprovince = associateEntity.Contact.Address.StateProvince,
                    Address1Country = associateEntity.Contact.Address.Country
                });

                MicrosoftDynamicsCRMspiceAccountcaseassignment accountContact = _dynamicsClient.Accountcaseassignments.Create(new MicrosoftDynamicsCRMspiceAccountcaseassignment()
                {
                    SpiceName = associateEntity.Contact.FirstName,
                    SpiceBusinessIdODataBind = accountEntityUri,
                    //SpiceBusinessIdODataBind = _dynamicsClient.GetEntityURI("accounts", account.Accountid),
                    SpiceContactCaseAssignmentIdODataBind = _dynamicsClient.GetEntityURI("contacts", associate.Contactid)
                });

                MicrosoftDynamicsCRMincident incident = new MicrosoftDynamicsCRMincident()
                {
                    SpiceApplicanttype = (int)SpiceApplicantType.Cannabis,
                    SpiceCannabisapplicanttype = (int)CannabisApplicantType.Associate,
                    CustomerIdContactOdataBind = _dynamicsClient.GetEntityURI("contacts", associate.Contactid),
                    ParentCaseIdOdataBind = _dynamicsClient.GetEntityURI("incidents", screeningId)
                };

                MicrosoftDynamicsCRMincident createdIncident = _dynamicsClient.Incidents.Create(incident);
            }
            else
            {
                foreach (var associate in associateEntity.Account.Associates)
                {
                    CreateAssociate(accountEntityUri, screeningId, associate);
                }
            }
        }
    }
}
