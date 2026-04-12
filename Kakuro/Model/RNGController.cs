using Microsoft.Ajax.Utilities;
using SQLitePCL;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Org.BouncyCastle.Security;

namespace Kakuro.Model
{
    public class RNGController
    {
        public Board tempBoard;
        public Board bTemplate;

        private SecureRandom rng;

        private int[,] cells;

        public RNGController(Board board)
        {
            tempBoard = new Board(board.Id, board.SizeX, board.SizeY, board.Difficulty);
            bTemplate = board;
            rng = new SecureRandom();
            cells = new int[board.SizeX, board.SizeY];

            cells = InputGenerator();
        }

        private int[,] InputGenerator()
        {


            foreach (Cell cell in bTemplate.Grid)
            {
                if (!(cell is Entry))
                {
                    cells[cell.X, cell.Y] = -1;
                }
            }

            //populate entire grid
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    cells[x, y] = rng.Next(1, 9);
                }
            }

            // Collect Entry positions in traversal order (row-major)
      
            var entryPositions = new List<(int x, int y)>();
            for (int y = 0; y < bTemplate.SizeY; y++)
                for (int x = 0; x < bTemplate.SizeX; x++)
                    if (bTemplate.Grid[x, y] is Entry)
                        entryPositions.Add((x, y));

            // Backtracking fill
            bool solved = Backtrack(entryPositions, 0);

            if (!solved)
                throw new InvalidOperationException(
                    "Could not fill the board with valid digits. " +
                    "Check that your board layout has no impossible runs.");

            // Write filled values back into tempBoard's Entry cells
            for (int y = 0; y < tempBoard.SizeY; y++)
                for (int x = 0; x < tempBoard.SizeX; x++)
                    if (tempBoard.Grid[x, y] is Entry entry && cells[x, y] > 0)
                        entry.CorrectValue = cells[x, y];

            // Recalculate clue sums from the filled grid
            RecalculateClues();

            return cells;
        }

        private bool Backtrack(List<(int x, int y)> positions, int index)
        {
            if (index == positions.Count)
                return true; // all cells filled successfully

            var (x, y) = positions[index];

            // Shuffle digits
            var candidates = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Shuffle(candidates);

            foreach (int value in candidates)
            {
                if (ValidatePlacement(x, y, value))
                {
                    cells[x, y] = value;

                    if (Backtrack(positions, index + 1))
                        return true;

                    cells[x, y] = -1; // undo and try next
                }
            }

            return false; // trigger backtrack to previous cell
        }

        private bool ValidatePlacement(int x, int y, int value)
        {
            // Check horizontal run (same row, scan left and right from (x,y))
            for (int cx = x - 1; cx >= 0; cx--)
            {
                if (!(bTemplate.Grid[cx, y] is Entry)) break;
                if (cells[cx, y] == value) return false;
            }
            for (int cx = x + 1; cx < bTemplate.SizeX; cx++)
            {
                if (!(bTemplate.Grid[cx, y] is Entry)) break;
                if (cells[cx, y] == value) return false;
            }

            // Check vertical run (same column, scan up and down from (x,y))
            for (int cy = y - 1; cy >= 0; cy--)
            {
                if (!(bTemplate.Grid[x, cy] is Entry)) break;
                if (cells[x, cy] == value) return false;
            }
            for (int cy = y + 1; cy < bTemplate.SizeY; cy++)
            {
                if (!(bTemplate.Grid[x, cy] is Entry)) break;
                if (cells[x, cy] == value) return false;
            }

            return true;
        }

        private void RecalculateClues()
        {
            for (int y = 0; y < tempBoard.SizeY; y++)
            {
                for (int x = 0; x < tempBoard.SizeX; x++)
                {
                    if (!(tempBoard.Grid[x, y] is Clue clueCell))
                        continue;

                    // Horizontal sum: add up the Entry run to the right
                    int hSum = 0;
                    for (int cx = x + 1; cx < tempBoard.SizeX; cx++)
                    {
                        if (!(bTemplate.Grid[cx, y] is Entry)) break;
                        hSum += cells[cx, y];
                    }
                    clueCell.HorizontalClue = hSum > 0 ? hSum : (int?)null;

                    // Vertical sum: add up the Entry run downward
                    int vSum = 0;
                    for (int cy = y + 1; cy < tempBoard.SizeY; cy++)
                    {
                        if (!(bTemplate.Grid[x, cy] is Entry)) break;
                        vSum += cells[x, cy];
                    }
                    clueCell.VerticalClue = vSum > 0 ? vSum : (int?)null;
                }
            }
        }

        private void Shuffle(List<int> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}