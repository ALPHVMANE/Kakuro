using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kakuro.Model
{
    public class Board
    {

        private int id;
        private int score;
        private int sizeX;
        private int sizeY;
        private int difficulty;
        private Cell[,] grid;

        private List<SumSegment> horizontalSegments;
        private List<SumSegment> verticalSegments;

        public Board(int id, int sizeX, int sizeY, int difficulty, Cell[,] grid, List<SumSegment> horSeg, List<SumSegment> verticalSegments)
        {
            this.id = id;
            this.score = 0;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.difficulty = difficulty;
            this.grid = grid;
            this.horizontalSegments = horSeg;
            this.verticalSegments = verticalSegments;
        }

        public Board(int id, int score, int sizeX, int sizeY, int difficulty, Cell[,] grid, List<SumSegment> horSeg, List<SumSegment> verticalSegments)
        {
            this.id = id;
            this.score = score;
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.difficulty = difficulty;
            this.grid = grid;
            this.horizontalSegments = horSeg;
            this.verticalSegments = verticalSegments;
        }

        public int Id { get => id; set => id = value; }
        public int Score { get => score; set => score = value; }
        public int SizeX { get => sizeX; set => sizeX = value; }
        public int SizeY { get => sizeY; set => sizeY = value; }
        public int Difficulty { get => difficulty; set => difficulty = value; }
        public Cell[,] Grid { get => grid; set => grid = value; }
        public List<SumSegment> HorizontalSegments { get => horizontalSegments; set => horizontalSegments = value; }
        public List<SumSegment> VerticalSegments { get => verticalSegments; set => verticalSegments = value; }

        private int calculateScore() { 
            //Coming Soon...
        }

    }
    public class SumSegment
    {
        public int TargetSum { get; set; }
        // Change List<Cell> to List<Entry>
        public List<Entry> Entries { get; set; }

        public SumSegment(int targetSum, List<Entry> entries)
        {
            this.TargetSum = targetSum;
            this.Entries = entries;
        }

        public bool IsValid()
        {
            // Now 'e' is recognized as an Entry, so CurrentValue works!
            var values = Entries
                .Where(e => e.CurrentValue.HasValue)
                .Select(e => e.CurrentValue.Value)
                .ToList();

            if (values.Count != values.Distinct().Count()) return false;
            if (values.Sum() > TargetSum) return false;
            if (values.Count == Entries.Count && values.Sum() != TargetSum) return false;

            return true;
        }
    }
}