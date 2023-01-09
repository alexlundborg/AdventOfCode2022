public class Day4_2
{
    public static void Solution(string path)
    {
        var lines = File.ReadAllLines(path);

        var counter = 0;
        foreach (var line in lines)
        {
            var firstRange = new Assignment();
            var secondRange = new Assignment();
            
            var assignment = line.Split(",");
            var firstSections = assignment[0].Split("-");
            var secondSection = assignment[1].Split("-");

            firstRange.Start = int.Parse(firstSections[0]);
            firstRange.End = int.Parse(firstSections[1]);
            
            secondRange.Start = int.Parse(secondSection[0]);
            secondRange.End = int.Parse(secondSection[1]);

            if (Overlap(firstRange, secondRange))
            {
                counter++;
            }
        }
        Console.WriteLine(counter);
    }

    private static bool Overlap(Assignment firstRange, Assignment secondRange)
    {
        var overlap = firstRange.Start <= secondRange.Start && secondRange.Start <= firstRange.End;
        var overlap2 = secondRange.Start <= firstRange.Start && firstRange.Start <= secondRange.End;
        return overlap || overlap2;
    }
}