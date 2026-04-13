using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Kakuro.Model
{
    public class PuzzleManager
    {
        private int boardID;
        public SQLManager sql;

        public PuzzleManager(SQLManager gameC)
        {
            sql = gameC;
        }

        public Board initBoard(int bID, int uID)
        {
            boardID = bID;

            Board currentBoard;

            if (!sql.SessionExists(boardID, uID)){  
                sql.InsertGameState(boardID, uID);
                currentBoard = sql.FetchBoardData(boardID);
            }
            else
            {
                currentBoard = sql.FetchBoardData(boardID, uID);
            }

            return currentBoard;
        }

        public Board initFromTemplate(int size, string difficulty)
        {
            Board template = sql.FetchRandomTemplate(size, difficulty);

            if (template == null)
                throw new Exception("No template found.");

            RNGController rng = new RNGController(template);

            return rng.tempBoard;
        }

    }
}