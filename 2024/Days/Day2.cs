namespace _2024.Days;

public class Day2
{
    private readonly string[] _lines;

    public Day2()
    {
        _lines = File.ReadAllLines("Inputs/InputDay2.txt");
    }

    public void Part1()
    {
        int safeCount = _lines.Count(line => IsSafe(ParseNumbers(line)));
        Console.WriteLine(safeCount);
    }

    public void Part2()
    {
        int safeCount = 0;

        foreach (var line in _lines)
        {
            var numbers = ParseNumbers(line);

            if (IsSafe(numbers))
            {
                safeCount++;
            }
            else
            {
                for (int index = 0; index < numbers.Count; index++)
                {
                    if (IsSafe(RemoveAt(numbers, index)))
                    {
                        safeCount++;
                        break;
                    }
                }
            }

        }

        Console.WriteLine(safeCount);
    }

    private static List<int> ParseNumbers(string line)
    {
        return line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
    }

    private static List<int> RemoveAt(List<int> numbers, int index)
    {
        var copy = new List<int>(numbers);
        copy.RemoveAt(index);
        return copy;
    }

    private static bool IsSafe(List<int> numbers)
    {
        if (numbers.Count < 2)
            return false;

        bool isIncreasing = numbers[0] < numbers[1];

        for (int i = 0; i < numbers.Count - 1; i++)
        {
            int difference = Math.Abs(numbers[i] - numbers[i + 1]);

            if (difference > 3 || difference == 0 || (numbers[i] < numbers[i + 1]) != isIncreasing)
            {
                return false;
            }
        }

        return true;
    }
}