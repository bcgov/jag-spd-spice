using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Gov.Jag.Spice.Public.ViewModels
{
    public class ScreeningType
    {
        public string Name { get; set; }
        public string Value { get; set; }

        [JsonConstructor]
        public ScreeningType(string name)
        {
            Name = name;
            Value = name;
        }
    }
}
