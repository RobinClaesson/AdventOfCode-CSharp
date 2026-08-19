using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day02;

[AdventOfCodeSolution(2017, 2)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var rows = input.RowsSplittedAsInt('\t');
        var divisors = rows.Select(r =>
            r.SelectMany(a => r.Select(b => new { A = a, B = b }))
                .First(p => p.A != p.B && p.A % p.B == 0)
        ).ToList();

        Output.Answer(rows.Sum(r => r.Max() - r.Min()));
        Output.Answer(divisors.Sum(d => d.A / d.B));
    }
}