namespace _2024;

public class MainDays
{
    public string[] ReadFile(string path)
    {
        var lines = File.ReadAllLines(path);
        return lines;
    }
}