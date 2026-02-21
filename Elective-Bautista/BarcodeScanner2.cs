using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
using ZXing.Windows.Compatibility;
using BitmapRenderer = ZXing.Rendering.BitmapRenderer;


namespace Elective_Bautista
{
    public partial class BarcodeScanner2 : Form
    {
        string connStr = @"Data Source=LAPTOP-0DMT6OS6\SQLEXPRESS2;Initial Catalog=BakeryDB;Integrated Security=True";
        string lastScannedCode = "";
        public BarcodeScanner2()
        {
            InitializeComponent();
        }

        // 1. SCANNER INPUT
       private async void txtScanInput_KeyDown(object sender, KeyEventArgs e)
{
    if (e.KeyCode == Keys.Enter)
    {
        // 1. Capture the code
        string code = txtScanInput.Text.Trim();
        
        // 2. Prevent empty scans
        if (string.IsNullOrEmpty(code))
        {
            return;
        }

        try 
        {
            // 3. Store for the 'Update' button logic
            lastScannedCode = code;

            // 4. Run the search and display logic
            await ProcessSmartScan(code);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Scan Error: " + ex.Message);
        }
        finally 
        {
            // 5. Always clear and refocus so the cashier can scan the next item immediately
            txtScanInput.Clear();
            txtScanInput.Focus();
            
            // This stops the 'Ding' sound Windows makes when pressing Enter in a textbox
            e.SuppressKeyPress = true;
        }
    }
}

        private void PrintReceipt()
        {
            try
            {
                string receiptPath = "Receipt_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
                using (StreamWriter sw = new StreamWriter(receiptPath))
                {
                    sw.WriteLine("      BELLE PATISSERIE      ");
                    sw.WriteLine("============================");
                    sw.WriteLine("Date: " + DateTime.Now.ToString());
                    sw.WriteLine("----------------------------");
                    sw.WriteLine(string.Format("{0,-15} {1,-5} {2,-7}", "Item", "Qty", "Price"));

                    foreach (DataGridViewRow row in dgvCart.Rows)
                    {
                        if (row.IsNewRow) continue;
                        string name = row.Cells[0].Value.ToString();
                        string qty = row.Cells[2].Value.ToString();
                        string sub = row.Cells[3].Value.ToString();

                        if (name.Length > 14) name = name.Substring(0, 11) + "...";
                        sw.WriteLine(string.Format("{0,-15} {1,-5} ₱{2,-7}", name, qty, sub));
                    }

                    sw.WriteLine("----------------------------");
                    sw.WriteLine("GRAND " + lblTotal.Text); // Shows the Total

                    // Added Cash and Change Details
                    decimal cash = decimal.TryParse(txtCashReceived.Text, out decimal c) ? c : 0;
                    sw.WriteLine(string.Format("{0,-15} ₱{1,-10}", "CASH RECEIVED:", cash.ToString("N2")));
                    sw.WriteLine(lblChange.Text);

                    sw.WriteLine("============================");
                    sw.WriteLine("  THANK YOU FOR YOUR VISIT  ");
                }

                System.Diagnostics.Process.Start(receiptPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error printing receipt: " + ex.Message);
            }
        }

        private async Task ProcessSmartScan(string code)
        {
            lastScannedCode = code;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                await conn.OpenAsync();

                // 1. Check if we already saved this barcode as a specific food
                string sql = "SELECT ProductName, Price, ShopType FROM Products WHERE BarcodeData = @code";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", code);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        UpdateUIFields(reader["ProductName"].ToString(), Convert.ToDecimal(reader["Price"]), reader["ShopType"].ToString());
                        AddToCart(reader["ProductName"].ToString(), Convert.ToDecimal(reader["Price"]), reader["ShopType"].ToString());
                        return;
                    }
                }

                // 2. THE FOOD MASTER LIST
                string[] treats = {
            "Strawberry Shortcake", "Chocolate Lava Cake", "Blueberry Cheesecake",
            "Ube Macapuno Cake", "Mango Graham Float", "Vanilla Bean Gelato",
            "Cookies & Cream Sundae", "Caramel Macchiato", "Iced Matcha Latte"
        };

                Random rnd = new Random();

                // This picks a real name, never "Unknown"
                string finalName = treats[rnd.Next(treats.Length)];
                decimal finalPrice = (decimal)rnd.Next(120, 300);
                string finalType = "Bakery";

                // 3. Save to Database
                string insertSql = "INSERT INTO Products (BarcodeData, ProductName, Price, ShopType, Quantity) VALUES (@c, @n, @p, @t, 100)";
                using (SqlCommand insCmd = new SqlCommand(insertSql, conn))
                {
                    insCmd.Parameters.AddWithValue("@c", code);
                    insCmd.Parameters.AddWithValue("@n", finalName);
                    insCmd.Parameters.AddWithValue("@p", finalPrice);
                    insCmd.Parameters.AddWithValue("@t", finalType);
                    await insCmd.ExecuteNonQueryAsync();
                }

                UpdateUIFields(finalName, finalPrice, finalType);
                AddToCart(finalName, finalPrice, finalType);
            }
        }
        private void UpdateUIFields(string name, decimal price, string type)
        {
            txtName.Text = name;
            txtPrice.Text = price.ToString("N2");
            cmbShopType.Text = type;
            this.Refresh(); // Forces the form to show the new data immediately
        }

        private void AddToCart(string name, decimal price, string type)
        {
            // Ensure qty is at least 1 even if the box is empty
            int qty = (int)numQty.Value > 0 ? (int)numQty.Value : 1;
            decimal subtotal = price * qty;

            dgvCart.Rows.Add(name, price.ToString("N2"), qty, subtotal.ToString("N2"), type);
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.Cells[3].Value != null)
                    total += Convert.ToDecimal(row.Cells[3].Value);
            }
            lblTotal.Text = "TOTAL: ₱" + total.ToString("N2");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvCart.Rows.Count == 0)
            {
                MessageBox.Show("The cart is empty! Scan some random treats first.");
                return;
            }

            // Ensure cash was entered
            if (string.IsNullOrEmpty(txtCashReceived.Text))
            {
                MessageBox.Show("Please enter the cash amount received.");
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    // Loop through every row in the DataGridView
                    foreach (DataGridViewRow row in dgvCart.Rows)
                    {
                        // Skip the empty 'new' row at the bottom
                        if (row.IsNewRow) continue;

                        // SQL Command to save the specific random item and price from the grid
                        string sql = @"INSERT INTO SalesLog (ProductName, Price, Qty, TotalPrice, ShopType, SaleDate) 
                               VALUES (@n, @p, @q, @total, @t, GETDATE())";

                        using (SqlCommand cmd = new SqlCommand(sql, conn))
                        {
                            // Pulling values directly from the Grid cells
                            cmd.Parameters.AddWithValue("@n", row.Cells[0].Value.ToString());
                            cmd.Parameters.AddWithValue("@p", decimal.Parse(row.Cells[1].Value.ToString()));
                            cmd.Parameters.AddWithValue("@q", int.Parse(row.Cells[2].Value.ToString()));
                            cmd.Parameters.AddWithValue("@total", decimal.Parse(row.Cells[3].Value.ToString()));
                            cmd.Parameters.AddWithValue("@t", row.Cells[4].Value.ToString());

                            cmd.ExecuteNonQuery();
                        }
                    }
                }

                // 2. Print the Receipt after saving
                PrintReceipt();

                MessageBox.Show("Sales logged successfully to BakeryDB!");

                // 3. Reset UI for the next customer
                dgvCart.Rows.Clear();
                lblTotal.Text = "TOTAL: ₱0.00";
                txtCashReceived.Clear();
                lblChange.Text = "CHANGE: ₱0.00";
                txtScanInput.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving sales: " + ex.Message);
            }
        }   

        private void button5_Click(object sender, EventArgs e)
        {
            dgvCart.Rows.Clear();
            lblTotal.Text = "TOTAL: ₱0.00";
            txtCashReceived.Clear();
            lblChange.Text = "CHANGE: ₱0.00";
            lblChange.ForeColor = Color.Black;
            txtScanInput.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                // Added ShopType=@t to the SQL command
                string sql = "UPDATE Products SET ProductName=@n, Price=@p, ShopType=@t WHERE BarcodeData=@c";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@c", lastScannedCode);
                cmd.Parameters.AddWithValue("@n", txtName.Text);
                cmd.Parameters.AddWithValue("@p", decimal.Parse(txtPrice.Text));
                cmd.Parameters.AddWithValue("@t", cmbShopType.Text); // Get category from ComboBox

                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Database Updated successfully!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Check if a row is actually selected in your DataGridView
            if (dgvCart.SelectedRows.Count > 0)
            {
                // Confirm with the user before removing
                DialogResult result = MessageBox.Show("Remove this item from the cart?", "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Remove the selected row
                    foreach (DataGridViewRow row in dgvCart.SelectedRows)
                    {
                        dgvCart.Rows.Remove(row);
                    }

                    // IMPORTANT: Recalculate the Grand Total after the item is gone
                    CalculateTotal();

                    // Return focus to the scanner input for the next item
                    txtScanInput.Focus();
                }
            }
            else
            {
                MessageBox.Show("Please select a row in the cart to remove.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {
            //
        }

        private void txtCashReceived_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // 1. Get the current Grand Total from the grid (or your total label)
                decimal grandTotal = 0;
                foreach (DataGridViewRow row in dgvCart.Rows)
                {
                    if (row.Cells[3].Value != null)
                        grandTotal += Convert.ToDecimal(row.Cells[3].Value);
                }

                // 2. Parse the cash received
                if (decimal.TryParse(txtCashReceived.Text, out decimal cashProvided))
                {
                    decimal change = cashProvided - grandTotal;

                    if (change >= 0)
                    {
                        lblChange.Text = "CHANGE: ₱" + change.ToString("N2");
                        lblChange.ForeColor = Color.Green; // Visual cue for success
                    }
                    else
                    {
                        lblChange.Text = "INSUFFICIENT CASH";
                        lblChange.ForeColor = Color.Red; // Warning cue
                    }
                }
                else
                {
                    lblChange.Text = "CHANGE: ₱0.00";
                }
            }
            catch { /* Handle unexpected input errors */ }
        }

        private void BarcodeScanner2_Load(object sender, EventArgs e)
        {
            cmbShopType.Items.Clear();
            cmbShopType.Items.Add("Bakery");
            cmbShopType.Items.Add("General");
            cmbShopType.SelectedIndex = 0; // Default to Bakery

            numQty.Minimum = 1; // Cashiers cannot sell 0 items
            numQty.Value = 1;   // Default to 1 item
        }
    }
    
}

        
       