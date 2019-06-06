using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Gov.Jag.Spice.Public.ViewModels
{
    public class ScreeningReason
    {
        public string Name { get; set; }
        public string Value { get; set; }

        [JsonConstructor]
        public ScreeningReason(string name)
        {
            Name = name;
            Value = name;
        }
    }
}
