using System;
using System.Collections.Generic;
using System.Linq;
using Gov.Jag.Spice.Interfaces.Models;
using Gov.Jag.Spice.Public.ViewModels;

namespace Gov.Jag.Spice.Public.Models.Extensions
{
    public static class ScreeningTypeExtensions
    {
        public static ScreeningType ToViewModel(this MicrosoftDynamicsCRMspiceServices screeningType)
        {
            return new ScreeningType
            {
                Name = screeningType.SpiceName,
                Value = screeningType.SpiceServicesid,
                ApplicantType = screeningType.SpiceSerApplicanttype,
                CannabisApplicantType = screeningType.SpiceScreeningtype,
            };
        }
    }
}
