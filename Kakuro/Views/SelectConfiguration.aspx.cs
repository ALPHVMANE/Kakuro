using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kakuro.Views
{
    public partial class SelectConfiguration : System.Web.UI.Page
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
                lblError.Visible = false;
            }
        }

        protected void SelectSize(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            string size = btn.CommandArgument;

            string[] parts = size.Split('x');

            hfSizeX.Value = parts[0];
            hfSizeY.Value = parts[1];

            hfDiff.Value = string.Empty;

            int sizeNum = int.Parse(parts[0]);

            btnMedium.Enabled = true;
            btnHard.Enabled = true;

            if (sizeNum <= 5)
            {
                btnMedium.Enabled = false;
                btnHard.Enabled = false;
            }
            else if (sizeNum <= 8)
            {
                btnHard.Enabled = false;
            }

            btnSize4.CssClass = "card size-card text-center p-3";
            btnSize5.CssClass = "card size-card text-center p-3";
            btnSize6.CssClass = "card size-card text-center p-3";
            btnSize7.CssClass = "card size-card text-center p-3";
            btnSize8.CssClass = "card size-card text-center p-3";
            btnSize9.CssClass = "card size-card text-center p-3";
            btnSize10.CssClass = "card size-card text-center p-3";

            if (hfSizeX.Value == "4") btnSize4.CssClass += " selected";
            if (hfSizeX.Value == "5") btnSize5.CssClass += " selected";
            if (hfSizeX.Value == "6") btnSize6.CssClass += " selected";
            if (hfSizeX.Value == "7") btnSize7.CssClass += " selected";
            if (hfSizeX.Value == "8") btnSize8.CssClass += " selected";
            if (hfSizeX.Value == "9") btnSize9.CssClass += " selected";
            if (hfSizeX.Value == "10") btnSize10.CssClass += " selected";

            btnEasy.CssClass = "btn btn-outline-success";
            btnMedium.CssClass = "btn btn-outline-warning";
            btnHard.CssClass = "btn btn-outline-danger";
        }

        protected void SelectDifficulty(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            hfDiff.Value = btn.CommandArgument;

            btnSize4.CssClass = "card size-card text-center p-3";
            btnSize5.CssClass = "card size-card text-center p-3";
            btnSize6.CssClass = "card size-card text-center p-3";
            btnSize7.CssClass = "card size-card text-center p-3";
            btnSize8.CssClass = "card size-card text-center p-3";
            btnSize9.CssClass = "card size-card text-center p-3";
            btnSize10.CssClass = "card size-card text-center p-3";

            if (hfSizeX.Value == "4") btnSize4.CssClass += " selected";
            if (hfSizeX.Value == "5") btnSize5.CssClass += " selected";
            if (hfSizeX.Value == "6") btnSize6.CssClass += " selected";
            if (hfSizeX.Value == "7") btnSize7.CssClass += " selected";
            if (hfSizeX.Value == "8") btnSize8.CssClass += " selected";
            if (hfSizeX.Value == "9") btnSize9.CssClass += " selected";
            if (hfSizeX.Value == "10") btnSize10.CssClass += " selected";

            btnEasy.CssClass = hfDiff.Value == "Easy" ? "btn btn-success" : "btn btn-outline-success";
            btnMedium.CssClass = hfDiff.Value == "Medium" ? "btn btn-warning" : "btn btn-outline-warning";
            btnHard.CssClass = hfDiff.Value == "Hard" ? "btn btn-danger" : "btn btn-outline-danger";
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string sizeX = hfSizeX.Value;
            string sizeY = hfSizeY.Value;
            string diff = hfDiff.Value;

            

            if (string.IsNullOrEmpty(sizeX) || string.IsNullOrEmpty(sizeY) || string.IsNullOrEmpty(diff))
            {
                lblError.Visible = true;
                return;
            }

            int size = int.Parse(sizeX);
            bool valid = (size <= 5 && diff == "Easy") || (size >= 6 && size <= 8 && (diff == "Easy" || diff == "Medium")) ||
                (size >= 9 && (diff == "Easy" || diff == "Medium" || diff == "Hard"));

            if (!valid) 
            {
                lblError.Text = "Invalid size and difficulty combination";
                lblError.Visible = true;
                return;
            }

            Session["IsRNG"] = true;
            Session["RNGSizeX"] = size;
            Session["RNGSizeY"] = int.Parse(sizeY);
            Session["RNGDifficulty"] = diff;
            Session["LvlBoardID"] = null;

            Response.Redirect("~/Views/Gameplay.aspx");
        }

    }
}