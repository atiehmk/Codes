/** COMPE 561
 * Lab 7
 * Atiehalsadat Kashanimoghaddam
 * RED ID: 817365647
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using MySql.Data.Types;



namespace WebApplication6
{
    public partial class HelpDesk : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                // Declare objects
                MySqlConnection conn;
                MySqlCommand categoryComm;
                MySqlCommand subjectComm;
                MySqlCommand employeeComm;
                MySqlDataReader reader;

                conn = new MySqlConnection("datasource=localhost; database=glacierpoint; username=root; password=");

                // Create command to read the help desk categories
                categoryComm = new MySqlCommand(
                "SELECT CategoryId, Category FROM gp_HelpDeskCategory",
                conn);
                // Create command to read the help desk subjects
                subjectComm = new MySqlCommand(
                "SELECT SubjectId, Subject FROM gp_HelpDeskSubject", conn);

                // Create command to read the employee 
                employeeComm = new MySqlCommand(
                "SELECT EmployeeId, Name FROM gp_employee", conn);

                try
                {
                    // Open the connection
                    conn.Open();
                    // Execute the category command
                    reader = categoryComm.ExecuteReader();
                    // Populate the list of categories
                    categoryList.DataSource = reader;
                    categoryList.DataValueField = "CategoryId";
                    categoryList.DataTextField = "Category";
                    categoryList.DataBind();
                    // Close the reader
                    reader.Close();
                    // Execute the subject command
                    reader = subjectComm.ExecuteReader();
                    // Populate the list of subjects
                    subjectList.DataSource = reader;
                    subjectList.DataValueField = "SubjectId";
                    subjectList.DataTextField = "Subject";
                    subjectList.DataBind();
                    // Close the reader
                    reader.Close();
                    // Execute the employee command
                    reader = employeeComm.ExecuteReader();
                    // Populate the list of employees
                    employeeList.DataSource = reader;
                    employeeList.DataValueField = "EmployeeId";
                    employeeList.DataTextField = "Name";
                    employeeList.DataBind();
                    // Close the reader
                    reader.Close();
                }
                finally
                {
                    // Close the connection
                    conn.Close();
                }
            }
        }




        protected void submitButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                // Declare objects
                MySqlConnection conn;
                MySqlCommand comm;

               
                conn = new MySqlConnection("datasource=localhost; database=glacierpoint; username=root; password=");

                // Initialize connection
               

                // Create insert command
                comm = new MySqlCommand(
                "INSERT INTO gp_Helpdesk(EmployeeId,StationNumber,CategoryId," + "SubjectId, Description, StatusId) " +
      "VALUES(@EmployeeId, @StationNumber," +
      "@CategoryId, @SubjectId, @Description, @StatusId)", conn);

                // Add command parameters
                comm.Parameters.Add("@EmployeeId", MySqlDbType.Int32);
                comm.Parameters["@EmployeeId"].Value = employeeList.SelectedItem.Value; ;
                comm.Parameters.Add("@StationNumber",
               MySqlDbType.Int32);
                comm.Parameters["@StationNumber"].Value = stationTextBox.Text;
                comm.Parameters.Add("@CategoryId", MySqlDbType.Int32);
                comm.Parameters["@CategoryId"].Value =
                categoryList.SelectedItem.Value;
                comm.Parameters.Add("@SubjectId", MySqlDbType.Int32);
                comm.Parameters["@SubjectId"].Value =
                subjectList.SelectedItem.Value;
                comm.Parameters.Add("@Description",
                MySqlDbType.VarChar, 50);
                comm.Parameters["@Description"].Value =
                descriptionTextBox.Text;
                comm.Parameters.Add("@StatusId", MySqlDbType.Int32);
                comm.Parameters["@StatusId"].Value = 1;
                // Enclose database code in Try-Catch-Finally
                try
                {
                    // Open the connection
                    conn.Open();
                    // Execute the command
                    comm.ExecuteNonQuery();
                    // Reload page if the query executed successfully
                    Response.Redirect("HelpDesk.aspx");
                }
                catch
                {

                   //  Display error message
                   dbErrorMessage.Text =
                     "Error submitting the help desk request! Please " +
                     "try again later, and/or change the entered data!";
                }
                finally
                {
                    // Close the connection
                    conn.Close();
                }
            }
        }
    }

}
