using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class PropertySold
    {
        public int ListingId { get; set; }
        public string DateSold { get; set; }
        public decimal PriceSold { get; set; }
        public string SoldComments { get; set; }
    }
}
