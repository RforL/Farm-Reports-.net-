using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    class Jersey : Cow
    {

        private string type;

        public new string Type { get => type; set => type = value; }


        public Jersey(int ID, double amtWater, double dailyCost, double weight, int age, string colour, double amtMilk, string type)
            : base(ID, amtWater, dailyCost, weight, age, colour, amtMilk, type)
        {
            this.AmtWater = amtWater;
            this.DailyCost = dailyCost;
            this.Weight = weight;
            this.Age = age;
            this.Colour = colour;
            this.AmtMilk = amtMilk;
            this.Type = Type;
        }

        //Get Tax
        public override double GetTax()
        {
            // Refer back to base GetTax() method then add to JerseyCowTax rate.
            double totalTax = base.GetTax() + CommodityPrices.JerseyCowTax;

            return (totalTax);
        }
    }
}
