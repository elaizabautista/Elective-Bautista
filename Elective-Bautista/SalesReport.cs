using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elective_Bautista
{
    public partial class SalesReport : Form
    {
        string connStr = @"Data Source=LAPTOP-0DMT6OS6\SQLEXPRESS2;Initial Catalog=BakeryDB;Integrated Security=True";

        public SalesReport()
        {
            InitializeComponent();
        }
        private void LoadSalesData(DateTime targetDate)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"SELECT ProductName, Quantity, Price, TotalPrice, ScanTime 
                                 FROM SalesLog 
                                 WHERE CAST(ScanTime AS DATE) = @target 
                                 ORDER BY ScanTime DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@target", targetDate.Date);

                DataTable dt = new DataTable();
                da.Fill(dt);

                // Bind the data to your grid
                dgvSales.DataSource = dt;
            }
        }

        private void SalesReport_Load(object sender, EventArgs e)
        {
            // Automatically load today's sales when the form opens
            LoadSalesData(DateTime.Now);
        }

        private void dtpFilterDate_ValueChanged(object sender, EventArgs e)
        {
            // Refresh whenever the date is changed
            LoadSalesData(dtpFilterDate.Value);
        }
    }
}
