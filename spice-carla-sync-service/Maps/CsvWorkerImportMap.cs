using System;
using CsvHelper.Configuration;
using SpiceCarlaSync.models;

namespace SpiceCarlaSync
{
    public class CsvWorkerImportMap : ClassMap<CsvWorkerImport>
    {
        public CsvWorkerImportMap()
        {
            Map(m => m.Lcrbworkerjobid).Name("Lcrbworkerjobid");
            Map(m => m.Legalfirstname).Name("Legalfirstname");
            Map(m => m.Legalsurname).Name("Legalsurname");
            Map(m => m.Legalmiddlename).Name("Legalmiddlename");
            Map(m => m.Result).Name("Result");
        }
    }
}
