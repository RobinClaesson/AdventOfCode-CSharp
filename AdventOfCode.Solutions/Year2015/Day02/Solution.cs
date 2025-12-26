using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day02;

[AdventOfCodeSolution(2015, 2)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var presents = input
            .RowsSplittedAsInt('x')
            .Select(p => new Present(p[0], p[1], p[2]))
            .ToList();

        Output.Answer(presents.Sum(p => p.CalcPaper()));
        Output.Answer(presents.Sum(p => p.CalcRibbon()));
    }
}