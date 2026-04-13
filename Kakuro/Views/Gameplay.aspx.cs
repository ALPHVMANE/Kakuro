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
        private Board board;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["MemberID"] == null)
            {
                Response.Redirect("~/Views/Login.aspx");
                return;
            }

            board = Session["CurrentBoard"] as Board;

            if (board == null)
            {
                Response.Redirect("~/Views/Configurations.aspx");
                return;
            }

            if (!IsPostBack)
                DisplayBoard();
        }

        protected void btnCheckSolution_Click(object sender, EventArgs e) //Checks for non-dynamic puzzles only
        {
            bool isCorrect = true;
            int[,] userSolution = new int[board.SizeX, board.SizeY];

            for (int i = 0; i < board.SizeY; i++)
            {
                for (int j = 0; j < board.SizeX; j++)
                {
                    if (board.Grid[j, i].GetType().Name == "Entry")
                    {
                        string cellId = $"cell_{j}_{i}";
                        
                        TextBox textBox = (TextBox)KakuroTable.FindControl(cellId);
                        var cell = (Entry)board.Grid[j, i];

                        if (textBox == null || textBox.Text != Convert.ToString(cell.CorrectValue))
                        {
                            ResultLabel.Text = "Solution is incorrect. Try again!";
                            ResultLabel.ForeColor = System.Drawing.Color.Red;
                            textBox.BackColor = System.Drawing.Color.LightPink;
                            isCorrect = false;
                        }
                    }
                }
            }

            if (isCorrect)
            {
                ResultLabel.Text = "Congratulations! You solved the puzzle!";
                ResultLabel.ForeColor = System.Drawing.Color.Green;
                SQLManager sqlm = new SQLManager("DataSource=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" +
                Server.MapPath("~\\App_Data\\Kakuro.mdf;Integrated Security=True"));
                sqlm.CompletedLevel((int)Session["MemberID"], (int)Session["LvlBoardID"]);

                // redirect to next level or back to level select
                Session["LvlBoardID"] = null;
                Response.Redirect("~/Views/Levels.aspx");
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