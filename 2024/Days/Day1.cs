namespace _2024.Days;

public class Day1
{
    public void Part1()
    {
        var (left, right) = ReadAndSplitInput("Inputs/InputDay1.txt");
        int total = left.Zip(right, (l, r) => Math.Abs(l - r)).Sum();

        Console.WriteLine(total);
    }

    public void Part2()
    {
        var (left, right) = ReadAndSplitInput("Inputs/InputDay1.txt");
        int total = left.Sum(num => num * right.Count(r => r == num));

        Console.WriteLine(total);
    }

    private static (List<int> Left, List<int> Right) ReadAndSplitInput(string filePath)
    {
        var left = new List<int>();
        var right = new List<int>();

        foreach (var line in File.ReadLines(filePath))
        {
            var parts = line.Split("   ", StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2 && int.TryParse(parts[0], out var leftValue) && int.TryParse(parts[1], out var rightValue))
            {
                left.Add(leftValue);
                right.Add(rightValue);
            }
        }

        left.Sort();
        right.Sort();
        return (left, right);
    }
}

