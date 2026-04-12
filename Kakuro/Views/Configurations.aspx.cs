using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kakuro.Views
{
    public partial class Configurations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BuildGridOptions();
            }
            else
            {
                RebuildGridOptions();
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
            string size = Session["SelectedSize"] as string;
            string diff = Session["SelectedDiff"] as string;

            previewWrap.Visible = true;
            previewWrap.Controls.Clear();

            var placeholder = new System.Web.UI.WebControls.Label
            {
                Text = $"Grid: {size} | Difficulty: {diff}"
            };
            previewWrap.Controls.Add(placeholder);
        }
    }


}