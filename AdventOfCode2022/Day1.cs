class Day1
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

        var maxCalories = calorieList.Max();

        Console.WriteLine(maxCalories);
    }
}
