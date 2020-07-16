using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class Defects
    {
        public int ListingId { get; set; }
        public bool Cracks { get; set; }
        public bool Leaks { get; set; }
        public bool Damp { get; set; }
        public bool Electricity { get; set; }
        public bool Plumbing { get; set; }
        public bool NoneVisible { get; set; }
        public bool Other { get; set; }
        public string DefectsComments { get; set; }
    }
}
