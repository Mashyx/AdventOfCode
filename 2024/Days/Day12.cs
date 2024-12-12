namespace _2024.Days;

public class Day12
{
    public void Part1()
    {
        string[] input = File.ReadAllLines("Inputs/InputDay12.txt");
        char[,] garden = ParseInput(input);
        Console.WriteLine(CalcPrices(garden));
    }

    public void Part2()
    {
        string[] input = File.ReadAllLines("Inputs/InputDay12.txt");
        char[,] garden = ParseInput(input);
       
    }

    private static char[,] ParseInput(string[] input)
    {
        int rows = input.Length;
        int cols = input[0].Length;
        char[,] garden = new char[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                garden[row, col] = input[row][col];
            }
        }
        return garden;
    }

    private static int CalcPrices(char[,] garden)
    {
        int rows = garden.GetLength(0);
        int cols = garden.GetLength(1);
        bool[,] visited = new bool[rows, cols];
        int totalPrice = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (!visited[row, col])
                {
                    var region = GetRegion(garden, visited, row, col);
                    int price = region.area * region.perimeter;
                    totalPrice += price;
                }
            }
        }
        
        return totalPrice;
    }

    private static (int area, int perimeter) GetRegion(char[,] garden, bool[,] visited, int startX, int startY)
    {
        int rows = garden.GetLength(0);
        int cols = garden.GetLength(1);
        char plantType = garden[startX, startY];
        int area = 0;
        int perimeter = 0;
        
        int[] dx = { 0, 1, 0, -1 };
        int[] dy = { 1, 0, -1, 0 };

        Stack<(int x, int y)> stack = new();
        stack.Push((startX, startY));
        visited[startX, startY] = true;

        while (stack.Count > 0)
        {
            (int x, int y) = stack.Pop();
            area++;

            for (int i = 0; i < 4; i++)
            {
                int nx = x + dx[i];
                int ny = y + dy[i];

                if (nx >= 0 && ny >= 0 && nx < rows && ny < cols)
                {
                    if (!visited[nx, ny] && garden[nx, ny] == plantType)
                    {
                        stack.Push((nx, ny));
                        visited[nx, ny] = true;
                    }
                    else if (garden[nx, ny] != plantType)
                    {
                        perimeter++;
                    }
                }
                else
                {
                    perimeter++;
                }
            }
        }
        return (area, perimeter);
    }

    

}