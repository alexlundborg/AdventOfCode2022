public class Day6_2
{
    public static void Solution()
    {
            var buffer = File.ReadAllText(@"C:\Users\alexa\OneDrive\Dokument\adventOfCode2022\input6.txt");

        var lastFourCharacters = "";
        var last14Characters = "";
        var marker = false;
        var messageMarker = false;
        var index = 0;
        var index2 = 0;
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

            if (marker && index == 0)
            {
                index = i;
            }
            
            if (last14Characters.Length < 14)
            {
                last14Characters += buffer[i];
                messageMarker = SubRoutine2(last14Characters);
            }
            else
            {
                last14Characters = last14Characters.Substring(1);
                last14Characters += buffer[i];
                messageMarker = SubRoutine2(last14Characters);
            }
            
            if (messageMarker)
            {
                index2 = i;
                break;
            }
        }
        Console.WriteLine(index + 1);
        Console.WriteLine(index2 + 1);
    }

    private static bool SubRoutine(string lastFourChars)
    {
        return lastFourChars.Distinct().Count() == 4;
    }
    
    private static bool SubRoutine2(string lastFourChars)
    {
        return lastFourChars.Distinct().Count() == 14;
    }
}