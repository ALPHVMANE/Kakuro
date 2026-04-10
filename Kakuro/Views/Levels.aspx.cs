using Kakuro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kakuro.Views
{
    public partial class Levels : System.Web.UI.Page
    {
        private string connStr;
        private SQLManager sqlm;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["MemberID"] == null)
            {
                Response.Redirect("~/Views/Login.aspx");
                return;
            }

            connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + Server.MapPath("~\\App_Data\\Kakuro.mdf;Integrated Security=True");
            sqlm = new SQLManager(connStr);

            int userCurrentLevel = sqlm.GetCompletedLevelsCount(Convert.ToInt32(Session["MemberID"])) + 1;

            for (int i = 1; i <= 11; i++)
            {
                string buttonId = "btn" + (i + 24);

                Button btn = (Button)pnlLevels.FindControl(buttonId);

                if (btn != null)
                {
                    btn.Enabled = (i == userCurrentLevel);
                }
            } //Locked || Unlocked lvl 
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                Session["LvlBoardID"] = (int)(Convert.ToInt32(btn.Text) + 24);
                Response.Redirect("~/Views/Gameplay.aspx");
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Views/Login.aspx");
        }
    }
}