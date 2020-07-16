using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class Garden
    {
        public int ListingId { get; set; }
        public bool Irrigation { get; set; }
        public bool Computerized { get; set; }
        public bool Borehole { get; set; }
        public bool JojoTanks { get; set; }
        public bool WaterFeature { get; set; }
    }
}
