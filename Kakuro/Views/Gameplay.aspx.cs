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
            connStr = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + Server.MapPath("~\\App_Data\\Kakuro.mdf;Integrated Security=True");
            GameCollection sqlm = new SQLManager(connStr);
            PuzzleManager pm = new PuzzleManager(sqlm);

            if (!IsPostBack)
            {
                Session["MemberID"] = 1; //temp

                int boardID = (int)Session["LvlBoardID"];


                board = pm.initBoard(boardID, (int)Session["MemberID"]);

                if (board != null)
                {
                    Session["CurrentBoard"] = board;
                }
            }
            else
            {
                board = (Board)Session["CurrentBoard"];
            }


            if (board != null)
            {
                DisplayBoard();
            }
            else
            {
                Response.Write("<script>alert('Unable to load board.')</script>");
            }

        }

        //public bool ValidateSolution(Board board, int[,] userInput)
        //{
        //    return board.HorizontalSegments.All(segment => segment.IsValid()) &&
        //           board.VerticalSegments.All(segment => segment.IsValid());
        //} ==> For rng tables

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
                            MaxLength = 1
                        };
                        tableCell.Controls.Add(txt);
                    }
                    else if (cell is Clue clueCell)
                    {
                        Panel clueDiv = new Panel
                        {
                            CssClass = "clue-container" // This is your <div> class
                        };

                        // Create labels for the values to go inside the div
                        if (clueCell.VerticalClue.HasValue)
                        {
                            Label vLabel = new Label
                            {
                                Text = clueCell.VerticalClue.Value.ToString(),
                                CssClass = "v-clue"
                            };
                            clueDiv.Controls.Add(vLabel);
                        }

                        if (clueCell.HorizontalClue.HasValue)
                        {
                            Label hLabel = new Label
                            {
                                Text = clueCell.HorizontalClue.Value.ToString(),
                                CssClass = "h-clue"
                            };
                            clueDiv.Controls.Add(hLabel);
                        }

                        
                        tableCell.CssClass = "clue-cell bg-dark text-white";
                        tableCell.Controls.Add(clueDiv);
                    }
                    else
                    {
                        tableCell.CssClass = "empty-cell bg-dark";
                    }

                    row.Cells.Add(tableCell);
                }
                KakuroTable.Rows.Add(row);
            }
        }

    }
}