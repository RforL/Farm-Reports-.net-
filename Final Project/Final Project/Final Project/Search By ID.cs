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
    public partial class Search_By_ID : Form
    {
        private int searchID;
        public int SearchID
        {
            get { return searchID; }
        }

        public Search_By_ID()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                searchID = int.Parse(idBox.Text);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
