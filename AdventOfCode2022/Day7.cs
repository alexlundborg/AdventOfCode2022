namespace AdventOfCode2022;
public class Day7
{
    
    public static void Solution()
    {
        var lines = File.ReadAllLines(@"C:\Users\alexa\OneDrive\Dokument\adventOfCode2022\input7.txt");
        var fileStructure = new Tree();
        var node = fileStructure.Root;
        var directorySizes = new Dictionary<string, int>();
        for (var i = 0; i < lines.Length; i++)
        {
            node = CheckLineValue(lines, i, node, directorySizes);
        }

        fileStructure.Root.PrintPretty("", true);

        var totalSum = directorySizes.Where(dir => dir.Value <= 100000).Sum(dir => dir.Value);

        Console.WriteLine("Total sum: " + totalSum);
    }

    private static Node? CheckLineValue(string[] lines, int i, Node? node, Dictionary<string, int> directorySize)
    {
        if (lines[i].Contains("$ ls"))
        {
            return node;
        }
        
        if (lines[i].Contains("$ cd"))
        {
            node = CheckCommand(lines, i, node);
        }
        else if (lines[i].StartsWith("dir "))
        {
            if (node == null) return node;
            var newNode = new Node(lines[i].Substring(4), node, node.Path + "/" + lines[i].Substring(4));
            node.Children.Add(newNode);
        }
        else if (lines[i].Any(char.IsDigit))
        {
            ProcessFileInfo(lines, i, node, directorySize);
        }

        return node;
    }

    private static void ProcessFileInfo(string[] lines, int i, Node? node, Dictionary<string, int> directorySize)
    {
        var formattingSizeAndName = lines[i].Split(" ");
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

    private static Node? CheckCommand(string[] lines, int i, Node? node)
    {
        node = lines[i].Contains("$ cd ..") ? node?.Parent : MoveIntoDirectory(lines, i, node);
        return node;
    }

    private static Node? MoveIntoDirectory(string[] lines, int i, Node? node)
    {
        if (node == null) return node;
        var newNode = new Node(lines[i].Substring(5), node, node.Path + "/" + lines[i].Substring(5));
        
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