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
            this.ProdName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProdPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.txtScanInput = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.numQty = new System.Windows.Forms.NumericUpDown();
            this.colQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSubtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.lblGrandTotal = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Pristina", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightPink;
            this.label1.Location = new System.Drawing.Point(256, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(618, 152);
            this.label1.TabIndex = 4;
            this.label1.Text = "Belle Patisserie";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Monotype Corsiva", 28.17391F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.label2.Location = new System.Drawing.Point(523, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(297, 54);
            this.label2.TabIndex = 5;
            this.label2.Text = "Cashering System";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightYellow;
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(366, 226);
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
            this.ProdPrice,
            this.colQty,
            this.colSubtotal});
            this.dataGridView1.Location = new System.Drawing.Point(6, 21);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 49;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(601, 288);
            this.dataGridView1.TabIndex = 0;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MV Boli", 13.77391F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.label3.Location = new System.Drawing.Point(93, 226);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(189, 29);
            this.label3.TabIndex = 7;
            this.label3.Text = "Barcode Number:";
            // 
            // txtScanInput
            // 
            this.txtScanInput.ForeColor = System.Drawing.Color.PaleVioletRed;
            this.txtScanInput.Location = new System.Drawing.Point(98, 267);
            this.txtScanInput.Multiline = true;
            this.txtScanInput.Name = "txtScanInput";
            this.txtScanInput.Size = new System.Drawing.Size(251, 32);
            this.txtScanInput.TabIndex = 8;
            this.txtScanInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScanInput_KeyDown_1);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Cornsilk;
            this.button1.Location = new System.Drawing.Point(98, 318);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(251, 44);
            this.button1.TabIndex = 9;
            this.button1.Text = "Save and Next";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // numQty
            // 
            this.numQty.Location = new System.Drawing.Point(98, 381);
            this.numQty.Name = "numQty";
            this.numQty.Size = new System.Drawing.Size(53, 22);
            this.numQty.TabIndex = 10;
            // 
            // colQty
            // 
            this.colQty.HeaderText = "Qty";
            this.colQty.MinimumWidth = 6;
            this.colQty.Name = "colQty";
            this.colQty.Width = 120;
            // 
            // colSubtotal
            // 
            this.colSubtotal.HeaderText = "Subtotal";
            this.colSubtotal.MinimumWidth = 6;
            this.colSubtotal.Name = "colSubtotal";
            this.colSubtotal.Width = 120;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(171, 383);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Total:";
            // 
            // lblGrandTotal
            // 
            this.lblGrandTotal.AutoSize = true;
            this.lblGrandTotal.Location = new System.Drawing.Point(218, 383);
            this.lblGrandTotal.Name = "lblGrandTotal";
            this.lblGrandTotal.Size = new System.Drawing.Size(44, 16);
            this.lblGrandTotal.TabIndex = 12;
            this.lblGrandTotal.Text = "label5";
            // 
            // CashierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FloralWhite;
            this.ClientSize = new System.Drawing.Size(1081, 587);
            this.Controls.Add(this.lblGrandTotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numQty);
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
            ((System.ComponentModel.ISupportInitialize)(this.numQty)).EndInit();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn colQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSubtotal;
        private System.Windows.Forms.NumericUpDown numQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblGrandTotal;
    }
}