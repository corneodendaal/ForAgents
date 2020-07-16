using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class ExtraAccommodation
    {
        public int ListingId { get; set; }
        public bool ExtraToilet { get; set; }
        public bool ExtraHandWashBasin { get; set; }
        public bool ExtraShower { get; set; }
        public bool ExtraBath { get; set; }
        public bool ExtraBedroom { get; set; }
        public bool ExtraKitchen { get; set; }
        public bool ExtraLivingArea { get; set; }
        public bool ExtraGarage { get; set; }
        public bool ExtraCoveredParkingBay { get; set; }

        public bool StaffToilet { get; set; }
        public bool StaffHandWashBasin { get; set; }
        public bool StaffShower { get; set; }
        public bool StaffBath { get; set; }
        public bool StaffBedroom { get; set; }
        public bool StaffKitchen { get; set; }
        public bool StaffLivingArea { get; set; }
        public bool StaffGarage { get; set; }
        public bool StaffCoveredParkingBay { get; set; }

        public string ExtraAccommodationComments { get; set; }
    }
}
