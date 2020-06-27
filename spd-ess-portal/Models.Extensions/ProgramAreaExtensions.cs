using System;
using System.Collections.Generic;
using System.Linq;
using Gov.Jag.Spice.Interfaces.Models;
using Gov.Jag.Spice.Public.ViewModels;

namespace Gov.Jag.Spice.Public.Models.Extensions
{
    public static class ProgramAreaExtensions
    {
        public static ProgramArea ToViewModel(this MicrosoftDynamicsCRMspiceMinistry programArea)
        {
            return new ProgramArea
            {
                Name = programArea.SpiceName,
                Value = programArea.SpiceMinistryid,
                OrgCode = programArea.SpiceOrgcode,
                ScreeningTypes = programArea.SpiceSpiceMinistrySpiceServices?.Select(screeningType => screeningType.ToViewModel()).ToList() ?? new List<ScreeningType>(),
            };
        }
    }
}
