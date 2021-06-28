namespace WindowsFormsApp1
{
    partial class FormMenu
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
            this.buttonManageCustomers = new System.Windows.Forms.Button();
            this.buttonManageBooks = new System.Windows.Forms.Button();
            this.buttonPlaceOrder = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonManageCustomers
            // 
            this.buttonManageCustomers.Location = new System.Drawing.Point(363, 91);
            this.buttonManageCustomers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonManageCustomers.Name = "buttonManageCustomers";
            this.buttonManageCustomers.Size = new System.Drawing.Size(227, 86);
            this.buttonManageCustomers.TabIndex = 0;
            this.buttonManageCustomers.Text = "Manage Customers";
            this.buttonManageCustomers.UseVisualStyleBackColor = true;
            this.buttonManageCustomers.Click += new System.EventHandler(this.buttonManageCustomers_Click);
            // 
            // buttonManageBooks
            // 
            this.buttonManageBooks.Location = new System.Drawing.Point(363, 220);
            this.buttonManageBooks.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonManageBooks.Name = "buttonManageBooks";
            this.buttonManageBooks.Size = new System.Drawing.Size(227, 89);
            this.buttonManageBooks.TabIndex = 1;
            this.buttonManageBooks.Text = "Manage Books";
            this.buttonManageBooks.UseVisualStyleBackColor = true;
            this.buttonManageBooks.Click += new System.EventHandler(this.buttonManageBooks_Click);
            // 
            // buttonPlaceOrder
            // 
            this.buttonPlaceOrder.Location = new System.Drawing.Point(363, 358);
            this.buttonPlaceOrder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonPlaceOrder.Name = "buttonPlaceOrder";
            this.buttonPlaceOrder.Size = new System.Drawing.Size(227, 85);
            this.buttonPlaceOrder.TabIndex = 2;
            this.buttonPlaceOrder.Text = "Place Order";
            this.buttonPlaceOrder.UseVisualStyleBackColor = true;
            this.buttonPlaceOrder.Click += new System.EventHandler(this.buttonPlaceOrder_Click);
            // 
            // FormMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 540);
            this.Controls.Add(this.buttonPlaceOrder);
            this.Controls.Add(this.buttonManageBooks);
            this.Controls.Add(this.buttonManageCustomers);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FormMenu";
            this.Text = "FormMenu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonManageCustomers;
        private System.Windows.Forms.Button buttonManageBooks;
        private System.Windows.Forms.Button buttonPlaceOrder;
    }
}