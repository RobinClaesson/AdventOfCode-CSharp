using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day19;

[AdventOfCodeSolution(2015, 19)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var rows = input.Rows();
        var medicine = rows.Last();
        var substitutions = rows[..^2]
            .Select(r => r.Split(" => "))
            .ToList();

        var created = substitutions.SelectMany(substitution => new Regex(substitution[0])
            .Matches(medicine)
            .Select(m => $"{medicine[..m.Index]}{substitution[1]}{medicine[(m.Index + m.Length)..]}")
        ).ToHashSet();

        Output.Answer(created.Count);

        // Part 2 based on insights from https://www.reddit.com/r/adventofcode/comments/3xflz8/comment/cy4etju
        var simplifiedInitial = substitutions.Aggregate(
            medicine.Replace("Rn", "(").Replace("Y", ",").Replace("Ar", ")"),
            (acc, substitution) => acc.Replace(substitution[0], "T"));

        var bracketCount = simplifiedInitial.Count(c => c is '(' or ')');
        var commaCount = simplifiedInitial.Count(c => c is ',');
        Output.Answer(simplifiedInitial.Length - bracketCount - (2 * commaCount) - 1);
    }
}