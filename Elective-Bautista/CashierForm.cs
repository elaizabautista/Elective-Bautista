using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Elective_Bautista
{
    public partial class CashierForm : Form
    {
        // Your Connection String
        string connStr = @"Data Source=LAPTOP-0DMT6OS6\SQLEXPRESS2;Initial Catalog=BakeryDB;Integrated Security=True";

        public CashierForm()
        {
            InitializeComponent();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dataGridView1.Columns.Clear();
            // These names MUST match the code in ProcessScannedItem
            dataGridView1.Columns.Add("ProdName", "Item Name");
            dataGridView1.Columns.Add("ProdPrice", "Price");
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
  
        private void ProcessScannedItem(string barcode)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // 1. Search for the product assigned to this barcode
                string sqlSelect = "SELECT ProductName, Price FROM Products WHERE BarcodeData = @code";
                SqlCommand cmdSelect = new SqlCommand(sqlSelect, conn);
                cmdSelect.Parameters.AddWithValue("@code", barcode);

                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmdSelect);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    // Data found! Extract name and price
                    string name = dt.Rows[0]["ProductName"].ToString();
                    decimal price = Convert.ToDecimal(dt.Rows[0]["Price"]);

                    // 2. DISPLAY: Add to the pink DataGridView
                    dataGridView1.Rows.Add(name, "₱" + price.ToString("N2"));

                    // 3. AUTO-SAVE: Insert into ScannedItems history table
                    string sqlSave = "INSERT INTO ScannedItems (BarcodeData, ProductName, Price, ScanTime) " +
                                     "VALUES (@code, @name, @price, GETDATE())";

                    SqlCommand cmdSave = new SqlCommand(sqlSave, conn);
                    cmdSave.Parameters.AddWithValue("@code", barcode);
                    cmdSave.Parameters.AddWithValue("@name", name);
                    cmdSave.Parameters.AddWithValue("@price", price);

                    cmdSave.ExecuteNonQuery();
                }
                else
                {
                    // If the ID isn't in your Products table
                    MessageBox.Show("ID '" + barcode + "' is not assigned to any product!");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // The "Save and Next" button clears the grid for the next customer
            dataGridView1.Rows.Clear();
            txtScanInput.Focus();
        }

        private void txtScanInput_KeyDown_1(object sender, KeyEventArgs e)
        {
            // Checks if the 'Enter' key was pressed (sent by your phone)
            if (e.KeyCode == Keys.Enter)
            {
                string scannedID = txtScanInput.Text.Trim();

                if (!string.IsNullOrEmpty(scannedID))
                {
                    // This runs your search and save logic
                    ProcessScannedItem(scannedID);
                }

                txtScanInput.Clear(); // Clears for the next scan
                txtScanInput.Focus(); // Keeps the cursor ready
                e.SuppressKeyPress = true; // Stops the 'beep' sound
            }
        }
    }
}