using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2018.Day01;

[AdventOfCodeSolution(2018, 1)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var rows = input.RowsAsInt();
        Output.Answer(rows.Sum());

        var frequencies = new HashSet<int>();
        var current = 0;
        for (var i = 0; !frequencies.Contains(current); i++)
        {
            frequencies.Add(current);
            i %= rows.Count;
            current += rows[i];
        }
        Output.Answer(current);
    }
}