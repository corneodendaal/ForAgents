using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class Window
    {
        public int ListingId { get; set; }
        public bool Steel { get; set; }
        public bool Wood { get; set; }
        public bool Aluminium { get; set; }

        // what about DoubleGlazed (2 pieces of glass with space in between)
    }
}
