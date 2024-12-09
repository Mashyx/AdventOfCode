using System.Reflection;

namespace _2024.Days;

public class Day6 : MainDays
{
    private char[,] map;
    int guardRow, guardCol;
    Direction direction;
    public Day6()
    {
        string[] input = File.ReadAllLines("Inputs/InputDay6.txt");
        int rows = input.Length;
        int cols = input[0].Length;
        map = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                map[i, j] = input[i][j];
            }
        }
        (guardRow, guardCol, direction) = FindGuard(map);
    }
    
    public void Part1()
    {
        HashSet<(int, int)> visited = new();
        int vistedLocations = WalkPath(map, guardRow, guardCol, direction, false, visited);
        Console.WriteLine(vistedLocations);
    }

    public void Part2()
    {
        int loops = FindLoops(map, guardRow, guardCol, direction);
        Console.WriteLine(loops);
    }

    private (int, int, Direction) FindGuard(char[,] map)
    {
        Dictionary<char, Direction> directions = new()
        {
            { '^', Direction.Up },
            { '>', Direction.Right },
            { 'v', Direction.Down },
            { '<', Direction.Left }
        };

        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                char cell = map[i, j];

                if (directions.ContainsKey(cell))
                {
                    Direction direction = directions[cell];
                    return (i, j, direction);
                }
            }
        }
        throw new Exception("No guard found");
    }

    private int WalkPath(char[,] map, int guardRow, int guardCol, Direction guardDirection, bool looping, HashSet<(int, int)> visited = null)
    {
        Dictionary<Direction, (int dx, int dy)> directionWalk = new()
        {
            { Direction.Up, (-1, 0) },
            { Direction.Right, (0, 1) },
            { Direction.Down, (1, 0) },
            { Direction.Left, (0, -1)}
        };
        
        HashSet<(int, int, Direction)>? visitedLoops = looping ? new HashSet<(int, int, Direction)>() : null;

        if (!looping)
        {
            visited.Add((guardRow, guardCol));
        }
        else
        {
            visitedLoops.Add((guardRow, guardCol, guardDirection));
        }
        

        while (true)
        {
            (int dx, int dy) = directionWalk[guardDirection];
            int nextRow = guardRow + dx;
            int nextCol = guardCol + dy;

            if (nextRow < 0 || nextRow >= map.GetLength(0) || nextCol < 0 || nextCol >= map.GetLength(1))
            {
                return looping ? 0 : visited.Count;
            }

            if (map[nextRow, nextCol] == '#')
            {
                guardDirection = Turn(guardDirection);
            }
            else
            {
                guardRow = nextRow;
                guardCol = nextCol;

                if (!looping)
                {
                    visited.Add((guardRow, guardCol));
                }
                else
                {
                    if (visitedLoops.Contains((guardRow, guardCol, guardDirection)))
                    {
                        return 1;
                    }
                    
                    visitedLoops.Add((guardRow, guardCol, guardDirection));
                }
            }
        }
    }

    private Direction Turn(Direction direction)
    {
        Direction[] directions =
        {
            Direction.Up, 
            Direction.Right, 
            Direction.Down, 
            Direction.Left
        };
        
        int currentIndex = Array.IndexOf(directions, direction);
        return directions[(currentIndex + 1) % directions.Length];
    }

    enum Direction
    {
        Up, 
        Right, 
        Down, 
        Left
    }

    private int FindLoops(char[,] map, int guardRow, int guardCol, Direction guardDirection)
    {
        List<(int, int)> possibleObstacles = new();
        int countLoops = 0;
        
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == '.' && !(i == guardRow && j == guardCol))
                {
                    possibleObstacles.Add((i, j));
                }
            }
        }

        foreach ((int row, int col) in possibleObstacles)
        {
            map[row, col] = '#';

            if (WalkPath(map, guardRow, guardCol, guardDirection, true) == 1)
            {
                countLoops++;
            }
            
            map[row, col] = '.';
        }
        
        return countLoops;
    }
}