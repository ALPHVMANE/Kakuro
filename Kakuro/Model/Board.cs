using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kakuro.Model
{
    public class Board
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public string Difficulty { get; set; }
        public Cell[,] Grid { get; set; }

        // Single flexible constructor using optional parameters
        public Board(int id, int sizeX, int sizeY, string difficulty, Cell[,] grid, int score = 0)
        {
            Id = id;
            SizeX = sizeX;
            SizeY = sizeY;
            Difficulty = difficulty;
            Grid = grid;
            Score = score;
        }

        // Example of a computed property for scoring
//         public int CalculatedScore => CalculateScore();

        //private int calculateScore() { 
        //    //Coming Soon...
        //}

    }
}