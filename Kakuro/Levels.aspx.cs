using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kakuro
{
    public partial class Levels : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            var levels = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            lsLevels.DataSource = levels;
            lsLevels.DataBind();
        }

        protected void btnLevel_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            string level = btnSender.Text.Substring("Level ".Length);
            Session["CurrentLevel"] = level;
            Response.Redirect("Gameplay.aspx");
        }
    }
}