using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using ZXing.Rendering;
using ZXing.Windows.Compatibility;
using BitmapRenderer = ZXing.Rendering.BitmapRenderer;
using System.Net.Http;
using System.Threading.Tasks;

namespace Elective_Bautista
{
    public partial class ProductwithBarcode : Form
    {
        // 1. Setup your Connection String
        string connStr = @"Data Source=LAPTOP-0DMT6OS6\SQLEXPRESS2;Initial Catalog=BakeryDB;Integrated Security=True";

        public ProductwithBarcode()
        {
            InitializeComponent();
        }

        // Run this when the Form opens to show your bakery items
        private void ProductwithBarcode_Load(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            try
            {
                dataGridView1.Rows.Clear();
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    string sql = "SELECT BarcodeData, ProductName, Price, Quantity FROM Products";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int stockValue = Convert.ToInt32(reader["Quantity"]);
                        int rowIndex = dataGridView1.Rows.Add(
                            reader["BarcodeData"].ToString(),
                            reader["ProductName"].ToString(),
                            "₱" + Convert.ToDecimal(reader["Price"]).ToString("N2"),
                            stockValue.ToString()
                        );

                        // If stock is less than 5, make the row look like a warning
                        if (stockValue < 5)
                        {
                            dataGridView1.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.Red;
                            dataGridView1.Rows[rowIndex].Cells[3].Value += " (LOW!)";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        public void LoadProductDetails(string barcodeToFind)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT * FROM Products WHERE BarcodeData = @code";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@code", barcodeToFind);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        lstDetails.Items.Clear();

                        string name = reader["ProductName"].ToString();
                        string price = reader["Price"].ToString();
                        string stock = reader["Quantity"].ToString();
                        string desc = reader["Description"].ToString();

                        lstDetails.Items.Add("NAME: " + name);
                        lstDetails.Items.Add("PRICE: ₱" + price);
                        lstDetails.Items.Add("STOCK: " + stock + " pcs");
                        lstDetails.Items.Add("INFO: " + desc);

                        // FIX: Updated to use 'numQuantity' to match your Update button
                        numQuantity.Value = decimal.Parse(stock);

                        // Image Loading
                        string path = reader["DessertImagePath"].ToString();
                        if (!string.IsNullOrEmpty(path) && File.Exists(path))
                        {
                            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                            {
                                picDessert.Image = Image.FromStream(fs);
                                picDessert.SizeMode = PictureBoxSizeMode.StretchImage;
                            }
                        }

                        // Barcode Generation
                        var writer = new BarcodeWriter<Bitmap>
                        {
                            Format = BarcodeFormat.CODE_128,
                            Options = new EncodingOptions { Height = 100, Width = 250, Margin = 1 },
                            Renderer = new BitmapRenderer()
                        };
                        picBarcode.Image = writer.Write(barcodeToFind);
                    }
                }
            }
        }

        private void BtnUpdateStock_Click(object sender, EventArgs e)
        {
            if (lstDetails.Items.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // Gets the name from the first line of the ListBox
                    string currentName = lstDetails.Items[0].ToString().Replace("NAME: ", "");

                    string sql = "UPDATE Products SET Quantity = @qty WHERE ProductName = @name";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@qty", (int)numQuantity.Value);
                    cmd.Parameters.AddWithValue("@name", currentName);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Stock updated for " + currentName + "!");
                    LoadDataGrid(); // Refresh the grid to show new stock
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // We tell the engine to find the item with this specific barcode ID
            LoadProductDetails("VGT01");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadProductDetails("MS02");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadProductDetails("GOL03");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadProductDetails("CHA04");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadProductDetails("HAG1");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadProductDetails("APP05");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadProductDetails("OI12");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            LoadProductDetails("YTU1");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            LoadProductDetails("HGF1");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            CashierForm cashier = new CashierForm();
            cashier.Show(); // This pops up the cashier window
        }


        private void btnUpdateStock_Click(object sender, EventArgs e)
        {
            // This code updates the stock for the item currently being viewed
            if (lstDetails.Items.Count > 0)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    // We get the Name from the listbox to find the right product
                    // (Or you can store the current barcode in a global variable)
                    string currentName = lstDetails.Items[0].ToString().Replace("NAME: ", "");

                    string sql = "UPDATE Products SET Quantity = @qty WHERE ProductName = @name";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@qty", (int)numQuantity.Value);
                    cmd.Parameters.AddWithValue("@name", currentName);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Stock updated for " + currentName + "!");
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

