using Kakuro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kakuro.Views
{
    public partial class Home : System.Web.UI.Page
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
                string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    Server.MapPath("~\\App_Data\\Kakuro.mdf;Integrated Security=True");
                SQLManager sqlm = new SQLManager(connStr);
                int completed = sqlm.GetCompletedLevelsCount((int)Session["MemberID"]);

                if (completed >= 10)
                    Response.Redirect("~/Views/SelectConfiguration.aspx");
                else
                    Response.Redirect("~/Views/Levels.aspx");
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