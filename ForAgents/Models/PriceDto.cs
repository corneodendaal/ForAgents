using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class Price
    {
        public int ListingId { get; set; }
        public decimal MarketPrice { get; set; }
        public decimal NettPrice { get; set; }
        public decimal AgentFee { get; set; }
        public decimal Levies { get; set; }
        public decimal Rates { get; set; }
        public decimal WaterElectricity { get; set; }
    }
}
