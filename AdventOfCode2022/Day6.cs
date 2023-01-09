public class Day6
{
    public static void Solution()
    {
        var buffer = File.ReadAllText(@"C:\Users\alexa\OneDrive\Dokument\adventOfCode2022\input6.txt");

        var lastFourCharacters = "";
        var marker = false;
        var index = 0;
        for (var i = 0; i < buffer.Length; i++)
        {
            if (lastFourCharacters.Length < 4)
            {
                lastFourCharacters += buffer[i];
                marker = SubRoutine(lastFourCharacters);
            }
            else
            {
                lastFourCharacters = lastFourCharacters.Substring(1);
                lastFourCharacters += buffer[i];
                marker = SubRoutine(lastFourCharacters);
            }

            if (marker)
            {
                index = i;
                break;
            }
        }
        Console.WriteLine(index + 1);
    }

    private static bool SubRoutine(string lastFourChars)
    {
        return lastFourChars.Distinct().Count() == 4;
    }
}