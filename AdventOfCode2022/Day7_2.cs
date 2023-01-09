public class Day7_2
{
    private const int AvailableDiskSpace = 70000000;   
    private const int SpaceRequiredForUpdate = 30000000;

    public static void Solution(string path)
    {
        var lines = File.ReadAllLines(path);
        var fileStructure = new Tree();
        var node = fileStructure.Root;
        var directorySizes = new Dictionary<string, int>();
        
        lines.Aggregate(node, (current, line) => CheckLineValue(line, current, directorySizes) ?? node);
        
        // uncomment to see folder structure
        // folderStructure.Root.PrintPretty("", true);

        var spaceRequiredToFreeUp = directorySizes[node.Children[0].Path] + SpaceRequiredForUpdate - AvailableDiskSpace;
        
        var result = directorySizes
            .Select(dir => dir.Value)
            .Where(value => value > spaceRequiredToFreeUp)
            .Min();
        
        Console.WriteLine("Delete the directory with this size: " + result);
    }

    private static Node? CheckLineValue(string line, Node? node, IDictionary<string, int> directorySizes)
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
            var newNode = new Node(line[4..], node, node.Path + "/" + line[4..]);
            node.Children.Add(newNode);
        }
        
        else if (line.Any(char.IsDigit))
        {
            ProcessFileInfo(line, node, directorySizes);
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
        var name = line[5..];
        var newNode = new Node(name, node, node.Path + "/" + name);
        
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

    private static void ProcessFileInfo(string line, Node? node, IDictionary<string, int> directorySizes)
    {
        var formattingSizeAndName = line.Split(" ");
        var sizeAndName = formattingSizeAndName[0];
        var size = int.Parse(sizeAndName);

        if (node?.Path != null)
        {
            directorySizes.TryGetValue(node.Path, out var prevSize);
            directorySizes[node.Path] = prevSize + size;
            AddChild(node, formattingSizeAndName, size);
        }

        if (node?.Parent != null) AddSizeToParent(node.Parent, size, directorySizes);
    }

    private static void AddChild(Node node, IReadOnlyList<string> formattingSizeAndName, int size)
    {
        var name = formattingSizeAndName[1] + $" (file, size={size})";
        var childNode = new Node(name, node,
            node.Path + "/" + name);
        node.Children.Add(childNode);
    }

    private static void AddSizeToParent(Node node, int size, IDictionary<string, int> directorySizes)
    {
        directorySizes.TryGetValue(node.Path, out var prevParentSize);
        directorySizes[node.Path] = prevParentSize + size;

        if (node.Parent is not null)
        {
            AddSizeToParent(node.Parent, size, directorySizes);
        }
    }

    private class Tree
    {
        public Tree()
        {
            Root = new Node("", null, "")
            {
                Children = new List<Node>()
            };
        }
        public readonly Node Root;
    }

    private class Node
    {
        public Node(string value, Node? parent, string path)
        {
            Value = value;
            Parent = parent;
            Path = path;
            Children = new List<Node>();
        }

        public readonly string Value;
        public readonly Node? Parent;
        public readonly string Path;
        public List<Node> Children;

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

            for (var i = 0; i < Children.Count; i++)
            {
                Children[i].PrintPretty(indent, i == Children.Count - 1);
            }
        }
    }
}