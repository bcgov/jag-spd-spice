using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SpiceCarlaSync.models;

namespace SpiceCarlaSync.models
{
    public class IncompleteWorkerScreening
    {
        public string RecordIdentifier { get; set; }
        public Contact Contact { get; set; }
    }
}
