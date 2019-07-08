using System;
using System.Collections.Generic;
using System.Linq;

namespace Gov.Jag.Spice.Public.Utils
{
    public class DynamicsEntityNotFoundException : Exception
    {
        public DynamicsEntityNotFoundException(string entity) : base($"No Dynamics entities of type {entity} could be found.") { }
        public DynamicsEntityNotFoundException(string entity, string filter) : base($"No Dynamics entities of type {entity} could be found using the filter {filter}.") { }
    }
}
