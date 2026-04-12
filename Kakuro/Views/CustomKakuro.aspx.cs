using Kakuro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kakuro.Views
{
    public partial class CustomKakuro : Page
    {
        private string connStr;

        protected void Page_Load(object sender, EventArgs e)
        {
            connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                Server.MapPath("~\\App_Data\\Kakuro.mdf;Integrated Security=True");

            if (Session["MemberID"] == null)
            {
                Response.Redirect("~/Views/Login.aspx");
                return;
            }
        }

        protected void BtnGenerate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(TxtSizeX.Text.Trim(), out int sizeX) ||
                !int.TryParse(TxtSizeY.Text.Trim(), out int sizeY) ||
                sizeX < 2 || sizeY < 2 || sizeX > 12 || sizeY > 12)
            {
                ResultLabel.Text = "Please enter a valid size between 2 and 12.";
                return;
            }

            EditorTable.Rows.Clear();

            for (int y = 0; y < sizeY; y++)
            {
                TableRow row = new TableRow();
                for (int x = 0; x < sizeX; x++)
                {
                    TableCell td = new TableCell();
                    td.CssClass = "entry-cell";

                    // Hidden field to track cell type (1=entry, 0=black)
                    var hidden = new HiddenField
                    {
                        ID = $"celltype_{x}_{y}",
                        Value = "1",
                        ClientIDMode = ClientIDMode.Static
                    };

                    // Input for entry value
                    var input = new System.Web.UI.HtmlControls.HtmlInputText
                    {
                        ID = $"cell_{x}_{y}",
                        MaxLength = 1,
                        Style = { Value = "width:100%;height:100%;border:none;text-align:center;font-size:16px;" }
                    };
                    input.Attributes["ClientIDMode"] = "Static";

                    td.Controls.Add(hidden);
                    td.Controls.Add(input);

                    // Toggle on click
                    td.Attributes["onclick"] = $"toggleCell({x},{y})";

                    row.Cells.Add(td);
                }
                EditorTable.Rows.Add(row);
            }

            EditorPanel.Visible = true;
            ResultLabel.Text = "";
        }

        protected void BtnPreview_Click(object sender, EventArgs e)
        {
            ResultLabel.Text = "Preview: clues will be computed when you Save & Play.";
        }

        protected void BtnSavePlay_Click(object sender, EventArgs e)
        {
            ResultLabel.Text = "";

            if (!int.TryParse(TxtSizeX.Text.Trim(), out int sizeX) ||
                !int.TryParse(TxtSizeY.Text.Trim(), out int sizeY))
            {
                ResultLabel.Text = "Invalid size.";
                return;
            }

            int userId = Convert.ToInt32(Session["MemberID"]);
            SQLManager sqlm = new SQLManager(connStr);

            // Check tutorial completion
            int completed = sqlm.GetCompletedLevelsCount(userId);
            if (completed < 10)
            {
                ResultLabel.Text = "Complete the 10 tutorial levels first.";
                return;
            }

            // Read cell values from posted form
            int[,] values = new int[sizeX, sizeY];
            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    string typeKey = Request.Form.AllKeys
                        .FirstOrDefault(k => k != null && k.EndsWith($"celltype_{x}_{y}"));
                    string valKey = Request.Form.AllKeys
                        .FirstOrDefault(k => k != null && k.EndsWith($"cell_{x}_{y}"));

                    string typeVal = typeKey != null ? Request.Form[typeKey] : "0";
                    string cellVal = valKey != null ? Request.Form[valKey] : "";

                    if (typeVal == "1" && int.TryParse(cellVal, out int v) && v >= 1 && v <= 9)
                        values[x, y] = v;
                    else
                        values[x, y] = 0; // black/empty
                }
            }

            // Build and validate the grid
            var outGrid = new Cell[sizeX, sizeY];
            string error = null;

            for (int y = 0; y < sizeY; y++)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    if (values[x, y] != 0)
                    {
                        outGrid[x, y] = new Entry(x, y, values[x, y]);
                        continue;
                    }

                    int? hSum = null, vSum = null;
                    var seen = new HashSet<int>();
                    var hList = new List<int>();

                    // Horizontal run
                    int hx = x + 1;
                    while (hx < sizeX && values[hx, y] != 0)
                    {
                        int val = values[hx, y];
                        if (!seen.Add(val))
                        {
                            error = $"Duplicate {val} in horizontal run at row {y + 1}.";
                            break;
                        }
                        hList.Add(val);
                        hx++;
                    }
                    if (error != null) break;
                    if (hList.Count > 0) hSum = hList.Sum();

                    // Vertical run
                    seen.Clear();
                    var vList = new List<int>();
                    int vy = y + 1;
                    while (vy < sizeY && values[x, vy] != 0)
                    {
                        int val = values[x, vy];
                        if (!seen.Add(val))
                        {
                            error = $"Duplicate {val} in vertical run at column {x + 1}.";
                            break;
                        }
                        vList.Add(val);
                        vy++;
                    }
                    if (error != null) break;
                    if (vList.Count > 0) vSum = vList.Sum();

                    outGrid[x, y] = (hSum.HasValue || vSum.HasValue)
                        ? (Cell)new Clue(x, y, hSum, vSum)
                        : new Empty(x, y);
                }
                if (error != null) break;
            }

            if (error != null)
            {
                ResultLabel.Text = "Validation error: " + error;
                return;
            }

            // Check at least one entry exists
            bool anyEntry = false;
            for (int y = 0; y < sizeY && !anyEntry; y++)
                for (int x = 0; x < sizeX; x++)
                    if (outGrid[x, y] is Entry) { anyEntry = true; break; }

            if (!anyEntry)
            {
                ResultLabel.Text = "Puzzle must have at least one entry cell.";
                return;
            }

            // Save to DB
            var board = new Board(0, sizeX, sizeY, "Custom", outGrid, 0);

            try
            {
                int boardId = sqlm.InsertCustomBoard(board, userId);
                sqlm.InsertGameState(boardId, userId);

                Session["LvlBoardID"] = boardId;
                Session["IsRNG"] = false;
                Response.Redirect("~/Views/Gameplay.aspx");
            }
            catch (Exception ex)
            {
                ResultLabel.Text = "Error saving board: " + ex.Message;
            }
        }
    }
}