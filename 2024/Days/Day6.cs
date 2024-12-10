using System.Reflection;

namespace _2024.Days;

public class Day6
{
    private readonly char[,] _map;
    private readonly int _guardRow, _guardCol;
    private readonly Direction _direction;

    public Day6()
    {
        var input = File.ReadAllLines("Inputs/InputDay6.txt");
        _map = ParseMap(input);
        (_guardRow, _guardCol, _direction) = FindGuard(_map);
    }

    public void Part1()
    {
        var visitedLocations = WalkPath(_map, _guardRow, _guardCol, _direction, false);
        Console.WriteLine(visitedLocations);
    }

    public void Part2()
    {
        var loopCount = FindLoops(_map, _guardRow, _guardCol, _direction);
        Console.WriteLine(loopCount);
    }

    private static char[,] ParseMap(string[] input)
    {
        int rows = input.Length;
        int cols = input[0].Length;
        var map = new char[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                map[i, j] = input[i][j];
            }
        }

        return map;
    }

    private static (int, int, Direction) FindGuard(char[,] map)
    {
        var directions = new Dictionary<char, Direction>
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
                if (directions.TryGetValue(map[i, j], out var direction))
                {
                    return (i, j, direction);
                }
            }
        }

        throw new Exception("No guard found");
    }

    private static int WalkPath(char[,] map, int row, int col, Direction direction, bool findLoop)
    {
        var directionOffsets = new Dictionary<Direction, (int dx, int dy)>
        {
            { Direction.Up, (-1, 0) },
            { Direction.Right, (0, 1) },
            { Direction.Down, (1, 0) },
            { Direction.Left, (0, -1) }
        };

        if (findLoop)
        {
            var visitedLoops = new HashSet<(int, int, Direction)>();
            while (true)
            {
                var (dx, dy) = directionOffsets[direction];
                int nextRow = row + dx;
                int nextCol = col + dy;

                if (!IsValidPosition(map, nextRow, nextCol)) return 0;

                if (map[nextRow, nextCol] == '#')
                {
                    direction = TurnRight(direction);
                }
                else
                {
                    row = nextRow;
                    col = nextCol;

                    if (!visitedLoops.Add((row, col, direction)))
                    {
                        return 1; // Loop detected
                    }
                }
            }
        }
        else
        {
            var visited = new HashSet<(int, int)>();
            visited.Add((row, col));

            while (true)
            {
                var (dx, dy) = directionOffsets[direction];
                int nextRow = row + dx;
                int nextCol = col + dy;

                if (!IsValidPosition(map, nextRow, nextCol)) return visited.Count;

                if (map[nextRow, nextCol] == '#')
                {
                    direction = TurnRight(direction);
                }
                else
                {
                    row = nextRow;
                    col = nextCol;
                    visited.Add((row, col));
                }
            }
        }
    }


    private static int FindLoops(char[,] map, int row, int col, Direction direction)
    {
        var possibleObstacles = Enumerable.Range(0, map.GetLength(0))
            .SelectMany(i => Enumerable.Range(0, map.GetLength(1)).Select(j => (i, j)))
            .Where(pos => map[pos.i, pos.j] == '.' && !(pos.i == row && pos.j == col))
            .ToList();

        int loopCount = 0;

        foreach (var (obstacleRow, obstacleCol) in possibleObstacles)
        {
            map[obstacleRow, obstacleCol] = '#';

            if (WalkPath(map, row, col, direction, true) == 1)
            {
                loopCount++;
            }

            map[obstacleRow, obstacleCol] = '.';
        }

        return loopCount;
    }

    private static bool IsValidPosition(char[,] map, int row, int col)
    {
        return row >= 0 && row < map.GetLength(0) && col >= 0 && col < map.GetLength(1);
    }

    private static Direction TurnRight(Direction direction)
    {
        return direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    private enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
}