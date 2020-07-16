using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class ListingInitiate
    {
        public string AgentNumber { get; set; }
        public string DateListed { get; set; }
        public string StandNumber { get; set; }
        public string StreetAddress { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateProvinceRegion { get; set; }
        public string ZipPostalAreaCode { get; set; }
        public string ReasonForSelling { get; set; }
        public string Category { get; set; }
    }
}
