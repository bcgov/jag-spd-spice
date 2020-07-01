using System;
using System.Collections.Generic;
using System.Linq;
using Gov.Jag.Spice.Interfaces.Models;
using Gov.Jag.Spice.Public.ViewModels;

namespace Gov.Jag.Spice.Public.Models.Extensions
{
    public static class MinistryExtensions
    {
        public static Ministry ToViewModel(this MicrosoftDynamicsCRMspiceGovministry ministry)
        {
            return new Ministry
            {
                Name = ministry.SpiceName,
                Value = ministry.SpiceGovministryid,
                ProgramAreas = ministry.SpiceGovministrySpiceMinistry?.Select(programArea => programArea.ToViewModel()).ToList() ?? new List<ProgramArea>(),
            };
        }
    }
}
