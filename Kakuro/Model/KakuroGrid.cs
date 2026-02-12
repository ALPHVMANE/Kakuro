using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kakuro.Model
{
    public class SumSegment
    {
        private int targetSum;
        private List<KakuroCell> cells;

        public SumSegment(int targetSum, List<KakuroCell> cells)
        {
            this.targetSum = targetSum;
            this.cells = new List<KakuroCell>();
        }

        public int TargetSum { get; set; }
        public List<KakuroCell> Cells { get; set; }

        public bool IsValid()
        {
            var values = Cells.Where(c => c.CurrentValue.HasValue).Select(c => c.CurrentValue.Value).ToList();  //placeholder, will fetch from db
        
            if (values.Count != values.Distinct().Count()) return false;

            if (values.Sum() > TargetSum) return false;

            if (values.Count == Cells.Count && values.Sum() != TargetSum) return false;

            return true;
        }
    }
    public class KakuroGrid
    {

        private int id;
        private int score;
        private KakuroCell[,] grid;
        private List<SumSegment> horizontalSegments;
        private List<SumSegment> verticalSegments;

        public KakuroGrid(int id, int score, KakuroCell[,] grid, List<SumSegment> horizontalSegments, List<SumSegment> verticalSegments)
        {
            this.id = id;
            this.score = score;
            this.grid = grid;
            this.horizontalSegments = horizontalSegments;
            this.verticalSegments = verticalSegments;
        }
        public int Id { get => id; set => id = value; }
        public int Score { get => score; set => score = value; }
        public KakuroCell[,] Grid { get => grid; set => grid = value; }
        public List<SumSegment> HorizontalSegments { get => horizontalSegments; set => horizontalSegments = value; }
        public List<SumSegment> VerticalSegments { get => verticalSegments; set => verticalSegments = value; }



        private int calculateScore() { return -1; } // Placeholder for score calculation logic

    }
}