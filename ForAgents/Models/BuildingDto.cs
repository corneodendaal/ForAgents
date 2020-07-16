using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class Building
    {
        public int ListingId { get; set; }
        public decimal SqmBuilding { get; set; }
        public decimal SqmStand { get; set; }
        //public string DateBuilt { get; set; }
        public string Facing { get; set; }
        public string DateAvailable { get; set; }
        public string Furnished { get; set; }
    }
}
