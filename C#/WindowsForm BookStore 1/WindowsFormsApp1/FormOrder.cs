/***
Atiehalsadat Kashanimoghaddam
RED ID: 817365647
COMPE 561 
Lab 2 : Book Store Part  3
Create Text File called book.txt to hold book objects
• Write a loop to process all books into an ArrayList that comes from the IO
• Write statements to load the Combo box with book items
• Use a try/catch to handle IO Errors
• Write the order to a file called orders.txt when the complete order button is click add proper IO error handling to ensure that no errors will be displayed when writing the information to the file
 
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

namespace WindowsFormsApp1
{
    public partial class FormOrder : Form
    {

        // Declare Arraylist to hold books list 

        ArrayList bookList = new ArrayList();

        // Declare variables for subTotal, tax and total values 
        // Declare variables for delimiter, and const variables for book and orders text files

        double dSubTotal;
        double tax;
        double total;
        const char DELIMITER = ',';
        const String FILEBOOK = "book.txt";
        const String FILEORDER = "orders.txt";




        public FormOrder()
        {
            InitializeComponent();
            dgOrderSummary.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        }


        private void Form1_Load(object sender, EventArgs e)
        {

            //open the input file for reading and declare a StreamReader object 
            try
            {

                FileStream inFile = new FileStream(FILEBOOK, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(inFile);

                // declare a string for records and a string array for fields
                String recordIn;
                String[] fields;

                // read the first record from the input file
                recordIn = reader.ReadLine();


                //while valid records come from the input file process them   
                while (recordIn != null)
                {
                    // create book object and 
                    Book bk = new Book();
                    // split the fields in the record
                    fields = recordIn.Split(DELIMITER);
                    // set the book object properties 
                    bk.Title = fields[0];
                    bk.Author = fields[1];
                    bk.ISBN = fields[2];
                    bk.Price = Convert.ToDouble(fields[3]);
                    // Add book object to the book arraylist
                    bookList.Add(bk);
                    recordIn = reader.ReadLine();
                }
                reader.Close();
                inFile.Close();
            }

            catch (FileNotFoundException)
           {
                MessageBox.Show("The file was not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              
            }

            catch (UnauthorizedAccessException)
            {
              
                MessageBox.Show("You do not have permission to create this file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("The file directory could not be found ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            catch (IOException)
            {
                MessageBox.Show("The file could not be opened.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }




           


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
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnConfirmOrder_Click(object sender, EventArgs e)
        {      

            if (dgOrderSummary.RowCount >= 1)
            {


                try
                {
                    FileStream outFile;
                    StreamWriter writer;

                   //Opens the orders.txt file to write if it exists and seeks to the end of the file, or creates a new file if it doen't exist

                    outFile = new FileStream(FILEORDER, FileMode.Append, FileAccess.Write);
          
                    writer = new StreamWriter(outFile);

                    // write the order information into the file 
                    writer.WriteLine(comboBoxTitle.SelectedItem.ToString()+ DELIMITER + txtAuthor.Text + DELIMITER + txtISBN.Text + DELIMITER + txtPrice.Text + DELIMITER + txtQuantity.Text + DELIMITER + txtTotal.Text + DELIMITER + txtTax.Text + DELIMITER + txtSubTotal.Text);


                    writer.Close();
                    outFile.Close();

                }

               catch (FileNotFoundException)
                {
                   MessageBox.Show("The file was not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    
                }
                catch (UnauthorizedAccessException)
                {

                    MessageBox.Show("You do not have permission to create this file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                catch (DirectoryNotFoundException)
                {
                    MessageBox.Show("The file directory could not be found ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                catch (IOException)
                {
                    MessageBox.Show("The file could not be opened.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }



                MessageBox.Show("You have a placed order");
                    txtAuthor.Text = "";
                    txtISBN.Text = "";
                    txtPrice.Text = "";
                    txtQuantity.Text = "";
                    txtTotal.Text = "";
                    txtTax.Text = "";
                    txtSubTotal.Text = "";
                    dgOrderSummary.Rows.Clear();


                
            }
            // without adding any books to the order, a messagebox will ask to add a book
            else
                MessageBox.Show("Please add a book");
                comboBoxTitle.Focus();  // set focus to title combo box


        }

       
    }
}
