using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Elective_Bautista
{
    public partial class CashierForm : Form
    {
        //Connection String
        string connStr = @"Data Source=LAPTOP-0DMT6OS6\SQLEXPRESS2;Initial Catalog=BakeryDB;Integrated Security=True";

        public CashierForm()
        {
            InitializeComponent();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("ProdName", "Item Name");
            dataGridView1.Columns.Add("ProdQty", "Qty");
            dataGridView1.Columns.Add("ProdPrice", "Price");
            dataGridView1.Columns.Add("ProdSubtotal", "Subtotal"); 

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ProcessScannedItem(string barcode)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                //Search for the product and get its current stock
                string sqlSelect = "SELECT ProductName, Price, Quantity FROM Products WHERE BarcodeData = @code";
                SqlCommand cmdSelect = new SqlCommand(sqlSelect, conn);
                cmdSelect.Parameters.AddWithValue("@code", barcode);

                conn.Open();
                SqlDataReader reader = cmdSelect.ExecuteReader();

                if (reader.Read())
                {
                    string name = reader["ProductName"].ToString();
                    decimal price = Convert.ToDecimal(reader["Price"]);
                    int currentInventory = Convert.ToInt32(reader["Quantity"]); // Stock from Product Form

                    //Get the amount the customer wants to buy from your NumericUpDown
                    int qtyToBuy = (int)numQty.Value;

                    // Check if there's enough stock to fulfill the purchase
                    if (currentInventory < qtyToBuy)
                    {
                        MessageBox.Show($"Not enough stock! You only have {currentInventory} left.", "Inventory Warning");
                        reader.Close();
                        return;
                    }
                    reader.Close(); // Close reader to allow the Update command to run

                    //Calculate Subtotal
                    decimal subtotal = price * qtyToBuy;

                    //Update Inverntory
                    string sqlUpdate = "UPDATE Products SET Quantity = Quantity - @qty WHERE BarcodeData = @code";
                    SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn);
                    cmdUpdate.Parameters.AddWithValue("@qty", qtyToBuy);
                    cmdUpdate.Parameters.AddWithValue("@code", barcode);
                    cmdUpdate.ExecuteNonQuery();

                    //Save the transaction to ScannedItems
                    string sqlSave = "INSERT INTO SalesLog (BarcodeData, ProductName, Price, Quantity, TotalPrice, ScanTime) " +
                 "VALUES (@code, @name, @price, @qty, @total, GETDATE())";

                    SqlCommand cmdSave = new SqlCommand(sqlSave, conn);
                    cmdSave.Parameters.AddWithValue("@code", barcode);
                    cmdSave.Parameters.AddWithValue("@name", name);
                    cmdSave.Parameters.AddWithValue("@price", price);
                    cmdSave.Parameters.AddWithValue("@qty", qtyToBuy); // Sending the quantity bought
                    cmdSave.Parameters.AddWithValue("@total", subtotal); // Sending the subtotal
                    cmdSave.ExecuteNonQuery();

                    // Add to the DataGridView
                    dataGridView1.Rows.Add(name, qtyToBuy, "₱" + price.ToString("N2"), "₱" + subtotal.ToString("N2"));

                    //Update the Grand Total label
                    UpdateGrandTotal();

                    //Reset for next scan
                    numQty.Value = 1;
                }
                else
                {
                    MessageBox.Show("Product ID not found!");
                }
            }
        }

        private void UpdateGrandTotal()
        {
            decimal grandTotal = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[3].Value != null)
                {
                    string val = row.Cells[3].Value.ToString().Replace("₱", "");
                    grandTotal += Convert.ToDecimal(val);
                }
            }
            lblGrandTotal.Text = "TOTAL: ₱" + grandTotal.ToString("N2");

            CalculateChange(); 
        }

        private void CalculateChange()
        {
            try
            {
                // Get Total from lblGrandTotal
                string totalText = lblGrandTotal.Text.Replace("TOTAL: ₱", "").Trim();
                decimal total = decimal.Parse(totalText);

                // Get Cash from txtCashReceived
                decimal cash = 0;
                if (!string.IsNullOrWhiteSpace(txtCashReceived.Text))
                {
                    cash = decimal.Parse(txtCashReceived.Text);
                }

                decimal change = cash - total;

                //Update the Change Label
                if (change < 0)
                {
                    lblChange.Text = "CHANGE: ₱0.00 (Incomplete)";
                    lblChange.ForeColor = Color.Red; // Warning color
                }
                else
                {
                    lblChange.Text = "CHANGE: ₱" + change.ToString("N2");
                    lblChange.ForeColor = Color.Green; // Success color
                }
            }
            catch
            {
                lblChange.Text = "CHANGE: ₱0.00";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            lblGrandTotal.Text = "TOTAL: ₱0.00";
            lblChange.Text = "CHANGE: ₱0.00"; // Reset change
            txtCashReceived.Clear();           // Clear cash input
            txtScanInput.Focus();
        }

        private void txtScanInput_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string scannedID = txtScanInput.Text.Trim();
                if (!string.IsNullOrEmpty(scannedID))
                {
                    ProcessScannedItem(scannedID);
                }
                txtScanInput.Clear();
                txtScanInput.Focus();
                e.SuppressKeyPress = true;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CashierForm_Load(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtCashReceived_TextChanged(object sender, EventArgs e)
        {
            CalculateChange();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Create an instance of your new Sales Report form
            SalesReport report = new SalesReport();

            // Show the form
            report.Show(); 
        }
    }
}