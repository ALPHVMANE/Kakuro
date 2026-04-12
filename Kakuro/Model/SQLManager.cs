using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.EnterpriseServices;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace Kakuro.Model
{
    public class SQLManager
    {
        private Board board = null;
        private string cStr;

        public SQLManager(string connStr)
        {
            cStr = connStr;
        }

        //====== Gameplay methods ======//
        public bool SessionExists(int bID, int uID) // uID == Session["MemberID"]
        {
            using (SqlConnection conn = new SqlConnection(cStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM GameState WHERE BoardID = @bID AND UserID = @uID", conn))
                {
                    cmd.Parameters.AddWithValue("@bID", bID);
                    cmd.Parameters.AddWithValue("@uID", uID);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int count = reader.GetInt32(0);
                        return count > 0;
                    }
                    return false;
                }
            }
        }
        public void InsertGameState(int bID, int uID)
        {
            using (SqlConnection conn = new SqlConnection(cStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO GameState (UserID, BoardID, Status, Score, Errors) VALUES (@uID, @bID, 0, 0, 0)", conn))
                {
                    cmd.Parameters.AddWithValue("@bID", bID);
                    cmd.Parameters.AddWithValue("@uID", uID);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public Board FetchBoardData(int bID, int uID)
        {
            using (SqlConnection conn = new SqlConnection(cStr))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT b.*, gs.SessionID, gs.Score as SavedScore, gs.Errors " +
                    "FROM Board b " +
                    "LEFT JOIN GameState gs ON b.Id = gs.BoardID AND gs.UserID = @uID" +
                    " WHERE b.Id = @bID", conn))
                {
                    cmd.Parameters.AddWithValue("@bID", bID);
                    cmd.Parameters.AddWithValue("@uID", uID);

                    using (SqlDataReader rSess = cmd.ExecuteReader())
                    {
                        if (rSess.Read())
                        {
                            int gID = (int)rSess["SessionID"];
                            int sx = (int)rSess["SizeX"];
                            int sy = (int)rSess["SizeY"];
                            string diff = rSess["Difficulty"].ToString();
                            int score = rSess["SavedScore"] == DBNull.Value ? 0 : (int)rSess["SavedScore"];

                            board = new Board(bID, sx, sy, diff, new Cell[sx, sy]);
                            rSess.Close();

                            if (board != null)
                            {
                                FetchCellState(gID, conn);
                            }

                        }
                    }
                }

                return board;
            }
        }
        private void FetchCellState(int? gID, SqlConnection conn)
        {

            using (SqlCommand cmd2 =
                new SqlCommand("SELECT cs.UserValue, cs.Id, c.* " +
                "FROM Cells c " +
                "LEFT JOIN CellState cs ON c.Id = cs.CellId AND cs.SessionId = @gID " +
                "WHERE c.BoardID = @bID", conn))
            {
                cmd2.Parameters.AddWithValue("@giD", gID);
                cmd2.Parameters.AddWithValue("@biD", board.Id);
                using (SqlDataReader rSess2 = cmd2.ExecuteReader())
                {
                    while (rSess2.Read())
                    {
                        int x = (int)rSess2["X"];
                        int y = (int)rSess2["Y"];
                        string type = rSess2["CellType"].ToString();

                        if (type == "Entry")
                        {
                            var entry = new Entry(x, y, (int)rSess2["CorrectValue"]);
                            if (rSess2["UserValue"] != DBNull.Value)
                            {
                                entry.CurrentValue = (int)rSess2["UserValue"];
                            }
                            board.Grid[x, y] = entry;
                        }
                        else if (type == "Clue")
                        {
                            int? h = rSess2["HorizontalClueValue"] == DBNull.Value ? null : (int?)rSess2["HorizontalClueValue"];
                            int? v = rSess2["VerticalClueValue"] == DBNull.Value ? null : (int?)rSess2["VerticalClueValue"];
                            board.Grid[x, y] = new Clue(x, y, h, v);
                        }
                        else
                        {
                            board.Grid[x, y] = new Empty(x, y);
                        }
                    }
                }

            }
        }
        public Board FetchBoardData(int bID)
        {
            using (SqlConnection conn = new SqlConnection(cStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Board WHERE Id = @bID", conn))
                {
                    cmd.Parameters.AddWithValue("@bID", bID);
                    SqlDataReader r = cmd.ExecuteReader();

                    if (r.Read())
                    {
                        int sx = (int)r["SizeX"];
                        int sy = (int)r["SizeY"];
                        string diff = r["Difficulty"].ToString();
                        int score = (int)r["Score"];


                        board = new Board(bID, sx, sy, diff, new Cell[sx, sy]);
                        r.Close();
                        FetchCells(bID, conn);
                    }
                }
            }
            return board;
        }
        private void FetchCells(int bID, SqlConnection conn)
        {
            using (SqlCommand cmd2 = new SqlCommand("SELECT * FROM Cells WHERE BoardId = @bID", conn))
            {
                cmd2.Parameters.AddWithValue("@bID", bID);
                using (SqlDataReader reader2 = cmd2.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        int x = (int)reader2["X"];
                        int y = (int)reader2["Y"];
                        string type = reader2["CellType"].ToString();

                        if (type == "Entry")
                        {
                            int correctVal = (int)reader2["CorrectValue"];
                            board.Grid[x, y] = new Entry(x, y, correctVal);
                        }
                        else if (type == "Clue")
                        {
                            int? h = reader2["HorizontalClueValue"] == DBNull.Value ? null : (int?)reader2["HorizontalClueValue"];
                            int? v = reader2["VerticalClueValue"] == DBNull.Value ? null : (int?)reader2["VerticalClueValue"];
                            board.Grid[x, y] = new Clue(x, y, h, v);
                        }
                        else
                        {
                            board.Grid[x, y] = new Empty(x, y);
                        }
                    }
                }
            }
        }


        //====== Levels methods ======//
        public int GetCompletedLevelsCount(int uID)
        {
            using (SqlConnection conn = new SqlConnection(cStr))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT LevelsCompleted FROM Users WHERE Id = @uID", conn))
                {
                    cmd.Parameters.AddWithValue("@uID", uID);
                    using (SqlDataReader rLvl = cmd.ExecuteReader())
                    {
                        if (!rLvl.Read())
                        {
                            return -1;
                        }

                        return (int)rLvl["LevelsCompleted"];
                    }
                }
            }
        }

        // updates the levels if correct
        public void CompletedLevel(int uID, int boardID)
        {
            using (SqlConnection conn = new SqlConnection(cStr))
            {
                conn.Open();
                //mark as completed in db
                using (SqlCommand cmd = new SqlCommand("UPDATE GameState SET Status = 1 WHERE UserID = @uID AND BoardID = @bID", conn))
                {
                    cmd.Parameters.AddWithValue("@uID", uID);
                    cmd.Parameters.AddWithValue("@bID", boardID);
                    cmd.ExecuteNonQuery();
                }

                //increment levels
                using (SqlCommand cmd2 = new SqlCommand("UPDATE Users SET LevelsCompleted = LevelsCompleted + 1 " +
                    "WHERE Id = @uID AND LevelsCompleted < @boardID", conn))
                {
                    cmd2.Parameters.AddWithValue("@uID", uID);
                    cmd2.Parameters.AddWithValue("@boardID", boardID);
                    cmd2.ExecuteNonQuery();
                }
            }
        }

    }
}