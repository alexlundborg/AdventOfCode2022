public class Day4
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

            if (Contains(firstRange, secondRange))
            {
                counter++;
            }
        }
        Console.WriteLine(counter);
    }

    private static bool Contains(Assignment firstRange, Assignment secondRange)
    {
        var firstContainSecond = firstRange.Start <= secondRange.Start && firstRange.End >= secondRange.End;
        var secondContainSecond = secondRange.Start <= firstRange.Start && secondRange.End >= firstRange.End;
        return firstContainSecond || secondContainSecond;
    }
}

struct Assignment
{
    public int Start { get; set; }
    public int End { get; set; }
}