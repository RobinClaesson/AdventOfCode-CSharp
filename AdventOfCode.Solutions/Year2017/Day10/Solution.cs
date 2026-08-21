using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day10;

[AdventOfCodeSolution(2017, 10)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var lengths = input.SplitAsInt(',');
        var knot = HashKnot.GetKnot(lengths, 1);
        Output.Answer(knot[0] * knot[1]);

        Output.Answer(HashKnot.GetHash(input));
    }
}