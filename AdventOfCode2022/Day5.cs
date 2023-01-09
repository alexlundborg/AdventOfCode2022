public class Day5
{
    private class CharStack
    {
        private readonly Stack<char> _stack = new ();

        public char Pop()
        {
            return _stack.Pop();
        }

        public void Push(char item)
        {
            _stack.Push(item);
        }
    }
    
    public static void Solution()
    {
        var stacks = new CharStack[9];
        for (var i = 0; i < 9; i++)
        {
            stacks[i] = new CharStack();
        }
        
        var lines = File.ReadAllLines(@"C:\Users\alexa\OneDrive\Dokument\adventOfCode2022\input5.txt");

        for (var i = 7; i >= 0; i--)
        {
            for (int j = 1; j < 35; j += 4)
            {
                if (!char.IsWhiteSpace(lines[i][j]))
                {
                    stacks[j / 4].Push(lines[i][j]);
                }
            } 
        }

        for (var k = 10; k < lines.Length; k++)
        {
            var split = lines[k].Split(" ");
            var amount = int.Parse(split[1]);
            var from = int.Parse(split[3]) - 1;
            var to = int.Parse(split[5]) - 1;
            for (var i = 0; i < amount; i++)
            {
                stacks[to].Push(stacks[from].Pop());
            }
        }

        var result = "";
        foreach (var stack in stacks)
        {
            result += stack.Pop();
        }
        Console.WriteLine(result);
    }
}