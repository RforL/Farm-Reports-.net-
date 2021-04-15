using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Final_Project
{
    public partial class Information : Form
    {
        public Information()
        {
            InitializeComponent();
        }

        //#######################################################################################################################################################/////////Define Variables///////
        /// Connection Variables
        static string path = "";
        string conn_string = "";
        static Dictionary<int, Animal> allAnimals = new Dictionary<int, Animal>();
        private void Form1_Load(object sender, EventArgs e)
        {
            connectToolStripMenuItem.Enabled = false;
            disconnectToolStripMenuItem.Enabled = false;
            createDictionaryToolStripMenuItem.Enabled = false;
            searchToolStripMenuItem.Enabled = false;
            reportToolStripMenuItem.Enabled = false;
            exportToolStripMenuItem.Enabled = false;

            //Automatic Database connection
            if(conn_string != null)
            {
                connectionToolStripMenuItem.Enabled = true;
            }
        }

        //Check dictionary
        private void CheckDictionary()
        {
            if (allAnimals == null)
            {
                searchToolStripMenuItem.Enabled = false;
                reportToolStripMenuItem.Enabled = false;
                exportToolStripMenuItem.Enabled = false;
            }
            else
            {
                searchToolStripMenuItem.Enabled = true;
                reportToolStripMenuItem.Enabled = true;
                exportToolStripMenuItem.Enabled = true;
                descriptionLabel.Text = "Dictionary avaliable";
            }
        }

        /// Report variables
        double totalMilk;

        /// Dictionary variables
        Animal tempAnimal;
        OleDbConnection conn = null;

        //#######################################################################################################################################################//
        ///////Menu buttons///////
        ///Connection Buttons
        private void browseFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Database Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "accdb",
                Filter = "accdb files (*.accdb)|*.accdb",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                conn_string = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + path + @"';Persist Security info=False";
                connectToolStripMenuItem.Enabled = true;
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new OleDbConnection(conn_string);
                conn.Open();

                disconnectToolStripMenuItem.Enabled = true;
                connectToolStripMenuItem.Enabled = false;
                createDictionaryToolStripMenuItem.Enabled = true;

                searchBox.Text = "Connected";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void disconnectToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                conn.Close();

                searchBox.Text = "Disconnected";

                disconnectToolStripMenuItem.Enabled = false;
                connectToolStripMenuItem.Enabled = true;
                createDictionaryToolStripMenuItem.Enabled = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void createDictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreateAnimalDictionary();
                disconnectToolStripMenuItem.PerformClick();
                CheckDictionary();
            }

            catch
            {
                MessageBox.Show("Could not create dictionary");
            }
        }

        ///File buttons
        //Search buttons
        private void ByIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Search_By_ID s = new Search_By_ID();
            if (s.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SearchByID(s.SearchID);
                } catch
                {
                    titleLabel.Text = "ID:";
                    descriptionLabel.Text = "No animal was found";
                    itemLabel.Text = null;
                    logoPanel.Image = null;
                    resultsBox.Text = null;
                }
            }
            searchBox.Text = s.SearchID.ToString();

        }

        private void byYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Search_By_Year y = new Search_By_Year();
            if (y.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SearchByYear(y.SearchYear);
                } 
                catch
                {
                    ErrorHandling();
                }
            }
        }

        //Report Buttons
        private void r1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TotalProfit();
            } catch
            {
                ErrorHandling();
            }
        }

        private void r2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TotalTax();
            }
            catch
            {
                ErrorHandling();
            }
        }

        private void R3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MilkPerDay();
            }
            catch
            {
                ErrorHandling();
            }
        }

        private void r4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AverageAge();
            } catch
            {
                ErrorHandling();
            }
        }

        private void R5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AverageProfitVS();
            }
            catch
            {
                ErrorHandling();
            }
        }

        private void r6ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RatioOfDogsVsAllCosts();
            }
            catch
            {
                ErrorHandling();
            }
        }

        private void r7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                RatioOfRedAnimals();
            } catch
            {
                ErrorHandling();
            }
        }

        private void r8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                JerseyCowTax();
            } catch
            {
                ErrorHandling();
            }
        }

        private void R9ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TotalProfitForJersey();
            }
            catch
            {
                ErrorHandling();
            }
        }
        //Export buttons
        private void e1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ExportToTxt();
            } catch
            {
                ErrorHandling();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //#######################################################################################################################################################//
        ///////Create Dictionary///////
        private void CreateAnimalDictionary()
        {
            string[] tableNames = { "cows", "dogs", "goats", "sheep"};

            //We should never need to run this code but just in case
            if (conn != null && conn.State == ConnectionState.Closed)
            {
                //If there's no established connection run this
                MessageBox.Show("Please connect to the database");
            }

            else
            {
                for (int i = 0; i < tableNames.Length; i++)
                {
                    try
                    {
                        AddData(tableNames[i]);
                    }
                    catch
                    {
                        MessageBox.Show("No data found for table " + tableNames[i]);
                    }
                }

                try { AddPrices(); } catch { MessageBox.Show("No prices found"); }
            }
        }

        //Add the data to the dictionary
        private void AddData(string tableName)
        {
            string query = "SELECT * FROM " + tableName;
            OleDbCommand cmd = new OleDbCommand(query, conn);

            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                //Read all data from the database
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int columns = reader.FieldCount;
                        // Holds data in an object
                        object[] rowInfo = new object[columns];
                        // Populate the object with data for each animal
                        reader.GetValues(rowInfo);
                        // Array that holds the data as strings
                        string[] data = new string[columns];

                        // Loop that populates the animal array
                        for (int i = 0; i < columns; i++)
                        {
                            data[i] = rowInfo[i].ToString();
                        }

                        // Determines which animal belongs to which sub animal
                        if (tableName.ToLower().Equals("dogs"))
                        {
                            AddDog(data);
                        }
                        else if (tableName.ToLower().Equals("sheep"))
                        {
                            AddSheep(data);
                        }
                        else if (tableName.ToLower().Equals("goats"))
                        {
                            AddGoat(data);
                        }
                        else
                        {
                            AddCow(data);
                        } 
                    }
                }
            }
        }

        private void AddDog(string[] animal)
        {
            // Converts all data to make a dog
            int id = Convert.ToInt32(animal[0]);
            double amtWater = Convert.ToDouble(animal[1]);
            double weight = Convert.ToDouble(animal[2]);
            int age = Convert.ToInt32(animal[3]);
            string colour = animal[4];
            double dailyCost = Convert.ToDouble(animal[5]);

            // Create new Dog
            tempAnimal = new Dogs(id, amtWater, weight, age, colour, dailyCost, "Dog");

            // Add the dog to the dictionary
            allAnimals.Add(id, tempAnimal);
        }
        private void AddSheep(string[] animal)
        {
            // Converts all data to make a sheep
            int id = Convert.ToInt32(animal[0]);
            double dailyCost = Convert.ToDouble(animal[1]);
            double amtWater = Convert.ToDouble(animal[2]);
            double weight = Convert.ToDouble(animal[3]);
            int age = Convert.ToInt32(animal[4]);
            string colour = animal[5];
            double amtOfWool = Convert.ToDouble(animal[6]);

            // Create new sheep
            tempAnimal = new Sheep(id, dailyCost, amtWater, weight, age, colour, amtOfWool, "Sheep");

            // Add the sheep to the dictionary
            try
            {
                allAnimals.Add(id, tempAnimal);
            } 
            catch
            {
                MessageBox.Show("Failed to add Sheep");
            }
        }
        private void AddGoat(string[] animal)
        {
            // Converts all data to make a goat
            int id = Convert.ToInt32(animal[0]);
            double dailyCost = Convert.ToDouble(animal[1]);
            double amtWater = Convert.ToDouble(animal[2]);
            double weight = Convert.ToDouble(animal[3]);
            int age = Convert.ToInt32(animal[4]);
            string colour = animal[5];
            double amtOfMilk = Convert.ToDouble(animal[6]);

            // Create new goat
            tempAnimal = new Goat(id, dailyCost, amtWater, weight, age, colour, amtOfMilk, "Goat");

            try
            {
                // Add the goat to the dictionary
                allAnimals.Add(id, tempAnimal);

                // Add Milk to total milk production - Report 4
                totalMilk += amtOfMilk;
            }
            catch
            {
                MessageBox.Show("Failed to add Goat");
            }
        }

        private void AddCow(string[] animal)
        {
            // Converts all data to cow
            int id = Convert.ToInt32(animal[0]);
            double dailyCost = Convert.ToDouble(animal[1]);
            double amtWater = Convert.ToDouble(animal[2]);
            double weight = Convert.ToDouble(animal[3]);
            int age = Convert.ToInt32(animal[4]);
            string colour = animal[5];
            double amtOfMilk = Convert.ToDouble(animal[6]);
            bool isJersey = Convert.ToBoolean(animal[7]);

            try
            {
                if (isJersey == true)
                {
                    //Create new Jersey Cow
                    tempAnimal = new Jersey(id, dailyCost, amtWater, weight, age, colour, amtOfMilk, "Jersey Cow");
                }
                else
                {
                    // Create new cow
                    tempAnimal = new Cow(id, dailyCost, amtWater, weight, age, colour, amtOfMilk, "Cow");
                }
                // Add the cow to the dictionary
                allAnimals.Add(id, tempAnimal);

                // Add milk to total milk production - Report 4
                totalMilk += amtOfMilk;
            }
            catch
            {
                MessageBox.Show("Failed to add cow");
            }
        }

        private void AddPrices()
        {
            // Array to hold commodity prices
            double[] prices = new double[6];

            // query string to retrieve data from database
            string query = "SELECT * FROM commodityprices";
            OleDbCommand cmd = new OleDbCommand(query, conn);
            // Counter to keep track of array index
            int count = 0;

            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                //Read all data from the database
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int columns = reader.FieldCount;
                        // Holds data in an object
                        object[] rowInfo = new object[columns];
                        // Populate the object with data for each animal
                        reader.GetValues(rowInfo);
                        prices[count] = Convert.ToDouble(rowInfo[1]);
                        count++;
                    }
                    // Set commodity prices 
                    CommodityPrices.GoatMilkPrice = prices[0];
                    CommodityPrices.SheepWoolPrice = prices[1];
                    CommodityPrices.WaterPrice = prices[2];
                    CommodityPrices.TaxPerKG = prices[3];
                    CommodityPrices.JerseyCowTax = prices[4];
                    CommodityPrices.CowMilkPrice = prices[5];
                }
            }
        }

//#######################################################################################################################################################//
        ///////Search and Report functions///////
        
        /// <summary>
        /// Report 1: Search animal by ID
        /// </summary>
        /// <param name="id"></param>
        private void SearchByID(int id)
        {
            // Create new Animal object using the ID number given by the user
            Animal animal = allAnimals[id];
            // Display the info of this object in the resultsBox
            resultsBox.Text = animal.DisplayInfo();
            titleLabel.Text = "ID:";
            descriptionLabel.Text = "This animal is a";
            itemLabel.Text = animal.DisplayType();
            if (itemLabel.Text.ToLower() == "cow" || itemLabel.Text.ToLower() == "jersey cow")
            {
                try
                {
                    logoPanel.Image = Final_Project.Properties.Resources.Cow_Silloette;
                }
                catch { logoPanel.Image = null; };
            }
            else if (itemLabel.Text.ToLower() == "sheep")
            {
                try {
                    logoPanel.Image = Final_Project.Properties.Resources.Sheep_Silhouette;
                }
                catch { logoPanel.Image = null; };
            }
            else if (itemLabel.Text.ToLower() == "goat")
            {
                try
                {
                    logoPanel.Image = Final_Project.Properties.Resources.Goat_Silhouette;
                }
                catch { logoPanel.Image = null; };
            }
            else if (itemLabel.Text.ToLower() == "dog")
            {
                try
                {
                    logoPanel.Image = Final_Project.Properties.Resources.Dog_silhouette;
                }
                catch { logoPanel.Image = null; };
            } else
            {
                logoPanel.Image = null;
            }
        }

        /// <summary>
        /// Report 2: This report will calculate the total profit per day over the
        /// entire farm.
        /// </summary>
        /// <param name="id"></param> 
        private void TotalProfit()
        {
            double profits = 0;

            foreach (KeyValuePair<int, Animal> a in allAnimals)
            {
                profits += a.Value.GetProfit();
            }

            titleLabel.Text = null;
            searchBox.Text = null;
            descriptionLabel.Text = "Total Profit per day";
            itemLabel.Text = null;
            logoPanel.Image = null;
            resultsBox.Text = String.Format("Total daily profit = {0:C2}", profits);
        }

        /// <summary>
        /// Report 3: Display the total tax paid to the government per month
        /// </summary>
        /// 
        private void TotalTax()
        {
            double taxes = 0;

            foreach (KeyValuePair<int, Animal> a in allAnimals)
            {
                taxes += a.Value.GetTax();
            }

            titleLabel.Text = null;
            searchBox.Text = null;
            descriptionLabel.Text = "Total tax per month";
            itemLabel.Text = null;
            logoPanel.Image = null;
            resultsBox.Text = String.Format("Total monthly tax = {0:C2}", (taxes * 28));
        }

        /// <summary>
        /// Report 4: This report will calculate the amount of milk for goats and cows.
        /// </summary>
        private void MilkPerDay()
        {
            titleLabel.Text = null;
            searchBox.Text = null;
            descriptionLabel.Text = "Total milk produced per day";
            itemLabel.Text = null;
            logoPanel.Image = null;
            resultsBox.Text = String.Format("Total daily milk produced = {0:0.00} litres", totalMilk);
        }



        /// <summary>
        /// Report 5: Display the average age of all farm animals (excluding dogs)
        /// </summary>
        /// 

        private void AverageAge()
        {
            double totalAge = 0;
            int count = 0;
            double averageAge = 0;
            string results;

            foreach (int ids in ExcludeAnimals("dogs"))
            {
                totalAge += allAnimals[ids].Age;
                count++;
            }

            averageAge = totalAge / count;
            results = String.Format("Average age (no dogs) {0:0.0}", averageAge);
            titleLabel.Text = null;
            searchBox.Text = null;
            descriptionLabel.Text = "Average age (no dogs)";
            itemLabel.Text = null;
            logoPanel.Image = null;
            resultsBox.Text = results;
        }

        /// <summary>
        /// Report 6: This report calculates the average profit between Goats and cows compared to sheep
        /// </summary>
        private void AverageProfitVS()
        {
            double totalProfitGC = 0;
            double totalProfitS = 0;
            int count1 = 0;
            int count2 = 0;

            foreach (KeyValuePair<int, Animal> a in allAnimals)
            {
                if (a.Value.GetType().ToString().ToLower().Contains("cow") || 
                    a.Value.GetType().ToString().ToLower().Contains("jersey") || 
                    a.Value.GetType().ToString().ToLower().Contains("goat"))
                {
                    totalProfitGC += a.Value.GetProfit();
                    count1++;
                }
                else if (a.Value.GetType().ToString().ToLower().Contains("sheep"))
                {
                    totalProfitS += a.Value.GetProfit();
                    count2++;
                }
            }

            double averageProfitGCPerDay = totalProfitGC / count1;
            double averageProfitSPerDay = totalProfitS / count2;

            double averageProfitGCMonthly = averageProfitGCPerDay * 28;
            double averageProfitSMonthly = averageProfitSPerDay * 28;

            double averageProfitGCYearly = averageProfitGCPerDay * 365;
            double averageProfitSYearly = averageProfitSPerDay * 365;

            titleLabel.Text = null;
            searchBox.Text = null;
            descriptionLabel.Text = "Average Milk Vs Wool";
            itemLabel.Text = null;
            logoPanel.Image = null;
            resultsBox.Text = String.Format("Average Profit for Goats and Cows per day: {0:C2} | Average Profit for Sheep per day: {1:C2} \r\n\r\n" +
                                            "Average Profit for Goats and Cows per month: {2:C2} | Average Profit for Sheep per month: {3:C2} \r\n\r\n" +
                                            "Average Profit for Goats and Cows per year: {4:C2} | Average Profit for Sheep per year: {5:C2} \r\n\r\n", 
                                                                                                                                            averageProfitGCPerDay, averageProfitSPerDay, 
                                                                                                                                            averageProfitGCMonthly, averageProfitSMonthly, 
                                                                                                                                            averageProfitGCYearly, averageProfitSYearly);
        }

        /// <summary>
        /// Report 7: Ratio of Cost of Dogs vs Cost of all Animals
        /// </summary>
        public void RatioOfDogsVsAllCosts()
        {
            double totalCostsPerDay = 0;
            double dogCostsPerDay = 0;

            foreach (KeyValuePair<int, Animal> a in allAnimals)
            {
                if(a.Value.GetType().ToString().ToLower().Contains("dogs"))
                {
                    dogCostsPerDay += a.Value.GetExpenses();
                } else
                {
                    totalCostsPerDay += a.Value.GetExpenses();
                }
            }

            double totalCostsPerMonth = totalCostsPerDay * 28;
            double dogCostsPerMonth = dogCostsPerDay * 28;

            double totalCostsPerYear = totalCostsPerDay * 365;
            double dogCostsPerYear = dogCostsPerDay * 365;

            titleLabel.Text = null;
            searchBox.Text = null;
            descriptionLabel.Text = "Ratio of Dog costs Vs All Animal Costs";
            itemLabel.Text = null;
            logoPanel.Image = null;
            resultsBox.Text = String.Format("Cost of all animals per day {0:C2} \r\n" +
                                            "Cost of dogs per day {1:C2} \r\n\r\n" +
                                            "Cost of all animals per month {2:C2} \r\n" +
                                            "Cost of dogs per month {3:C2} \r\n\r\n" +
                                            "Cost of all animals yearly {4:C2} \r\n" +
                                            "Cost of dogs yearly {5:C2} \r\n\r\n" +
                                            "Ratio per day {0:C2}:{1:C2} \r\n" +
                                            "Ratio per month {2:C2}:{3:C2} \r\n" +
                                            "Ratio per year {4:C2}:{5:C2}", 
                                                                    totalCostsPerDay, dogCostsPerDay, 
                                                                    totalCostsPerMonth, dogCostsPerMonth, 
                                                                    totalCostsPerYear, dogCostsPerYear);
        }

        /// <summary>
        /// Report 8: Export animals to .txt file, ordered by profitability (excluding dogs)
        /// </summary>
        public void ExportToTxt()
        {
            List<int> noDogsIDs = new List<int>();
            List<double> profits = new List<double>();

            foreach (int i in ExcludeAnimals("dogs"))
            {
                noDogsIDs.Add(i);
                profits.Add(allAnimals[i].GetProfit());
            }

            List<int> sortedIDs = Sort(profits, noDogsIDs);

            //Create Text File
            StreamWriter file = new StreamWriter(@"C:\Users\Ray Smith\OneDrive - Wintec\COMP609 l Application Development\Final Project\Final Project\SortedByProfit.txt");
            //Create Exported Strings
            string fileString = ExportedString(sortedIDs);
            file.Write(fileString);
            // Close StreamWriter
            file.Close();
            // Output result to resultBox
            titleLabel.Text = null;
            searchBox.Text = null;
            descriptionLabel.Text = "Exported .txt file";
            itemLabel.Text = null;
            resultsBox.Text = "Written to file\r\n" + fileString;
        }

        /// <summary>
        /// Report 9: Ratio of animals with colour red
        /// </summary>
        public void RatioOfRedAnimals()
        {
            int redAnimals = 0;
            int nonRedAnimals = 0;

            foreach (KeyValuePair<int, Animal> a in allAnimals)
            {
                if(a.Value.Colour.ToString().ToLower().Contains("red")) {
                    redAnimals++;
                }
            }

            nonRedAnimals = allAnimals.Count - redAnimals;

            titleLabel.Text = null;
            searchBox.Text = null;
            descriptionLabel.Text = "Red Animals";
            itemLabel.Text = null;
            resultsBox.Text = String.Format("Total amount of animals = {0} \r\n " +
                                            "Total amount of red animals = {1} \r\n " +
                                            "Total amount of non-red animals = {2} \r\n" +
                                            "Ratio = {2}:{1}", 
                                                              allAnimals.Count, 
                                                              redAnimals, 
                                                              nonRedAnimals);
        }

        /// <summary>
        /// Report 10: Total Tax Paid for Jersey Cows
        /// </summary>
        public void JerseyCowTax()
        {
            double totalDailyTax = 0;
            double totalMonthlyTax = 0;
            double totalYearlyTax = 0;
            foreach (KeyValuePair<int, Animal> a in allAnimals)
            {
                if(a.Value.GetType().ToString().ToLower().Contains("jersey"))
                {
                    totalDailyTax += a.Value.GetTax();
                }
            }
            totalMonthlyTax = totalDailyTax * 28;
            totalYearlyTax = totalDailyTax * 365;

            titleLabel.Text = null;
            searchBox.Text = null;
            descriptionLabel.Text = "Total Tax paid for Jersey Cows";
            itemLabel.Text = null;
            logoPanel.Image = null;
            resultsBox.Text = String.Format("Tax paid for Jersey Cows: \r\n\r\n" +
                                            "Daily tax: {0:C2} \r\n" +
                                            "Monthly tax: {1:C2} \r\n" +
                                            "Yearly tax: {2:C2}", totalDailyTax, totalMonthlyTax, totalYearlyTax);
        }

        /// <summary>
        /// Report 11: Search by year (age). Display ratio of number of animals who are above the threshold compared to all animals.
        /// </summary>
        public void SearchByYear(int year)
        {
            int olderCount = 0;
            int youngerCount = 0;
            int allAnimalCounts = allAnimals.Count();
            int sameAgeCount = 0;
            string results = "";

            foreach (KeyValuePair<int, Animal> a in allAnimals)
            {
                if (a.Value.Age > year)
                {
                    olderCount++;
                }
                if (a.Value.Age == year)
                {
                    sameAgeCount++;
                }
            }

            youngerCount = (allAnimalCounts - sameAgeCount) - olderCount;
            results = String.Format("Amount of animals = {0} \r\n " +
                                    "Animals older than {1} = {2} \r\n" +
                                    "Animals younger than {1} = {3} \r\n" +
                                    "Animals who are {1} = {4} \r\n" +
                                    "Ratio {0}:{2}:{3}", allAnimalCounts, year, olderCount, youngerCount, sameAgeCount);

            // Display the info of this object in the resultsBox
            titleLabel.Text = "Year:";
            descriptionLabel.Text = "All animals above age";
            itemLabel.Text = year.ToString();
            resultsBox.Text = results;
            logoPanel.Image = null;
            searchBox.Text = null;
        }
        /// <summary>
        /// Report 12: Total profitability of Jersey cows
        /// </summary>
        public void TotalProfitForJersey()
        {
            double totalDailyProfits = 0;
            double totalMonthlyProfits = 0;
            double totalYearlyProfits = 0;
            string results = "";
            foreach(KeyValuePair<int, Animal> a in allAnimals)
            {
                if (a.Value.GetType().ToString().ToLower().Contains("jersey"))
                {
                    totalDailyProfits += a.Value.GetProfit();
                }
            }

            totalMonthlyProfits = totalDailyProfits * 28;
            totalYearlyProfits = totalDailyProfits * 365;
            results = String.Format("Total profits for Jersey Cows \r\n\r\n " +
                                    "Total Daily Profits = {0:C2} \r\n" +
                                    "Total Monthly Profits = {1:C2} \r\n" +
                                    "Total Yearly Profits = {2:C2}", totalDailyProfits, totalMonthlyProfits, totalYearlyProfits);

            // Display the info of this object in the resultsBox
            titleLabel.Text = null;
            searchBox.Text = null;
            itemLabel.Text = null;
            logoPanel.Image = null;
            descriptionLabel.Text = "Jersey Cow profits";
            resultsBox.Text = results;
        }
        //#######################################################################################################################################################//
        ///Universal Methods

        //Bubble Sort
        public List<int> Sort(List<double> profits, List<int> ids)
        {
            double profitTemp = 0;
            int idTemp = 0;

            bool swapped = false;

            //Loop 1
            for (int i = 0; i < profits.Count; i++)
            {
                // Loop 2
                for (int j = 1; j < profits.Count - i; j++)
                {
                    // Value greater than the next
                    if (profits[j - 1] > profits[j])
                    {
                        // Swap
                        profitTemp = profits[j];
                        profits[j] = profits[j - 1];
                        profits[j - 1] = profitTemp;
                        // Swap their corresponding ID numbers
                        idTemp = ids[j];
                        ids[j] = ids[j - 1];
                        ids[j - 1] = idTemp;
                        swapped = true;
                    }
                }
                // If the array is sorted
                if (!swapped) 
                {
                    // Return the ID array
                    return (ids);
                }
            }
            // Return the ID array once it has been sorted
            return (ids);
        }

        //Export strings to .txt file
        public string ExportedString(List<int> sorted)
        {

            // String to hold output
            string output = "";
            // Temporary FarmAnimal object
            Animal temp;
            string type = "Animal";

            // Traverse through array create output string
            for (int i = 0; i < sorted.Count; i++)
            {
                // Populate temporary livestock object
                allAnimals.TryGetValue(sorted[i], out temp);

                if (temp.GetType().ToString().ToLower().Contains("cow"))
                {
                    type = "Cow";
                } else if (temp.GetType().ToString().ToLower().Contains("jersey"))
                {
                    type = "Jersey Cow";
                } else if (temp.GetType().ToString().ToLower().Contains("goat"))
                {
                    type = "Goat";
                } else if (temp.GetType().ToString().ToLower().Contains("sheep"))
                {
                    type = "Sheep";
                }
                output += String.Format("{0,-15} ID: {1:10}\tProfit: {2:C2}\r\n",
                                        type, 
                                        sorted[i], 
                                        temp.GetProfit());
            }
            return (output);
        }

        //Exclude a specified animal from a report
        public List<int> ExcludeAnimals(string animal)
        {
            List<int> ids = new List<int>();
            foreach(KeyValuePair<int, Animal> a in allAnimals)
            {
                if (a.Value.GetType().ToString().ToLower().Contains(animal))
                {
                } else
                {
                    ids.Add(a.Key);
                }
            }

            return (ids);
        }

        public void ErrorHandling()
        {
            titleLabel.Text = null;
            searchBox.Text = "ERROR";
            descriptionLabel.Text = null;
            itemLabel.Text = null;
            logoPanel.Image = null;
            resultsBox.Text = null;
        }
    }
}
