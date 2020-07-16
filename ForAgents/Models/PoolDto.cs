using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class Pool
    {
        public int ListingId { get; set; }
        public bool SaltChlorinated { get; set; }
        public bool StandardChemicals { get; set; }
        public bool Cleaner { get; set; }
        public bool Cover { get; set; }
        public bool Net { get; set; }
        public bool ElectricHeated { get; set; }
        public bool SolarHeated { get; set; }
        public bool PoolFence { get; set; }
        public bool FibreGlass { get; set; }
        public bool Marbelite { get; set; }
        public bool Gunite { get; set; }
        public string PoolComments { get; set; }
    }
}
