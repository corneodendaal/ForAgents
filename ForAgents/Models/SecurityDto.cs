using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class Security
    {
        public int ListingId { get; set; }
        public bool Alarm { get; set; }
        public bool BurglarProofing { get; set; }
        public bool SecurityGates { get; set; }
        public bool PassiveBeams { get; set; }
        public bool Intercom { get; set; }
        public string SecurityComments { get; set; }
    }
}
