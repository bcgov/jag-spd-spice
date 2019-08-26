using CsvHelper.Configuration;
using SpiceCarlaSync.models;

namespace SpiceCarlaSync
{
    public class CsvBusinessImportMap : ClassMap<CsvBusinessImport>
    {
        public CsvBusinessImportMap()
        {
            Map(m => m.LcrbBusinessJobId).Name("LCRB BUSINESS JOB ID");
            Map(m => m.Result).Name("RESULT");
        }
    }
}
