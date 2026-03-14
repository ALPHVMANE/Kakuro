using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kakuro
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Visible = false;
        }

        protected void btn_submitLogin_Click(object sender, EventArgs e)
        {
            // get user's inputs
            string emlInput = txtLoginEmail.Text.Trim();
            string pwdInput = txtLoginPw.Text.Trim();

            // connect to DB
            SqlConnection mycon = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
            + Server.MapPath(@"~\App_Data\Kakuro.mdf") +
            @";Integrated Security=True");
            mycon.Open();
            // sql with 2 params
            string sql = "SELECT Id,Email, Username FROM Users ";
            sql += "WHERE Email=@eml AND Password=@pwd";
            SqlCommand mycmd = new SqlCommand(sql, mycon);

            mycmd.Parameters.AddWithValue("@eml", emlInput);
            mycmd.Parameters.AddWithValue("@pwd", pwdInput);

            SqlDataReader myrder = mycmd.ExecuteReader();
            // test if user = found
            if (myrder.Read() == true)
            {
                // global var.
                Session["MemberName"] = myrder["Username"];
                Session["MemberID"] = myrder["Id"];

                myrder.Close();
                mycon.Close();
                Response.Redirect("Home.aspx");
            }
            else // user not found
            {
                myrder.Close();
                mycon.Close();
                lblError.Visible = true;
                lblError.Text = "Wrong Email or Password, Try again.";
            }
        }
    }
}