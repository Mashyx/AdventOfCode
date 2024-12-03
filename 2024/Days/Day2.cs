namespace _2024.Days;

public class Day2 : MainDays
{
    private List<int> numbers;
    private string[] lines;

    public Day2()
    {
       lines = ReadFile("Inputs/InputDay2.txt");
    }
    
    public void Part1()
    {
        int counter = 0;
        
        for (int i = 0; i < lines.Length; i++)
        {
            numbers = lines[i].Split(' ').Select(int.Parse).ToList();

            if (isSafe(numbers))
            {
                counter++;
            }
        }
        Console.WriteLine(counter);
    }

    public void Part2()
    {
        int counter = 0;
        
        for (int i = 0; i < lines.Length; i++)
        {
            numbers = lines[i].Split(' ').Select(int.Parse).ToList();
            

            if (isSafe(numbers))
            {
                counter++;
            }
            else
            {
                for (int j = 0; j < numbers.Count; j++)
                {
                    List<int> numbersCopy = new List<int>(numbers);
                    numbersCopy.RemoveAt(j);

                    if (isSafe(numbersCopy))
                    {
                        counter++;
                        break;
                    }
                }
            }
        }
        Console.WriteLine(counter);
    }

    /*private void ReadFile()
    {
        lines = File.ReadAllLines("Inputs/InputDay2.txt");
    }*/

    private bool isSafe(List<int> numbs)
    {
        bool increasing = numbs[0] < numbs[1];
        for (int i = 0; i < numbs.Count - 1; i++)
        {
            int result = Math.Abs(numbs[i] - numbs[i + 1]);
            if (result > 3 || result <= 0 || numbs[i] < numbs[i + 1] != increasing)
            {
                return false;
            }
        }
        return true;
    }
}