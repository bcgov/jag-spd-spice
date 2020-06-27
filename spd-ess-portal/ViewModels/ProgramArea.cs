using System;
using System.Collections.Generic;
using System.Linq;

namespace Gov.Jag.Spice.Public.ViewModels
{
    public class ProgramArea
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string OrgCode { get; set; }
        public List<ScreeningType> ScreeningTypes { get; set; }
    }
}
