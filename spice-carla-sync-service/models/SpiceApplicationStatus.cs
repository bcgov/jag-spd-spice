namespace SpiceCarlaSync.models
{
    public enum SpiceApplicationStatus
    {
        Cancelled = 100000000,
        Cleared = 525840010,
        FitAndProper = 525840021,
        InProgress = 525840003,
        InvestigationRequired = 525840000,
        NotCleared = 525840011,
        NotFitAndProper = 525840023,
        OTRReceived = 525840019,
        OTRRequired = 525840017,
        OTRSent = 525840018,
        ReportForReview = 525840006,
        ForLegalReview = 525840020,
        Unassigned = 525840005,
        Withdrawn = 525840012
    }

    public class SpiceApplicationStatusMapper
    {
        public static CarlaBusinessSecurityStatus MapToCarlaApplicationResult(SpiceApplicationStatus status)
        {
            switch( status )
            {
                case SpiceApplicationStatus.FitAndProper:
                    return CarlaBusinessSecurityStatus.Passed;
                case SpiceApplicationStatus.NotFitAndProper:
                    return CarlaBusinessSecurityStatus.Failed;
                case SpiceApplicationStatus.Withdrawn:
                    return CarlaBusinessSecurityStatus.Withdrawn;
            }
            return CarlaBusinessSecurityStatus.Unknown;
        }

        public static CarlaWorkerSecurityStatus MapToCarlaWorkerResult(SpiceApplicationStatus status)
        {
            switch( status )
            {
                case SpiceApplicationStatus.Cleared:
                    return CarlaWorkerSecurityStatus.Pass;
                case SpiceApplicationStatus.NotCleared:
                    return CarlaWorkerSecurityStatus.Fail;
                case SpiceApplicationStatus.Withdrawn:
                    return CarlaWorkerSecurityStatus.Withdrawn;
            }
            return CarlaWorkerSecurityStatus.Unknown;
        }
    }
}
