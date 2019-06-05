using System;
using System.Collections.Generic;
using System.Linq;

namespace Gov.Jag.Spice.Public.ViewModels
{
    public class ScreeningRequest
    {
        public string ClientMinistry { get; set; }
        public string ProgramArea { get; set; }
        public string ScreeningType { get; set; }
        public string Reason { get; set; }
        public string OtherReason { get; set; }

        public string CandidateFirstName { get; set; }
        public string CandidateMiddleName { get; set; }
        public string CandidateLastName { get; set; }
        public DateTime CandidateDateOfBirth { get; set; }
        public string CandidateEmail { get; set; }
        public string CandidatePosition { get; set; }

        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
    }
}
