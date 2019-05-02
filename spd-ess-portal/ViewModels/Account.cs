using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.Spice.Public.ViewModels
{
    public class Account
    {
        public string id { get; set; }
        public string name { get; set; } //dynamics = name
        public string description { get; set; } //dynamics = description
        public string externalId { get; set; }
        public string bcIncorporationNumber { get; set; } //dynamics = adoxio_bcincorporationnumber
        public DateTimeOffset? dateOfIncorporationInBC { get; set; } //dynamics = adoxio_dateofincorporationinbc
        public string businessNumber { get; set; } //dynamics = accountnumber
        public string pstNumber { get; set; } //dynamics = adoxio_pstnumber
        public string contactEmail { get; set; } //dynamics = emailaddress1
        public string contactPhone { get; set; } //dynamics = telephone1

        public string mailingAddressName { get; set; } //dynamics = address1_name
        public string mailingAddressStreet { get; set; } //dynamics = address1_line1
        public string mailingAddressStreet2 { get; set; } //dynamics = address1_line2
        public string mailingAddressCity { get; set; } //dynamics = address1_city
        public string mailingAddressCountry { get; set; } //dynamics = address1_country
        public string mailingAddressProvince { get; set; } //dynamics = address1_stateorprovince
        public string mailingAddressPostalCode { get; set; } //dynamics = address1_postalcode

        public string physicalAddressName { get; set; } //dynamics = address2_name
        public string physicalAddressStreet { get; set; } //dynamics = address2_line1
        public string physicalAddressStreet2 { get; set; } //dynamics = address2_line2
        public string physicalAddressCity { get; set; } //dynamics = address2_city
        public string physicalAddressCountry { get; set; } //dynamics = address2_country
        public string physicalAddressProvince { get; set; } //dynamics = address2_stateorprovince
        public string physicalAddressPostalCode { get; set; } //dynamics = address2_postalcode

        public ViewModels.Contact primarycontact { get; set; }

        public string businessType { get; set; }

        

    }
}
