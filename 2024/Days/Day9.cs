namespace _2024.Days;

public class Day9
{
    private readonly List<char> input;
    private readonly int length;

    public Day9()
    {
        input = File.ReadAllText("Inputs/InputDay9.txt").ToCharArray().ToList();
        length = input.Count;
    }

    public void Part1()
    {
        var disk = BuildDisk(input, length);
        SwapDisk(disk);
        Console.WriteLine(CalculateChecksum(disk));
    }

    private List<char> BuildDisk(List<char> input, int length)
    {
        var disk = new List<char>();
        char idFile = '0';

        for (int i = 0; i < length; i++)
        {
            int count = int.Parse(input[i].ToString());

            char toAdd = i % 2 == 0 ? idFile : '.';
            disk.AddRange(Enumerable.Repeat(toAdd, count));

            if (i % 2 == 0 && count > 0)
                idFile++;
        }

        return disk;
    }

    private void SwapDisk(List<char> disk)
    {
        for (int i = 0; i < disk.Count; i++)
        {
            if (disk[i] == '.')
            {
                for (int j = disk.Count - 1; j > i; j--)
                {
                    if (disk[j] != '.')
                    {
                        disk[i] = disk[j];
                        disk[j] = '.';
                        break;
                    }
                }
            }
        }
    }

    private long CalculateChecksum(List<char> disk)
    {
        long sum = 0;

        for (int position = 0; position < disk.Count; position++)
        {
            if (disk[position] != '.')
            {
                sum += position * (disk[position] - '0');
            }
        }

        return sum;
    }


    public void Part2()
    {
        var disk = BuildDisk(input, length);
        SwapWholeDisk(disk);
        Console.WriteLine(CalculateChecksum(disk));
    }

    private void SwapWholeDisk(List<char> disk)
    {
        var files = new List<(char id, int start, int length)>();
        int i = 0;

        while (i < disk.Count)
        {
            if (disk[i] != '.')
            {
                char id = disk[i];
                int start = i;
                
                while (i < disk.Count && disk[i] == id)
                    i++;

                files.Add((id, start, i - start)); 
            }
            else
            {
                i++;
            }
        }
        
        files.Sort((a, b) => b.id.CompareTo(a.id)); 

        foreach (var file in files)
        {
            int targetStart = -1;
            int freeLength = 0;

            for (i = 0; i < disk.Count; i++)
            {
                if (disk[i] == '.')
                {
                    if (freeLength == 0) 
                        targetStart = i;

                    freeLength++;

                    if (freeLength >= file.length)
                    {
                        break;
                    }
                }
                else
                {
                    freeLength = 0; 
                }
            }
            
            if (freeLength >= file.length && targetStart != -1 && targetStart < file.start)
            {
                for (i = file.start; i < file.start + file.length; i++)
                    disk[i] = '.';
                
                for (i = targetStart; i < targetStart + file.length; i++)
                    disk[i] = file.id;
            }
        }
    }

}
