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
            if (Session["MemberID"] != null)
            {
                string connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                    Server.MapPath("~\\App_Data\\Kakuro.mdf;Integrated Security=True");
                SQLManager sqlm = new SQLManager(connStr);
                int completed = sqlm.GetCompletedLevelsCount((int)Session["MemberID"]);

                bool tutorialDone = completed >= 10; //change this to match the levels int the other levels are created
               

            }
        }
    }
}