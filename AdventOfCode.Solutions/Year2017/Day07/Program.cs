namespace AdventOfCode.Solutions.Year2017.Day07;

public class Program(string name, int weight, List<string> childNames)
{
    public string Name { get; } = name;
    public List<string> ChildNames { get; } = childNames;
    public int Weight { get; } = weight;
    public List<Program> Children { get; } = [];
    public Program? Parent { get; set; }

    private int? _totalWeight = null;

    public int TotalWeight
    {
        get
        {
            _totalWeight ??= Weight + Children.Sum(c => c.TotalWeight);
            return _totalWeight.Value;
        }
    }

    public bool IsBalanced
    {
        get
        {
            if (Children.Count == 0)
                return true;

            var childWeights = Children.Select(c => c.TotalWeight).ToList();
            var averageChildWeight = (int)childWeights.Average();
            return childWeights.All(w => w == averageChildWeight);
        }
    }

    public bool AllChildrenAreBalanced() => Children.All(c => c.IsBalanced);
}