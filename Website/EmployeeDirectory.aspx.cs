using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace WebApplication6
{
    public partial class EmployeeDirectory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Create sql connection
            MySqlConnection databaseConnection = new MySqlConnection("datasource=localhost; database=glacierpoint; username=root; password=");

            //create command and assign the query and connection 
            MySqlCommand commandDatabase = new MySqlCommand("SELECT EmployeeId, Name, Username FROM gp_Employee", databaseConnection);

            MySqlDataReader reader;
           
            try
            {
                // Open the connection
                databaseConnection.Open();
                // Execute the command
                reader = commandDatabase.ExecuteReader();
                // Bind the reader to the repeater
                employeesRepeater.DataSource = reader;
                employeesRepeater.DataBind();
                // Close the reader
                reader.Close();
            }
            finally
            {
                // Close the connection
                databaseConnection.Close();
            }


        }
    }
}