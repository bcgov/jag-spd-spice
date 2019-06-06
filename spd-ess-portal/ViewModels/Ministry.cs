using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Gov.Jag.Spice.Public.ViewModels
{
    public class Ministry
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public List<ProgramArea> ProgramAreas { get; set; }

        public Ministry() { }

        [JsonConstructor]
        public Ministry(string name)
        {
            Name = name;
            Value = name;
        }
    }
}
