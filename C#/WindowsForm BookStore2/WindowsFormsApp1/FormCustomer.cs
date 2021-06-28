
/***
Atiehalsadat Kashanimoghaddam
RED ID: 817365647
COMPE 561 
Lab 5
 ***/
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;
using MySql.Data.MySqlClient;


namespace WindowsFormsApp1
{
    /* Class Form customer to manage customers
    * Create Database connection and load customer combobox from database
   * Cancel adding book or updating customer
   * Back to main menu
   * The new customer Button should clear the fields of the textboxes and set the combo box enable to false.
    * The Save Button will do two different things
    * First this button will need to if editing a selected customer in the database it will call the sqlcommand update and update the record that exists there will need to be validation that changes have been made to the current customer.
    * Second if the New customer button has been pressed then the new information for the customer should be INSERTED into database.
    * For both of these options a messsage box should display if the user wants to complete the actions they have selected and then perform them 
     * */

    public partial class FormCustomer : Form
    {

        // Declare list for customer objects
        ArrayList customerList = new ArrayList();
        //Declare variable to check if New Customer button is pressed
        bool newBtnCLicked = false;
        MySqlConnection databaseConnection = new MySqlConnection("datasource=localhost; database=bookstore; username=root; password=");


        // load the customer combobox from database 

        void loadCombobox()

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






        public FormCustomer()
        {
            InitializeComponent();
            loadCombobox();
            comboBoxCustomer.Text = "Edit an Existing Customer";

        }

        /// <summary>
        /// Click event for Save button to save the new customer data or updated existing customer based on checking if New Customer button is pressed or not
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnSave_Click(object sender, EventArgs e)
        {

            // method isValidEntry to check the text boxes are not empty
            // Define methods to check the each text boxe is valid Use regular expressions
            if (isValidEntry(txtFirstName) && isValidEntry(txtLastName) && isValidEntry(txtAddress) && isValidEntry(txtCity) && isValidEntry(txtZip) && isValidEntry(txtPhone) && isValidEntry(txtEmail) && isValidName(txtFirstName) && isValidName(txtLastName) && isValidAddress(txtAddress) && isValidZipCode(txtZip) && isValidState(txtState) && isValidCity(txtCity) && isValidPhone(txtPhone) && isValidEmail(txtEmail))
            {

                /// Check if new button is clicked then the new information for the customer should be INSERTED into database

                if (newBtnCLicked == true)
                {

                    string insertQuery = null;
                    if (DialogResult.Yes == MessageBox.Show("Do you want to create a new Customer?", "Save Customer Book Information?", MessageBoxButtons.YesNo))
                    {


                        insertQuery = "INSERT INTO bs_customer(cust_pid,first_name,last_name,Address,City, State, Zip, Phone, Email) VALUES ('" + (comboBoxCustomer.Items.Count + 1).ToString() + "','" + txtFirstName.Text + "','" + txtLastName.Text + "','" + txtAddress.Text + "','" + txtCity.Text + "','" + txtState.Text + "','" + txtZip.Text + "','" + txtPhone.Text + "','" + txtEmail.Text + "')";
                        databaseConnection.Open();
                       
                    }

                    try
                    {

                        MySqlCommand commandDatabase = new MySqlCommand(insertQuery, databaseConnection);
                        MySqlDataReader myReader = commandDatabase.ExecuteReader();
                        MessageBox.Show("New Customer Added");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error:" + ex.Message);
                    }
                    databaseConnection.Close();
                    this.Hide();
                    FormCustomer bookForm = new FormCustomer();
                    bookForm.ShowDialog();
                    this.Close();

                }



                // If new button is not clicked, edit a selected customer information , an existing customer in the database,  it will call the sqlcommand update and update the record that exists there 
                else
                {

                    string insertQuery;
                    if (DialogResult.Yes == MessageBox.Show("Do you want to save Customer information?", "Update Customer Information", MessageBoxButtons.YesNo))
                    {

                        insertQuery = "UPDATE bs_customer SET first_name= '" + txtFirstName.Text + "',last_name='" + txtLastName.Text + "',Address='" + txtAddress.Text + "',City='" + txtCity.Text + "',State='" + txtState.Text + "',Zip='" + txtZip.Text + "',Phone='" + txtPhone.Text + "',Email='" + txtEmail.Text + "'WHERE cust_pid=' " + (comboBoxCustomer.SelectedIndex + 1).ToString() + "'";
                
                        databaseConnection.Open();
                      

                        try
                        {

                            MySqlCommand commandDatabase = new MySqlCommand(insertQuery, databaseConnection);
                            MySqlDataReader myReader = commandDatabase.ExecuteReader();
                            MessageBox.Show("Customer Information Updated");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error:" + ex.Message);
                        }
                        databaseConnection.Close();
                        this.Hide();
                        FormCustomer bookCustomer = new FormCustomer();
                        bookCustomer.ShowDialog();
                        this.Close();

                    }
                }
            }
    
    }

          
            
        /// <summary>
        /// check the textbox is not empty 
        /// </summary>
        /// <param name="txtBox"></param>
        /// <returns></returns>
            private Boolean isValidEntry(TextBox txtBox)
            {
                
                if (string.IsNullOrEmpty(txtBox.Text))
                {
                MessageBox.Show("Please fill all the input");
                txtBox.Focus();
                    return false;

                }
                return true;


            }
        /// <summary>
        /// check the name format is valid using regular expression
        /// </summary>
        /// <param name="txtBox"></param>
        /// <returns></returns>
        private Boolean isValidName(TextBox txtBox)
        {
            if (!Regex.Match(txtBox.Text, @"^^[A-Za-zÀ-ú]*$").Success) 
            
            {
                MessageBox.Show("Invalid name");
                txtBox.Focus();
                return false;

            }
            return true;

        }


        /// <summary>
        ///  check the address is valid using regular expression. Example of valid address: 123 Sun Drive 
        /// </summary>
        /// <param name="txtBox"></param>
        /// <returns></returns>
        private Boolean isValidAddress(TextBox txtBox)
        {
            if (!Regex.Match(txtBox.Text, @"^[0-9]+\s+([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)$").Success)

            {
                MessageBox.Show("Invalid address");
                txtBox.Focus();
                return false;

            }
            return true;

        }

        /// <summary>
        ///  check the zipcode is valid using regular expression ( 5 digit zipcode) . Example of valid zip code: 12345
        /// </summary>
        /// <param name="txtBox"></param>
        /// <returns></returns>
        private Boolean isValidZipCode(TextBox txtBox)
        {
            if  (!Regex.Match(txtBox.Text, @"^\d{5}$").Success)  

            {

                MessageBox.Show("Invalid zip code");
                txtBox.Focus();
                return false;

            }
            return true;

        }


        /// <summary>
        ///  check the phone numebr is valid using regular expression. Example of valid phone: xxx-xxx-xxxx 
        /// </summary>
        /// <param name="txtBox"></param>
        /// <returns></returns>
        private Boolean isValidPhone(TextBox txtBox)
        {
            if (!Regex.Match(txtBox.Text, @"^\d{10}$").Success)

            {

                MessageBox.Show("Invalid phone number");
                txtBox.Focus();
                return false;

            }
            return true;

        }

        /// <summary>
        ///  check the name of the city is valid
        /// </summary>
        /// <param name="txtBox"></param>
        /// <returns></returns>

        private Boolean isValidCity(TextBox txtBox)
        {
            if  (!Regex.Match(txtBox.Text, @"^([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)$").Success)

                {

                MessageBox.Show("Invalid city");
                txtBox.Focus();
                return false;

            }
            return true;

        }

        /// <summary>
        /// check the state is valid
        /// </summary>
        /// <param name="txtBox"></param>
        /// <returns></returns>
        private Boolean isValidState(TextBox txtBox)
        {
            if (!Regex.Match(txtBox.Text, @"^([a-zA-Z]+|[a-zA-Z]+\s[a-zA-Z]+)$").Success)

            {
                MessageBox.Show("Invalid state");
                txtBox.Focus();
                return false;

            }
            return true;

        }

        /// <summary>
        /// Check the email address format is valid
        /// </summary>
        /// <param name="txtBox"></param>
        /// <returns></returns>
        private Boolean isValidEmail(TextBox txtBox)
        {
            if (!Regex.Match(txtBox.Text, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$").Success)

            {
                MessageBox.Show("Invalid email address");
                txtBox.Focus();
                return false;

            }
            return true;

        }




        /// <summary>
        /// Click event for New Customer button 
        /// Set the combo box enable property to false 
        /// Focus on the first name textbox field 
        /// Set the tab order for the text boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewCustomer_Click(object sender, EventArgs e)
            {
            newBtnCLicked = true;
                comboBoxCustomer.Enabled = false;
                txtFirstName.Focus();
                txtFirstName.TabIndex = 1;
                txtLastName.TabIndex = 2;
                txtAddress.TabIndex = 3;
                txtCity.TabIndex = 4;
                txtState.TabIndex = 5;
                txtZip.TabIndex = 6;
                txtPhone.TabIndex = 7;
                txtEmail.TabIndex = 8;
                txtFirstName.Text ="";
                txtLastName.Text = "";
                txtAddress.Text = "";
                txtCity.Text = "";
                txtState.Text = "";
                txtZip.Text = "";
                txtPhone.Text = "";
                txtEmail.Text = "";
        }



        /// <summary>
        ///  Click event for customer combo box when item is changed to select the exisiting customer from list 
        ///  the text boxes are populate with the selected customer data 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void comboBoxCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {




           Customer oCustomer;    // create a book object
            oCustomer = (Customer)customerList[comboBoxCustomer.SelectedIndex];

                    txtFirstName.Text = oCustomer.FirstName;
                    txtLastName.Text = oCustomer.LastName;
                    txtAddress.Text = oCustomer.Address;
                    txtCity.Text = oCustomer.City;
                    txtState.Text = oCustomer.State;
                    txtZip.Text = oCustomer.ZipCode;
                    txtPhone.Text = oCustomer.Phone;
                    txtEmail.Text = oCustomer.Email;

                   
                

                
            }
        /// <summary>
        /// Click event for back button to back to menu form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnBack_Click(object sender, EventArgs e)
        {

            this.Close();

          

        }
        /// <summary>
        /// Click event for cancel to cancel adding new customer or updaitg the exciting customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnCancel_Click(object sender, EventArgs e)
        {

            if (DialogResult.Yes == MessageBox.Show("Do you want to cancel?", "Cancel?", MessageBoxButtons.YesNo))
            {
                if (comboBoxCustomer.SelectedIndex < 0)
                {

                    txtFirstName.Text = "";
                    txtLastName.Text = "";
                    txtAddress.Text = "";
                    txtCity.Text = "";
                    txtState.Text = "";
                    txtZip.Text = "";
                    txtPhone.Text = "";
                    txtEmail.Text = "";

                }
                else
                {
                    Customer oCustomer = (Customer)customerList[comboBoxCustomer.SelectedIndex];
                    txtFirstName.Text = oCustomer.FirstName;
                    txtLastName.Text = oCustomer.LastName;
                    txtAddress.Text = oCustomer.Address;
                    txtCity.Text = oCustomer.City;
                    txtState.Text = oCustomer.State;
                    txtZip.Text = oCustomer.ZipCode;
                    txtPhone.Text = oCustomer.Phone;
                    txtEmail.Text = oCustomer.Email;


                }



            }

        }


    } 
    }

