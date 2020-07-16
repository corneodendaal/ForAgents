using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class Entertainment
    {
        public int ListingId { get; set; }
        public bool CoveredPatio { get; set; }
        public bool EnclosedPatio { get; set; }
        public bool Boma { get; set; }
        public bool GasBarbeque { get; set; }
        public bool WoodBarbeque { get; set; }
        public bool Bar { get; set; }
        public bool WineCellar { get; set; }
        public bool Jacuzzi { get; set; }
        public bool Sauna { get; set; }
        public bool Gym { get; set; }
        public bool Pool { get; set; }
        public string EntertainmentComments { get; set; }
    }
}
