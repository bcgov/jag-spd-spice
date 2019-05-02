﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gov.Jag.Spice.Public.Utils
{
    public static class LoggingEvents
    {
        public const int HttpGet = 1000;
        public const int HttpPost = 1001;
        public const int HttpPut = 1002;
        public const int HttpDelete = 1003;

        public const int Get = 2000;
        public const int Save = 2001;
        public const int Update = 2002;
        public const int Delete = 2003;

        public const int NotFound = 4000;

        public const int Error = 5000;
        public const int BadRequest = 5001;
    }
}
