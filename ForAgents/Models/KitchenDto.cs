using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorneTest.Models
{
    public class Kitchen
    {
        public int ListingId { get; set; }
        public bool SeperateLaundry { get; set; }
        public bool SeperateScullery { get; set; }
        public bool WalkInPantry { get; set; }
        public bool PantryCupboard { get; set; }
        public string KitchenComments { get; set; }
        public bool Granite { get; set; }
        public bool CaeserStone { get; set; }
        public bool Melamine { get; set; }
        public bool Wood { get; set; }
        public string TypeOfWood { get; set; }
        public string FinishesComments { get; set; }
        public bool ElectricStove { get; set; }
        public bool GasStove { get; set; }
        public bool Hob { get; set; }
        public bool ExtractorFan { get; set; }
        public bool OvenUnderCounter { get; set; }
        public bool OvenEyeLevel { get; set; }
        public bool Microwave { get; set; }
        public bool CoffeeMachine { get; set; }
        public bool DoubleDoorFridge { get; set; }
        public bool TopLoader { get; set; }
        public int NumSpaces { get; set; }
        public string KitchenSpacesComments { get; set; }
    }
}
