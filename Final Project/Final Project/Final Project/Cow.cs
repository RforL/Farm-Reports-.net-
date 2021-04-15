using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    class Cow : Animal
    {
        //Variables
        private string type;
        private double amtMilk;

        //Get, set and construct
        public new string Type{ get => type; set => type = value; }
        public double AmtMilk { get => amtMilk; set => amtMilk = value; }

        public Cow(int ID, double amtWater, double dailyCost, double weight, int age, string colour, double amtMilk, string type) 
            : base(ID, amtWater, dailyCost, weight, age, colour)
        {
            this.AmtWater = amtWater;
            this.DailyCost = dailyCost;
            this.Weight = weight;
            this.Age = age;
            this.Colour = colour;
            this.AmtMilk = amtMilk;
            this.Type = type;

        }

        //Display Info
        public override string DisplayInfo()
        {
            // String to hold all Cow information
            string info = "Amount of Water \tDaily Cost \tWeight \tAge \tColour \tAmount of Milk \r\n" +
                          Convert.ToString(AmtWater) + "\t\t" +
                          Convert.ToString(DailyCost) + "\t\t" +
                          Convert.ToString(Weight) + "\t" +
                          Convert.ToString(Age) + "\t" +
                          Colour + "\t" +
                          Convert.ToString(AmtMilk) + "\t\t";

            // Return string
            return (info);
        }

        public override string DisplayType()
        {
            string type_info = type;
            return (type_info);
        }

        public override double GetProfit()
        {
            double profit = AmtMilk * CommodityPrices.CowMilkPrice;

            double total = profit - GetExpenses();

            return (total);
        }
    }
}
