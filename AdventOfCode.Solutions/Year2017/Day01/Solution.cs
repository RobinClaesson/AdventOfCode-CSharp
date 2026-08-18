using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day01;

[AdventOfCodeSolution(2017, 1)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var rows = input.Rows();

        var part1 = rows.Select(r => r.Append(r[0]).ToList())
            .SelectMany(r => r[..^1]
                .Where((c, i) => c == r[i + 1])
                .Select(c => int.Parse(c.ToString()))
            ).Sum();
        Output.Answer(part1);

        var part2 = rows.Select(r => r + r)
            .SelectMany(r => r
                .Where((c, i) => i < r.Length / 2 && c == r[i + (r.Length / 4)])
                .Select(c => int.Parse(c.ToString()))
            ).Sum();
        Output.Answer(part2);
    }
}