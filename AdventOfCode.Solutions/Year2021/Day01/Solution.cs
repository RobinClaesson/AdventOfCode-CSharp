using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2021.Day01;

[AdventOfCodeSolution(2021, 1)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var rows = input.RowsAsInt();

        var increases = Enumerable.Range(1, rows.Count - 1)
            .Count(i => rows[i] > rows[i - 1]);
        Output.Answer(increases);

        var windowIncreases = Enumerable.Range(1, rows.Count - 3)
            .Count(i => Window(i) > Window(i - 1));
        Output.Answer(windowIncreases);

        return;
        int Window(int i) => rows[i] + rows[i + 1] + rows[i + 2];
    }
}