using System.Text.RegularExpressions;

namespace _2024.Days;

public class Day5
{
    private readonly string _lines;
    private List<(int X, int Y)> _rules;
    private List<List<int>> _updates;

    public Day5()
    {
        _lines = File.ReadAllText("Inputs/InputDay5.txt");
    }

    public void Part1()
    {
        ParseInput();
        int count = _updates
            .Where(IsValid)
            .Sum(update => update[update.Count / 2]);

        Console.WriteLine(count);
    }

    public void Part2()
    {
        int count = _updates
            .Where(update => !IsValid(update))
            .Select(SortUpdate)
            .Sum(sortedUpdate => sortedUpdate[sortedUpdate.Count / 2]);

        Console.WriteLine(count);
    }

    private void ParseInput()
    {
        var sections = _lines.Split("\r\n\r\n");
        _rules = ParseRules(sections[0]);
        _updates = ParseUpdates(sections[1]);
    }

    private List<(int X, int Y)> ParseRules(string rulePart)
    {
        return rulePart.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                var parts = line.Split('|');
                return (X: int.Parse(parts[0]), Y: int.Parse(parts[1]));
            })
            .ToList();
    }

    private List<List<int>> ParseUpdates(string updatePart)
    {
        return updatePart.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(',').Select(int.Parse).ToList())
            .ToList();
    }

    private bool IsValid(List<int> update)
    {
        var positions = update
            .Select((value, index) => (value, index))
            .ToDictionary(x => x.value, x => x.index);

        return _rules.All(rule =>
            !positions.ContainsKey(rule.X) || 
            !positions.ContainsKey(rule.Y) || 
            positions[rule.X] <= positions[rule.Y]);
    }

    private List<int> SortUpdate(List<int> update)
    {
        return update.OrderBy(page => page, Comparer<int>.Create((a, b) =>
        {
            if (_rules.Any(rule => rule.X == a && rule.Y == b)) return -1;
            if (_rules.Any(rule => rule.X == b && rule.Y == a)) return 1;
            return 0;
        })).ToList();
    }
}