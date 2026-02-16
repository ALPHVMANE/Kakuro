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

        public List<SumSegment> HorizontalSegments { get; set; }
        public List<SumSegment> VerticalSegments { get; set; }

        // Single flexible constructor using optional parameters
        public Board(int id, int sizeX, int sizeY, string difficulty, Cell[,] grid,
                     List<SumSegment> horSeg = null, List<SumSegment> verSeg = null, int score = 0)
        {
            Id = id;
            SizeX = sizeX;
            SizeY = sizeY;
            Difficulty = difficulty;
            Grid = grid;
            Score = score;
            HorizontalSegments = horSeg ?? new List<SumSegment>();
            VerticalSegments = verSeg ?? new List<SumSegment>();
        }

        // Example of a computed property for scoring
//         public int CalculatedScore => CalculateScore();

        //private int calculateScore() { 
        //    //Coming Soon...
        //}

    }

    public class SumSegment
    {
        public int TargetSum { get; set; }
        public List<Entry> Entries { get; set; }

        public SumSegment(int targetSum, List<Entry> entries)
        {
            TargetSum = targetSum;
            Entries = entries;
        }
        public bool IsValid()
        {
            var values = Entries
                .Select(e => e.CurrentValue)
                .Where(v => v.HasValue)
                .Cast<int>()
                .ToList();

            // 1. Check for duplicates
            if (values.Count != values.Distinct().Count()) return false;

            var currentSum = values.Sum();

            // 2. Cannot exceed target
            if (currentSum > TargetSum) return false;

            // 3. If all cells filled, must equal target
            if (values.Count == Entries.Count && currentSum != TargetSum) return false;

            return true;
        }
    }
}