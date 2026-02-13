using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kakuro.Model
{
    public abstract class Cell
    {
        private int x;
        private int y;

        public Cell(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

    }

    public class Clue : Cell
    {
        private int? horizontalSumClue;
        private int? verticalSumClue;

        public Clue(int x, int y, int? horizontalSumClue, int? verticalSumClue) : base(x, y)
        {
            this.horizontalSumClue = horizontalSumClue;
            this.verticalSumClue = verticalSumClue;
        }

        public int? HorizontalSumClue { get => horizontalSumClue; set => horizontalSumClue = value; }
        public int? VerticalSumClue { get => verticalSumClue; set => verticalSumClue = value; }
    }

    public class Entry : Cell
    {
        private int? currentValue;
        private int correctValue; // validation check

        public Entry(int x, int y, int? currentValue, int correctValue) : base(x, y)
        {
            this.currentValue = currentValue;
            this.correctValue = correctValue;
        }

        public int? CurrentValue { get => currentValue; set => currentValue = value; }
        public int CorrectValue { get => correctValue; set => correctValue = value; }
    }

    public class Blacked : Cell
    {
        public Blacked(int x, int y) : base(x, y)
        {
        }
    }

}