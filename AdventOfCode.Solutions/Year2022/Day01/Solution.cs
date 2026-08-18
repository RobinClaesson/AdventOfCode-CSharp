using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2022.Day01;

[AdventOfCodeSolution(2022, 1)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var elves = input.Split($"{Environment.NewLine}{Environment.NewLine}",
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(elf => elf.RowsAsInt().Sum())
            .ToList();
        
        Output.Answer(elves.Max());
        Output.Answer(elves.OrderDescending().Take(3).Sum());
    }
}