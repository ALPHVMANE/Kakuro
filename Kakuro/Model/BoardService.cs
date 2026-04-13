using Kakuro.Model;
using System;
using System.Collections.Generic;
using System.Web.SessionState;

public class BoardService
{
    private readonly PuzzleManager _pm;

    public BoardService(PuzzleManager pm)
    {
        _pm = pm;
    }

    public Board GenerateBoard(HttpSessionState session)
    {
        string type = session["BoardType"]?.ToString();

        switch (type)
        {

            case "RNG":
                return GenerateRNGBoard(session);

            case "Custom":
                return GenerateCustomBoard(session);

            default:
                throw new Exception("Invalid board type");
        }
    }

    private Board GenerateLevelBoard(HttpSessionState session)
    {
        return _pm.initBoard(
            (int)session["LvlBoardID"],
            (int)session["MemberID"]
        );
    }

    private Board GenerateRNGBoard(HttpSessionState session)
    {
        string sizeStr = session["RNG_Size"].ToString(); // "6x6"
        string diff = session["RNG_Diff"].ToString();

        int size = int.Parse(sizeStr.Split('x')[0]);

        return _pm.initFromTemplate(size, diff);
    }

    private Board GenerateCustomBoard(HttpSessionState session)
    {
        var grid = session["CustomGrid"] as List<List<string>>;

        if (grid == null)
            throw new Exception("Custom grid missing");

        CustomBoardBuilder builder = new CustomBoardBuilder();
        return builder.Build(grid);
    }
}

public class CustomBoardBuilder
{
    public Board Build(List<List<string>> gridState)
    {
        int sizeY = gridState.Count;
        int sizeX = gridState[0].Count;

        Cell[,] grid = new Cell[sizeX, sizeY];

        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                string type = gridState[y][x];

                switch (type)
                {
                    case "black":
                        grid[x, y] = new Empty(x, y);
                        break;

                    case "white":
                        grid[x, y] = new Entry(x, y, 0);
                        break;

                    case "clue":
                        grid[x, y] = new Clue(x, y, null, null);
                        break;
                }
            }
        }

        return new Board(67, sizeX, sizeY, "Custom", grid); // ID: C
    }
}