/***
Atiehalsadat Kashanimoghaddam
RED ID: 817365647
COMPE 561 
Lab 5
 ***/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
   
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void buttonManageCustomers_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormCustomer formCustomer = new FormCustomer();
            formCustomer.ShowDialog();
            this.Show();
        }

        private void buttonManageBooks_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormBook formBook = new FormBook();
            formBook.ShowDialog();
            this.Show();

        }

        private void buttonPlaceOrder_Click(object sender, EventArgs e)
        {
            
                this.Hide();
                FormOrder formOrder = new FormOrder();
                formOrder.ShowDialog();
                this.Show();

            
        }
    }
}
