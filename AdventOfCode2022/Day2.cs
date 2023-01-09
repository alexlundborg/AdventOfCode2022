class Day2
{
    public static void Solution()
    {
        var lines = File.ReadAllLines(@"C:\Users\alexa\OneDrive\Dokument\adventOfCode2022\input2.txt");

        var sum = 0;
        foreach (var line in lines)
        {
            sum += GetWinner(ConvertLetter(line[0].ToString()), ConvertLetter(line[2].ToString()));
        }

        Console.WriteLine(sum);
    }

    static int GetWinner(Options opponentMove, Options yourMove)
    {
        switch (opponentMove, yourMove)
        {
            case (Options.Rock, Options.Paper): return 6 + 2;
            case (Options.Rock, Options.Rock): return 3 + 1;
            case (Options.Rock, Options.Scissors): return 0 + 3;
            case (Options.Paper, Options.Paper): return 3 + 2;
            case (Options.Paper, Options.Rock): return 0 + 1;
            case (Options.Paper, Options.Scissors): return 6 + 3;
            case (Options.Scissors, Options.Paper): return 0 + 2;
            case (Options.Scissors, Options.Rock): return 6 + 1;
            case (Options.Scissors, Options.Scissors): return 3 + 3;
            default: return 0;
        }
    }

    static Options ConvertLetter(string letter)
    {
        switch (letter)
        {
            case "A":
                return Options.Rock;
            case "B":
                return Options.Paper;
            case "C":
                return Options.Scissors;
            case "X":
                return Options.Rock;
            case "Y":
                return Options.Paper;
            case "Z":
                return Options.Scissors;
            default: throw new Exception();
        }
    }
}

