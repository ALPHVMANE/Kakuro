using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kakuro
{
    public partial class Leaderboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLeaderboard();
            }
        }
        private void LoadLeaderboard()
        {
            SqlConnection mycon = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
            + Server.MapPath(@"~\App_Data\Kakuro.mdf") +
            @";Integrated Security=True");

            mycon.Open();

            string sql = "SELECT TOP 5 Username, Score FROM Users ORDER BY Score DESC";
            SqlCommand mycmd = new SqlCommand(sql, mycon);

            SqlDataReader reader = mycmd.ExecuteReader();

            rptLeaderboard.DataSource = reader;
            rptLeaderboard.DataBind();

            reader.Close();
            mycon.Close();
        }
    }
}