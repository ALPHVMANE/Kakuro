using Kakuro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kakuro
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool isLoggedIn = Session["MemberID"] != null;

            navHistory.Visible = isLoggedIn;
            navProfile.Visible = isLoggedIn;
            navLogout.Visible = isLoggedIn;
            navLogin.Visible = !isLoggedIn;
            navRegister.Visible = !isLoggedIn;

            if (isLoggedIn)
            {
                string connStr = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename =" +
                     Server.MapPath("~\\App_Data\\Kakuro.mdf;Integrated Security=True");
                SQLManager sqlm = new SQLManager(connStr);
                int completed = sqlm.GetCompletedLevelsCount((int)Session["MemberID"]);

                bool tutorialDone = completed >= 10; //change this to match the levels int the other levels are created
                navLevels.Visible = !tutorialDone;
                navRNG.Visible = tutorialDone;
            }
            else
            {
                navLevels.Visible = false;
                navRNG.Visible = false;
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