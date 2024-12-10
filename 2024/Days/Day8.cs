namespace _2024.Days;

public class Day8
{
    private readonly Dictionary<char, List<(int x, int y)>> _map = new();
    private readonly int _rows, _cols;

    public Day8()
    {
        string[] input = File.ReadAllLines("Inputs/InputDay8.txt");
        _rows = input.Length;
        _cols = input[0].Length;
        
        for (int y = 0; y < _rows; y++)
        {
            for (int x = 0; x < _cols; x++)
            {
                char cell = input[y][x];
                if (cell == '.') continue;
                
                if (!_map.ContainsKey(cell))
                {
                    _map[cell] = new List<(int x, int y)>();
                }
                _map[cell].Add((x, y));
            }
        }
    }

    public void Part1()
    {
        Console.WriteLine(CountAntinodes());
    }

    public void Part2()
    {
        Console.WriteLine(CountAntinodes2());
    }

    private int CountAntinodes()
    {
        var result = new HashSet<(int x, int y)>();

        foreach (var chr in _map.Keys)
        {
            var antennas = _map[chr];
            foreach (var antenna in antennas)
            {
                foreach (var otherAntenna in antennas)
                {
                    if (antenna == otherAntenna) continue;
                    var (dx, dy) = (antenna.x - otherAntenna.x, antenna.y - otherAntenna.y);
                    var (newX, newY) = (antenna.x + dx, antenna.y + dy);

                    if (IsValidPosition(newX, newY)) 
                        result.Add((newX, newY));
                }
            }
        }

        return result.Count;
    }

    private int CountAntinodes2()
    {
        var result = new HashSet<(int x, int y)>();

        foreach (var chr in _map.Keys)
        {
            var antennas = _map[chr];
            foreach (var antenna in antennas)
            {
                result.Add(antenna);
                foreach (var otherAntenna in antennas)
                {
                    if (antenna == otherAntenna) continue;
                    
                    var (dx, dy) = (antenna.x - otherAntenna.x, antenna.y - otherAntenna.y);
                    int multiplier = 1;
                    var (X, Y) = (antenna.x, antenna.y);
                    
                    while (IsValidPosition(X + dx * multiplier, Y + dy * multiplier))
                    {
                        result.Add((X + dx * multiplier, Y + dy * multiplier));
                        multiplier++;
                    }
                }
            }
        }

        return result.Count;
    }
    
    private bool IsValidPosition(int x, int y)
    {
        return x >= 0 && y >= 0 && x < _cols && y < _rows;
    }
}
