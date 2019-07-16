using System;
using System.Collections.Generic;
using System.Globalization;
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

                string servicesFilter = "spice_name eq 'Cannabis Applicant (Business)'";
                var service = _dynamicsClient.Serviceses.Get(filter: servicesFilter).Value[0];

                string clientFilter = "spice_name eq 'LCRB'";
                var client = _dynamicsClient.Ministries.Get(filter: clientFilter).Value[0];
                string clientEntityUri = _dynamicsClient.GetEntityURI("spice_ministries", client.SpiceMinistryid);

                MicrosoftDynamicsCRMincident incident = _dynamicsClient.Incidents.Create(new MicrosoftDynamicsCRMincident()
                {
                    SpiceCannabisapplicanttype = (int)CannabisApplicantType.Business,
                    SpiceApplicanttype = (int)SpiceApplicantType.Cannabis,
                    Prioritycode = (int)PriorityCode.Normal,
                    CustomerIdAccountOdataBind = accountEntityUri,
                    SpiceServiceIdODataBind = _dynamicsClient.GetEntityURI("spice_serviceses", service.SpiceServicesid),
                    ClientOdataBind = clientEntityUri
                });

                foreach (var associate in applicationRequest.Associates)
                {
                    CreateAssociate(clientEntityUri, accountEntityUri, incident.Incidentid, associate);
                }

            }
        }

        public void CreateAssociate(string clientEntityUri, string accountEntityUri, string screeningId, LegalEntity associateEntity)
        {
            if (associateEntity.IsIndividual)
            {
                MicrosoftDynamicsCRMcontact associate = new MicrosoftDynamicsCRMcontact()
                {
                    Firstname = associateEntity.Contact.FirstName,
                    Middlename = associateEntity.Contact.MiddleName,
                    Lastname = associateEntity.Contact.LastName,
                    Emailaddress1 = associateEntity.Contact.Email,
                    Telephone1 = associateEntity.Contact.PhoneNumber,
                    //SpiceDriverslicensenumber = associateEntity.Contact.DriversLicenceNumber,
                    SpiceBcidcardnumber = associateEntity.Contact.BCIdCardNumber,
                    SpiceDateofbirth = associateEntity.Contact.BirthDate.Value.UtcDateTime,
                    SpiceBirthplace = associateEntity.Contact.Birthplace,
                    SpiceSelfdisclosed = associateEntity.Contact.SelfDisclosure == GeneralYesNo.Yes,
                    Address1Line1 = associateEntity.Contact.Address.AddressStreet1,
                    Address1Line2 = associateEntity.Contact.Address.AddressStreet2,
                    Address1Line3 = associateEntity.Contact.Address.AddressStreet3,
                    Address1City = associateEntity.Contact.Address.City,
                    Address1Postalcode = associateEntity.Contact.Address.Postal,
                    Address1Stateorprovince = associateEntity.Contact.Address.StateProvince,
                    Address1Country = associateEntity.Contact.Address.Country,
                    SpiceContactSpicePreviousaddresses = new List<MicrosoftDynamicsCRMspicePreviousaddresses>(),
                    SpiceContactSpiceAliases = new List<MicrosoftDynamicsCRMspiceAliases>(),
                    SpicePositiontitle = associateEntity.Title
                };

                if ((int)associateEntity.Contact.Gender != 0)
                {
                    associate.Gendercode = (int)associateEntity.Contact.Gender;
                }

                foreach (var address in associateEntity.PreviousAddresses)
                {
                    associate.SpiceContactSpicePreviousaddresses.Add(new MicrosoftDynamicsCRMspicePreviousaddresses()
                    {
                        SpiceName = address.AddressStreet1,
                        SpiceCity = address.City,
                        SpiceStateprovince = address.StateProvince,
                        SpiceZippostalcode = address.Postal,
                        SpiceCountry = address.Country,
                        SpiceStartdate = address.FromDate,
                        SpiceEnddate = address.ToDate
                    });
                }

                foreach (var alias in associateEntity.Aliases)
                {
                    associate.SpiceContactSpiceAliases.Add(new MicrosoftDynamicsCRMspiceAliases()
                    {
                        SpiceName = alias.GivenName,
                        SpiceMiddlename = alias.SecondName,
                        SpiceLastname = alias.Surname,
                    });
                }

                associate = _dynamicsClient.Contacts.Create(associate);

                MicrosoftDynamicsCRMspiceAccountcaseassignment accountContact = _dynamicsClient.Accountcaseassignments.Create(new MicrosoftDynamicsCRMspiceAccountcaseassignment()
                {
                    SpiceName = associateEntity.Contact.FirstName,
                    SpiceBusinessIdODataBind = accountEntityUri,
                    SpiceContactCaseAssignmentIdODataBind = _dynamicsClient.GetEntityURI("contacts", associate.Contactid),
                    SpicePosition = GetLegalEntityPositions(associateEntity.Positions)
                });

                string servicesFilter = "spice_name eq 'Cannabis Associate'";
                var service = _dynamicsClient.Serviceses.Get(filter: servicesFilter).Value[0];

                MicrosoftDynamicsCRMincident incident = new MicrosoftDynamicsCRMincident()
                {
                    SpiceApplicanttype = (int)SpiceApplicantType.Cannabis,
                    SpiceCannabisapplicanttype = (int)CannabisApplicantType.Associate,
                    CustomerIdODataBind = _dynamicsClient.GetEntityURI("contacts", associate.Contactid),
                    ParentCaseIdOdataBind = _dynamicsClient.GetEntityURI("incidents", screeningId),
                    SpiceServiceIdODataBind = _dynamicsClient.GetEntityURI("spice_serviceses", service.SpiceServicesid),
                    ClientOdataBind = clientEntityUri
                };

                MicrosoftDynamicsCRMincident createdIncident = _dynamicsClient.Incidents.Create(incident);
            }
            else
            {
                foreach (var associate in associateEntity.Account.Associates)
                {
                    CreateAssociate(clientEntityUri, accountEntityUri, screeningId, associate);
                }
            }
        }

        public string GetLegalEntityPositions(List<string> positions)
        {
            List<int> positionValues = new List<int>();
            foreach (var position in positions)
            {
                if (position == "director")
                {
                    positionValues.Add((int)Positions.Director);
                }
                if (position == "officer")
                {
                    positionValues.Add((int)Positions.Officer);
                }
                if (position == "senior manager")
                {
                    positionValues.Add((int)Positions.SeniorManager);
                }
                if (position == "key personnel")
                {
                    positionValues.Add((int)Positions.KeyPersonnel);
                }
                if (position == "shareholder")
                {
                    positionValues.Add((int)Positions.Shareholder);
                }
                if (position == "owner")
                {
                    positionValues.Add((int)Positions.Owner);
                }
                if (position == "trustee")
                {
                    positionValues.Add((int)Positions.Trustee);
                }
                if (position == "deemed associate")
                {
                    positionValues.Add((int)Positions.DeemedAssociate);
                }
                if (position == "partner")
                {
                    positionValues.Add((int)Positions.Partner);
                }
            }
            return string.Join(", ", positionValues);
        }
    }
}
