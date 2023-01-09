// See https://aka.ms/new-console-template for more information

class Day1
{
    public static void  Solution()
    {
        Console.WriteLine("Hello, World!");

        var calorieList = new List<int>();

        string[] lines = System.IO.File.ReadAllLines(@"C:\Users\alexa\OneDrive\Dokument\adventOfCode2022\input.txt");

        var sum = 0;
        foreach (var line in lines)
        {
    
            if (line == string.Empty)
            {
                calorieList.Add(sum);
                sum = 0;
            }
            else
            {
                sum += int.Parse(line);
            }
        }

        var byDescending = calorieList.OrderByDescending(c => c).ToArray();

        var maxSum = 0;
        for (int i = 0; i < 3; i++)
        {
            maxSum += byDescending[i];
        }

        Console.WriteLine(maxSum);
    }
}
