using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kakuro.Model
{
    public interface GameCollection
    {
        bool SessionExists(int bID, int uID);
        void InsertGameState(int bID, int uID);
        Board FetchBoardData(int bID, int uID);
        Board FetchBoardData(int bID);
    }
}