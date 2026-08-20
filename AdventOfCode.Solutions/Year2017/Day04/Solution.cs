using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day04;

[AdventOfCodeSolution(2017, 4)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var rows = input.RowsSplitted(' ');

        Output.Answer(rows.Count(r => r.Length == r.Distinct().Count()));
        Output.Answer(rows.Count(r => r.Length == DistinctAnagramCount(r)));
    }

    private static int DistinctAnagramCount(IEnumerable<string> words) => words
        .Select(s => s.Order().JoinToString())
        .Distinct()
        .Count();
}