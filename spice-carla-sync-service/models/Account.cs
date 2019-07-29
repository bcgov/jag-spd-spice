using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpdSync.models
{
    public class Account
    {
        public string AccountId { get; set; }
        public string Name { get; set; }
        public string BCIncorporationNumber { get; set; }
        public string BusinessNumber { get; set; }
        public string BusinessType { get; set; }
        public List<LegalEntity> Associates { get; set; }
    }
}
