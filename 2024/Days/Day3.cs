using System.Text.RegularExpressions;

namespace _2024.Days;

public class Day3 : MainDays
{
    private string lines;
    public Day3()
    {
        lines = File.ReadAllText("Inputs/InputDay3.txt");
    }
    public void Part1()
    {
        int result = 0;
        
        List<string> pattern = Regex.Matches(lines, @"mul\(\d+,\d+\)").Select(match => match.Value).ToList();

        foreach (string p in pattern)
        {
            result += Mulitplying(p);
        }
        
        Console.WriteLine(result);
    }

    public void Part2()
    {
        int result = 0;
        List<string> pattern = Regex.Matches(lines, @"mul\(\d+,\d+\)|do\(\)|don't\(\)").Select(match => match.Value).ToList();
        bool dont = false;

        foreach (string p in pattern)
        {
                if (p == "don't()")
                {
                    dont = true;
                    continue;
                }

                if (p == "do()")
                {
                    dont = false;
                    continue;
                }
                
                if (!dont)
                {
                    result += Mulitplying(p);
                }
            
        }
        Console.WriteLine(result);
    }

    private int Mulitplying(string p)
    {
        var replace = p.Replace("mul(", "").Replace(")", "");
        var split = replace.Split(",").Select(int.Parse).ToArray();
        int num1 = split[0];
        int num2 = split[1];
        return num1 * num2;
    }
}