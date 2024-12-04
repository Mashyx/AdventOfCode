namespace _2024.Days;

public class Day4 : MainDays
{
    private string[] _lines;
    private int _rows, _columns;
    private char[,] _grid;

    public Day4()
    {
        _lines = ReadFile("Inputs/InputDay4.txt");
        SetGrid();
    }

    public void Part1()
    {
        string word = "XMAS";
        Console.WriteLine(FindOccurrences(word));
    }

    public void Part2()
    {
        string[] words = { "MAS", "SAM" };
        Console.WriteLine(FindXmasPatterns(words));
    }

    private void SetGrid()
    {
        _rows = _lines.Length;
        _columns = _lines[0].Length;
        _grid = new char[_rows, _columns];

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                _grid[i, j] = _lines[i][j];
            }
        }
    }

    private int FindOccurrences(string word)
    {
        int count = 0;
        (int dx, int dy)[] directions = new (int, int)[]
        {
            (0, 1), // Right
            (0, -1), // Left
            (1, 0), // Down
            (-1, 0), // Up
            (1, 1), // D-R
            (1, -1), // D-L
            (-1, 1), // U-R
            (-1, -1) // U-L
        };

        for (int i = 0; i < _grid.GetLength(0); i++)
        {
            for (int j = 0; j < _grid.GetLength(1); j++)
            {
                if (_grid[i, j] == word[0])
                {
                    foreach (var (dx, dy) in directions)
                    {
                        bool match = true;
                        for (int k = 0; k < word.Length; k++)
                        {
                            int newX = i + k * dx;
                            int newY = j + k * dy;

                            if (newX < 0 || newX >= _grid.GetLength(0) || newY < 0 || newY >= _grid.GetLength(1))
                            {
                                match = false;
                                break;
                            }

                            if (_grid[newX, newY] != word[k])
                            {
                                match = false;
                                break;
                            }
                        }

                        if (match)
                        {
                            count++;
                        }
                    }
                }
            }
        }

        return count;
    }

    private int FindXmasPatterns(string[] words)
    {
        int count = 0;
        int rows = _grid.GetLength(0);
        int cols = _grid.GetLength(1);
        
        for (int i = 1; i < rows - 1; i++)
        {
            for (int j = 1; j < cols - 1; j++)
            {
                var topLeft = _grid[i - 1, j - 1];
                var topRight = _grid[i - 1, j + 1];
                var center = _grid[i, j];
                var bottomLeft = _grid[i + 1, j - 1];
                var bottomRight = _grid[i + 1, j + 1];
                
                if (center == 'A')
                {
                    if (
                        (topLeft == 'M' && topRight == 'S' && center == 'A' && bottomLeft == 'M' && bottomRight == 'S') ||
                        (topLeft == 'S' && topRight == 'M' && center == 'A' && bottomLeft == 'S' && bottomRight == 'M') ||
                        (topLeft == 'S' && topRight == 'S' && center == 'A' && bottomLeft == 'M' && bottomRight == 'M') ||
                        (topLeft == 'M' && topRight == 'M' && center == 'A' && bottomLeft == 'S' && bottomRight == 'S')
                    ) {
                        count++;
                    }
                } 
            }
        }
        return count;
    }
}
