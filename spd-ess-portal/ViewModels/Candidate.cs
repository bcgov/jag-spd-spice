using System;
using System.Collections.Generic;
using System.Linq;

namespace Gov.Jag.Spice.Public.ViewModels
{
    public class Candidate
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
    }
}
