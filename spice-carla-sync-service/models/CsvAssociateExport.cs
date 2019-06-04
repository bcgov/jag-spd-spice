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
                        Lcrbworkerjobid = entity.Contact.ContactId?.Replace(",", ""),
                        Legalfirstname = entity.Contact.FirstName?.Replace(",", ""),
                        Legalsurname = entity.Contact.LastName?.Replace(",", ""),
                        Legalmiddlename = entity.Contact.MiddleName?.Replace(",", ""),
                        Contactphone = entity.Contact.PhoneNumber?.Replace(",", ""),
                        Personalemailaddress = entity.Contact.Email?.Replace(",", ""),
                        Addressline1 = entity.Contact.Address.AddressStreet1?.Replace(",", ""),
                        Addresscity = entity.Contact.Address.City?.Replace(",", ""),
                        Addressprovstate = entity.Contact.Address.StateProvince?.Replace(",", ""),
                        Addresscountry = entity.Contact.Address.Country?.Replace(",", ""),
                        Addresspostalcode = entity.Contact.Address.Postal?.Replace(",", ""),
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
                        newAssociate[$"Alias{aliasId}surname"] = alias.Surname?.Replace(",", "");
                        newAssociate[$"Alias{aliasId}middlename"] = alias.SecondName?.Replace(",", "");
                        newAssociate[$"Alias{aliasId}firstname"] = alias.GivenName?.Replace(",", "");
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
                        newAssociate[$"Previousstreetaddress{addressId}"] = address.AddressStreet1?.Replace(",", "");
                        newAssociate[$"Previouscity{addressId}"] = address.City?.Replace(",", "");
                        newAssociate[$"Previousprovstate{addressId}"] = address.StateProvince?.Replace(",", "");
                        newAssociate[$"Previouscountry{addressId}"] = address.Country?.Replace(",", "");
                        newAssociate[$"Previouspostalcode{addressId}"] = address.Postal?.Replace(",", "");
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
