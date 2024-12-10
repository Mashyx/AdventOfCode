using System.Text.RegularExpressions;

namespace _2024.Days;

public class Day3
{
    private readonly string _input;

    public Day3()
    {
        _input = File.ReadAllText("Inputs/InputDay3.txt");
    }

    public void Part1()
    {
        int result = Regex.Matches(_input, @"mul\(\d+,\d+\)")
            .Select(match => Multiply(match.Value))
            .Sum();

        Console.WriteLine(result);
    }

    public void Part2()
    {
        int result = 0;
        bool skip = false;

        var patterns = Regex.Matches(_input, @"mul\(\d+,\d+\)|do\(\)|don't\(\)")
            .Select(match => match.Value);

        foreach (var pattern in patterns)
        {
            switch (pattern)
            {
                case "don't()":
                    skip = true;
                    break;

                case "do()":
                    skip = false;
                    break;

                default:
                    if (!skip)
                    {
                        result += Multiply(pattern);
                    }
                    break;
            }
        }

        Console.WriteLine(result);
    }

    private static int Multiply(string expression)
    {
        var numbers = expression.Replace("mul(", "").Replace(")", "")
            .Split(',')
            .Select(int.Parse)
            .ToArray();
        return numbers[0] * numbers[1];
    }
}