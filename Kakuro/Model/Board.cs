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
        public Board(int id, int sizeX, int sizeY, string difficulty, Cell[,] grid)
        {
            Id = id;
            SizeX = sizeX;
            SizeY = sizeY;
            Difficulty = difficulty;
            Grid = grid;
            Score = 0;
        }

        public Board(int id, int sizeX, int sizeY, string difficulty)
        {
            Id = id;
            SizeX = sizeX;
            SizeY = sizeY;
            Difficulty = difficulty;
            Grid = new Cell[sizeX, sizeY];
            Score = 0;
        }

    }
}