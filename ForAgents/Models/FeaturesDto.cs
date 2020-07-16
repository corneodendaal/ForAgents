using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class Features
    {
        public int ListingId{get;set;}
        public bool Office { get; set; }
        public bool OpenPlan { get; set; }
        public bool EntranceHall { get; set; }
        public bool CoveredBalcony { get; set; }
        public bool OpenBalcony { get; set; }
        public bool Loft { get; set; }
        public bool HomeAutomation { get; set; }
        public bool View { get; set; }
        public bool SolarHeating { get; set; }
        public bool RoofInsulation { get; set; }
        public bool FireHearthGas { get; set; }
        public bool FireHearthWood { get; set; }
        public bool Safe { get; set; }
        public bool StrongRoom { get; set; }
        public bool StoreRoom { get; set; }
        public bool PrepaidElectricity { get; set; }
        public bool Generator { get; set; }
        public bool WiredForGenerator { get; set; }
        public bool ADSL { get; set; }
        public bool Fibre { get; set; }
        public bool CentralVacuum { get; set; }
        public bool HomeCinema { get; set; }
        public bool SatelliteDish { get; set; }
        public bool SolarGeyser { get; set; }
        public string OtherInformation { get; set; }
        public int NumberOfGeysers { get; set; }
    }
}
