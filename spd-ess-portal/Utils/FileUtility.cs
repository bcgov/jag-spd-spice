using System;
using System.Collections.Generic;
using System.Linq;

namespace Gov.Jag.Spice.Public.Utils
{
    public static class FileUtility
    {
        private const string NameDocumentTypeSeparator = "__";

        public static string CombineNameDocumentType (string name, string documentType)
        {
            // 2018-7-13:  GW changed order of document type and name to fix problems downloading files.
            string result = documentType + NameDocumentTypeSeparator + name;
            return result;
        }

    }
}
