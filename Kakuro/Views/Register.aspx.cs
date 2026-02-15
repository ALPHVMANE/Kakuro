using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Kakuro
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            //Recuperate the user input
            string userInput = txtUsername.Text.Trim();
            string emailInput = txtEmail.Text.Trim();
            string pwdInput = txtPassword.Text.Trim();

            // Connect to DB
            SqlConnection mycon = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
            + Server.MapPath(@"~\App_Data\Kakuro.mdf") +
            @";Integrated Security=True");
            mycon.Open();

            //Verification: Is the email or username already taken?
            string sql = "SELECT Username FROM Users WHERE Email = @eml OR Username = @usr";
            SqlCommand mycmd = new SqlCommand(sql, mycon);

            mycmd.Parameters.AddWithValue("@eml", emailInput);
            mycmd.Parameters.AddWithValue("@usr", userInput);

            SqlDataReader myrder = mycmd.ExecuteReader();

            if (myrder.Read()) // Email or Username already exists
            {
                myrder.Close();
                mycon.Close();
                lblError.Text = "Email or Username already taken. Please use another one.";
            }
            else //Not yet a user: register
            {
                myrder.Close();
                //Insert new user in the Users table
                sql = "INSERT INTO Users (Username,Email,Password) ";
                sql += "VALUES (@usr,@eml,@pwd)";
                SqlCommand mycmd2 = new SqlCommand(sql, mycon);
                mycmd2.Parameters.AddWithValue("@usr", userInput);
                mycmd2.Parameters.AddWithValue("@eml", emailInput);
                mycmd2.Parameters.AddWithValue("@pwd", pwdInput);
                mycmd2.ExecuteNonQuery();
                mycon.Close();
                //Go to the login page
                Response.Redirect("Login.aspx");
            }
        }
    }
}