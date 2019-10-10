using System;
using CsvHelper.Configuration;
using SpiceCarlaSync.models;

namespace SpiceCarlaSync
{
    public class CsvWorkerImportMap : ClassMap<CsvWorkerImport>
    {
        public CsvWorkerImportMap()
        {
            Map(m => m.RecordIdentifier).Name("RecordIdentifier");
            Map(m => m.FirstName).Name("Name");
            Map(m => m.LastName).Name("Surname");
            Map(m => m.MiddleName).Name("GivenName");
            Map(m => m.Result).Name("Result");
            Map(m => m.DateProcessed).Name("DateProcessed");
        }
    }
}
