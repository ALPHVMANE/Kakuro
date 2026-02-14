using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kakuro.Model
{
    public abstract class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }

    public class Clue : Cell
    {
        public int? HorizontalSumClue { get; set; }
        public int? VerticalSumClue { get; set; }

        public Clue(int x, int y, int? horizontalSum, int? verticalSum) : base(x, y)
        {
            this.HorizontalSumClue = horizontalSum;
            this.VerticalSumClue = verticalSum;
        }
    }

    public class Entry : Cell
    {
        public int? CurrentValue { get; set;} 
        public int CorrectValue { get; set;}

        // Logic helper: Check if the user's input is right
        public bool IsCorrect => CurrentValue == CorrectValue;

        public Entry(int x, int y, int correctValue) : base(x, y)
        {
            this.CorrectValue = correctValue;
            this.CurrentValue = null; // Initially empty
        }
    }

    public class Empty : Cell 
    { 
        public Empty(int x, int y) : base(x, y)
        {
        }
    }


}