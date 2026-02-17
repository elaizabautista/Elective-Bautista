namespace Elective_Bautista
{
    partial class CashierForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtScanInput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ProdName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProdPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Pristina", 48.20869F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightPink;
            this.label1.Location = new System.Drawing.Point(137, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(635, 101);
            this.label1.TabIndex = 4;
            this.label1.Text = "Czarina\'s Bakery Shop";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Monotype Corsiva", 26.29565F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.label2.Location = new System.Drawing.Point(233, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(277, 51);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cashering System";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightYellow;
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(285, 159);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(613, 315);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SAVED ITEMS";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Pink;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProdName,
            this.ProdPrice});
            this.dataGridView1.Location = new System.Drawing.Point(6, 21);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 49;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(601, 288);
            this.dataGridView1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MV Boli", 13.77391F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.label3.Location = new System.Drawing.Point(12, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 29);
            this.label3.TabIndex = 7;
            this.label3.Text = "Barcode Number:";
            // 
            // txtScanInput
            // 
            this.txtScanInput.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.txtScanInput.Location = new System.Drawing.Point(17, 200);
            this.txtScanInput.Multiline = true;
            this.txtScanInput.Name = "txtScanInput";
            this.txtScanInput.Size = new System.Drawing.Size(251, 32);
            this.txtScanInput.TabIndex = 8;
            this.txtScanInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScanInput_KeyDown_1);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Cornsilk;
            this.button1.Location = new System.Drawing.Point(17, 251);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(251, 44);
            this.button1.TabIndex = 9;
            this.button1.Text = "Save and Next";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ProdName
            // 
            this.ProdName.HeaderText = "Item Name";
            this.ProdName.MinimumWidth = 6;
            this.ProdName.Name = "ProdName";
            this.ProdName.Width = 120;
            // 
            // ProdPrice
            // 
            this.ProdPrice.HeaderText = "Price";
            this.ProdPrice.MinimumWidth = 6;
            this.ProdPrice.Name = "ProdPrice";
            this.ProdPrice.Width = 120;
            // 
            // CashierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(910, 500);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtScanInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CashierForm";
            this.Text = "CashierForm";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtScanInput;
        public System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProdName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProdPrice;
    }
}