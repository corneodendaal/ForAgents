using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class Garage
    {
        public int ListingId { get; set; }
        public bool AutomatedGarages { get; set; }
        public int NumberOfGarages { get; set; }
        public int NumberOfGaragesAutomated { get; set; }
        public int NumberGarageMotors { get; set; }
    }
}
