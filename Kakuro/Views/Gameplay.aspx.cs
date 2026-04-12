using Kakuro.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kakuro
{
    public partial class Gameplay : System.Web.UI.Page
    {
        private string connStr;
        private Board board;

        protected void Page_Load(object sender, EventArgs e)
        {
            connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                Server.MapPath("~\\App_Data\\Kakuro.mdf;Integrated Security=True");
            SQLManager sqlm = new SQLManager(connStr);
            PuzzleManager pm = new PuzzleManager(sqlm);

            if (!IsPostBack)
            {
                if (Session["MemberID"] == null)
                {
                    Response.Redirect("~/Views/Login.aspx");
                    return;
                }

                bool isRNG = Session["IsRNG"] != null && (bool)Session["IsRNG"];

                if (isRNG)
                {
                    int size = (int)Session["RNGSizeX"];
                    string diff = Session["RNGDifficulty"]?.ToString() ?? "Easy";
                    board = pm.initFromTemplate(size, diff);

                    if (board != null)
                    {
                        Session["CurrentBoard"] = board;
                        Session["IsRNG"] = true;
                    }
                    else
                        Response.Write("<script>alert('No template found for that size and difficulty.');</script>");
                }
                else
                {
                    if (Session["LvlBoardID"] == null || Session["MemberID"] == null)
                    {
                        Response.Redirect("~/Views/Levels.aspx");
                        return;
                    }

                    int boardID = (int)Session["LvlBoardID"];
                    board = pm.initBoard(boardID, (int)Session["MemberID"]);

                    if (board != null)
                        Session["CurrentBoard"] = board;
                }
            }
            else
            {
                board = (Board)Session["CurrentBoard"];
            }

            if (board != null)
                DisplayBoard();
            else
                Response.Write("<script>alert('Unable to load board.');</script>");
        }

        //public bool ValidateSolution(Board board, int[,] userInput)
        //{
        //    return board.HorizontalSegments.All(segment => segment.IsValid()) &&
        //           board.VerticalSegments.All(segment => segment.IsValid());
        //} ==> For rng tables

        private void SaveUserInputToBoard()
        {
            for (int i = 0; i < board.SizeY; i++)
            {
                for (int j = 0; j < board.SizeX; j++)
                {
                    if (board.Grid[j, i] is Entry entry)
                    {
                        string cellId = $"cell_{j}_{i}";
                        // Read raw posted value before table is recreated
                        string key = Request.Form.AllKeys
                            .FirstOrDefault(k => k != null && k.EndsWith(cellId));
                        if (key != null)
                        {
                            int val;
                            if (int.TryParse(Request.Form[key], out val))
                                entry.CurrentValue = val;
                        }
                    }
                }
            }
            Session["CurrentBoard"] = board;
        }

        protected void btnCheckSolution_Click(object sender, EventArgs e)
        {
            if (board == null)
                board = (Board)Session["CurrentBoard"];

            if (board == null)
            {
                ResultLabel.Text = "Error: board not loaded. Please refresh.";
                return;
            }

            // Save what user typed into the board before table is recreated
            SaveUserInputToBoard();

            bool isCorrect = true;
            bool isRNG = Session["IsRNG"] != null && (bool)Session["IsRNG"];

            for (int i = 0; i < board.SizeY; i++)
            {
                for (int j = 0; j < board.SizeX; j++)
                {
                    if (board.Grid[j, i] is Entry cell)
                    {
                        if (cell.CurrentValue != cell.CorrectValue)
                        {
                            ResultLabel.Text = "Solution is incorrect. Try again!";
                            ResultLabel.ForeColor = System.Drawing.Color.Red;
                            isCorrect = false;
                        }
                    }
                }
            }

            if (isCorrect)
            {
                if (isRNG)
                {
                    ResultLabel.Text = "Congratulations! Puzzle solved!";
                    ResultLabel.ForeColor = System.Drawing.Color.Green;
                    Session["IsRNG"] = null;
                    Response.Write("<script>setTimeout(function(){ window.location='SelectConfiguration.aspx'; }, 2000);</script>");
                }
                else
                {
                    SQLManager sqlm2 = new SQLManager(connStr);
                    sqlm2.CompletedLevel((int)Session["MemberID"], (int)Session["LvlBoardID"]);
                    Session["LvlBoardID"] = null;
                    Response.Redirect("~/Views/Levels.aspx");
                }
            }
        }

        private void DisplayBoard()
        {
            KakuroTable.Rows.Clear();

            for (int i = 0; i < board.SizeY; i++)
            {
                TableRow row = new TableRow();
                for (int j = 0; j < board.SizeX; j++)
                {
                    TableCell tableCell = new TableCell();
                    var cell = board.Grid[j, i];

                    if (cell is Entry entryCell)
                    {
                        TextBox txt = new TextBox
                        {
                            ID = $"cell_{j}_{i}",
                            ClientIDMode = ClientIDMode.Static,
                            CssClass = "form-control input-cell",
                            MaxLength = 1,
                            // Persist value if it's already in the entry
                            Text = entryCell.CurrentValue != 0 ? entryCell.CurrentValue.ToString() : ""
                        };
                        tableCell.Controls.Add(txt);
                    }
                    else if (cell is Clue clueCell)
                    {
                        // The Panel renders as a <div> in the browser
                        Panel clueDiv = new Panel { CssClass = "clue-container" };

                        if (clueCell.VerticalClue.HasValue)
                        {
                            clueDiv.Controls.Add(new Label
                            {
                                Text = clueCell.VerticalClue.Value.ToString(),
                                CssClass = "v-clue"
                            });
                        }

                        if (clueCell.HorizontalClue.HasValue)
                        {
                            clueDiv.Controls.Add(new Label
                            {
                                Text = clueCell.HorizontalClue.Value.ToString(),
                                CssClass = "h-clue"
                            });
                        }

                        tableCell.CssClass = "clue-cell";
                        tableCell.Controls.Add(clueDiv);
                    }
                    else
                    {
                        tableCell.CssClass = "empty-cell bg-dark";
                        tableCell.BackColor = System.Drawing.Color.FromArgb(30, 41, 59); // Matches slate-800
                    }

                    row.Cells.Add(tableCell);
                }
                KakuroTable.Rows.Add(row);

            }
        }

    }
}