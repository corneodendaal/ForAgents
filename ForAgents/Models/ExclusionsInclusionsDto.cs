using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class ExclusionsInclusions
    {
        public int ListingId { get; set; }
        public bool ExclusionPlants { get; set; }
        public bool ExclusionGardenFeatures { get; set; }
        public bool ExclusionBlinds { get; set; }
        public bool ExclusionCurtains { get; set; }
        public bool ExclusionSatelliteDish { get; set; }
        public bool ExclusionWendyHouse { get; set; }
        public bool ExclusionGardenShed { get; set; }
        public bool ExclusionSafe { get; set; }

        public bool InclusionPlants { get; set; }
        public bool InclusionGardenFeatures { get; set; }
        public bool InclusionBlinds { get; set; }
        public bool InclusionCurtains { get; set; }
        public bool InclusionSatelliteDish { get; set; }
        public bool InclusionWendyHouse { get; set; }
        public bool InclusionGardenShed { get; set; }
        public bool InclusionSafe { get; set; }

        public string ExclusionInclusionComments { get; set; }
    }
}
