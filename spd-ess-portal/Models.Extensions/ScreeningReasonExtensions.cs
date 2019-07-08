using System;
using System.Collections.Generic;
using System.Linq;
using Gov.Jag.Spice.Interfaces.Models;
using Gov.Jag.Spice.Public.ViewModels;

namespace Gov.Jag.Spice.Public.Models.Extensions
{
    public static class ScreeningReasonExtensions
    {
        public static ScreeningReason ToViewModel(this MicrosoftDynamicsCRMspiceReasonforscreening screeningReason)
        {
            return new ScreeningReason
            {
                Name = screeningReason.SpiceName,
                Value = screeningReason.SpiceReasonforscreeningid,
            };
        }
    }
}
