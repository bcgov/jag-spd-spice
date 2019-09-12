using System;
using Gov.Lclb.Cllb.Interfaces.Models;

namespace SpiceCarlaSync.models
{
    public class CsvBusinessImport
    {
        public string LcrbBusinessJobId { get; set; }
        public string Result { get; set; }
        
        public static SpiceApplicationStatus? TranslateStatus(string result)
        {
            switch(result)
            {
                case "PASS":
                    return SpiceApplicationStatus.Cleared;
                case "FAIL":
                    return SpiceApplicationStatus.NotCleared;
                case "WITHDRAWN":
                    return SpiceApplicationStatus.Withdrawn;
                default:
                    return null;
            }
        }
    }
}
