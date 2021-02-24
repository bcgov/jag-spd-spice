using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gov.Jag.Spice.Interfaces;
using Gov.Jag.Spice.Interfaces.Models;
using Hangfire.Server;
using Hangfire.Console;
using Microsoft.Rest;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SpiceCarlaSync.models;
using Gov.Lclb.Cllb.Interfaces.Models;

namespace Gov.Jag.Spice.CarlaSync
{
    public class DynamicsUtils
    {
        private IDynamicsClient _dynamicsClient;
        private IConfiguration Configuration { get; }
        private ILogger _logger { get; }
        private ILoggerFactory _loggerFactory { get; }

        public DynamicsUtils(IConfiguration configuration, ILoggerFactory loggerFactory, IDynamicsClient dynamicsClient)
        {
            Configuration = configuration;
            _dynamicsClient = dynamicsClient;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger(typeof(DynamicsUtils));
        }

        /// <summary>
        /// Import requests to Dynamics.
        /// </summary>
        /// <returns></returns>
        public async Task ImportApplicationRequests(PerformContext hangfireContext, List<IncompleteApplicationScreening> requests)
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
                MicrosoftDynamicsCRMspiceCompanyCollection companiesResponse;
                try
                {
                    companiesResponse = _dynamicsClient.Companies.Get(1, filter: uniqueFilter);
                }
                catch (OdataerrorException e)
                {
                    _logger.LogError(e, "Failed to query companies");
                    _logger.LogError(e.Request.Content);
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
                        _logger.LogError(e, "Failed to create new company");
                        _logger.LogError(e.Request.Content);
                        _logger.LogError(e.Response.Content);
                        return;
                    }
                }
                 
                // Contact person
                uniqueFilter = "externaluseridentifier eq '" + applicationRequest.ContactPerson.ContactId + "'";
                MicrosoftDynamicsCRMcontactCollection contactResponse;
                try
                {
                     contactResponse = _dynamicsClient.Contacts.Get(1, filter: uniqueFilter);
                }
                catch (OdataerrorException e)
                {
                    _logger.LogError(e, "Failed to query contacts");
                    _logger.LogError(e.Request.Content);
                    _logger.LogError(e.Response.Content);
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
                        _logger.LogError(e, "Failed to create new contact");
                        _logger.LogError(e.Request.Content);
                        _logger.LogError(e.Response.Content);
                        return;
                    }
                }


                uniqueFilter = "spice_carla_account eq '" + applicationRequest.ApplicantAccount.AccountId + "'";
                MicrosoftDynamicsCRMaccountCollection accountResponse;
                try
                {
                    accountResponse = _dynamicsClient.Accounts.Get(1, filter: uniqueFilter);
                }
                catch (OdataerrorException e)
                {
                    _logger.LogError(e, "Failed to query accounts");
                    _logger.LogError(e.Request.Content);
                    _logger.LogError(e.Response.Content);
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
                            Name = applicationRequest.Establishment.Name,
                            Address1Line1 = applicationRequest.Establishment.Address.AddressStreet1,
                            Address1City = applicationRequest.Establishment.Address.City,
                            Address1Country = applicationRequest.Establishment.Address.Country,
                            Address1Postalcode = applicationRequest.Establishment.Address.Postal,
                            SpiceLcrbjobid = applicationRequest.RecordIdentifier,
                            SpiceParcelidnumber = applicationRequest.Establishment.ParcelId,
                            SpiceBccorpregnumber = applicationRequest.ApplicantAccount.BCIncorporationNumber,
                            SpiceCompanyIdOdataBind = _dynamicsClient.GetEntityURI("spice_companies", company.SpiceCompanyid),
                            PrimaryContactIdOdataBind = _dynamicsClient.GetEntityURI("contacts", contactPerson.Contactid)
                        });
                    }
                    catch (OdataerrorException e)
                    {
                        _logger.LogError(e, "Failed to create new account");
                        _logger.LogError(e.Request.Content);
                        _logger.LogError(e.Response.Content);
                        return;
                    }
                }

                string accountEntityUri = _dynamicsClient.GetEntityURI("accounts", account.Accountid);
                bool isMarketing = applicationRequest.ApplicationType == "Marketing";
                string servicesFilter = isMarketing ? "spice_name eq 'Marketing Business - Initial Check'" : "spice_name eq 'Cannabis Applicant (Business)'";
                MicrosoftDynamicsCRMspiceServices service;
                try
                {
                    service = _dynamicsClient.Serviceses.Get(filter: servicesFilter).Value[0];
                }
                catch (OdataerrorException e)
                {
                    _logger.LogError(e, "Failed to query services");
                    _logger.LogError(e.Request.Content);
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
                    _logger.LogError(e, "Failed to query ministries");
                    _logger.LogError(e.Request.Content);
                    _logger.LogError(e.Response.Content);
                    return;
                }
                string clientEntityUri = _dynamicsClient.GetEntityURI("spice_ministries", client.SpiceMinistryid);

                MicrosoftDynamicsCRMincident incident = new MicrosoftDynamicsCRMincident()
                {
                    SpiceCannabisapplicanttype = isMarketing ? (int)CannabisApplicantType.MarketingBusiness : (int)CannabisApplicantType.Business,
                    SpiceApplicanttype = (int)SpiceApplicantType.Cannabis,
                    Prioritycode = (int)PriorityCode.Normal,
                    CustomerIdAccountOdataBind = accountEntityUri,
                    SpiceServiceIdODataBind = _dynamicsClient.GetEntityURI("spice_serviceses", service.SpiceServicesid),
                    SpiceClientIdODataBind = clientEntityUri
                };

                MicrosoftDynamicsCRMspiceLcrblicencetypeCollection response = _dynamicsClient.Lcrblicencetypes.Get(filter: "spice_name eq '" + applicationRequest.ApplicationType + "'");
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
                    // Create the business incident
                    incident = _dynamicsClient.Incidents.Create(incident);
                }
                catch (OdataerrorException e)
                {
                    _logger.LogError(e, "Failed to create new incident");
                    _logger.LogError(e.Request.Content);
                    _logger.LogError(e.Response.Content);
                    return;
                }

                foreach (var associate in applicationRequest.Associates)
                {
                    CreateAssociate(clientEntityUri, accountEntityUri, incident.Incidentid, associate, isMarketing);
                }
            }
        }

        public async Task ImportWorkerRequests(PerformContext hangfireContext, List<IncompleteWorkerScreening> requests)
        {
            _logger.LogInformation("Started worker import into dynamics");
            foreach (IncompleteWorkerScreening workerRequest in requests)
            {
                try
                {
                    MicrosoftDynamicsCRMcontact contact = CreateOrUpdateContact(
                        workerRequest.Contact.ContactId,
                        workerRequest.Contact.FirstName,
                        workerRequest.Contact.MiddleName,
                        workerRequest.Contact.LastName,
                        GetGenderCode(workerRequest.Contact.Gender),
                        workerRequest.Contact.Email,
                        workerRequest.Contact.PhoneNumber,
                        workerRequest.Contact.DriversLicenceNumber,
                        workerRequest.Contact.DriverLicenceJurisdiction,
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

                    MicrosoftDynamicsCRMincident incident = _dynamicsClient.Incidents.Create(new MicrosoftDynamicsCRMincident()
                    {
                        SpiceCannabisapplicanttype = (int)CannabisApplicantType.Worker,
                        SpiceApplicanttype = (int)SpiceApplicantType.Cannabis,
                        Prioritycode = (int)PriorityCode.Normal,
                        SpiceServiceIdODataBind = _dynamicsClient.GetEntityURI("spice_serviceses", service.SpiceServicesid),
                        SpiceClientIdODataBind = _dynamicsClient.GetEntityURI("spice_ministries", client.SpiceMinistryid),
                        CustomerIdODataBind = _dynamicsClient.GetEntityURI("contacts", contact.Contactid),
                        SpiceConsentformReceived = true
                    });
                }
                catch (OdataerrorException e)
                {
                    _logger.LogError(e, "Failed to import worker requests");
                    _logger.LogError(e.Request.Content);
                    _logger.LogError(e.Response.Content);
                    return;
                }
            }
        }

        public void CreateAssociate(string clientEntityUri, string accountEntityUri, string screeningId, LegalEntity associateEntity, bool isMarketer)
        {
            if (associateEntity.IsIndividual)
            {
                MicrosoftDynamicsCRMcontact associate = CreateOrUpdateContact(
                    associateEntity.Contact.ContactId,
                    associateEntity.Contact.FirstName,
                    associateEntity.Contact.MiddleName,
                    associateEntity.Contact.LastName,
                    GetGenderCode(associateEntity.Contact.Gender),
                    associateEntity.Contact.Email,
                    associateEntity.Contact.PhoneNumber,
                    associateEntity.Contact.DriversLicenceNumber,
                    associateEntity.Contact.DriverLicenceJurisdiction,
                    associateEntity.Contact.BCIdCardNumber,
                    associateEntity.Contact.BirthDate?.UtcDateTime,
                    associateEntity.Contact.Birthplace,
                    associateEntity.Contact.SelfDisclosure == GeneralYesNo.Yes,
                    associateEntity.Contact.Address?.AddressStreet1,
                    associateEntity.Contact.Address?.AddressStreet2,
                    associateEntity.Contact.Address?.AddressStreet3,
                    associateEntity.Contact.Address?.City,
                    associateEntity.Contact.Address?.Postal,
                    associateEntity.Contact.Address?.StateProvince,
                    associateEntity.Contact.Address?.Country,
                    associateEntity.PreviousAddresses != null ? associateEntity.PreviousAddresses : new List<Address>(),
                    associateEntity.Aliases != null ? associateEntity.Aliases : new List<Alias>(),
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
                catch (OdataerrorException e)
                {
                    _logger.LogError(e, "Failed to create new account case assignment");
                    _logger.LogError(e.Request.Content);
                    _logger.LogError(e.Response.Content);
                    return;
                }

                string servicesFilter = isMarketer ? "spice_name eq 'Marketing Associate - Initial Check'" : "spice_name eq 'Cannabis Associate'";
                var service = _dynamicsClient.Serviceses.Get(filter: servicesFilter).Value[0];

                MicrosoftDynamicsCRMincident incident = new MicrosoftDynamicsCRMincident()
                {
                    SpiceApplicanttype = (int)SpiceApplicantType.Cannabis,
                    SpiceCannabisapplicanttype = isMarketer ? (int)CannabisApplicantType.MarketingAssociate : (int)CannabisApplicantType.Associate,
                    CustomerIdODataBind = _dynamicsClient.GetEntityURI("contacts", associate.Contactid),
                    ParentCaseIdOdataBind = _dynamicsClient.GetEntityURI("incidents", screeningId),
                    SpiceServiceIdODataBind = _dynamicsClient.GetEntityURI("spice_serviceses", service.SpiceServicesid),
                    SpiceClientIdODataBind = clientEntityUri,
                    SpiceConsentformReceived = true,
                    SpicePrimeCheckrequired = !isMarketer
                };
                try
                {
                    // Create the associate incident
                    MicrosoftDynamicsCRMincident createdIncident = _dynamicsClient.Incidents.Create(incident);
                }
                catch (OdataerrorException e)
                {
                    _logger.LogError(e, "Failed to create new incident");
                    _logger.LogError(e.Request.Content);
                    _logger.LogError(e.Response.Content);
                    return;
                }
            }
            else
            {
                foreach (var associate in associateEntity.Account.Associates)
                {
                    CreateAssociate(clientEntityUri, accountEntityUri, screeningId, associate, isMarketer);
                }
            }
        }

        public async Task ProcessBusinessResults(PerformContext hangfireContext)
        {
            string[] select = {"incidentid"};
            string businessFilter = 
                $@"spice_businessreadyforlcrb eq {(int)ReadyForLCRBStatus.ReadyForLCRB}
                 and (spice_cannabisapplicanttype eq {(int)CannabisApplicantType.Business} or spice_cannabisapplicanttype eq {(int)CannabisApplicantType.MarketingBusiness})
                 and spice_applicanttype eq {(int)SpiceApplicantType.Cannabis}
                 and statecode eq 1 and statuscode eq 5";
            MicrosoftDynamicsCRMincidentCollection resp = _dynamicsClient.Incidents.Get(filter: businessFilter, select: select);
            if(resp.Value.Count == 0)
            {
                hangfireContext.WriteLine("No completed business screenings found.");
                _logger.LogInformation("No completed business screenings found.");
                return;
            }
            CarlaUtils carlaUtils = new CarlaUtils(Configuration, _loggerFactory, null);
            hangfireContext.WriteLine($"Found {resp.Value.Count} resolved business screenings.");
            _logger.LogInformation($"Found {resp.Value.Count} resolved business screenings.");
            foreach(MicrosoftDynamicsCRMincident incident in resp.Value)
            {
                CompletedApplicationScreening screening = GenerateCompletedBusinessScreening(incident.Incidentid);
                hangfireContext.WriteLine($"Sending business screening [{screening.RecordIdentifier}] to Carla.");
                _logger.LogError($"Sending business screening [{screening.RecordIdentifier}] to Carla.");
                ToggleResolution(incident.Incidentid, false);
                bool statusSet = SetLCRBStatus(incident.Incidentid, (int)ReadyForLCRBStatus.SentToLCRB, isBusiness: true);
                if (statusSet)
                {
                    try
                    {
                        bool applicationSendSuccessStatus = await carlaUtils.SendApplicationScreeningResult(new List<CompletedApplicationScreening>() { screening });
                        if (applicationSendSuccessStatus)
                        {
                            statusSet = SetLCRBStatus(incident.Incidentid, (int)ReadyForLCRBStatus.ReceivedByLCRB, isBusiness: true);
                            ToggleResolution(incident.Incidentid, true);
                            hangfireContext.WriteLine($"Successfully sent completed application screening request [LCRB Job Id: {screening.RecordIdentifier}] to Carla.");
                            _logger.LogError($"Successfully sent completed application screening request [LCRB Job Id: {screening.RecordIdentifier}] to Carla.");
                        }
                        else
                        {
                            this.HandleSendToLCRBFail(incident.Incidentid, screening.RecordIdentifier);
                        }
                    }
                    catch (Exception e)
                    {
                        this.HandleSendToLCRBFail(incident.Incidentid, screening.RecordIdentifier);
                    }
                }
                else
                {
                    this.HandleSendToLCRBFail(incident.Incidentid, screening.RecordIdentifier);
                }
            }
        }

        public async Task ProcessWorkerResults(PerformContext hangfireContext)
        {
            string[] select = {"incidentid"};
            string workerFilter = $"spice_workersreadyforlcrb eq {(int)ReadyForLCRBStatus.ReadyForLCRB} and spice_cannabisapplicanttype eq {(int)CannabisApplicantType.Worker} and statecode eq 1 and statuscode eq 5";
            MicrosoftDynamicsCRMincidentCollection resp = _dynamicsClient.Incidents.Get(filter: workerFilter, select: select);
            if(resp.Value.Count == 0)
            {
                hangfireContext.WriteLine("No completed worker screenings found.");
                _logger.LogInformation("No completed worker screenings found.");
                return;
            }
            CarlaUtils carlaUtils = new CarlaUtils(Configuration, _loggerFactory, null);
            hangfireContext.WriteLine($"Found {resp.Value.Count} resolved worker screenings.");
            _logger.LogInformation($"Found {resp.Value.Count} resolved worker screenings.");
            foreach(MicrosoftDynamicsCRMincident incident in resp.Value)
            {
                CompletedWorkerScreening screening = GenerateCompletedWorkerScreening(incident.Incidentid);
                hangfireContext.WriteLine($"Sending worker screening [{screening.RecordIdentifier}] to Carla.");
                _logger.LogError($"Sending worker screening [{screening.RecordIdentifier}] to Carla.");
                ToggleResolution(incident.Incidentid, false);
                bool statusSet = SetLCRBStatus(incident.Incidentid, (int)ReadyForLCRBStatus.SentToLCRB, isBusiness: false);
                if (statusSet)
                {
                    try
                    {
                        bool workerResultSendStatus = await carlaUtils.SendWorkerScreeningResult(new List<CompletedWorkerScreening>() { screening });
                        if (workerResultSendStatus) {
                            statusSet = SetLCRBStatus(incident.Incidentid, (int)ReadyForLCRBStatus.ReceivedByLCRB, isBusiness: false);
                            ToggleResolution(incident.Incidentid, true);
                            hangfireContext.WriteLine($"Successfully sent completed worker screening request [LCRB Job Id: {screening.RecordIdentifier}] to Carla.");
                            _logger.LogError($"Successfully sent completed worker screening request [LCRB Job Id: {screening.RecordIdentifier}] to Carla.");
                        }
                        else
                        {
                            this.HandleSendToLCRBFail(incident.Incidentid, screening.RecordIdentifier);
                        }

                    }
                    catch (HttpOperationException httpOperationException)
                    {
                        this.HandleSendToLCRBFail(incident.Incidentid, screening.RecordIdentifier);
                    }
                }
                else
                {
                    this.HandleSendToLCRBFail(incident.Incidentid, screening.RecordIdentifier);
                }
            }
        }


        public MicrosoftDynamicsCRMcontact CreateOrUpdateContact(
            string contactId, string firstName, string middleName, string lastName,
            int? gender, string email, string phoneNumber, string driversLicenceNumber,
            string driversLicenceJurisdiction, string bcIdCardNumber,
            DateTimeOffset? dateOfBirth, string birthPlace, bool selfDisclosed,
            string addressLine1, string addressLine2, string addressLine3,
            string city, string postalCode, string stateProvince, string country,
            List<Address> addresses, List<Alias> aliases, string title
        )
        {
            try
            {
                string uniqueFilter = "externaluseridentifier eq '" + contactId + "'";
                MicrosoftDynamicsCRMcontactCollection contactResponse = _dynamicsClient.Contacts.Get(1, filter: uniqueFilter);
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
                    SpiceIdJurisdiction = driversLicenceJurisdiction,
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
                    _dynamicsClient.Contacts.Update(contactResponse.Value[0].Contactid, contact);
                    contact.Contactid = contactResponse.Value[0].Contactid;
                }
                else
                {
                    contact = _dynamicsClient.Contacts.Create(contact);
                }

                string entityUri = _dynamicsClient.GetEntityURI("contacts", contact.Contactid);

                MicrosoftDynamicsCRMspicePreviousaddressesCollection currentPreviousAddresses = _dynamicsClient.Previousaddresseses.Get(filter: $"_spice_contactid_value eq {contact.Contactid}");
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

                MicrosoftDynamicsCRMspiceAliasesCollection currentAliases = _dynamicsClient.Aliaseses.Get(filter: $"_spice_aliascontact_value eq {contact.Contactid}");
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
            catch (OdataerrorException e)
            {
                _logger.LogError(e, "Failed to create or update contact");
                _logger.LogError(e.Request.Content);
                _logger.LogError(e.Response.Content);
                throw;
            }
        }

        public bool SetLCRBStatus(string incidentId, int status, bool isBusiness)
        {
            MicrosoftDynamicsCRMincident incident = new MicrosoftDynamicsCRMincident();
            if (isBusiness)
            {
                incident.SpiceBusinessreadyforlcrb = status;
            }
            else
            {
                incident.SpiceWorkersreadyforlcrb = status;
            }
            try
            {
                _dynamicsClient.Incidents.Update(incidentId, incident);
                return true;
            }
            catch (OdataerrorException e)
            {
                _logger.LogError(e, "Failed to update screening with new ready for LCRB status");
                _logger.LogError(e.Request.Content);
                _logger.LogError(e.Response.Content);
                return false;
            }
        }

        public CompletedApplicationScreening GenerateCompletedBusinessScreening(string incidentId)
        {
            string[] expand = {"customerid_account"};
            string[] select = {"customerid_account", "incidentid", "spice_applicationstatus"};
            MicrosoftDynamicsCRMincident incident = _dynamicsClient.Incidents.GetByKey(incidentId, expand: expand, select: select);
            
            SpiceApplicationStatus application = (SpiceApplicationStatus)incident.SpiceApplicationstatus;


            CompletedApplicationScreening screening = new CompletedApplicationScreening()
            {
                RecordIdentifier = incident.CustomeridAccount.SpiceLcrbjobid,
                Result = SpiceApplicationStatusMapper.MapToCarlaApplicationResult((SpiceApplicationStatus)incident.SpiceApplicationstatus).ToString(),
                Associates = new List<Associate>()
            };

            string filter = $"_parentcaseid_value eq {incident.Incidentid}";
            string[] associateExpand = {"customerid_contact"};
            string[] associateSelect = {"customerid_contact", "incidentid"};
            MicrosoftDynamicsCRMincidentCollection resp = _dynamicsClient.Incidents.Get(filter: filter, expand: associateExpand, select: associateSelect);

            foreach(var associate in resp.Value)
            {
                screening.Associates.Add(new Associate()
                {
                    SpdJobId = associate.CustomeridContact.Contactid,
                    LastName = associate.CustomeridContact.Lastname,
                    FirstName = associate.CustomeridContact.Firstname,
                    MiddleName = associate.CustomeridContact.Middlename
                });
            }
            
            return screening;
        }

        public CompletedWorkerScreening GenerateCompletedWorkerScreening(string incidentId)
        {
            string[] expand = {"customerid_contact"};
            string[] select = {"customerid_contact", "incidentid", "spice_applicationstatus"};
            MicrosoftDynamicsCRMincident incident = _dynamicsClient.Incidents.GetByKey(incidentId, expand: expand, select: select);

            CompletedWorkerScreening screening = new CompletedWorkerScreening()
            {
                RecordIdentifier = incident.CustomeridContact.Externaluseridentifier,
                ScreeningResult = SpiceApplicationStatusMapper.MapToCarlaWorkerResult((SpiceApplicationStatus)incident.SpiceApplicationstatus).ToString(),
                Worker = new Lclb.Cllb.Interfaces.Models.Worker()
                {
                    FirstName = incident.CustomeridContact.Firstname,
                    MiddleName = incident.CustomeridContact.Middlename,
                    LastName = incident.CustomeridContact.Lastname
                }
            };
            
            return screening;
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

        public int? GetGenderCode(AdoxioGenderCode? gender)
        {
            if (gender != null && (gender == AdoxioGenderCode.Male || gender == AdoxioGenderCode.Female))
            {
                return (int?)gender;
            }
            return null;
        }

        public void ToggleResolution(string incidentId, bool resolve)
        {
            MicrosoftDynamicsCRMincident incident = new MicrosoftDynamicsCRMincident();
            if(resolve)
            {
                MicrosoftDynamicsCRMCloseIncidentresolution resolution = new MicrosoftDynamicsCRMCloseIncidentresolution()
                {
                    //Statuscode = 5,
                    Incidentidodatabind = _dynamicsClient.GetEntityURI("incidents", incidentId),
                    //Statecode = 1,
                    Subject = "Sent to LCRB"
                };
                try
                {
                    _dynamicsClient.Incidents.CloseIncident(new MicrosoftDynamicsCRMCloseIncidentParameters(resolution, 5));
                }
                catch (HttpOperationException ex)
                {
                    _logger.LogError(ex, "Failed to close incident");
                    _logger.LogError(ex.Request.Content);
                    _logger.LogError(ex.Response.Content);
                }
            }
            else
            {
                incident.Statuscode = 1;
                incident.Statecode = 0;
                _dynamicsClient.Incidents.Update(incidentId, incident);
            }
        }

        public void HandleSendToLCRBFail(string incidentId, string recordIdentifier)
        {
            ToggleResolution(incidentId, true);
            _logger.LogError($"Failed to send screening request [LCRB Job Id: {recordIdentifier}] to Carla.");
        }
    }
}

