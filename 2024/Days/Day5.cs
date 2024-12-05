using System.Text.RegularExpressions;

namespace _2024.Days;

public class Day5 : MainDays
{
    private string lines;
    private List<(int, int)> rules;
    private List<List<int>> updates;

    public Day5()
    {
        lines = File.ReadAllText("Inputs/InputDay5.txt");
    }
    public void Part1()
    {
        var sections = lines.Split("\r\n\r\n");
        rules = makeRules(sections[0]);
        updates = makeUpdates(sections[1]);

        int count = 0;

        foreach (var update in updates)
        {
            if (isValid(update))
            {
                int middleIndex = update.Count / 2;
                count += update[middleIndex];
            }
        }
        Console.WriteLine(count);
    }

    public void Part2()
    {
        int count = 0;
        var unsortedUpdates = updates.Where(update => !isValid(update)).ToList();
        foreach (var update in unsortedUpdates)
        {
            var sortedUpdate = SortList(update);
            int middleIndex = sortedUpdate.Count / 2;
            count += sortedUpdate[middleIndex];
        }
        
        Console.WriteLine(count);
    }

    private List<(int X,int Y)> makeRules(string rulePart)
    {
        var rules = rulePart.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line =>
            {
                var parts = line.Split('|');
                return (X: int.Parse(parts[0]), Y: int.Parse(parts[1]));
            })
            .ToList();
        
        return rules;
    }
    
    private List<List<int>> makeUpdates(string updatePart)
    {
        var update = updatePart.Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(line => line.Split(',').Select(int.Parse).ToList())
            .ToList();
        return update;
    }

    private bool isValid(List<int> update)
    {
        var positions = update
            .Select((value, index) => (value, index))
            .ToDictionary(x => x.value, x => x.index);

        foreach (var (X, Y) in rules)
        {
            if (positions.ContainsKey(X) && positions.ContainsKey(Y))
            {
                if (positions[X] > positions[Y])
                {
                    return false;
                }
            }
        }
        return true;
    }

    private List<int> SortList(List<int> update)
    {
        var list = update.OrderBy(page => page, Comparer<int>.Create((a, b) =>
            {
                if (rules.Any(rule => rule.Item1 == a && rule.Item2 == b)) return -1;
                if (rules.Any(rule => rule.Item1 == b && rule.Item2 == a)) return 1;
                return 0;
            }))
            .ToList();
        return list;
    }
}