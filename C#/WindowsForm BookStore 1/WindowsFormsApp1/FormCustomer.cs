/* 
* Customer Form for Adding a new customer and save existing customer updated information.
* When the save button is pressed, the program should make sure that none of the text boxes are left blank, 
* Use regular expressions to ensure the first name, last name, address, city, state, zip code, phone, and email are valid.
* If a text box is blank or invalid a messagebox should be displayed, and the focus should be set to the invalid textbox.
* If all of the textboxes are valid then the information will be taken out of the textboxes and placed into a file named customers.txt. 
* After this the combo box should reflect any updates to existing customer’s first name or last name as well as adding any new customer to that list.
* When the new customer button is pressed the form will set the combo box enable property to false and then focus on the first name textbox field ensuring that tab order is correct.
 *After running the code for the customer data need to be entered in order to save the data into the list and into the file
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class FormCustomer : Form
    {

       
        // Declare the list for the customer objects 
        List<Customer> customerList = new List<Customer>();

      
        const char DELIMITER = ',';
        const String FILECUSTOMER = "customers.txt";


        public FormCustomer()
        {
            InitializeComponent();
            comboBoxCustomer.Text = "Edit an Existing Customer";
        
        }

       /// <summary>
       /// Click event for Save button to save the new customer data or updated existing customer
       /// 
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>

        private void btnSave_Click(object sender, EventArgs e)
            {



            // Define method isValidEntry to check the text boxes are not empty
            // Define methods to check the text boxes are valid Use regular expressions
            if (isValidEntry(txtFirstName)&& isValidEntry(txtLastName)&& isValidEntry(txtAddress)&& isValidEntry(txtCity) && isValidEntry(txtZip) && isValidEntry(txtPhone) && isValidEntry(txtEmail) && isValidName(txtFirstName) && isValidName(txtLastName) && isValidAddress(txtAddress) && isValidZipCode(txtZip) && isValidState(txtState) && isValidCity(txtCity) && isValidPhone(txtPhone) && isValidEmail(txtEmail)) { 

                    try
                    {

                        FileStream outFile;
                        StreamWriter writer;

                        //Opens the customer.txt file to write if it exists, or creates a new file if it doen't exist

                        outFile = new FileStream(FILECUSTOMER, FileMode.Append, FileAccess.Write);

                        writer = new StreamWriter(outFile);

                  
                       // check if the customer already is in the list, if not create a new customer object  
                        if (!customerList.Any(item => item.FirstName == txtFirstName.Text && item.LastName == txtLastName.Text)){

                        // Create new customer object using information taken out of the textboxes
                        Customer customer = new Customer(txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtCity.Text, txtState.Text, txtZip.Text, txtPhone.Text, txtEmail.Text);
                        // Add customer object to the customer list    
                        customerList.Add(customer);

                        MessageBox.Show("New Customer added");

                        // Customer information is saved into the file
                        writer.WriteLine(customer.FirstName + DELIMITER + customer.LastName + DELIMITER + customer.Address + DELIMITER + customer.City + DELIMITER + customer.State + DELIMITER + customer.ZipCode + DELIMITER + customer.Phone + DELIMITER + customer.Email);
                         // customer first name and last name added to the combo box 
                         comboBoxCustomer.Items.Add(customer.FirstName + " " + customer.LastName);
                          
                        }


                    // If the customer already exist update the exisiting customer’s data for the customer's first name or last name
                    else if  (customerList.Any(item => item.FirstName == txtFirstName.Text || item.LastName == txtLastName.Text))
                            {

                        foreach (Customer customer in customerList)
                        {
                            if (customer.FirstName == txtFirstName.Text || customer.LastName == txtLastName.Text)
                            {
                                //check if the text boxes data is different from customer's information, update the customer data  
                                if (!customer.State.Equals(txtState.Text) ||
                                !customer.ZipCode.Equals(txtZip.Text) ||
                                 !customer.Phone.Equals(txtPhone.Text) ||
                                  !customer.Address.Equals(txtAddress.Text) ||
                                 !customer.Email.Equals(txtEmail.Text) ||
                                  !customer.City.Equals(txtCity.Text))
                                {
                                    customer.State = txtState.Text;
                                    customer.Address = txtAddress.Text;
                                    customer.ZipCode = txtZip.Text;
                                    customer.State = txtState.Text;
                                    customer.City = txtCity.Text;
                                    customer.Email = txtEmail.Text;
                                    customer.Phone = txtPhone.Text;

                                    MessageBox.Show("The existing customer data has been updated");
                                    writer.WriteLine(customer.FirstName + DELIMITER + customer.LastName + DELIMITER + customer.Address + DELIMITER + customer.City + DELIMITER + customer.State + DELIMITER + customer.ZipCode + DELIMITER + customer.Phone + DELIMITER + customer.Email);
                                }

                            }
                        }

                            

                        }


                        comboBoxCustomer.Enabled = true;

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
            if (!Regex.Match(txtBox.Text, @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$").Success)

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

            foreach (var oCustomer in customerList)
            {
                if (comboBoxCustomer.SelectedItem.Equals(oCustomer.FirstName + " " + oCustomer.LastName))
                {
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
