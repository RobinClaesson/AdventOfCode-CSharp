using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2018.Day02;

[AdventOfCodeSolution(2018, 2)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var rows = input.Rows();
        var groups = rows
            .Select(r => r.GroupBy(c => c).ToList())
            .ToList();

        var twoGroups = groups.Count(r => r.Any(g => g.Count() == 2));
        var threeGroups = groups.Count(r => r.Any(g => g.Count() == 3));
        Output.Answer(twoGroups * threeGroups);

        var minDiffPair = rows.SelectMany(r1 => rows.Select(r2 => (r1, r2)))
            .Where(p => p.r1 != p.r2)
            .Select(p =>
            (
                r1: p.r1.Select((c, i) => (c, i)).ToList(),
                r2: p.r2.Select((c, i) => (c, i)).ToList()
            ))
            .MinBy(p => p.r1.Concat(p.r2).Distinct().Count());

        var commonLetters = minDiffPair.r1
            .Intersect(minDiffPair.r2)
            .OrderBy(l => l.i)
            .Select(l => l.c)
            .JoinToString();

        Output.Answer(commonLetters);
    }
}