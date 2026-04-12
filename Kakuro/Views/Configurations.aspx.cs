using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kakuro.Views
{
    public partial class Configurations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildGridOptions(); // Your existing method

            if (IsPostBack && !string.IsNullOrEmpty(hfGridState.Value))
            {
                // Re-render the grid every postback to maintain event wireups
                RenderKakuroGrid();
            }

            UpdateSummary();
        }

        // Builds grid size buttons on first load with full inner HTML
        private void BuildGridOptions()
        {
            var sizes = new[] { "4", "5", "6", "7", "8", "9", "10" };

            foreach (var size in sizes)
            {
                var btn = new LinkButton
                {
                    ID = "btnSize_" + size,
                    CssClass = "grid-btn",
                    CommandArgument = size + "x" + size
                };

                // Build the mini-grid dots visually matching the image
                int n = int.Parse(size);
                int dotCount = Math.Min(n, 5); // cap dots for larger grids

                btn.Controls.Add(new LiteralControl($@"
            <div class='mini-grid' style='grid-template-columns: repeat({dotCount}, 1fr);'>
                {string.Concat(Enumerable.Repeat("<div class='mini-cell'></div>", dotCount * dotCount))}
            </div>
            <div class='grid-num'>{size}</div>
            <div class='grid-label'>×{size}</div>
        "));

                btn.Click += SelectSize_Click;
                gridOptions.Controls.Add(btn);
            }
        }

        // Re-registers click events on postback without re-adding controls visually
        private void RebuildGridOptions()
        {
            var sizes = new[] { "4", "5", "6", "7", "8", "9", "10" };

            foreach (var size in sizes)
            {
                var btn = new LinkButton
                {
                    ID = "btnSize_" + size,
                    CssClass = "grid-btn",
                    CommandArgument = size + "x" + size
                };

                btn.Click += SelectSize_Click;
                gridOptions.Controls.Add(btn);
            }
        }

        protected void SelectSize_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            Session["SelectedSize"] = btn.CommandArgument;
            UpdateSummary();
            ApplyActiveClass();
        }

        protected void SelectDiff_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            Session["SelectedDiff"] = btn.CommandArgument;
            UpdateSummary();
            ApplyActiveClass();
        }

        private void UpdateSummary()
        {
            string selectedSize = Session["SelectedSize"] as string;
            string selectedDiff = Session["SelectedDiff"] as string;

            tagSize.Text = !string.IsNullOrEmpty(selectedSize)
                ? selectedSize.Replace("x", "×")
                : "No size selected";

            tagDiff.Text = !string.IsNullOrEmpty(selectedDiff)
                ? char.ToUpper(selectedDiff[0]) + selectedDiff.Substring(1)
                : "No difficulty";

            tagSize.CssClass = !string.IsNullOrEmpty(selectedSize) ? "tag active" : "tag";
            tagDiff.CssClass = !string.IsNullOrEmpty(selectedDiff) ? "tag active" : "tag";

            lnkGenerate.Enabled = !string.IsNullOrEmpty(selectedSize)
                               && !string.IsNullOrEmpty(selectedDiff);
        }

        private void ApplyActiveClass()
        {
            string selectedSize = Session["SelectedSize"] as string;
            string selectedDiff = Session["SelectedDiff"] as string;

            var sizes = new[] { "4", "5", "6", "7", "8", "9", "10" };
            foreach (var size in sizes)
            {
                var btn = gridOptions.FindControl("btnSize_" + size) as LinkButton;
                if (btn != null)
                    btn.CssClass = selectedSize == size + "x" + size ? "grid-btn active" : "grid-btn";
            }

            lnkEasy.CssClass = selectedDiff == "easy" ? "diff-btn active" : "diff-btn";
            lnkMedium.CssClass = selectedDiff == "medium" ? "diff-btn active" : "diff-btn";
            lnkHard.CssClass = selectedDiff == "hard" ? "diff-btn active" : "diff-btn";
        }

        protected void GenerateGrid_Click(object sender, EventArgs e)
        {
            int n = int.Parse(Session["SelectedSize"].ToString().Split('x')[0]);
            string diff = Session["SelectedDiff"].ToString();

            double blackRatio = diff == "easy" ? 0.22 : diff == "medium" ? 0.30 : 0.38;
            Random rand = new Random();

            var newState = new List<List<string>>();

            for (int r = 0; r < n; r++)
            {
                var row = new List<string>();
                for (int c = 0; c < n; c++)
                {
                    if (r == 0 || c == 0) row.Add("clue");
                    else row.Add(rand.NextDouble() < blackRatio ? "black" : "white");
                }
                newState.Add(row);
            }

            // Save to hidden field as JSON
            hfGridState.Value = new JavaScriptSerializer().Serialize(newState);
            previewWrap.Visible = true;
            RenderKakuroGrid();
        }

        private void RenderKakuroGrid()
        {
            var gridData = new JavaScriptSerializer().Deserialize<List<List<string>>>(hfGridState.Value);
            int n = gridData.Count;

            pnlKakuroGrid.Controls.Clear();
            pnlKakuroGrid.Style["grid-template-columns"] = $"repeat({n}, 52px)";
            previewTitle.InnerText = $"Your template — {n}×{n}";

            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    string type = gridData[r][c];
                    LinkButton cell = new LinkButton();
                    cell.CssClass = "k-cell " + (type == "black" ? "black-cell" : type == "clue" ? "clue-cell" : "");
                    cell.ID = $"cell_{r}_{c}";
                    cell.CommandArgument = $"{r},{c}";

                    if (type != "clue")
                    {
                        cell.Click += Cell_Click;
                    }
                    else if (r > 0 && c > 0)
                    {
                        cell.Controls.Add(new LiteralControl("<div class='clue-across'>–</div><div class='clue-down'>–</div>"));
                    }

                    pnlKakuroGrid.Controls.Add(cell);
                }
            }
        }

        protected void Cell_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            string[] coords = btn.CommandArgument.Split(',');
            int r = int.Parse(coords[0]);
            int c = int.Parse(coords[1]);

            var gridData = new JavaScriptSerializer().Deserialize<List<List<string>>>(hfGridState.Value);

            // Toggle based on paint mode
            gridData[r][c] = hfPaintMode.Value;

            hfGridState.Value = new JavaScriptSerializer().Serialize(gridData);
            RenderKakuroGrid(); // Refresh UI
        }

        protected void SetPaintMode_Click(object sender, EventArgs e)
        {
            string mode = ((Button)sender).CommandArgument;
            hfPaintMode.Value = mode;

            // Update UI Classes
            btnToolBlack.CssClass = mode == "black" ? "tool-btn active" : "tool-btn";
            btnToolWhite.CssClass = mode == "white" ? "tool-btn active" : "tool-btn";
        }

        protected void SaveTemplate_Click(object sender, EventArgs e)
        {
            
        }
    }


}