using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project
{
    public partial class Search_By_Year : Form
    {
        private int searchYear;

        public int SearchYear
        {
            get { return searchYear; }
        }
        public Search_By_Year()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                searchYear = int.Parse(yearBox.Text);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
