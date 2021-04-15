using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    class CommodityPrices
    {
        private static double goatMilkPrice;
        private static double cowMilkPrice;
        private static double sheepWoolPrice;
        private static double waterPrice;
        private static double taxPerKG;
        private static double jerseyCowTax;

        public static double GoatMilkPrice { get; set; }
        public static double CowMilkPrice { get; set; }
        public static double SheepWoolPrice { get; set; }
        public static double WaterPrice { get; set; }
        public static double TaxPerKG { get; set; }
        public static double JerseyCowTax { get; set; }
    }
}
