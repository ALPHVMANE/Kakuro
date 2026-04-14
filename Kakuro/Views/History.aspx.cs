using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kakuro.Views
{
    public partial class History : System.Web.UI.Page
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
                // connect to DB
                SqlConnection mycon = new SqlConnection(
                @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
                + Server.MapPath(@"~\App_Data\Kakuro.mdf") +
                @";Integrated Security=True");
                mycon.Open();

                // sql with 1 param
                string sql = "SELECT [SessionID], [BoardID], [Status], [Score], [Errors] FROM [GameState] ";
                sql += "WHERE [UserID] = @UserID";
                SqlCommand mycmd = new SqlCommand(sql, mycon);

                mycmd.Parameters.AddWithValue("@UserID", (int)Session["MemberID"]);

                DataTable dt = new DataTable();
                dt.Load(mycmd.ExecuteReader());
                ListView.DataSource = dt;
                ListView.DataBind();

                mycon.Close();
            }
        }

        protected void btnResume_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int boardId = Convert.ToInt32(btn.CommandArgument);

            Session["IsRNG"] = false;
            Session["LvlBoardID"] = boardId;
            Response.Redirect("~/Views/Gameplay.aspx");
        }

        protected void btnReplay_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int boardId = Convert.ToInt32(btn.CommandArgument);

            SqlConnection mycon = new SqlConnection(
               @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="
               + Server.MapPath(@"~\App_Data\Kakuro.mdf") +
               @";Integrated Security=True");
            mycon.Open();
            SqlCommand mycmd = new SqlCommand(
            "UPDATE GameState SET Status = 0, Score = 0, Errors = 0 " +
            "WHERE BoardID = @bID AND UserID = @uID", mycon);
            mycmd.Parameters.AddWithValue("@bID", boardId);
            mycmd.Parameters.AddWithValue("@uID", (int)Session["MemberID"]);
            mycmd.ExecuteNonQuery();
            mycon.Close();

            Session["IsRNG"] = false;
            Session["LvlBoardID"] = boardId;
            Response.Redirect("~/Views/Gameplay.aspx");
        }
    }
}