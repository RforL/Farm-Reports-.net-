using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project
{
    class Animal
    {
        //Variables
        private string type;
        private int iD;
        private double amtWater;
        private double dailyCost;
        private double weight;
        private int age;
        private string colour;


        //Get, set and construct
        public int ID { get => iD; set => iD = value; }
        public string Type { get => type; set => type = value; }
        public double AmtWater { get => amtWater; set => amtWater = value; }
        public double DailyCost { get => dailyCost; set => dailyCost = value; }
        public double Weight { get => weight; set => weight = value; }
        public int Age { get => age; set => age = value; }
        public string Colour { get => colour; set => colour = value; }

        public Animal(int ID, double amtWater, double dailyCost, double weight, int age, string colour)
        {
            this.ID = iD;
            this.AmtWater = amtWater;
            this.DailyCost = dailyCost;
            this.Weight = weight;
            this.Age = age;
            this.Colour = colour;
            this.Type = type;
        }

        //Display Info
        public virtual string DisplayInfo()
        {
            // String to hold all animal information
            string info = "";

            // Return string
            return (info);
        }

        //Display Type
        public virtual string DisplayType()
        {
            string type_info = Type;
            return (type_info);
        }

        //Get Profit
        public virtual double GetProfit()
        {
            double profit = 0;

            double total = profit - GetExpenses();

            return (total);
        }

        //Get Expenses
        public double GetExpenses()
        {
            double dailyWater = CommodityPrices.WaterPrice / 1000 * AmtWater;

            double expenses = DailyCost + dailyWater + GetTax();

            return (expenses);
        }

        //Get Tax
        public virtual double GetTax()
        {
            // Calculate the daily cost of tax, as tax rate given per kg is set as a yearly cost 
            double dailyTax = CommodityPrices.TaxPerKG * Weight;

            return (dailyTax);
        }
    }
}