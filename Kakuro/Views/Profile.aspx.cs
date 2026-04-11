using Kakuro.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kakuro.Views
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MemberID"] == null)
            {
                Response.Redirect("~/Views/Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                LoadProfile();
            }
        }

        private void LoadProfile()
        {
            int userId = Convert.ToInt32(Session["MemberID"]);

            string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename="
                + Server.MapPath("~\\App_Data\\Kakuro.mdf;Integrated Security=True");

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT Username, Email, Score, LevelsCompleted FROM Users WHERE Id = @uID", conn))
                {
                    cmd.Parameters.AddWithValue("@uID", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string username = reader["Username"].ToString();
                            string email = reader["Email"].ToString();
                            int score = (int)reader["Score"];
                            int levelsCompleted = (int)reader["LevelsCompleted"];

                            lblInitial.Text = username.Substring(0, 1).ToUpper();
                            lblUsername.Text = username;
                            lblEmail.Text = email;
                            lblScore.Text = score.ToString();
                            lblLevelsCompleted.Text = levelsCompleted.ToString();
                        }
                    }
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Home.aspx");
        }
<<<<<<< HEAD
=======

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Views/Login.aspx");
        }
>>>>>>> 6afbe3f89885402e0a065e64d894f1def0b613d9
    }
}