using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
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
            // Updated columns to handle Qty and Subtotal
            dataGridView1.Columns.Add("ProdName", "Item Name");
            dataGridView1.Columns.Add("ProdQty", "Qty");
            dataGridView1.Columns.Add("ProdPrice", "Price");
            dataGridView1.Columns.Add("ProdSubtotal", "Subtotal"); // Index 3

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void ProcessScannedItem(string barcode)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // 1. Search for the product and get its current stock
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

                    // 2. Get the amount the customer wants to buy from your NumericUpDown
                    int qtyToBuy = (int)numQty.Value;

                    // 3. Check if you have enough in the kitchen
                    if (currentInventory < qtyToBuy)
                    {
                        MessageBox.Show($"Not enough stock! You only have {currentInventory} left.", "Inventory Warning");
                        reader.Close();
                        return;
                    }
                    reader.Close(); // Close reader to allow the Update command to run

                    // 4. MATH: Calculate Subtotal
                    decimal subtotal = price * qtyToBuy;

                    // 5. UPDATE INVENTORY: Subtract the bought quantity from the Products table
                    string sqlUpdate = "UPDATE Products SET Quantity = Quantity - @qty WHERE BarcodeData = @code";
                    SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn);
                    cmdUpdate.Parameters.AddWithValue("@qty", qtyToBuy);
                    cmdUpdate.Parameters.AddWithValue("@code", barcode);
                    cmdUpdate.ExecuteNonQuery();

                    // 6. RECORD SALE: Save the transaction to ScannedItems
                    string sqlSave = "INSERT INTO ScannedItems (BarcodeData, ProductName, Price, Quantity, TotalPrice, ScanTime) " +
                                     "VALUES (@code, @name, @price, @qty, @total, GETDATE())";

                    SqlCommand cmdSave = new SqlCommand(sqlSave, conn);
                    cmdSave.Parameters.AddWithValue("@code", barcode);
                    cmdSave.Parameters.AddWithValue("@name", name);
                    cmdSave.Parameters.AddWithValue("@price", price);
                    cmdSave.Parameters.AddWithValue("@qty", qtyToBuy); // Sending the quantity bought
                    cmdSave.Parameters.AddWithValue("@total", subtotal); // Sending the subtotal
                    cmdSave.ExecuteNonQuery();

                    // 7. UI UPDATE: Add to the DataGridView
                    dataGridView1.Rows.Add(name, qtyToBuy, "₱" + price.ToString("N2"), "₱" + subtotal.ToString("N2"));

                    // 8. TOTALS: Update the Grand Total label
                    UpdateGrandTotal();

                    // Reset for next scan
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

            CalculateChange(); // Add this line here!
        }

        private void CalculateChange()
        {
            try
            {
                // 1. Get Total from lblGrandTotal (removing the ₱ and 'TOTAL: ' text)
                string totalText = lblGrandTotal.Text.Replace("TOTAL: ₱", "").Trim();
                decimal total = decimal.Parse(totalText);

                // 2. Get Cash from txtCashReceived
                decimal cash = 0;
                if (!string.IsNullOrWhiteSpace(txtCashReceived.Text))
                {
                    cash = decimal.Parse(txtCashReceived.Text);
                }

                // 3. MATH: Change = Cash - Total
                decimal change = cash - total;

                // 4. Update the Change Label
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
    }
}