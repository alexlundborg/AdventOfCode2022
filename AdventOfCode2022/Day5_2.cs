public class Day5_2
{
    public class MyStack
    {
        public readonly Stack<char> _theStack = new ();

        public char Pop()
        {
            return _theStack.Pop();
        }

        public void Push(char item)
        {
            _theStack.Push(item);
        }
    }
    
    public static void Solution()
    {
        var stacks = new MyStack[9];
        for (var i = 0; i < 9; i++)
        {
            stacks[i] = new MyStack();
        }
        
        var lines = File.ReadAllLines(@"C:\Users\alexa\OneDrive\Dokument\adventOfCode2022\input5.txt");

        for (var i = 7; i >= 0; i--)
        {
            for (int j = 1; j < 35; j += 4)
            {
                if (!char.IsWhiteSpace(lines[i][j]))
                {
                    Console.WriteLine(lines[i][j]);
                    stacks[j / 4].Push(lines[i][j]);
                }
            } 
        }

        foreach (var stack in stacks)
        {
            Console.WriteLine("stack");

            foreach (var item in stack._theStack)
            {
                Console.WriteLine("item: " + item);
            }

        }
        
        for (var k = 10; k < lines.Length; k++)
        {
            var split = lines[k].Split(" ");
            var amount = int.Parse(split[1]);
            var from = int.Parse(split[3]) - 1;
            var to = int.Parse(split[5]) - 1;
            Console.WriteLine(k);
            var tempStack = new MyStack();
            PushPop(amount, stacks[from], tempStack);
            PushPop(amount, tempStack, stacks[to]);
        }

        var result = "";
        foreach (var stack in stacks)
        {
            result += stack.Pop();
        }
        Console.WriteLine(result);
    }

    private static void PushPop(int amount, MyStack stackTo, MyStack stacksFrom)
    {
        for (var i = 0; i < amount; i++)
        {
            stacksFrom.Push(stackTo.Pop());
        }
    }
}