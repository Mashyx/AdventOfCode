using System.Diagnostics;

namespace _2024.Days;

public class Day7
{
    private List<string> lines;
    public Day7()
    {
        lines = new List<string>(File.ReadAllLines("Inputs/InputDay7.txt"));
    }
    public void Part1()
    {
        (List<long> results,  List<string> equations) = ParseInput(lines);
        long sum = 0;
        for (int i = 0; i < equations.Count; i++)
        {
            List<long> split = equations[i].Trim().Split(" ").Select(Int64.Parse).ToList();
            bool isMatch = tryCalc(split, results[i], 1, split[0]);

            if (isMatch)
            {
                sum += results[i];
            }
        }
        
        Console.WriteLine(sum);
    }

    public void Part2()
    {
        (List<long> results,  List<string> equations) = ParseInput(lines);
        long sum = 0;
        for (int i = 0; i < equations.Count; i++)
        {
            List<long> split = equations[i].Trim().Split(" ").Select(Int64.Parse).ToList();
            bool isMatch = tryCalcWithThree(split, results[i], 1, split[0]);

            if (isMatch)
            {
                sum += results[i];
            }
        }
        
        Console.WriteLine(sum);
    }

    private (List<long>, List<string>) ParseInput(List<string> input)
    {
        List<long> result = new(); 
        List<string> equations = new();
        foreach (var line in input)
        {
            var split = line.Split(":").ToList();
            result.Add(long.Parse(split[0]));
            equations.Add(split[1]);
        }
        
        return (result, equations);
    }

    private bool tryCalc(List<long> numbers, long target, int index, long currentValue)
    {
        
        if (index == numbers.Count)
        {
            return currentValue == target;
        }

        if (tryCalc(numbers, target, index + 1, currentValue + numbers[index]))
        {
            return true;
        }

        if (tryCalc(numbers, target, index + 1, currentValue * numbers[index]))
        {
            return true;
        }
        
        return false;
    }

    private bool tryCalcWithThree(List<long> numbers, long target, int index, long currentValue)
    {
        if (index == numbers.Count)
        {
            return currentValue == target;
        }

        if (tryCalcWithThree(numbers, target, index + 1, currentValue + numbers[index]))
        {
            return true;
        }

        if (tryCalcWithThree(numbers, target, index + 1, currentValue * numbers[index]))
        {
            return true;
        }
        
        if (tryCalcWithThree(numbers, target, index + 1, Int64.Parse($"{currentValue}{numbers[index]}")))
        {
            return true;
        }
        
        return false;
    }
}