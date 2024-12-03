namespace _2024.Days;

public class Day1 : MainDays
{
    public void Part1()
    {
        int total = 0;
        (List<int>, List<int>) splitlines = split();
        var left = splitlines.Item1;
        var right = splitlines.Item2;
        
        for (int i = 0; i < left.Count; i++)
        {
            total += Math.Abs(left[i] - right[i]);
        }
        
        Console.WriteLine(total);
    }

    public void Part2()
    {
        int count = 0;
        int total = 0;
        (List<int>, List<int>) splitlines = split();
        var left = splitlines.Item1;
        var right = splitlines.Item2;

        foreach (var num in left)
        {
            count = right.Where(number => number == num).ToList().Count;
            total += num * count;
        }
        
        Console.WriteLine(total);
    }

    private (List<int>, List<int>) split()
    {
        string[] lines = ReadFile("Inputs/InputDay1.txt");
        List<int> locationsLeft = new List<int>(); 
        List<int> locationsRight = new List<int>();
        foreach (string line in lines)
        {
            var splitline  = line.Split("   ");
            locationsLeft.Add(int.Parse(splitline[0]));
            locationsRight.Add(int.Parse(splitline[1]));
        }
        locationsLeft.Sort();
        locationsRight.Sort();
        return (locationsLeft, locationsRight);
    }
    
}

