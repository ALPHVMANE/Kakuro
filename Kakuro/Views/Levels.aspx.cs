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

            Session["IsRNG"] = null;
            Session["RNGSizeX"] = null;
            Session["RNGSizeY"] = null;
            Session["RNGDifficulty"] = null;

            connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + Server.MapPath("~\\App_Data\\Kakuro.mdf;Integrated Security=True");
            sqlm = new SQLManager(connStr);

            int completed = sqlm.GetCompletedLevelsCount(Convert.ToInt32(Session["MemberID"]));

            if(completed >= 10)
            {
                Response.Redirect("~/Views/SelectConfiguration.aspx");
                return;
            }

            int userCurrentLevel = completed + 1;
            // change it back to i <= 11 after levels fully created
            for (int i = 1; i <= 10; i++)
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
    }
}