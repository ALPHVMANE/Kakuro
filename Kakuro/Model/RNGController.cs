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
            tempBoard = new Board(board.Id, board.SizeX, board.SizeY, board.Difficulty, board.Grid);
            bTemplate = board;
            rng = new SecureRandom();
            cells = new int[board.SizeX, board.SizeY];

            InputGenerator();
        }

        private void InputGenerator()
        {

            //populate entire grid
            for (int x = 0; x < cells.GetLength(0); x++)
            {
                for (int y = 0; y < cells.GetLength(1); y++)
                {
                    cells[x, y] = rng.Next(1, 9);
                }
            }

            var entryPositions = new List<(int x, int y)>();
            for (int y = 0; y < bTemplate.SizeY; y++)
                for (int x = 0; x < bTemplate.SizeX; x++)
                    if (bTemplate.Grid[x, y] is Entry)
                        entryPositions.Add((x, y));

            bool solved = Backtrack(entryPositions, 0);

            if (!solved)
                throw new InvalidOperationException(
                    "Could not fill the board with valid digits. " +
                    "Check that your board layout has no impossible runs.");

            for (int y = 0; y < tempBoard.SizeY; y++)
                for (int x = 0; x < tempBoard.SizeX; x++)
                    if (tempBoard.Grid[x, y] is Entry entry && cells[x, y] > 0)
                        entry.CorrectValue = cells[x, y];

            RecalculateClues();
        }

        private bool Backtrack(List<(int x, int y)> positions, int index)
        {
            if (index == positions.Count)
                return true;

            var (x, y) = positions[index];


            var candidates = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Shuffle(candidates);

            foreach (int value in candidates)
            {
                if (ValidatePlacement(x, y, value))
                {
                    cells[x, y] = value;

                    if (Backtrack(positions, index + 1))
                        return true;

                    cells[x, y] = -1; 
                }
            }

            return false; 
        }

        private bool ValidatePlacement(int x, int y, int value)
        {
           
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


                    int hSum = 0;
                    for (int cx = x + 1; cx < tempBoard.SizeX; cx++)
                    {
                        if (!(bTemplate.Grid[cx, y] is Entry)) break;
                        hSum += cells[cx, y];
                    }
                    clueCell.HorizontalClue = hSum > 0 ? hSum : (int?)null;

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