using System;
using System.Data.SqlClient;
using System.Web.UI;

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

            SqlConnection mycon = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
            + Server.MapPath(@"~\App_Data\Kakuro.mdf") +
            @";Integrated Security=True");

            mycon.Open();

            string sql = "SELECT Username, Email, Score, LevelsCompleted FROM Users WHERE Id = @uID";
            SqlCommand mycmd = new SqlCommand(sql, mycon);
            mycmd.Parameters.AddWithValue("@uID", userId);

            SqlDataReader reader = mycmd.ExecuteReader();

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

            reader.Close();
            mycon.Close();
        }
    }
}