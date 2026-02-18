using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elective_Bautista
{
    public partial class WelcomePageBakery : Form
    {
        public WelcomePageBakery()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void WelcomePageBakery_Load(object sender, EventArgs e)
        {
            
            string cuteQuote = "Welcome to Czarina's Bakery! \n\n\"Baked with love and a sprinkle of magic.\" ✨";

            MessageBox.Show(cuteQuote,
                            "Czarina's Bakery Shop",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.None); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductwithBarcode productForm = new ProductwithBarcode();
            productForm.Show();
            this.Hide(); // Hide welcome screen so user can see the menu
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
