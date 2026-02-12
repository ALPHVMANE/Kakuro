using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kakuro.Model
{
    public enum CellType { Clue, Entry, Empty }

    public class KakuroCell
    {
        private int id;
        private int x;
        private int y;
        private CellType type;

        //Entry Cells / Empty cell
        private int? currentValue;
        private int correctValue; // For validation

        //Clue Cells
        private int? horizontalSumClue;
        private int? verticalSumClue;

        public KakuroCell(int id, int x, int y, CellType type, int? currentValue, int correctValue, int? horizontalSumClue, int? verticalSumClue)
        {
            this.id = id;
            this.x = x;
            this.y = y;
            this.type = type;
            this.currentValue = currentValue;
            this.correctValue = correctValue;
            this.horizontalSumClue = horizontalSumClue;
            this.verticalSumClue = verticalSumClue;
        }

        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public CellType Type { get => type; set => type = value; }
        public int? CurrentValue { get => currentValue; set => currentValue = value; }
        public int CorrectValue { get => correctValue; set => correctValue = value; }
        public int? HorizontalSumClue { get => horizontalSumClue; set => horizontalSumClue = value; }
        public int? VerticalSumClue { get => verticalSumClue; set => verticalSumClue = value; }
        public int Id { get => id; set => id = value; }

    }
}