namespace _2024.Days;

public class Day8
{
    private Dictionary<char, List<(int x, int y)>> map = new();
    private int rows, cols;
    public Day8()
    {
        string[] input = File.ReadAllLines("Inputs/InputDay8.txt");
        rows = input.Length;
        cols = input[0].Length;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if (input[y][x] == '.') continue;
                if (!map.ContainsKey(input[y][x]))
                {
                    map[input[y][x]] = new List<(int x, int y)>();
                }
                map[input[y][x]].Add((x, y));
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
        foreach (var chr in map.Keys)
        {
            foreach (var antenna in map[chr])
            {
                foreach (var otherAntenna in map[chr])
                {
                    if (antenna == otherAntenna) continue;
                    var dx = antenna.x - otherAntenna.x;
                    var dy = antenna.y - otherAntenna.y;
                    
                    var newX = antenna.x + dx;
                    var newY = antenna.y + dy;
                    
                    if (newX < 0 || newY < 0 || newX >= cols || newY >= rows) continue;
                    result.Add((newX, newY));
                }
            }
        }
        return result.Count;
    }
    
    private int CountAntinodes2()
    {
        var result = new HashSet<(int x, int y)>();
        foreach (var chr in map.Keys)
        {
            foreach (var antenna in map[chr])
            {
                result.Add((antenna.x, antenna.y));
                foreach (var otherAntenna in map[chr])
                {
                    if (antenna == otherAntenna) continue;
                    var dx = antenna.x - otherAntenna.x;
                    var dy = antenna.y - otherAntenna.y;
                    
                    var X = antenna.x;
                    var Y = antenna.y;
                    int multiplier = 1;

                    while (!(X + dx*multiplier < 0 || Y + dy*multiplier < 0 ||X + dx*multiplier >= cols || Y + dy*multiplier >= rows))
                    {
                        result.Add((X + dx*multiplier,Y + dy*multiplier));
                        multiplier++;
                    }
                }
            }
        }
        return result.Count;
    }
    
}