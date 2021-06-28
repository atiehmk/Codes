/***
Atieh Kashani

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
using System.Collections;
using System.IO;

using MySql.Data.MySqlClient;

namespace WindowsFormsApp1
{
    public partial class FormOrder : Form
    {

        // Declare Arraylist to hold books list 

        ArrayList bookList = new ArrayList();

        // Declare Arraylist to hold customers list 

        ArrayList customerList = new ArrayList();

        Customer oCustomer;

        // Declare variables for subTotal, tax and total values 
        // Declare variables for delimiter, and const variables for book and orders text files

        double dSubTotal;
        double tax;
        double total;
        int cust_id;
        int book_id;
        int order_id;
        


        // Create database connection

        MySqlConnection databaseConnection = new MySqlConnection("datasource=localhost; database=bookstore; username=root; password=");


        //Load book combobox from book tabel and add the books to the list
            void loadComboboxBook()

            {

                try
                {

                    MySqlCommand commandDatabase = new MySqlCommand("SELECT * FROM bs_books", databaseConnection);
                    commandDatabase.CommandTimeout = 60;
                    databaseConnection.Open();
                    MySqlDataReader myReader = commandDatabase.ExecuteReader();
                    while (myReader.Read())
                    {

                        string book = myReader.GetString("Title");
                        comboBoxTitle.Items.Add(book);
                        Book newBook = new Book(myReader.GetString("Title"), myReader.GetString("Author"), myReader.GetString("ISBN"), Convert.ToDouble(myReader.GetString("Price").Replace("$", "")));
                        bookList.Add(newBook);
                    }


                    databaseConnection.Close();


                }
                catch (Exception e)
                {
                    MessageBox.Show("Query error:" + e.Message);
                }



            }

        //Load customer combobox from customer tabel and add the customers to the list
        void loadComboboxCustomer()

        {

            try
            {

                MySqlCommand commandDatabase = new MySqlCommand("SELECT * FROM bs_customer", databaseConnection);
                commandDatabase.CommandTimeout = 60;
                databaseConnection.Open();
                MySqlDataReader myReader = commandDatabase.ExecuteReader();
                while (myReader.Read())
                {

                    string customer = myReader.GetString("first_name") + " " + myReader.GetString("last_name");
                    comboBoxCustomer.Items.Add(customer);
                    Customer newCustomer = new Customer(myReader.GetString("first_name"), myReader.GetString("last_name"), myReader.GetString("Address"), myReader.GetString("City"), myReader.GetString("State"), myReader.GetString("Zip"), myReader.GetString("Phone"), myReader.GetString("Email"));
                    customerList.Add(newCustomer);
                }


                databaseConnection.Close();


            }
            catch (Exception e)
            {
                MessageBox.Show("Query error:" + e.Message);
            }



        }


        public FormOrder()
        {
            InitializeComponent();
            loadComboboxBook();
            loadComboboxCustomer();
            dgOrderSummary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }



        private void comboBoxTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            Book oBook;    // create a book object
            oBook = (Book)bookList[comboBoxTitle.SelectedIndex];  // get the book object from the comboBox 
            // get the author name, ISBN and price of the selected book and fill the realted text boxes
            txtAuthor.Text = oBook.Author;
            txtISBN.Text = oBook.ISBN;
            txtPrice.Text = oBook.Price.ToString("C");
    
        }

        /// <summary>
        ///  Click event for Cancel order button to cancel the order request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnAddTitle_Click(object sender, EventArgs e)
        {
            // check if no book is selected from combo box or the author text box is empty

            if (comboBoxTitle.SelectedItem == null || txtAuthor.Text == "")
            {
                //Display message
                MessageBox.Show("Please select a book");
                comboBoxTitle.Focus(); //focus will be set to title combo box
            }

             // check if the quantity text box is null or empty or contains white space or the number for quantity of book is invalid
            else if  (string.IsNullOrEmpty(txtQuantity.Text) || string.IsNullOrWhiteSpace(txtQuantity.Text) || Convert.ToInt32(txtQuantity.Text)< 1)
                {
                MessageBox.Show("Please enter a valid number for quantity");
                txtQuantity.Focus(); // focus will be set to quantity text box
            }

            else
            {

                Book oBook = (Book)bookList[comboBoxTitle.SelectedIndex]; 
              
                
                double lineTotal = oBook.Price * Convert.ToInt32(txtQuantity.Text); // Calculat the line total price that is the multiplication of the quantity and price
                dgOrderSummary.Rows.Add(oBook.Title, oBook.Price.ToString("C"), txtQuantity.Text, lineTotal.ToString("C")); // Add the book title, price, quantity and totalbook price to data grid view 

                // Check if the sub total text only contains dollar sign ( or it is empty ) , set the value of sub total to zero befor calculating the sum of the line totals
                string subTotal = txtSubTotal.Text.Replace("$", "");
                if (subTotal.Equals(""))
                    subTotal = "0";

                //calculate the subtotal (the sum of the line totals) 
                dSubTotal = Convert.ToDouble(subTotal);
                dSubTotal += lineTotal;
                txtSubTotal.Text = dSubTotal.ToString("C");

                //calculate tax (10% of the subtotal)
                tax = dSubTotal * .1;
                txtTax.Text = tax.ToString("C");

                //calculate total price (sub total + tax)
                total = dSubTotal + tax;
                txtTotal.Text = total.ToString("C");


            }


        }
        /// <summary>
        /// Click event for Cancel order button to cancel the order request
        /// It displays a cancel order confirm message
        /// If click yes, the DataGridView andn the text boxes will be cleared out, the tax, subtotal, and total will be set back to zero 
        /// If click no, nothing will happen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelOrder_Click(object sender, EventArgs e)
        {  
            if (DialogResult.Yes ==MessageBox.Show("Do you really want to cancel the order?","Cancel order?", MessageBoxButtons.YesNo))
            {

                txtAuthor.Text = "";
                txtISBN.Text = "";
                txtPrice.Text = "";
                txtQuantity.Text = "";
                txtTotal.Text = "";
                txtTax.Text = "";
                txtSubTotal.Text = "";
                dgOrderSummary.Rows.Clear();
                tax = 0;
                total = 0;
                dSubTotal = 0;

            }

        }
        /// <summary>
        /// Click event for Confirm order button to confirm the order request
        /// It displays a confirm order message. The DataGridView and the text boxes will be cleared out  
        /// If no book is added to the order, a messagebox will ask to add order
        /// The order will be inserted in the orders table 
        /// and insert the information in the order_details tabel
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnConfirmOrder_Click(object sender, EventArgs e)
        {

            // check if the customer is selected 
            if (comboBoxCustomer.SelectedItem == null)
            {
                //Display message
                MessageBox.Show("Please select a customer");
                comboBoxTitle.Focus(); //focus will be set to customer combo box
            }

            else if (comboBoxCustomer.SelectedItem != null)
            {
                oCustomer = (Customer)customerList[comboBoxCustomer.SelectedIndex];
            }

            // check if the book is selected 
            if (comboBoxTitle.SelectedItem == null)
            {
                //Display message
                MessageBox.Show("Please select a book");
                comboBoxTitle.Focus(); //focus will be set to book combo box
            }



            if (dgOrderSummary.RowCount >= 1)
                {


                    //Get the today's date
                    DateTime dateTime = DateTime.Now;
                    DateTime dateOnly = dateTime.Date;
                    String date = dateOnly.ToString("yyyy-MM-dd");

                    string insertQuery;

                    if (DialogResult.Yes == MessageBox.Show("Do you want to place an order?", "Place Order", MessageBoxButtons.YesNo))
                    {


                    // Query to get customer id based on the customer's name
                        string query_selectCustomerID = "select cust_pid from bs_customer where first_name = " + "'"
                             + oCustomer.FirstName + "'" + "and last_name = " + "'" + oCustomer.LastName + "'";

                    // Insert query to insert the order information into orders tabel
                        insertQuery = "INSERT INTO bs_orders(sub_total,tax,total,order_date,cust_fid) VALUES ('" + Convert.ToDecimal(txtSubTotal.Text.Replace("$", ""))
                            + "','" + Convert.ToDecimal(txtTax.Text.Replace("$", "")) + "','" + Convert.ToDecimal(txtTotal.Text.Replace("$", "")) + "','" + date + "'," + "(" + query_selectCustomerID + ")" + ")";
                        databaseConnection.Open();



                        try
                        {

                            MySqlCommand commandDatabase = new MySqlCommand(insertQuery, databaseConnection);
                            MySqlDataReader myReader = commandDatabase.ExecuteReader();



                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:" + ex.Message);
                        }


                        databaseConnection.Close();


                        Book oBook = (Book)bookList[comboBoxTitle.SelectedIndex];

                        // Quesry to select  book id 
                        string query_selectBookID = "select book_pid from bs_books where title = " + "'" + oBook.Title + "'" + " AND " + "isbn = " + "'" + oBook.ISBN + "'";

                        databaseConnection.Open();

                        try
                        {

                            MySqlCommand commandDatabase2 = new MySqlCommand(query_selectBookID, databaseConnection);

                            book_id = int.Parse(commandDatabase2.ExecuteScalar() + "");
                            commandDatabase2.Dispose();


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:" + ex.Message);
                        }




                        databaseConnection.Close();



                        databaseConnection.Open();


                        try
                        {

                            MySqlCommand commandDatabase3 = new MySqlCommand(query_selectCustomerID, databaseConnection);
                            cust_id = int.Parse(commandDatabase3.ExecuteScalar() + "");
                            commandDatabase3.Dispose();


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:" + ex.Message);
                        }
                        databaseConnection.Close();



                        // get the last order id for the selected customer which is used to insert the data into the order_details tabel 
                        string query_selectOrderID = "select max(order_pid) from bs_orders where cust_fid = " + cust_id;

                        databaseConnection.Open();

                        try
                        {

                            MySqlCommand commandDatabase4 = new MySqlCommand(query_selectOrderID, databaseConnection);
                            order_id = int.Parse(commandDatabase4.ExecuteScalar() + "");
                            commandDatabase4.Dispose();



                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:" + ex.Message);
                        }




                        databaseConnection.Close();


                       // Insert query to insert the information into order_details tabel
                        string query_insert_orderdetial = "Insert into bs_orderdetails (order_fid, book_fid, quantity, lines_total) Values( " + order_id + "," + book_id + ","
                            + txtQuantity.Text + "," + Convert.ToDecimal(txtTotal.Text.Replace("$", "")) + ")";

                        databaseConnection.Open();


                        try
                        {

                            MySqlCommand commandDatabase5 = new MySqlCommand(query_insert_orderdetial, databaseConnection);
                            MySqlDataReader myReader2 = commandDatabase5.ExecuteReader();



                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:" + ex.Message);
                        }



                        databaseConnection.Close();


                    MessageBox.Show("You have a placed order");

                }
                    
                    else
                    comboBoxTitle.Focus();  // set focus to title combo box
                }

           
        }

       
    }
}
