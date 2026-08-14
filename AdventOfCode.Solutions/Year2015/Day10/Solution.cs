using System.Text.RegularExpressions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day10;

[AdventOfCodeSolution(2015, 10)]
public partial class Solution : IAdventOfCodeSolution
{
    private readonly Regex _groupRegex = GroupRegex();

    public void Run(string input)
    {
        var part1 = Enumerable.Range(0, 40).Aggregate(input, LookAndSay);
        Output.Answer(part1.Length);

        var part2 = Enumerable.Range(0, 10).Aggregate(part1, LookAndSay);
        Output.Answer(part2.Length);
    }

    private string LookAndSay(string input, int _) =>
        string.Join(string.Empty, _groupRegex.Matches(input).Select(m => $"{m.Length}{m.Value.First()}"));

    [GeneratedRegex(@"(\d)\1*")]
    private static partial Regex GroupRegex();
}