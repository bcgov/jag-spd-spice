using System;
using System.Collections.Generic;
using SpdSync.models;

namespace SpiceCarlaSync.models
{
    public class CsvAssociateExport : CsvWorkerExport
    {
        const int MAX_ASSOCIATE_ALIAS = 5; // maximum number of aliases to send in the CSV export.
        const int MAX_ASSOCIATE_PREVIOUS_ADDRESS = 10;

        public string LCRBBusinessJobId { get; set; }

        public static List<CsvAssociateExport> CreateListFromRequest(ApplicationScreeningRequest request)
        {
            List<CsvAssociateExport> export = new List<CsvAssociateExport>();
            export.AddRange(CreateFromAssociatesList(request.RecordIdentifier, request.Associates));
            return export;
        }

        public static List<CsvAssociateExport> CreateFromAssociatesList(string jobNumber, List<LegalEntity> associates)
        {
            List<CsvAssociateExport> export = new List<CsvAssociateExport>();
            foreach (var entity in associates)
            {
                if (entity.IsIndividual)
                {
                    var newAssociate = new CsvAssociateExport()
                    {
                        LCRBBusinessJobId = jobNumber?.Replace(",", ""),
                        Lcrbworkerjobid = entity.Contact.SpdJobId?.Replace(",", ""),
                        Legalfirstname = entity.Contact.FirstName?.Replace(",", ""),
                        Legalsurname = entity.Contact.LastName?.Replace(",", ""),
                        Legalmiddlename = entity.Contact.MiddleName?.Replace(",", ""),
                        Contactphone = entity.Contact.PhoneNumber?.Replace(",", ""),
                        Personalemailaddress = entity.Contact.Email?.Replace(",", ""),
                        Addressline1 = entity.Contact.Address != null && !string.IsNullOrEmpty(entity.Contact.Address.AddressStreet1) ? entity.Contact.Address.AddressStreet1.Replace(",", "") : "N/A",
                        Addresscity = entity.Contact.Address != null && !string.IsNullOrEmpty(entity.Contact.Address.City) ? entity.Contact.Address.City.Replace(",", "") : "N/A",
                        Addressprovstate = entity.Contact.Address != null && !string.IsNullOrEmpty(entity.Contact.Address.StateProvince) ? entity.Contact.Address.StateProvince.Replace(",", "") : "N/A",
                        Addresscountry = entity.Contact.Address != null && !string.IsNullOrEmpty(entity.Contact.Address.Country) ? entity.Contact.Address.Country.Replace(",", "") : "N/A",
                        Addresspostalcode = entity.Contact.Address != null && !string.IsNullOrEmpty(entity.Contact.Address.Postal) ? entity.Contact.Address.Postal.Replace(",", "") : "N/A",
                        Selfdisclosure = ((GeneralYesNo)entity.Contact.SelfDisclosure).ToString().Substring(0, 1),
                        Gendermf = (entity.Contact.Gender == 0) ? null : ((AdoxioGenderCode)entity.Contact.Gender).ToString().Substring(0, 1),
                        Driverslicence = entity.Contact.DriversLicenceNumber?.Replace(",", ""),
                        Bcidentificationcardnumber = entity.Contact.BCIdCardNumber?.Replace(",", ""),
                        Birthplacecity = entity.Contact.Birthplace?.Replace(",", ""),
                        Birthdate = $"{entity.Contact.BirthDate:yyyy-MM-dd}"
                    };

                    /* Flatten up the aliases */
                    var aliasId = 1;
                    foreach (var alias in entity.Aliases)
                    {
                        if (alias.Surname != null)
                        {
                            newAssociate[$"Alias{aliasId}surname"] = alias.Surname.Replace(",", "");
                        }
                        else
                        {
                            newAssociate[$"Alias{aliasId}surname"] = entity.Contact.LastName.Replace(",", "");
                        }
                        if (alias.GivenName != null)
                        {
                            newAssociate[$"Alias{aliasId}firstname"] = alias.GivenName.Replace(",", "");
                        }
                        else
                        {
                            newAssociate[$"Alias{aliasId}firstname"] = entity.Contact.FirstName.Replace(",", "");
                        }

                        newAssociate[$"Alias{aliasId}middlename"] = alias.SecondName?.Replace(",", "");
                        aliasId++;
                        if (aliasId > MAX_ASSOCIATE_ALIAS)
                        {
                            break;
                        }
                    }

                    /* Flatten up the previous addresses */
                    var addressId = 1;
                    foreach (var address in entity.PreviousAddresses)
                    {
                        string addressIdString = addressId.ToString();
                        if (addressId == 10)
                        {
                            addressIdString = "x";
                        }
                        newAssociate[$"Previousstreetaddress{addressId}"] = !string.IsNullOrEmpty(address.AddressStreet1) ? address.AddressStreet1.Replace(",", "") : "N/A";
                        newAssociate[$"Previouscity{addressId}"] = !string.IsNullOrEmpty(address.City) ? address.City.Replace(",", "") : "N/A";
                        newAssociate[$"Previousprovstate{addressId}"] = !string.IsNullOrEmpty(address.StateProvince) ? address.StateProvince.Replace(",", "") : "N/A";
                        newAssociate[$"Previouscountry{addressId}"] = !string.IsNullOrEmpty(address.Country) ? address.Country.Replace(",", "") : "N/A";
                        newAssociate[$"Previouspostalcode{addressId}"] = !string.IsNullOrEmpty(address.Postal) ? address.Postal.Replace(",", "") : "N/A";
                        addressId++;

                        if(addressId > MAX_ASSOCIATE_PREVIOUS_ADDRESS)
                        {
                            break;
                        }
                    }
                    export.Add(newAssociate);
                }
                else
                {
                    export.AddRange(CreateFromAssociatesList(jobNumber, entity.Account.Associates));
                }
            }
            return export;
        }
    }
}
