using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    class Sheep : Animal
    {
        //Variables
        private string type;
        private double amtWool;

        //Get, set and construct
        public new string Type { get => type; set => type = value; }
        public double AmtWool { get => amtWool; set => amtWool = value; }

        public Sheep(int ID, double dailyCost, double amtWater, double weight, int age, string colour, double amtWool, string type) 
            : base(ID, amtWater, dailyCost, weight, age, colour)
        {
            this.AmtWater = amtWater;
            this.DailyCost = dailyCost;
            this.Weight = weight;
            this.Age = age;
            this.Colour = colour;
            this.AmtWool = amtWool;
            this.Type = type;
        }

        public override string DisplayInfo()
        {
            // String to hold all sheep information
            string info = "Amount of Water \tDaily Cost \tWeight \tAge \tColour \tAmount of Wool\r\n" +
                          Convert.ToString(AmtWater) + "\t\t" +
                          Convert.ToString(DailyCost) + "\t\t" +
                          Convert.ToString(Weight) + "\t" +
                          Convert.ToString(Age) + "\t" +
                          Colour + "\t" +
                          Convert.ToString(AmtWool);

            // Return string
            return (info);
        }

        public override string DisplayType()
        {
            string type_info = Type;
            return (type_info);
        }

        public override double GetProfit()
        {
            double profit = AmtWool * CommodityPrices.SheepWoolPrice;

            double total = profit - GetExpenses();

            return (total);
        }
    }
}
