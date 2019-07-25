using System;
using CsvHelper.Configuration;
using Gov.Jag.Spice.CarlaSync.models;

namespace Gov.Jag.Spice.CarlaSync
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
