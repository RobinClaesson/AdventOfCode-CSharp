using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2020.Day01;

[AdventOfCodeSolution(2020, 1)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var rows = input.RowsAsInt();

        var pair = rows.SelectMany(a =>
                rows.Select(b => (a, b)))
            .First(pair => pair.a + pair.b == 2020);
        Output.Answer(pair.a * pair.b);

        var trio = rows.SelectMany(a =>
                rows.SelectMany(b =>
                    rows.Select(c => (a, b, c))))
            .First(trio => trio.a + trio.b + trio.c == 2020);
        Output.Answer(trio.a * trio.b * trio.c);
    }
}