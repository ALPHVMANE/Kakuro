using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kakuro.Model
{
    public class RNGController
    {
        private int sizeX;
        private int sizeY;
        private Random rng = new Random();
        private Cell[,] grid;

        public RNGController(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.grid = new Cell[sizeX, sizeY];
        }

        public Board GenerateBoard(int puzzleId, string difficulty)
        {
            RNGEntryCells();
            CreateClueCells();
            return addCellsToBoard(puzzleId, difficulty);
        }

        private void RNGEntryCells()
        {
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++) 
                {
                    bool isEdge = (x == 0 || y == 0);
                    if (isEdge) 
                    {
                        grid[x, y] = new Empty(x, y);
                    }
                    else
                    {
                        grid[x, y] = rng.NextDouble() < 0.65 ? (Cell)new Entry(x, y, 0) : new Empty(x, y);
                    }
                }
            }
        }

        private void CreateClueCells()
        {
            AssignEntryValuesForRows();
            AssignEntryValuesForColumns();

            PlaceClueCells();
        }

        private void AssignEntryValuesForRows()
        {
            for (int y = 1; y < sizeY; y++)
            {
                int x = 1;
                while (x < sizeX) 
                {
                    if (grid[x, y] is Entry)
                    {
                        List<int> runXs = new List<int>();
                        while (x < sizeX && grid[x, y] is Entry)
                        {
                            runXs.Add(x);
                            x++;
                        }
                        AssignUniqueDigits(runXs.Select(rx => (rx, y)).ToList());
                    }
                    else
                    {
                        x++;
                    }
                }
            }
        }

        private void AssignEntryValuesForColumns()
        {
            for (int x = 1; x < sizeX; x++)
            {
                int y = 1;
                while (y < sizeY)
                {
                    if (grid[x, y] is Entry)
                    {
                        List<(int cx, int cy)> run = new List<(int, int)>();
                        while (y < sizeY && grid[x, y] is Entry)
                        {
                            run.Add((x, y));
                            y++;
                        }
                        EnsureUniqueInColumn(run);
                    }
                    else
                    {
                        y++;
                    }
                }
            }
        }

        private void AssignUniqueDigits(List<(int x, int y)> run)
        {
            int len = run.Count;
            if (len == 0 || len > 9) return;

            List<int> digits = Enumerable.Range(1, 9).OrderBy(_ => rng.Next()).Take(len).ToList();
            for (int i = 0; i < run.Count; i++) 
            {
                ((Entry)grid[run[i].x, run[i].y]).CorrectValue = digits[i];
            }
        }

        private void EnsureUniqueInColumn(List<(int x, int y)> run)
        {
            int maxTries = 20;
            while (maxTries-- > 0)
            {
                HashSet<int> seen = new HashSet<int>();
                bool ok = true;
                foreach (var (cx, cy) in run)
                {
                    int v = ((Entry)grid[cx, cy]).CorrectValue;
                    if (!seen.Add(v)) { ok = false; break; }
                }
                if (ok) return;

                AssignUniqueDigits(run);
            }
        }

        private void PlaceClueCells()
        {
            for (int y = 1; y < sizeY; y++)
            {
                for (int x = 1; x < sizeX; x++)
                {
                    if (grid[x, y] is Entry && !(grid[x - 1, y] is Entry))
                    {
                        int sum = SumRunRight(x, y);
                        int clueX = x - 1;

                        if (grid[clueX, y] is Clue existingClue)
                            grid[clueX, y] = new Clue(clueX, y, sum, existingClue.VerticalClue);
                        else
                            grid[clueX, y] = new Clue(clueX, y, sum, null);
                    }
                }
            }

            for(int x = 1; x < sizeX; x++)
            {
                for(int y = 1; y < sizeY; y++)
                {
                    if (grid[x,y] is Entry && !(grid[x,y -1] is Entry))
                    {
                        int sum = SumRunDown(x, y);
                        int clueY = y - 1;
                        if (grid[x, clueY] is Clue existingClue)
                            grid[x, clueY] = new Clue(x, clueY, existingClue.HorizontalClue, sum);
                        else
                            grid[x, clueY] = new Clue(x, clueY, null, sum);
                    }
                }
            }
        }

        private int SumRunRight(int startX, int y)
        {
            int sum = 0;
            for (int x = startX; x < sizeX && grid[x, y] is Entry e; x++)
                sum += e.CorrectValue;
            return sum;
        }

        private int SumRunDown(int x, int startY)
        {
            int sum = 0;
            for (int y = startY; y < sizeY && grid[x, y] is Entry e; y++)
                sum += e.CorrectValue;
            return sum;
        }

        private Board addCellsToBoard(int puzzleId, string difficulty)
        {
            return new Board(puzzleId, sizeX, sizeY, difficulty, grid);
        }

    }
}