using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForAgents.Models
{
    public class History
    {
        public int ListingId { get; set; }
        public string DateSold { get; set; }
        public decimal PriceSold { get; set; }
        public string SoldComments { get; set; }
        public string DateRented { get; set; }
        public decimal RentalGrossAmount { get; set; }
        public string RentalComments { get; set; }
        public string WidthdrawnComments { get; set; }
    }
}
