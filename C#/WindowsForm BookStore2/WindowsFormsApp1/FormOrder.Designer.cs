namespace WindowsFormsApp1
{
    partial class FormOrder
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
            this.txtISBN = new System.Windows.Forms.TextBox();
            this.txtAuthor = new System.Windows.Forms.TextBox();
            this.labelISBN = new System.Windows.Forms.Label();
            this.labelAuthor = new System.Windows.Forms.Label();
            this.labelPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.labelQuantity = new System.Windows.Forms.Label();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnAddTitle = new System.Windows.Forms.Button();
            this.dgOrderSummary = new System.Windows.Forms.DataGridView();
            this.ColumnTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnQTY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnLineTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelSubTotal = new System.Windows.Forms.Label();
            this.txtSubTotal = new System.Windows.Forms.TextBox();
            this.labelTax = new System.Windows.Forms.Label();
            this.txtTax = new System.Windows.Forms.TextBox();
            this.labelTotal = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.btnConfirmOrder = new System.Windows.Forms.Button();
            this.btnCancelOrder = new System.Windows.Forms.Button();
            this.labelOrderSummary = new System.Windows.Forms.Label();
            this.comboBoxTitle = new System.Windows.Forms.ComboBox();
            this.comboBoxCustomer = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrderSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // txtISBN
            // 
            this.txtISBN.Location = new System.Drawing.Point(437, 158);
            this.txtISBN.Margin = new System.Windows.Forms.Padding(1);
            this.txtISBN.Name = "txtISBN";
            this.txtISBN.Size = new System.Drawing.Size(112, 32);
            this.txtISBN.TabIndex = 22;
            // 
            // txtAuthor
            // 
            this.txtAuthor.Location = new System.Drawing.Point(201, 158);
            this.txtAuthor.Margin = new System.Windows.Forms.Padding(1);
            this.txtAuthor.Name = "txtAuthor";
            this.txtAuthor.Size = new System.Drawing.Size(101, 32);
            this.txtAuthor.TabIndex = 21;
            // 
            // labelISBN
            // 
            this.labelISBN.AutoSize = true;
            this.labelISBN.Location = new System.Drawing.Point(368, 164);
            this.labelISBN.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelISBN.Name = "labelISBN";
            this.labelISBN.Size = new System.Drawing.Size(58, 26);
            this.labelISBN.TabIndex = 2;
            this.labelISBN.Text = "ISBN:";
            // 
            // labelAuthor
            // 
            this.labelAuthor.AutoSize = true;
            this.labelAuthor.Location = new System.Drawing.Point(122, 161);
            this.labelAuthor.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelAuthor.Name = "labelAuthor";
            this.labelAuthor.Size = new System.Drawing.Size(77, 26);
            this.labelAuthor.TabIndex = 3;
            this.labelAuthor.Text = "Author:";
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(249, 201);
            this.labelPrice.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(58, 26);
            this.labelPrice.TabIndex = 5;
            this.labelPrice.Text = "Price:";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(322, 198);
            this.txtPrice.Margin = new System.Windows.Forms.Padding(1);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(104, 32);
            this.txtPrice.TabIndex = 4;
            // 
            // labelQuantity
            // 
            this.labelQuantity.AutoSize = true;
            this.labelQuantity.Location = new System.Drawing.Point(314, 238);
            this.labelQuantity.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelQuantity.Name = "labelQuantity";
            this.labelQuantity.Size = new System.Drawing.Size(88, 26);
            this.labelQuantity.TabIndex = 6;
            this.labelQuantity.Text = "Quantity";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(322, 265);
            this.txtQuantity.Margin = new System.Windows.Forms.Padding(1);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(71, 32);
            this.txtQuantity.TabIndex = 7;
            // 
            // btnAddTitle
            // 
            this.btnAddTitle.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAddTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAddTitle.Location = new System.Drawing.Point(300, 305);
            this.btnAddTitle.Margin = new System.Windows.Forms.Padding(1);
            this.btnAddTitle.Name = "btnAddTitle";
            this.btnAddTitle.Size = new System.Drawing.Size(116, 25);
            this.btnAddTitle.TabIndex = 8;
            this.btnAddTitle.Text = "Add Title";
            this.btnAddTitle.UseVisualStyleBackColor = false;
            this.btnAddTitle.Click += new System.EventHandler(this.btnAddTitle_Click);
            // 
            // dgOrderSummary
            // 
            this.dgOrderSummary.AllowUserToAddRows = false;
            this.dgOrderSummary.ColumnHeadersHeight = 40;
            this.dgOrderSummary.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnTitle,
            this.ColumnPrice,
            this.ColumnQTY,
            this.ColumnLineTotal});
            this.dgOrderSummary.Location = new System.Drawing.Point(138, 381);
            this.dgOrderSummary.Margin = new System.Windows.Forms.Padding(1);
            this.dgOrderSummary.Name = "dgOrderSummary";
            this.dgOrderSummary.RowHeadersVisible = false;
            this.dgOrderSummary.RowHeadersWidth = 72;
            this.dgOrderSummary.RowTemplate.Height = 31;
            this.dgOrderSummary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgOrderSummary.Size = new System.Drawing.Size(429, 117);
            this.dgOrderSummary.TabIndex = 20;
            // 
            // ColumnTitle
            // 
            this.ColumnTitle.HeaderText = "Title";
            this.ColumnTitle.MinimumWidth = 9;
            this.ColumnTitle.Name = "ColumnTitle";
            this.ColumnTitle.Width = 175;
            // 
            // ColumnPrice
            // 
            this.ColumnPrice.HeaderText = "Price";
            this.ColumnPrice.MinimumWidth = 9;
            this.ColumnPrice.Name = "ColumnPrice";
            this.ColumnPrice.Width = 175;
            // 
            // ColumnQTY
            // 
            this.ColumnQTY.HeaderText = "QTY";
            this.ColumnQTY.MinimumWidth = 9;
            this.ColumnQTY.Name = "ColumnQTY";
            this.ColumnQTY.Width = 175;
            // 
            // ColumnLineTotal
            // 
            this.ColumnLineTotal.HeaderText = "Line Total";
            this.ColumnLineTotal.MinimumWidth = 9;
            this.ColumnLineTotal.Name = "ColumnLineTotal";
            this.ColumnLineTotal.Width = 175;
            // 
            // labelSubTotal
            // 
            this.labelSubTotal.AutoSize = true;
            this.labelSubTotal.Location = new System.Drawing.Point(93, 519);
            this.labelSubTotal.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelSubTotal.Name = "labelSubTotal";
            this.labelSubTotal.Size = new System.Drawing.Size(99, 26);
            this.labelSubTotal.TabIndex = 11;
            this.labelSubTotal.Text = "Sub Total:";
            // 
            // txtSubTotal
            // 
            this.txtSubTotal.Location = new System.Drawing.Point(219, 516);
            this.txtSubTotal.Margin = new System.Windows.Forms.Padding(1);
            this.txtSubTotal.Name = "txtSubTotal";
            this.txtSubTotal.Size = new System.Drawing.Size(57, 32);
            this.txtSubTotal.TabIndex = 10;
            // 
            // labelTax
            // 
            this.labelTax.AutoSize = true;
            this.labelTax.Location = new System.Drawing.Point(306, 518);
            this.labelTax.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelTax.Name = "labelTax";
            this.labelTax.Size = new System.Drawing.Size(45, 26);
            this.labelTax.TabIndex = 13;
            this.labelTax.Text = "Tax:";
            // 
            // txtTax
            // 
            this.txtTax.Location = new System.Drawing.Point(353, 515);
            this.txtTax.Margin = new System.Windows.Forms.Padding(1);
            this.txtTax.Name = "txtTax";
            this.txtTax.Size = new System.Drawing.Size(57, 32);
            this.txtTax.TabIndex = 12;
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Location = new System.Drawing.Point(443, 518);
            this.labelTotal.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(60, 26);
            this.labelTotal.TabIndex = 15;
            this.labelTotal.Text = "Total:";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(505, 515);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(1);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(57, 32);
            this.txtTotal.TabIndex = 14;
            // 
            // btnConfirmOrder
            // 
            this.btnConfirmOrder.Location = new System.Drawing.Point(232, 560);
            this.btnConfirmOrder.Margin = new System.Windows.Forms.Padding(1);
            this.btnConfirmOrder.Name = "btnConfirmOrder";
            this.btnConfirmOrder.Size = new System.Drawing.Size(91, 34);
            this.btnConfirmOrder.TabIndex = 16;
            this.btnConfirmOrder.Text = "Confirm Order";
            this.btnConfirmOrder.UseVisualStyleBackColor = true;
            this.btnConfirmOrder.Click += new System.EventHandler(this.btnConfirmOrder_Click);
            // 
            // btnCancelOrder
            // 
            this.btnCancelOrder.Location = new System.Drawing.Point(371, 560);
            this.btnCancelOrder.Margin = new System.Windows.Forms.Padding(1);
            this.btnCancelOrder.Name = "btnCancelOrder";
            this.btnCancelOrder.Size = new System.Drawing.Size(91, 34);
            this.btnCancelOrder.TabIndex = 17;
            this.btnCancelOrder.Text = "Cancel Order";
            this.btnCancelOrder.UseVisualStyleBackColor = true;
            this.btnCancelOrder.Click += new System.EventHandler(this.btnCancelOrder_Click);
            // 
            // labelOrderSummary
            // 
            this.labelOrderSummary.AutoSize = true;
            this.labelOrderSummary.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelOrderSummary.Location = new System.Drawing.Point(248, 348);
            this.labelOrderSummary.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.labelOrderSummary.Name = "labelOrderSummary";
            this.labelOrderSummary.Size = new System.Drawing.Size(214, 32);
            this.labelOrderSummary.TabIndex = 18;
            this.labelOrderSummary.Text = "Order Summary";
            // 
            // comboBoxTitle
            // 
            this.comboBoxTitle.FormattingEnabled = true;
            this.comboBoxTitle.Location = new System.Drawing.Point(198, 36);
            this.comboBoxTitle.Margin = new System.Windows.Forms.Padding(1);
            this.comboBoxTitle.Name = "comboBoxTitle";
            this.comboBoxTitle.Size = new System.Drawing.Size(351, 32);
            this.comboBoxTitle.TabIndex = 19;
            this.comboBoxTitle.SelectedIndexChanged += new System.EventHandler(this.comboBoxTitle_SelectedIndexChanged);
            // 
            // comboBoxCustomer
            // 
            this.comboBoxCustomer.FormattingEnabled = true;
            this.comboBoxCustomer.Location = new System.Drawing.Point(198, 92);
            this.comboBoxCustomer.Margin = new System.Windows.Forms.Padding(1);
            this.comboBoxCustomer.Name = "comboBoxCustomer";
            this.comboBoxCustomer.Size = new System.Drawing.Size(351, 32);
            this.comboBoxCustomer.TabIndex = 23;
          //  this.comboBoxCustomer.SelectedIndexChanged += new System.EventHandler(this.comboBoxCustomer_SelectedIndexChanged);
            // 
            // FormOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(705, 621);
            this.Controls.Add(this.comboBoxCustomer);
            this.Controls.Add(this.comboBoxTitle);
            this.Controls.Add(this.labelOrderSummary);
            this.Controls.Add(this.btnCancelOrder);
            this.Controls.Add(this.btnConfirmOrder);
            this.Controls.Add(this.labelTotal);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.labelTax);
            this.Controls.Add(this.txtTax);
            this.Controls.Add(this.labelSubTotal);
            this.Controls.Add(this.txtSubTotal);
            this.Controls.Add(this.dgOrderSummary);
            this.Controls.Add(this.btnAddTitle);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.labelQuantity);
            this.Controls.Add(this.labelPrice);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.labelAuthor);
            this.Controls.Add(this.labelISBN);
            this.Controls.Add(this.txtAuthor);
            this.Controls.Add(this.txtISBN);
            this.Font = new System.Drawing.Font("Microsoft PhagsPa", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "FormOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Book Order Form ";
            ((System.ComponentModel.ISupportInitialize)(this.dgOrderSummary)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtISBN;
        private System.Windows.Forms.TextBox txtAuthor;
        private System.Windows.Forms.Label labelISBN;
        private System.Windows.Forms.Label labelAuthor;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label labelQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnAddTitle;
        private System.Windows.Forms.DataGridView dgOrderSummary;
        private System.Windows.Forms.Label labelSubTotal;
        private System.Windows.Forms.TextBox txtSubTotal;
        private System.Windows.Forms.Label labelTax;
        private System.Windows.Forms.TextBox txtTax;
        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Button btnConfirmOrder;
        private System.Windows.Forms.Button btnCancelOrder;
        private System.Windows.Forms.Label labelOrderSummary;
        private System.Windows.Forms.ComboBox comboBoxTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnQTY;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnLineTotal;
        private System.Windows.Forms.ComboBox comboBoxCustomer;
    }
}

