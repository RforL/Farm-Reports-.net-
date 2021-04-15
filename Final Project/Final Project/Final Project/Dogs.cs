using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    class Dogs : Animal
    {
        //Variables
        private string type;

        //Get, set and construct
        public new string Type { get { return type; } }

        public Dogs(int ID, double amtWater, double weight, int age, string colour, double dailyCost, string type) 
            : base(ID, amtWater, dailyCost, weight, age, colour)
        {
            this.AmtWater = amtWater;
            this.Weight = weight;
            this.Age = age;
            this.Colour = colour;
            this.DailyCost = dailyCost;
            this.type = type;
        }

        public override string DisplayInfo()
        {
            // String to hold all dog informations
            string info = "Amount of Water \tWeight \tAge \tColour \tDaily Cost \r\n" +
                          Convert.ToString(AmtWater) + "\t\t" +
                          Convert.ToString(Weight) + "\t" +
                          Convert.ToString(Age) + "\t" +
                          Colour + "\t" +
                          Convert.ToString(DailyCost);


            // Return string
            return (info);
        }

        public override string DisplayType()
        {
            string type_info = type;
            return (type_info);
        }

        public override double GetTax()
        {
            double totalTax = 0;
            return (totalTax);
        }
    }
}
