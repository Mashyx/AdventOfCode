namespace _2024.Days;

public class Day10
{
    private readonly string[] _map;
    public Day10()
    {
        _map = File.ReadAllLines("Inputs/InputDay10.txt");
    }
    
    public void Part1()
    {
        List<(int, int)> trailheads = FindTrailheads(_map);
        int walkablePaths = CalculateWalkablePath(_map, trailheads);
        
        Console.WriteLine(walkablePaths);
    }

    public void Part2()
    {
        Console.WriteLine(CalculateTrailheadRatings(_map));
    }

    static List<(int, int)> FindTrailheads(string[] map)
    {
        List<(int, int)> trailheads = new List<(int, int)>();
        
        for (int i = 0; i < map.Length; i++)
        {
            for (int j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == '0') 
                {
                    trailheads.Add((i, j));
                }
            }
        }
        return trailheads;
    }

    static int CalculateWalkablePath(string[] map, List<(int, int)> trailheads)
    {
        int totalWalkablePaths = 0;

        foreach (var trailhead in trailheads)
        {
            totalWalkablePaths += CalculatePaths(map, trailhead.Item1, trailhead.Item2);
        }
        return totalWalkablePaths;
    }

    static int CalculatePaths(string[] map, int startRow, int startCol)
    {
        int reachable9s = 0;
        int rows = map.Length;
        int cols = map[0].Length;

        int[] dx = { -1, 1, 0, 0 };
        int[] dy = { 0, 0, -1, 1 };
        
        Queue<(int, int)> queue = new();
        queue.Enqueue((startRow, startCol));
        
        bool[,] visited = new bool[rows, cols];
        visited[startRow, startCol] = true;
        
        
        while (queue.Count > 0)
        {
            var (row, col) = queue.Dequeue();
            
            if (map[row][col] == '9')
            {
                reachable9s++;
            }
            
            for (int i = 0; i < 4; i++)
            {
                int newRow = row + dx[i];
                int newCol = col + dy[i];
                
                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols && !visited[newRow, newCol])
                {
                    if (map[newRow][newCol] == map[row][col] + 1)
                    {
                        queue.Enqueue((newRow, newCol));
                        visited[newRow, newCol] = true;
                    }
                }
            }
        }
        return reachable9s;
    }
    
    static int CalculateDistinctPaths(string[] map, int startRow, int startCol) 
    {
        int distinctPaths = 0;
        int rows = map.Length;
        int cols = map[0].Length;

        int[] dx = { -1, 1, 0, 0 }; 
        int[] dy = { 0, 0, -1, 1 };

        Queue<(int, int, List<(int, int)>)> queue = new();
        queue.Enqueue((startRow, startCol, new List<(int, int)> { (startRow, startCol) }));
        
        
        while (queue.Count > 0)
        {
            var (row, col, path) = queue.Dequeue();
            
            if (map[row][col] == '9')
            {
                distinctPaths++;
                continue;  
            }
            
            for (int i = 0; i < 4; i++)
            {
                int newRow = row + dx[i];
                int newCol = col + dy[i];
                
                if (newRow >= 0 && newRow < rows && newCol >= 0 && newCol < cols)
                {
                    if (map[newRow][newCol] == map[row][col] + 1)
                    {
                        var newPath = new List<(int, int)>(path) { (newRow, newCol) };
                        queue.Enqueue((newRow, newCol, newPath));
                        
                    }
                }
            }
        }
        return distinctPaths;
    }

    static int CalculateTrailheadRatings(string[] map)
    {
        int totalRatings = 0;

        int rows = map.Length;
        int cols = map[0].Length;
        
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (map[i][j] == '0')
                {
                    totalRatings += CalculateDistinctPaths(map, i, j);
                }
            }
        }
        return totalRatings;
    }
}