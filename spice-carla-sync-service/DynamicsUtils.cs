using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gov.Jag.Spice.Interfaces;
using Gov.Jag.Spice.Interfaces.Models;
using Hangfire.Server;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SpiceCarlaSync.models;

namespace Gov.Jag.Spice.CarlaSync
{
    public class DynamicsUtils
    {
        private IDynamicsClient _dynamicsClient;
        private IConfiguration Configuration { get; }
        private ILogger _logger { get; }

        public DynamicsUtils(IConfiguration configuration, ILoggerFactory loggerFactory, IDynamicsClient dynamicsClient)
        {
            Configuration = configuration;
            _dynamicsClient = dynamicsClient;
            _logger = loggerFactory.CreateLogger(typeof(DynamicsUtils));
        }

        /// <summary>
        /// Import requests to Dynamics.
        /// </summary>
        /// <returns></returns>
        public async Task ImportApplicationRequests(List<IncompleteApplicationScreening> requests)
        {
            foreach (IncompleteApplicationScreening applicationRequest in requests)
            {
                if (applicationRequest.ApplicantAccount == null)
                {
                    _logger.LogError("Application sent without a valid account");
                    return;
                }
                // Company
                string uniqueFilter = "spice_carla_company eq '" + applicationRequest.ApplicantAccount.AccountId + "'";
                CompaniesGetResponseModel companiesResponse;
                try
                {
                    companiesResponse = _dynamicsClient.Companies.Get(1, filter: uniqueFilter);
                }
                catch (OdataerrorException e)
                {
                    _logger.LogError("Failed to query companies");
                    _logger.LogError(e.Message);
                    _logger.LogError("Request:");
                    _logger.LogError(e.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(e.Response.Content);
                    return;
                }
                MicrosoftDynamicsCRMspiceCompany company;
                if (companiesResponse.Value.Count > 0)
                {
                    company = companiesResponse.Value[0];
                }
                else
                {
                    try
                    {
                        company = _dynamicsClient.Companies.Create(new MicrosoftDynamicsCRMspiceCompany()
                        {
                            SpiceCarlaCompany = applicationRequest.ApplicantAccount.AccountId,
                            SpiceName = applicationRequest.ApplicantAccount.Name,
                            SpiceBusinesstypes = (int)Enum.Parse(typeof(BusinessTypes), applicationRequest.ApplicantAccount.BusinessType),
                            SpiceStreet = applicationRequest.BusinessAddress.AddressStreet1,
                            SpiceCity = applicationRequest.BusinessAddress.City,
                            SpiceProvince = applicationRequest.BusinessAddress.StateProvince,
                            SpiceCountry = applicationRequest.BusinessAddress.Country,
                            SpicePostalcode = applicationRequest.BusinessAddress.Postal
                        });
                    }
                    catch (OdataerrorException e)
                    {
                        _logger.LogError("Failed to create new account");
                        _logger.LogError(e.Message);
                        _logger.LogError("Request:");
                        _logger.LogError(e.Request.Content);
                        _logger.LogError("Response:");
                        _logger.LogError(e.Response.Content);
                        return;
                    }
                }
                 
                // Contact person
                uniqueFilter = "externaluseridentifier eq '" + applicationRequest.ContactPerson.ContactId + "'";
                ContactsGetResponseModel contactResponse;
                try
                {
                     contactResponse = _dynamicsClient.Contacts.Get(1, filter: uniqueFilter);
                }
                catch (OdataerrorException e)
                {
                    _logger.LogError($"Failed to query contacts: {e.Message.ToString()}");
                    _logger.LogError($"Request: {e.Request.Content.ToString()}");
                    _logger.LogError($"Response: {e.Response.Content.ToString()}");
                    return;
                }
                MicrosoftDynamicsCRMcontact contactPerson;
                if (contactResponse.Value.Count > 0)
                {
                    contactPerson = contactResponse.Value[0];
                }
                else
                {
                    try
                    {
                        contactPerson = _dynamicsClient.Contacts.Create(new MicrosoftDynamicsCRMcontact()
                        {
                            Externaluseridentifier = applicationRequest.ContactPerson.ContactId,
                            Firstname = applicationRequest.ContactPerson.FirstName,
                            Middlename = applicationRequest.ContactPerson.MiddleName,
                            Lastname = applicationRequest.ContactPerson.LastName,
                            Emailaddress1 = applicationRequest.ContactPerson.Email,
                            Telephone1 = applicationRequest.ContactPerson.PhoneNumber
                        });
                    }
                    catch (OdataerrorException e)
                    {
                        _logger.LogError($"Failed to create new account: {e.Message.ToString()}");
                        _logger.LogError($"Request: {e.Request.Content.ToString()}");
                        _logger.LogError($"Response: {e.Response.Content.ToString()}");
                        return;
                    }
                }


                uniqueFilter = "spice_carla_account eq '" + applicationRequest.ApplicantAccount.AccountId + "'";
                AccountsGetResponseModel accountResponse;
                try
                {
                    accountResponse = _dynamicsClient.Accounts.Get(1, filter: uniqueFilter);
                }
                catch (OdataerrorException e)
                {
                    _logger.LogError($"Failed to query accounts: {e.Message.ToString()}");
                    _logger.LogError($"Request: {e.Request.Content.ToString()}");
                    _logger.LogError($"Response: {e.Response.Content.ToString()}");
                    return;
                }
                MicrosoftDynamicsCRMaccount account;
                if (accountResponse.Value.Count > 10)
                {
                    account = accountResponse.Value[0];
                }
                else
                {
                    try
                    {
                        account = _dynamicsClient.Accounts.Create(new MicrosoftDynamicsCRMaccount()
                        {
                            SpiceCarlaAccount = applicationRequest.ApplicantAccount.AccountId,
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
                    }
                    catch (OdataerrorException e)
                    {
                        _logger.LogError($"Failed to create new account: {e.Message.ToString()}");
                        _logger.LogError($"Request: {e.Request.Content.ToString()}");
                        _logger.LogError($"Response: {e.Response.Content.ToString()}");
                        return;
                    }
                }

                string accountEntityUri = _dynamicsClient.GetEntityURI("accounts", account.Accountid);

                string servicesFilter = "spice_name eq 'Cannabis Applicant (Business)'";
                MicrosoftDynamicsCRMspiceServices service;
                try
                {
                    service = _dynamicsClient.Serviceses.Get(filter: servicesFilter).Value[0];
                }
                catch (OdataerrorException e)
                {
                    _logger.LogError("Failed to query services");
                    _logger.LogError(e.Message);
                    _logger.LogError("Request:");
                    _logger.LogError(e.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(e.Response.Content);
                    return;
                }

                string clientFilter = "spice_name eq 'LCRB'";
                MicrosoftDynamicsCRMspiceMinistry client;
                try
                {
                   client = _dynamicsClient.Ministries.Get(filter: clientFilter).Value[0];
                }
                catch (OdataerrorException e)
                {
                    _logger.LogError("Failed to query companies");
                    _logger.LogError(e.Message);
                    _logger.LogError("Request:");
                    _logger.LogError(e.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(e.Response.Content);
                    return;
                }
                string clientEntityUri = _dynamicsClient.GetEntityURI("spice_ministries", client.SpiceMinistryid);

                MicrosoftDynamicsCRMincident incident = new MicrosoftDynamicsCRMincident()
                {
                    SpiceCannabisapplicanttype = (int)CannabisApplicantType.Business,
                    SpiceApplicanttype = (int)SpiceApplicantType.Cannabis,
                    Prioritycode = (int)PriorityCode.Normal,
                    CustomerIdAccountOdataBind = accountEntityUri,
                    SpiceServiceIdODataBind = _dynamicsClient.GetEntityURI("spice_serviceses", service.SpiceServicesid),
                    SpiceClientIdODataBind = clientEntityUri
                };

                LcrblicencetypesGetResponseModel response = _dynamicsClient.Lcrblicencetypes.Get(filter: "spice_name eq '" + applicationRequest.ApplicationType + "'");
                if (response.Value.Count > 0)
                {
                    incident.LCRBLicenceTypeIdOdataBind = _dynamicsClient.GetEntityURI("spice_lcrblicencetypes", response.Value[0].SpiceLcrblicencetypeid);
                }
                else
                {
                    _logger.LogError($"Licence type {applicationRequest.ApplicationType} not found");
                }

                try
                {
                    incident = _dynamicsClient.Incidents.Create(incident);
                }
                catch (OdataerrorException e)
                {
                    _logger.LogError("Failed to create new account");
                    _logger.LogError(e.Message);
                    _logger.LogError("Request:");
                    _logger.LogError(e.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(e.Response.Content);
                    return;
                }

                foreach (var associate in applicationRequest.Associates)
                {
                    await CreateAssociate(clientEntityUri, accountEntityUri, incident.Incidentid, associate);
                }
            }
        }

        public async Task ImportWorkerRequests(PerformContext hangfireContext, List<IncompleteWorkerScreening> requests)
        {
            foreach (IncompleteWorkerScreening workerRequest in requests)
            {
                MicrosoftDynamicsCRMcontact contact = await CreateOrUpdateContact(
                    workerRequest.Contact.ContactId,
                    workerRequest.Contact.FirstName,
                    workerRequest.Contact.MiddleName,
                    workerRequest.Contact.LastName,
                    GetGenderCode(workerRequest.Contact.Gender),
                    workerRequest.Contact.Email,
                    workerRequest.Contact.PhoneNumber,
                    workerRequest.Contact.DriversLicenceNumber,
                    workerRequest.Contact.BCIdCardNumber,
                    workerRequest.Contact.BirthDate,
                    workerRequest.Contact.Birthplace,
                    workerRequest.Contact.SelfDisclosure == GeneralYesNo.Yes,
                    workerRequest.Contact.Address.AddressStreet1,
                    workerRequest.Contact.Address.AddressStreet2,
                    workerRequest.Contact.Address.AddressStreet3,
                    workerRequest.Contact.Address.City,
                    workerRequest.Contact.Address.Postal,
                    workerRequest.Contact.Address.StateProvince,
                    workerRequest.Contact.Address.Country,
                    workerRequest.Contact.PreviousAddresses,
                    workerRequest.Contact.Aliases,
                    null
                );

                string servicesFilter = "spice_name eq 'Cannabis Worker'";
                var service = _dynamicsClient.Serviceses.Get(filter: servicesFilter).Value[0];

                string clientFilter = "spice_name eq 'LCRB'";
                var client = _dynamicsClient.Ministries.Get(filter: clientFilter).Value[0];

                try
                {
                    MicrosoftDynamicsCRMincident incident = _dynamicsClient.Incidents.Create(new MicrosoftDynamicsCRMincident()
                    {
                        SpiceCannabisapplicanttype = (int)CannabisApplicantType.Worker,
                        SpiceApplicanttype = (int)SpiceApplicantType.Cannabis,
                        Prioritycode = (int)PriorityCode.Normal,
                        SpiceServiceIdODataBind = _dynamicsClient.GetEntityURI("spice_serviceses", service.SpiceServicesid),
                        SpiceClientIdODataBind = _dynamicsClient.GetEntityURI("spice_ministries", client.SpiceMinistryid),
                        CustomerIdODataBind = _dynamicsClient.GetEntityURI("contacts", contact.Contactid)
                    });
                }
                catch (OdataerrorException e)
                {
                    _logger.LogError("Failed to create new account");
                    _logger.LogError(e.Message);
                    _logger.LogError("Request:");
                    _logger.LogError(e.Request.Content);
                    _logger.LogError("Response:");
                    _logger.LogError(e.Response.Content);
                    return;
                }
            }
        }

        public async Task CreateAssociate(string clientEntityUri, string accountEntityUri, string screeningId, LegalEntity associateEntity)
        {
            if (associateEntity.IsIndividual)
            {
                MicrosoftDynamicsCRMcontact associate = await CreateOrUpdateContact(
                    associateEntity.Contact.ContactId,
                    associateEntity.Contact.FirstName,
                    associateEntity.Contact.MiddleName,
                    associateEntity.Contact.LastName,
                    GetGenderCode(associateEntity.Contact.Gender),
                    associateEntity.Contact.Email,
                    associateEntity.Contact.PhoneNumber,
                    associateEntity.Contact.DriversLicenceNumber,
                    associateEntity.Contact.BCIdCardNumber,
                    associateEntity.Contact.BirthDate?.UtcDateTime,
                    associateEntity.Contact.Birthplace,
                    associateEntity.Contact.SelfDisclosure == GeneralYesNo.Yes,
                    associateEntity.Contact.Address.AddressStreet1,
                    associateEntity.Contact.Address.AddressStreet2,
                    associateEntity.Contact.Address.AddressStreet3,
                    associateEntity.Contact.Address.City,
                    associateEntity.Contact.Address.Postal,
                    associateEntity.Contact.Address.StateProvince,
                    associateEntity.Contact.Address.Country,
                    new List<Address>(),
                    new List<Alias>(),
                    associateEntity.Title
                );

                try
                {
                    MicrosoftDynamicsCRMspiceAccountcaseassignment accountContact = _dynamicsClient.Accountcaseassignments.Create(new MicrosoftDynamicsCRMspiceAccountcaseassignment()
                    {
                        SpiceName = associateEntity.Contact.FirstName,
                        SpiceBusinessIdODataBind = accountEntityUri,
                        SpiceContactCaseAssignmentIdODataBind = _dynamicsClient.GetEntityURI("contacts", associate.Contactid),
                        SpicePosition = GetLegalEntityPositions(associateEntity.Positions)
                    });
                }
                catch (Exception e)
                {
                    _logger.LogError("Failed to create new account case assignment");
                    _logger.LogError(e.Message);
                    _logger.LogError(e.Data.ToString());
                    return;
                }

                string servicesFilter = "spice_name eq 'Cannabis Associate'";
                var service = _dynamicsClient.Serviceses.Get(filter: servicesFilter).Value[0];

                MicrosoftDynamicsCRMincident incident = new MicrosoftDynamicsCRMincident()
                {
                    SpiceApplicanttype = (int)SpiceApplicantType.Cannabis,
                    SpiceCannabisapplicanttype = (int)CannabisApplicantType.Associate,
                    CustomerIdODataBind = _dynamicsClient.GetEntityURI("contacts", associate.Contactid),
                    ParentCaseIdOdataBind = _dynamicsClient.GetEntityURI("incidents", screeningId),
                    SpiceServiceIdODataBind = _dynamicsClient.GetEntityURI("spice_serviceses", service.SpiceServicesid),
                    SpiceClientIdODataBind = clientEntityUri
                };
                try
                {
                    MicrosoftDynamicsCRMincident createdIncident = _dynamicsClient.Incidents.Create(incident);
                }
                catch (Exception e)
                {
                    _logger.LogError("Failed to create new incident");
                    _logger.LogError(e.Message);
                    _logger.LogError(e.Data.ToString());
                    return;
                }
            }
            else
            {
                foreach (var associate in associateEntity.Account.Associates)
                {
                    await CreateAssociate(clientEntityUri, accountEntityUri, screeningId, associate);
                }
            }
        }

        public async Task<MicrosoftDynamicsCRMcontact> CreateOrUpdateContact(
            string contactId, string firstName, string middleName, string lastName,
            int? gender, string email, string phoneNumber, string driversLicenceNumber,
            string bcIdCardNumber, DateTimeOffset? dateOfBirth, string birthPlace,
            bool selfDisclosed, string addressLine1, string addressLine2,
            string addressLine3, string city, string postalCode, string stateProvince,
            string country, List<Address> addresses, List<Alias> aliases, string title
        )
        {
            string uniqueFilter = "externaluseridentifier eq '" + contactId + "'";
            ContactsGetResponseModel contactResponse = _dynamicsClient.Contacts.Get(1, filter: uniqueFilter);
            MicrosoftDynamicsCRMcontact contact = new MicrosoftDynamicsCRMcontact()
            {
                Externaluseridentifier = contactId,
                Firstname = firstName,
                Middlename = middleName,
                Lastname = lastName,
                Gendercode = gender,
                Emailaddress1 = email,
                Telephone1 = phoneNumber,
                SpiceDriverslicencenum = driversLicenceNumber,
                SpiceBcidcardnumber = bcIdCardNumber,
                SpiceDateofbirth = dateOfBirth,
                SpiceBirthplace = birthPlace,
                SpiceSelfdisclosed = selfDisclosed,
                Address1Line1 = addressLine1,
                Address1Line2 = addressLine2,
                Address1Line3 = addressLine3,
                Address1City = city,
                Address1Postalcode = postalCode,
                Address1Stateorprovince = stateProvince,
                Address1Country = country,
                SpicePositiontitle = title

            };

            if (contactResponse.Value.Count > 0)
            {
                await _dynamicsClient.Contacts.UpdateAsync(contactResponse.Value[0].Contactid, contact);
                contact.Contactid = contactResponse.Value[0].Contactid;
            }
            else
            {
                contact = _dynamicsClient.Contacts.Create(contact);
            }

            string entityUri = _dynamicsClient.GetEntityURI("contacts", contact.Contactid);
            
            PreviousaddressesesGetResponseModel currentPreviousAddresses = _dynamicsClient.Previousaddresseses.Get(filter: $"_spice_contactid_value eq {contact.Contactid}");
            if(currentPreviousAddresses.Value.Count > 0)
            {
                foreach(var address in currentPreviousAddresses.Value)
                {
                    _dynamicsClient.Previousaddresseses.Delete(address.SpicePreviousaddressesid);
                }
            }
            foreach (var address in addresses)
            {
                _dynamicsClient.Previousaddresseses.Create(new MicrosoftDynamicsCRMspicePreviousaddresses()
                {
                    SpiceContactIdODataBind = entityUri,
                    SpiceName = address.AddressStreet1,
                    SpiceCity = address.City,
                    SpiceStateprovince = address.StateProvince,
                    SpiceZippostalcode = address.Postal,
                    SpiceCountry = address.Country,
                    SpiceStartdate = address.FromDate,
                    SpiceEnddate = address.ToDate
                });
            }

            AliasesesGetResponseModel currentAliases = _dynamicsClient.Aliaseses.Get(filter: $"_spice_aliascontact_value eq {contact.Contactid}");
            if(currentAliases.Value.Count > 0)
            {
                foreach(var alias in currentAliases.Value)
                {
                    _dynamicsClient.Aliaseses.Delete(alias.SpiceAliasesid);
                }
            }
            foreach (var alias in aliases)
            {
                _dynamicsClient.Aliaseses.Create(new MicrosoftDynamicsCRMspiceAliases()
                {
                    SpiceAliascontactODataBind = entityUri,
                    SpiceName = alias.GivenName,
                    SpiceMiddlename = alias.SecondName,
                    SpiceLastname = alias.Surname,
                });
            }

            return contact;
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
        public int? GetGenderCode(AdoxioGenderCode gender)
        {
            if (gender == AdoxioGenderCode.Male || gender == AdoxioGenderCode.Female)
            {
                return (int?)gender;
            }
            return null;
        }
    }
}

