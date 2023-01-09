namespace AdventOfCode2022;
public class Day7
{
    
    public static void Solution()
    {
        var lines = File.ReadAllLines(@"C:\Users\alexa\OneDrive\Dokument\adventOfCode2022\input7.txt");
        var fileStructure = new Tree();
        var node = fileStructure.Root;
        var directorySizes = new Dictionary<string, int>();
        lines.Aggregate(node, (current, line) => CheckLineValue(line, current, directorySizes) ?? node);

        fileStructure.Root.PrintPretty("", true);

        var totalSum = directorySizes.Where(dir => dir.Value <= 100000).Sum(dir => dir.Value);
        Console.WriteLine("Total sum: " + totalSum);
    }

    private static Node? CheckLineValue(string line, Node? node, Dictionary<string, int> directorySize)
    {
        if (line.Contains("$ ls"))
        {
            return node;
        }

        if (line.Contains("$ cd"))
        {
            node = CheckCommand(line, node);
        }
        
        else if (line.StartsWith("dir "))
        {
            if (node == null) return node;
            var newNode = new Node(line.Substring(4), node, node.Path + "/" + line.Substring(4));
            node.Children.Add(newNode);
        }
        
        else if (line.Any(char.IsDigit))
        {
            ProcessFileInfo(line, node, directorySize);
        }

        return node;
    }
    
    private static Node? CheckCommand(string line, Node? node)
    {
        node = line.Contains("$ cd ..") ? node?.Parent : MoveIntoDirectory(line, node);
        return node;
    }
    
    private static Node? MoveIntoDirectory(string line, Node? node)
    {
        if (node == null) return node;
        var newNode = new Node(line.Substring(5), node, node.Path + "/" + line.Substring(5));
        
        if (node.Children.All(n => n.Value != newNode.Value))
        {
            node.Children.Add(newNode);
            node = newNode;
        }
        else
        {
            node = node.Children.First(n => n.Value == newNode.Value);
        }

        return node;
    }

    private static void ProcessFileInfo(string line, Node? node, Dictionary<string, int> directorySize)
    {
        var formattingSizeAndName = line.Split(" ");
        var sizeAndName = formattingSizeAndName[0];
        var size = int.Parse(sizeAndName);

        if (node?.Path != null)
        {
            directorySize.TryGetValue(node.Path, out var prevSize);
            var childNode = new Node(formattingSizeAndName[1] + $" (file, size={size})", node,
                node.Path + "/" + formattingSizeAndName[1] + $" (file, size={size})");
            node.Children.Add(childNode);
            directorySize[node.Path] = prevSize + size;
        }

        if (node?.Parent != null) AddSizeToParent(node.Parent, size, directorySize);
    }

    private static void AddSizeToParent(Node node, int size, Dictionary<string, int> directorySize)
    {
        directorySize.TryGetValue(node.Path, out var prevParentSize);
        directorySize[node.Path] = prevParentSize + size;

        if (node.Parent is not null)
        {
            AddSizeToParent(node.Parent, size, directorySize);
        }
    }
}

public class Tree
{
    public Tree()
    {
        Root = new Node("", null, "");
        Root.Children = new List<Node>();
    }
    public readonly Node Root;
}

public class Node
{
    public Node(string value, Node? parent, string path)
    {
        Parent = parent;
        Value = value;
        Path = path;
        Children = new List<Node>();
    }
    
    public readonly string Value;
    public readonly string Path;
    public List<Node> Children;
    public Node? Parent;

    public void PrintPretty(string indent, bool last)
    {
        Console.Write(indent);
        if (last)
        {
            Console.Write("- ");
            indent += "  ";
        }
        else
        {
            Console.Write("- ");
            indent += "  ";
        }
        Console.WriteLine(Value);

        for (int i = 0; i < Children.Count; i++)
        {
            Children[i].PrintPretty(indent, i == Children.Count - 1);
        }
    }
}