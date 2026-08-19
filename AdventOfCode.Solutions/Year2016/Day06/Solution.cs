using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day06;

[AdventOfCodeSolution(2016, 6)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var mostCommon = input.Columns()
            .Select(r => r.OrderByDescending(c => r.Count(c2 => c == c2)).First())
            .JoinToString();
        Output.Answer(mostCommon);
        
        var leastCommon = input.Columns()
            .Select(r => r.OrderBy(c => r.Count(c2 => c == c2)).First())
            .JoinToString();
        Output.Answer(leastCommon);
    }
}