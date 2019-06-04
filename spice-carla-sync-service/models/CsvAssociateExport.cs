using System;
using System.Collections.Generic;
using SpdSync.models;

namespace SpiceCarlaSync.models
{
    public class CsvAssociateExport : CsvWorkerExport
    {
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
                        LCRBBusinessJobId = jobNumber,
                        Lcrbworkerjobid = entity.Contact.ContactId,
                        Legalfirstname = entity.Contact.FirstName,
                        Legalsurname = entity.Contact.LastName,
                        Legalmiddlename = entity.Contact.MiddleName,
                        Contactphone = entity.Contact.PhoneNumber,
                        Personalemailaddress = entity.Contact.Email,
                        Addressline1 = entity.Contact.Address.AddressStreet1,
                        Addresscity = entity.Contact.Address.City,
                        Addressprovstate = entity.Contact.Address.StateProvince,
                        Addresscountry = entity.Contact.Address.Country,
                        Addresspostalcode = entity.Contact.Address.Postal,
                        Selfdisclosure = ((GeneralYesNo)entity.Contact.SelfDisclosure).ToString().Substring(0, 1),
                        Gendermf = (entity.Contact.Gender == 0) ? null : ((AdoxioGenderCode)entity.Contact.Gender).ToString(),
                        Driverslicence = entity.Contact.DriversLicenceNumber,
                        Bcidentificationcardnumber = entity.Contact.BCIdCardNumber,
                        Birthplacecity = entity.Contact.Birthplace,
                        Birthdate = $"{entity.Contact.BirthDate:yyyy-MM-dd}"
                    };

                    /* Flatten up the aliases */
                    var aliasId = 1;
                    foreach (var alias in entity.Aliases)
                    {
                        newAssociate[$"Alias{aliasId}surname"] = alias.Surname;
                        newAssociate[$"Alias{aliasId}middlename"] = alias.SecondName;
                        newAssociate[$"Alias{aliasId}firstname"] = alias.GivenName;
                        aliasId++;
                    }

                    /* Flatten up the previous addresses */
                    var addressId = 1;
                    foreach (var address in entity.PreviousAddresses)
                    {
                        newAssociate[$"Previousstreetaddress{addressId}"] = address.AddressStreet1;
                        newAssociate[$"Previouscity{addressId}"] = address.City;
                        newAssociate[$"Previousprovstate{addressId}"] = address.StateProvince;
                        newAssociate[$"Previouscountry{addressId}"] = address.Country;
                        newAssociate[$"Previouspostalcode{addressId}"] = address.Postal;
                        addressId++;
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
