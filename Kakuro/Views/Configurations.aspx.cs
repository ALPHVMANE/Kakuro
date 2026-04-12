using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Kakuro.Views
{
    public partial class Configurations : System.Web.UI.Page
    {
        // ── Session key constants ────────────────────────────────────────────
        private const string SK_SIZE = "SelectedSize";
        private const string SK_DIFF = "SelectedDiff";
        private const string SK_GRID = "GridState";
        private const string SK_PAINT = "PaintMode";

        // ── Grid state helpers (no JavaScriptSerializer) ─────────────────────
        // Stored in Session as List<List<string>> — no serialization needed.
        // Each cell value is one of: "clue" | "black" | "white"

        private List<List<string>> GetGridState()
            => Session[SK_GRID] as List<List<string>>;

        private void SetGridState(List<List<string>> state)
            => Session[SK_GRID] = state;

        // Paint mode stored in Session so it survives every postback reliably.
        private string PaintMode
        {
            get => Session[SK_PAINT] as string ?? "black";
            set => Session[SK_PAINT] = value;
        }

        // ── Page lifecycle ───────────────────────────────────────────────────
        protected void Page_Load(object sender, EventArgs e)
        {
            // Always rebuild size buttons so their inner HTML is present on
            // both first load and every postback (dynamic controls are not
            // kept in ViewState — Controls.Clear() prevents duplicate IDs).
            BuildGridOptions();

            if (IsPostBack && GetGridState() != null)
            {
                previewWrap.Visible = true;
                RenderKakuroGrid();
            }

            UpdateSummary();
            ApplyActiveClass();
            SyncPaintToolbar();
        }

        // ── Size button builder ──────────────────────────────────────────────
        private void BuildGridOptions()
        {
            gridOptions.Controls.Clear();
            var sizes = new[] { "4", "5", "6", "7", "8", "9", "10" };

            foreach (var size in sizes)
            {
                int n = int.Parse(size);
                int dotCount = Math.Min(n, 5);

                var btn = new LinkButton
                {
                    ID = "btnSize_" + size,
                    CssClass = "grid-btn",
                    CommandArgument = size + "x" + size
                };

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

        // ── Selection handlers ───────────────────────────────────────────────
        protected void SelectSize_Click(object sender, EventArgs e)
        {
            Session[SK_SIZE] = ((LinkButton)sender).CommandArgument;
            UpdateSummary();
            ApplyActiveClass();
        }

        protected void SelectDiff_Click(object sender, EventArgs e)
        {
            Session[SK_DIFF] = ((LinkButton)sender).CommandArgument;
            UpdateSummary();
            ApplyActiveClass();
        }

        // ── Summary bar ──────────────────────────────────────────────────────
        private void UpdateSummary()
        {
            string sz = Session[SK_SIZE] as string;
            string diff = Session[SK_DIFF] as string;

            tagSize.Text = !string.IsNullOrEmpty(sz) ? sz.Replace("x", "×") : "No size selected";
            tagDiff.Text = !string.IsNullOrEmpty(diff) ? char.ToUpper(diff[0]) + diff.Substring(1) : "No difficulty";
            tagSize.CssClass = !string.IsNullOrEmpty(sz) ? "tag active" : "tag";
            tagDiff.CssClass = !string.IsNullOrEmpty(diff) ? "tag active" : "tag";

            lnkGenerate.Enabled = !string.IsNullOrEmpty(sz) && !string.IsNullOrEmpty(diff);
            lnkSkip.Enabled = !string.IsNullOrEmpty(sz) && !string.IsNullOrEmpty(diff);
        }

        private void ApplyActiveClass()
        {
            string sz = Session[SK_SIZE] as string;
            string diff = Session[SK_DIFF] as string;

            foreach (var size in new[] { "4", "5", "6", "7", "8", "9", "10" })
            {
                var btn = gridOptions.FindControl("btnSize_" + size) as LinkButton;
                if (btn != null)
                    btn.CssClass = (sz == size + "x" + size) ? "grid-btn active" : "grid-btn";
            }

            lnkEasy.CssClass = diff == "easy" ? "diff-btn active" : "diff-btn";
            lnkMedium.CssClass = diff == "medium" ? "diff-btn active" : "diff-btn";
            lnkHard.CssClass = diff == "hard" ? "diff-btn active" : "diff-btn";
        }

        // ── Grid generation ──────────────────────────────────────────────────
        protected void GenerateGrid_Click(object sender, EventArgs e)
        {
            int n = int.Parse(Session[SK_SIZE].ToString().Split('x')[0]);
            string diff = Session[SK_DIFF].ToString();
            double blackRatio = diff == "easy" ? 0.22 : diff == "medium" ? 0.30 : 0.38;
            var rand = new Random();

            var state = new List<List<string>>();
            for (int r = 0; r < n; r++)
            {
                var row = new List<string>();
                for (int c = 0; c < n; c++)
                    row.Add((r == 0 || c == 0) ? "clue"
                          : rand.NextDouble() < blackRatio ? "black" : "white");
                state.Add(row);
            }

            SetGridState(state);
            previewWrap.Visible = true;
            RenderKakuroGrid();
        }

        // ── Grid renderer ────────────────────────────────────────────────────
        private void RenderKakuroGrid()
        {
            var gridData = GetGridState();
            if (gridData == null) return;

            int n = gridData.Count;
            pnlKakuroGrid.Controls.Clear();
            pnlKakuroGrid.Style["grid-template-columns"] = $"repeat({n}, 52px)";

            var titleDiv = previewTitle as HtmlGenericControl;
            if (titleDiv != null)
                titleDiv.InnerText = $"Your template — {n}×{n}";

            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    string type = gridData[r][c];
                    var cell = new LinkButton
                    {
                        ID = $"cell_{r}_{c}",
                        CommandArgument = $"{r},{c}",
                        CssClass = "k-cell " + (type == "black" ? "black-cell"
                                                     : type == "clue" ? "clue-cell" : "")
                    };

                    if (type == "clue")
                    {
                        // Only interior clue cells get across/down labels
                        if (r > 0 && c > 0)
                            cell.Controls.Add(new LiteralControl(
                                "<div class='clue-across'>–</div><div class='clue-down'>–</div>"));
                    }
                    else
                    {
                        cell.Click += Cell_Click;
                    }

                    pnlKakuroGrid.Controls.Add(cell);
                }
            }
        }

        // ── Cell click ───────────────────────────────────────────────────────
        protected void Cell_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            var parts = btn.CommandArgument.Split(',');
            int r = int.Parse(parts[0]);
            int c = int.Parse(parts[1]);

            var gridData = GetGridState();
            if (gridData == null) return;

            // PaintMode comes from Session — always reliable across postbacks
            gridData[r][c] = PaintMode;

            SetGridState(gridData);
            RenderKakuroGrid();
        }

        // ── Paint mode ───────────────────────────────────────────────────────
        protected void SetPaintMode_Click(object sender, EventArgs e)
        {
            PaintMode = ((Button)sender).CommandArgument;
            SyncPaintToolbar();
        }

        private void SyncPaintToolbar()
        {
            btnToolBlack.CssClass = PaintMode == "black" ? "tool-btn active" : "tool-btn";
            btnToolWhite.CssClass = PaintMode == "white" ? "tool-btn active" : "tool-btn";
        }

        // ── Pick from DB templates ───────────────────────────────────────────
        protected void lnkSkip_Click(object sender, EventArgs e)
        {
            // TODO: implement — available template fields: Id, Score, SizeX, SizeY, Difficulty
            // Example:
            //   var t = db.Templates.Find(selectedId);
            //   Session[SK_SIZE] = t.SizeX + "x" + t.SizeY;
            //   Session[SK_DIFF] = t.Difficulty;
            //   SetGridState( /* build from t.GridData */ );
            //   previewWrap.Visible = true;
            //   RenderKakuroGrid();
        }

        // ── Save ─────────────────────────────────────────────────────────────
        protected void SaveTemplate_Click(object sender, EventArgs e)
        {
            
        }
    }
}
