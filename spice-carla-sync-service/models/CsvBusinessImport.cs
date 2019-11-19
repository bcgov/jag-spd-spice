namespace SpiceCarlaSync.models
{
    public class CsvBusinessImport
    {
        public string LcrbBusinessJobId { get; set; }
        public string Result { get; set; }
        
        public static string TranslateStatus(string result)
        {
            switch(result)
            {
                case "PASS":
                    return CarlaBusinessSecurityStatus.Passed.ToString();
                case "FAIL":
                    return CarlaBusinessSecurityStatus.Failed.ToString();
                case "WITHDRAWN":
                    return CarlaBusinessSecurityStatus.Withdrawn.ToString();
                default:
                    return null;
            }
        }
    }
}
