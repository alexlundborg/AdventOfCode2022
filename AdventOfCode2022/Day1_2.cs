class Day1_2
{
    public static void Solution(string path)
    {
        var calorieList = new List<int>();

        var lines = File.ReadAllLines(path);

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
