using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day07;

[AdventOfCodeSolution(2017, 7)]
public partial class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var programs = ProgramRegex().Matches(input)
            .Select(m => new Program(
                name: m.Groups["name"].Value,
                weight: int.Parse(m.Groups["weight"].Value),
                childNames: m.Groups["children"].Value.Split(", ", StringSplitOptions.RemoveEmptyEntries).ToList()
            )).ToList();

        foreach (var parent in programs)
        {
            var children = parent.ChildNames
                .Select(name => programs.First(child => child.Name == name));
            foreach (var child in children)
            {
                parent.Children.Add(child);
                child.Parent = parent;
            }
        }

        Output.Answer(programs.First(p => p.Parent is null).Name);

        var unbalanced = programs.First(p => !p.IsBalanced && p.AllChildrenAreBalanced());
        var mostCommonTotalWeight = unbalanced.Children.Select(c => c.TotalWeight).MostCommon();
        var errorChild = unbalanced.Children.First(c => c.TotalWeight != mostCommonTotalWeight);
        Output.Answer(errorChild.Weight + (mostCommonTotalWeight - errorChild.TotalWeight));
    }

    [GeneratedRegex(@"(?'name'\w+) \((?'weight'\d+)\)( -> )*(?'children'\w+(, \w+)+)*")]
    private static partial Regex ProgramRegex();
}