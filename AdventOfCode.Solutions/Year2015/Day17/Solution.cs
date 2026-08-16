using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day17;

[AdventOfCodeSolution(2015, 17)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var solutions = input.RowsAsInt()
            .Subsets()
            .Where(c => c.Sum() == 150)
            .ToList();
        
        Output.Answer(solutions.Count);

        var min = solutions.Min(s => s.Count);
        Output.Answer(solutions.Count(s => s.Count == min));
    }
}