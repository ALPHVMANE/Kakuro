using Kakuro.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Kakuro
{
    public partial class Gameplay : System.Web.UI.Page
    {
        protected SqlConnection conn;

        protected Board board;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int boardID = Convert.ToInt32(Request.QueryString["BoardId"]);
                conn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + Server.MapPath("~\\App_Data\\Kakuro.mdf;Integrated Security=True"));

                conn.Open();
                board = FetchBoard(boardID);

                if (board != null)
                {
                    Session["CurrentBoard"] = board;
                    GenerateGrid(board);

                    //timerPuzzle.Enabled = true;
                    Session["ElapsedTime"] = 0;
                }

                GenerateGrid(board);
            }
            else
            {
                if (Session["CurrentBoard"] == board)
                {
                    GenerateGrid(board);
                }
            }
        }

        protected void btnCheckSolution_Click(object sender, EventArgs e)
        {
            Board board = (Board)Session["CurrentBoard"];
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

                        if (textBox != null && int.TryParse(textBox.Text, out int value))
                        {
                            userSolution[j, i] = value;
                        }
                        else
                        {
                            userSolution[j, i] = 0; // Default for empty/invalid
                        }
                    }
                }
            }

            bool isValid = ValidateSolution(board, userSolution);

            ResultLabel.Text = isValid ? "Solution is correct!" : "Solution is incorrect. Try again!";
            ResultLabel.ForeColor = isValid ? System.Drawing.Color.Green : System.Drawing.Color.Red;
        }

        private void GenerateGrid(Board board)
        {
            KakuroTable.Rows.Clear();
            for (int i = 0; i < board.SizeY; i++)
            {
                TableRow row = new TableRow();
                for (int j = 0; j < board.SizeX; j++)
                {
                    TableCell tableCell = new TableCell();

                    if (board.Grid[j, i].GetType().Name == "Entry")
                    {
                        TextBox textBox = new TextBox 
                        {
                            ID = $"cell_{j}_{i}",
                            CssClass = "form-control input-cell",
                            TextMode = TextBoxMode.Number,
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
            SqlCommand cmd = new SqlCommand("SELECT * FROM Board WHERE Id = @bID", conn);
            cmd.Parameters.AddWithValue("@bID", bID);

            SqlDataReader reader = cmd.ExecuteReader();
            
            if (!reader.Read())
            {
                conn.Close();
                return null;
            }
            int sizeX = (int)reader["Width"];
            int sizeY = (int)reader["Height"];
            int difficulty = (int)reader["Difficulty"];
            int score = (int)reader["Score"];

            var grid = new Cell[sizeX, sizeY];
            var horSeg = new List<SumSegment>();
            var verSeg = new List<SumSegment>();

            FetchCells(bID, grid, horSeg, verSeg);

            return new Board(bID, sizeX, sizeY, difficulty, grid, horSeg, verSeg, score);
        }

        private void FetchCells(int bID, Cell[,] grid, List<SumSegment> horSeg, List<SumSegment> verSeg)
        {

            SqlCommand cmd = new SqlCommand("SELECT * FROM Cells WHERE BoardId = @PuzzleId", conn);
            cmd.Parameters.AddWithValue("@PuzzleId", bID);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int x = (int)reader["X"];
                int y = (int)reader["Y"];
                string cellType = (string)reader["CellType"];
                int? correctValue = (int?)reader["CorrectValue"] ?? null;
                int? horizontalClue = (int?)reader["HorizontalClueValue"] ?? null;
                int? verticalClue = (int?)reader["VerticalClueValue"] ?? null;

                if (cellType == "Entry")
                {
                    grid[x, y] = new Entry(x, y, correctValue ?? 0);
                }
                else if (cellType == "Clue")
                {
                    grid[x, y] = new Clue(x, y, horizontalClue ?? null, verticalClue ?? null);
                }
                else
                {
                    grid[x, y] = new Empty(x, y);
                }

                // Populate segments
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


        private string[] GetClueText(Clue cell)
        {
            // Formats the cell to show "V \ H" or similar logic
            string v = cell.VerticalClue.HasValue ? cell.VerticalClue.ToString() : "";
            string h = cell.HorizontalClue.HasValue ? cell.HorizontalClue.ToString() : "";

            string[] clue = string.IsNullOrEmpty(v) && string.IsNullOrEmpty(h) ? null : new[] {v, h};

            return clue;
        }
        public bool ValidateSolution(Board board, int[,] userInput)
        {
            return board.HorizontalSegments.All(segment => segment.IsValid()) &&
                   board.VerticalSegments.All(segment => segment.IsValid());
        }

        //protected void timerPuzzle_Tick(object sender, EventArgs e)
        //{
        //    int elapsedTime = (Session["ElapsedTime"] != null ? (int)Session["ElapsedTime"] : 0) + 1;
        //    Session["ElapsedTime"] = elapsedTime;
        //    TimeLabel.Text = $"Time Elapsed: {elapsedTime} seconds";
        //}
    }
}