namespace _2024.Days;

public class Day11
{
    public void Part1()
    {
        string input = File.ReadAllText("Inputs/InputDay11.txt");
        Dictionary<(string, int), long> dict = new();
        string[] spl = input.Split(" ");
        long count = 0;
        foreach (string s in spl)
        {
            count += AmountStones(s, 25, dict);
        }
        Console.WriteLine(count);
    }

    public void Part2()
    {
        string input = File.ReadAllText("Inputs/InputDay11.txt");
        Dictionary<(string, int), long> dict = new();
        string[] spl = input.Split(" ");
        long count = 0;
        foreach (string s in spl)
        {
            count += AmountStones(s, 75, dict);
        }
        Console.WriteLine(count);
    }
    
    private long AmountStones(string stone, int blinks, Dictionary<(string, int), long> dict)
    {
        if (blinks == 0)
        {
            return 1;
        }

        var key = (stone, blinks--);
        if (dict.ContainsKey(key))
        {
            return dict[key];
        }
        
        if (stone == "0")
        {
            string newStone = "1";
            dict.Add(key, AmountStones(newStone, blinks, dict));
            return dict[key];
        }
        if (stone.Length % 2 == 0)
        {
            int length = stone.Length / 2;
            string leftStone = stone[..length];
            string rightStone = stone[length..].TrimStart('0');
            if (rightStone.Length == 0)
            {
                rightStone = "0";
            }
            dict.Add(key, AmountStones(leftStone, blinks, dict) + AmountStones(rightStone, blinks, dict));
            return dict[key];
        }
      
        string multiplyStone = $"{long.Parse(stone) * 2024L}";
        dict.Add(key, AmountStones(multiplyStone, blinks, dict));
        return dict[key];
        
    }
}