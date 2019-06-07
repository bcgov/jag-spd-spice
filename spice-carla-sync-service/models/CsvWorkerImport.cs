using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using SpdSync.models;

namespace SpiceCarlaSync.models
{
    public class CsvWorkerImport
    {
        public string Lcrbworkerjobid { get; set; }
        public string Legalfirstname { get; set; }
        public string Legalmiddlename { get; set; }
        public string Legalsurname { get; set; }
        public string Result { get; set; }
    }
}
