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
                        // 1. Clear UI
                        lstDetails.Items.Clear();
                        picDessert.Image = null;
                        picBarcode.Image = null;

                        // 2. Load Text Info
                        lstDetails.Items.Add("NAME: " + reader["ProductName"].ToString());
                        lstDetails.Items.Add("PRICE: ₱" + reader["Price"].ToString());
                        lstDetails.Items.Add("INFO: " + reader["Description"].ToString());

                        // 3. Load Image (No file lock)
                        string path = reader["DessertImagePath"].ToString();
                        if (!string.IsNullOrEmpty(path) && File.Exists(path))
                        {
                            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                            {
                                picDessert.Image = Image.FromStream(fs);
                                picDessert.SizeMode = PictureBoxSizeMode.StretchImage;
                            }
                        }

                        // 4. Generate Barcode (Fixed for Generic BarcodeWriter)
                        var writer = new BarcodeWriter<Bitmap>
                        {
                            Format = BarcodeFormat.CODE_128,
                            Options = new EncodingOptions
                            {
                                Height = 100,
                                Width = 250,
                                Margin = 1
                            },
                            Renderer = new BitmapRenderer()
                        };

                        picBarcode.Image = writer.Write(reader["BarcodeData"].ToString());
                        picBarcode.SizeMode = PictureBoxSizeMode.Zoom;
                    }
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
    }
}

