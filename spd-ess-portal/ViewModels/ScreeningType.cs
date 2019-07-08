using System;
using System.Collections.Generic;
using System.Linq;

namespace Gov.Jag.Spice.Public.ViewModels
{
    public class ScreeningType
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public int? ApplicantType { get; set; }
        public int? CannabisApplicantType { get; set; }
    }
}
