



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
using MySql.Data.MySqlClient;



namespace WindowsFormsApp1

{

    /* Class Form book to manage books
    /* Create Database connectionand load book title combobox from database
     * Cancel adding book or updating book
     * Back to main menu
    * The new book Button should clear the fields of the textboxes and set the combo box enable to false.
   * The Save Button will do two different things
   * First this button will need to if editing a selected book: the Title, Author , Price of an existing book in the database it will call the sqlcommand update and update the record that exists there will need to be validation that changes have been made to the current book.
   * Second if the New Book button has been pressed then the new information for the book should be INSERTED into database.
   * For both of these options a messsage box should display if the user wants to complete the actions they have selected and then perform them 
    * */

    public partial class FormBook : Form
    {

        ArrayList bookList = new ArrayList();
        // Declare variable to check if New Book button is pressed
        bool newBtnCLicked = false;
        MySqlConnection databaseConnection = new MySqlConnection("datasource=localhost; database=bookstore; username=root; password=");

        // load the book combobox from database 
        void loadCombobox()

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
                    comboBoxBook.Items.Add(book);
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




        public FormBook()
        {
            InitializeComponent();
            loadCombobox();
            comboBoxBook.Text = "Edit an Existing Book";
        }

        /// <summary>
        /// Click event for New book button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnNewBook_Click(object sender, EventArgs e)
        {
            comboBoxBook.Enabled = false;
            newBtnCLicked = true;
            txtTitle.Text = "";
            txtAuthor.Text = "";
            txtISBN.Text = "";
            txtPrice.Text = "";




        }

        /// <summary>
        /// Click event for Save button to save the new book data or updated existing book int he database based on checking if New Book button is pressed or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtAuthor.Text) || string.IsNullOrEmpty(txtAuthor.Text) || string.IsNullOrEmpty(txtISBN.Text) || string.IsNullOrEmpty(txtPrice.Text)){

                MessageBox.Show("Please fill all the input");

            }

            else {
                // Check if New button pressed create new book and insert into tabel 
                if (newBtnCLicked == true)
                {

                    string insertQuery = null;
                    if (DialogResult.Yes == MessageBox.Show("Do you want to create a new Book?", "Save new Book Information?", MessageBoxButtons.YesNo))
                    {
                        insertQuery = "INSERT INTO bs_books(Title,Author,ISBN, Price) VALUES ('" + txtTitle.Text + "','" + txtAuthor.Text + "','" + txtISBN.Text + "','" + txtPrice.Text + "')";
                        if (databaseConnection.State != ConnectionState.Open)
                        {
                            databaseConnection.Open();
                        }
                    }

                    try
                    {

                        MySqlCommand commandDatabase = new MySqlCommand(insertQuery, databaseConnection);
                        MySqlDataReader myReader = commandDatabase.ExecuteReader();
                        MessageBox.Show("Book Information Saved");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error:" + ex.Message);
                    }
                    databaseConnection.Close();
                    this.Hide();
                    FormBook bookForm = new FormBook();
                    bookForm.ShowDialog();
                    this.Close();

                }

                // Check if New button not pressed update the existing book 
                else
                {

                    string insertQuery;
                    if (DialogResult.Yes == MessageBox.Show("Do you want to save book information?", "Update Book Information", MessageBoxButtons.YesNo))
                    {
                        Book oBook = (Book)bookList[comboBoxBook.SelectedIndex];
                        insertQuery = "UPDATE bs_books SET Title = '" + txtTitle.Text + "',Author='" + txtAuthor.Text + "',ISBN='" + txtISBN.Text + "',Price='" + txtPrice.Text + "'WHERE Title='" + oBook.Title + "'";
                        if (databaseConnection.State != ConnectionState.Open)
                        {
                            databaseConnection.Open();
                        }

                        try
                        {

                            MySqlCommand commandDatabase = new MySqlCommand(insertQuery, databaseConnection);
                            MySqlDataReader myReader = commandDatabase.ExecuteReader();
                            MessageBox.Show("Book Information Updated");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:" + ex.Message);
                        }
                        databaseConnection.Close();
                        this.Hide();
                        FormBook bookForm = new FormBook();
                        bookForm.ShowDialog();
                        this.Close();

                    }
                }

            }
        }

        private void comboBoxBook_SelectedIndexChanged(object sender, EventArgs e)
        {


            Book oBook;    // create a book object
            oBook = (Book)bookList[comboBoxBook.SelectedIndex];  // get the book object from the comboBox 
                                                                 // get the author name, ISBN and price of the selected book and fill the realted text boxes

            txtTitle.Text = oBook.Title;
            txtAuthor.Text = oBook.Author;
            txtISBN.Text = oBook.ISBN;
            txtPrice.Text = oBook.Price.ToString("C");


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Do you want to cancel?", "Cancel?", MessageBoxButtons.YesNo))
            {
                if (comboBoxBook.SelectedIndex < 0)
                {

                    txtTitle.Text = "";
                    txtAuthor.Text = "";
                    txtISBN.Text = "";
                    txtPrice.Text = "";

                }
                else
                {

                   Book oBook = (Book)bookList[comboBoxBook.SelectedIndex];
                    txtTitle.Text = oBook.Title;
                    txtAuthor.Text = oBook.Author;
                    txtISBN.Text = oBook.ISBN;
                    txtPrice.Text = oBook.Price.ToString("C");


                }



            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

