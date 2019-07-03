using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gov.Jag.Spice.Public.Utils
{
    public static class StringEx
    {
        public static string Base64Encode(this string s)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(s);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string s)
        {
            var base64EncodedBytes = Convert.FromBase64String(s);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static int? AsInt(this string s)
        {
            if (int.TryParse(s, out int result))
            {
                return result;
            }

            return null;
        }
    }
}
