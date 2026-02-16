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
        private SqlConnection conn;
        private Board board;

        protected void Page_Load(object sender, EventArgs e)
        {
                //int boardID = (int)Session["boardId"];
                int boardID = 25; // Temporary hardcoded for testing, replace with session value when ready
                conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + Server.MapPath("~\\App_Data\\Kakuro.mdf;Integrated Security=True"));

                board = FetchBoard(boardID);


                if (board != null)
                {
                    Session["CurrentBoard"] = board;
                    GenerateGrid();

                    //timerPuzzle.Enabled = true;
                    Session["ElapsedTime"] = 0;
                }

                GenerateGrid();
        }

        //public bool ValidateSolution(Board board, int[,] userInput)
        //{
        //    return board.HorizontalSegments.All(segment => segment.IsValid()) &&
        //           board.VerticalSegments.All(segment => segment.IsValid());
        //} ==> For rng tables

        protected void btnCheckSolution_Click(object sender, EventArgs e) //Checks for non-dynamic puzzles only
        {
            int[,] userSolution = new int[board.SizeX, board.SizeY];

            for (int i = 0; i < board.SizeY; i++)
            {
                for (int j = 0; j < board.SizeX; j++)
                {
                    if (board.Grid[j, i].GetType().Name == "Entry")
                    {
                        string cellId = $"cell_{j}_{i}";
                        // Search inside the KakuroTable for the control
                        TextBox textBox = (TextBox)KakuroTable.FindControl(cellId);
                        var cell = (Entry)board.Grid[j, i];

                        if (textBox != null && textBox.Text == Convert.ToString(cell.CorrectValue))
                        {
                            ResultLabel.Text = "Solution is correct!";
                            ResultLabel.ForeColor = System.Drawing.Color.Green;

                        }
                        else
                        {
                            ResultLabel.Text = "Solution is incorrect. Try again!";
                            ResultLabel.ForeColor = System.Drawing.Color.Red;
                            textBox.BackColor = System.Drawing.Color.LightPink;
                        }
                    }
                }
            }
        }

        private void GenerateGrid()
        {
            KakuroTable.Rows.Clear();
            for (int i = 0; i < this.board.SizeY; i++)
            {
                TableRow row = new TableRow();
                for (int j = 0; j < board.SizeX; j++)
                {
                    TableCell tableCell = new TableCell();
                    var test = board.Grid[j, i].GetType().Name;

                    if (board.Grid[j, i].GetType().Name == "Entry")
                    {
                        TextBox textBox = new TextBox
                        {
                            ID = $"cell_{j}_{i}",
                            CssClass = "form-control input-cell",
                            MaxLength = 1
                        };
                        tableCell.Controls.Add(textBox);
                    }
                    else if(board.Grid[j, i].GetType().Name == "Clue")
                    {
                        tableCell.CssClass = "clue-cell bg-dark text-white";
                        string[] clue = GetClueText((Clue)board.Grid[j,i]);
                        tableCell.Text = $"{clue[0]} /{clue[1]}";
                    }
                    else
                    {
                        tableCell.CssClass = "clue-cell bg-dark text-white";
                    }
                    row.Cells.Add(tableCell);
                }
                KakuroTable.Rows.Add(row);
            }
        }

        public Board FetchBoard(int bID)
        {
            using (conn)
            {
                conn.Open();

                // 1. Get Board metadata first
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Board WHERE Id = @bID", conn))
                {
                    cmd.Parameters.AddWithValue("@bID", bID);
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    if (!reader.Read())
                    {
                        return null;
                    }

                    int sizeX = (int)reader["SizeX"];
                    int sizeY = (int)reader["SizeY"];
                    string difficulty = (string)reader["Difficulty"];
                    int score = (int)reader["Score"];
                    var grid = new Cell[sizeX, sizeY];
                    reader.Close();
                    var horSeg = new List<SumSegment>();
                    var verSeg = new List<SumSegment>();

                    FetchCells(bID, grid, horSeg, verSeg);

                    return new Board(bID, sizeX, sizeY, difficulty, grid, horSeg, verSeg, score);
                    
                }
            }
        }

        private void FetchCells(int bID, Cell[,] grid, List<SumSegment> horSeg, List<SumSegment> verSeg)
        {

            using (SqlCommand cmd2 = new SqlCommand("SELECT * FROM Cells WHERE BoardId = @PuzzleId", conn))
            {
                cmd2.Parameters.AddWithValue("@PuzzleId", bID);
                using (SqlDataReader reader2 = cmd2.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        int x = (int)reader2["X"];
                        int y = (int)reader2["Y"];
                        string cellType = (string)reader2["CellType"];
                        int? correctValue = reader2["CorrectValue"] == DBNull.Value ? null : (int?)reader2["CorrectValue"];
                        int? horizontalClue = reader2["HorizontalClueValue"] == DBNull.Value ? null : (int?)reader2["HorizontalClueValue"];
                        int? verticalClue = reader2["VerticalClueValue"] == DBNull.Value ? null : (int?)reader2["VerticalClueValue"];

                        if (cellType == "Entry")
                        {
                            grid[x, y] = new Entry(x, y, correctValue ?? 0);
                        }
                        else if (cellType == "Clue")
                        {
                            grid[x, y] = new Clue(x, y, horizontalClue, verticalClue);
                        }
                        else
                        {
                            grid[x, y] = new Empty(x, y);
                        }


                        if (horizontalClue.HasValue)
                        {
                            horSeg.Add(new SumSegment(horizontalClue.Value, new List<Entry>()));
                        }
                        if (verticalClue.HasValue)
                        {
                            verSeg.Add(new SumSegment(verticalClue.Value, new List<Entry>()));
                        }
                    }
                }
            }
        }


        private string[] GetClueText(Clue cell)
        {
            // Formats the cell to show "V \ H" or similar logic
            string v = cell.VerticalClue.HasValue ? cell.VerticalClue.ToString() : "";
            string h = cell.HorizontalClue.HasValue ? cell.HorizontalClue.ToString() : "";

            string[] clue = string.IsNullOrEmpty(v) && string.IsNullOrEmpty(h) ? null : new[] {v, h};

            return clue;
        }
    

        //protected void timerPuzzle_Tick(object sender, EventArgs e)
        //{
        //    int elapsedTime = (Session["ElapsedTime"] != null ? (int)Session["ElapsedTime"] : 0) + 1;
        //    Session["ElapsedTime"] = elapsedTime;
        //    TimeLabel.Text = $"Time Elapsed: {elapsedTime} seconds";
        //}
    }
}