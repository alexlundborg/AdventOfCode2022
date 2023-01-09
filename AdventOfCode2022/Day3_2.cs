class Day3_2
{
    public static void Solution()
    {
        var lines = File.ReadAllLines(@"C:\Users\alexa\OneDrive\Dokument\adventOfCode2022\input3.txt");

        var prioritiesDict = new Dictionary<char, int>();

        for (char letter = 'a'; letter <= 'z'; letter++)
        {
            var index = (int) letter % 32;
            prioritiesDict.Add(letter, index);
        }

        for (char letter = 'A'; letter <= 'Z'; letter++)
        {
            var index = (int) letter % 32 + 26;
            prioritiesDict.Add(letter, index);
        }

        var elfGroups = new List<List<string>>();
        var groupOfRuckSacks = new List<string>();

        for (int i = 0; i < lines.Length; i++)
        {
            if ((i + 1) % 3 != 0 )
            {
                groupOfRuckSacks.Add(lines[i]);
            }
            else
            {
                groupOfRuckSacks.Add(lines[i]);
                elfGroups.Add(groupOfRuckSacks);
                groupOfRuckSacks = new List<string>();
            }
        }

        var prioritySum = 0;

        foreach (var group in elfGroups)
        {
            var firstRucksack = group[0];
            var secondRucksack = group[1];
            var thirdRuckSack = group[2];

            var commonItemsBetweenFirstTwoRucksacks = firstRucksack
                .Intersect(secondRucksack);
            var commonItemsBetweenAllRucksacks = commonItemsBetweenFirstTwoRucksacks
                .Intersect(thirdRuckSack)
                .First();
            
            prioritiesDict.TryGetValue(commonItemsBetweenAllRucksacks, out var priority);
            prioritySum += priority;
        }
        
        Console.WriteLine(prioritySum);
    }
}