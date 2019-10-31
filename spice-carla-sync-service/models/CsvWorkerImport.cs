using Gov.Lclb.Cllb.Interfaces.Models;

namespace SpiceCarlaSync.models
{
    public class CsvWorkerImport
    {
        public string RecordIdentifier { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Result { get; set; }
        public string DateProcessed { get; set; }

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
