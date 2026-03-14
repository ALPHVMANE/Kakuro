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
        private readonly GameCollection gc;

        public PuzzleManager(GameCollection gameC)
        {
            gc = gameC;
        }

        public Board initBoard(int bID, int uID)
        {
            boardID = bID;

            Board currentBoard;

            if (!gc.SessionExists(boardID, uID)){  
                gc.InsertGameState(boardID, uID);
                currentBoard = gc.FetchBoardData(boardID);
            }
            else
            {
                currentBoard = gc.FetchBoardData(boardID, uID);
            }

            return currentBoard;
        }
    }
}