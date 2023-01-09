public class Day6_2
{
    public static void Solution(string path)
    {
        var buffer = File.ReadAllText(path);
        var lastFourCharacters = "";
        var last14Characters = "";
        var messageMarker = false;
        var index = 0;
        for (var i = 0; i < buffer.Length; i++)
        {
            if (lastFourCharacters.Length < 4)
            {
                lastFourCharacters += buffer[i];
            }
            else
            {
                lastFourCharacters = lastFourCharacters.Substring(1);
                lastFourCharacters += buffer[i];
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
                index = i;
                break;
            }
        }
        Console.WriteLine(index + 1);
    }

    private static bool SubRoutine2(string lastFourChars)
    {
        return lastFourChars.Distinct().Count() == 14;
    }
}