using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class StaffTenant
    {
        public int ListingId { get; set; }
        public string StaffName { get; set; }
        public string StaffMobile { get; set; }
        public string TenantName { get; set; }
        public string TenantMobile { get; set; }
        public decimal GrossRentalIncome { get; set; }
        public string DateRentalExpires { get; set; }
        public string DateAvailable { get; set; }
    }
}
