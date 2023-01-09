class Day2_2
{
    public static void Solution()
    {
        var lines = System.IO.File.ReadAllLines(@"C:\Users\alexa\OneDrive\Dokument\adventOfCode2022\input2.txt");

        var sum = 0;
        foreach (var line in lines)
        {
            sum += GetWinner(ConvertLetter(line[0].ToString()), ConvertLetter(line[2].ToString()));
        }

        Console.WriteLine(sum);
    }

    static int GetWinner(Options_2 opponentMove, Options_2 yourMove)
    {
        switch (opponentMove, yourMove)
        {
            case (Options_2.Rock, Options_2.Lose): return 0 + 3;
            case (Options_2.Rock, Options_2.Draw): return 3 + 1;
            case (Options_2.Rock, Options_2.Win): return 6 + 2;
            case (Options_2.Paper, Options_2.Lose): return 0 + 1;
            case (Options_2.Paper, Options_2.Draw): return 3 + 2;
            case (Options_2.Paper, Options_2.Win): return 6 + 3;
            case (Options_2.Scissors, Options_2.Lose): return 0 + 2;
            case (Options_2.Scissors, Options_2.Draw): return 3 + 3;
            case (Options_2.Scissors, Options_2.Win): return 6 + 1;
            default: return 0;
        }
    }

    static Options_2 ConvertLetter(string letter)
    {
        switch (letter)
        {
            case "A":
                return Options_2.Rock;
            case "B":
                return Options_2.Paper;
            case "C":
                return Options_2.Scissors;
            case "X":
                return Options_2.Lose;
            case "Y":
                return Options_2.Draw;
            case "Z":
                return Options_2.Win;
            default: throw new Exception();
        }
    }
}

public enum Options
{
    Rock,
    Paper,
    Scissors
}

public enum Options_2
{
    Rock,
    Paper,
    Scissors,
    Lose,
    Draw,
    Win
}
