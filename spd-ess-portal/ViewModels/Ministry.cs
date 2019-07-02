using System;
using System.Collections.Generic;
using System.Linq;

namespace Gov.Jag.Spice.Public.ViewModels
{
    public class Ministry
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public List<ProgramArea> ProgramAreas { get; set; }
    }
}
