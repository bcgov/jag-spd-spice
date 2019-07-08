using System;
using CsvHelper.Configuration;
using Gov.Jag.Spice.CarlaSync.models;

namespace Gov.Jag.Spice.CarlaSync
{
    public class CsvAssociateImportMap : ClassMap<CsvAssociateImport>
    {
        public CsvAssociateImportMap()
        {
            Map(m => m.LcrbBusinessJobId).Name("LCRB BUSINESS JOB ID");
            Map(m => m.LcrbAssociateJobId).Name("LCRB ASSOCIATE JOB ID");
            Map(m => m.Last).Name("LAST");
            Map(m => m.First).Name("FIRST");
            Map(m => m.Middle).Name("MIDDLE");
        }
    }
}
