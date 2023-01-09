class Day3
{
    public static void Solution()
    {
        var lines = System.IO.File.ReadAllLines(@"C:\Users\alexa\OneDrive\Dokument\adventOfCode2022\input3.txt");

        var prioritiesDict = new Dictionary<char, int>();

        for (char letter = 'a'; letter <= 'z'; letter++)
        {
            var index = (int) letter % 32;
            prioritiesDict.Add(letter, index);
            Console.WriteLine($"letterToNumber: {letter}{index}");
        }

        for (char letter = 'A'; letter <= 'Z'; letter++)
        {
            var index = (int) letter % 32 + 26;
            prioritiesDict.Add(letter, index);
            Console.WriteLine($"letterToNumber: {letter}{index}");
        }

        var commonItems = new List<char>();
        foreach (var line in lines)
        {
            var lineLength = line.Length;
            var halfLine = lineLength / 2;
            var firstCompartment = line.Substring(halfLine);
            var secondCompartment = line.Remove(halfLine);
            var commonItem = firstCompartment.First(c => secondCompartment.Contains(c));
            commonItems.Add(commonItem);
        }

        var prioritySum = 0;
        foreach (var item in commonItems)
        {
            Console.WriteLine($"item: {item}");
            prioritiesDict.TryGetValue(item, out var priority);
            prioritySum += priority;
        }

        Console.WriteLine(prioritySum);
    }
}