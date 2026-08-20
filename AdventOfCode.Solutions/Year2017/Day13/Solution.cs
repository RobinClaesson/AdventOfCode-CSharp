using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day13;

[AdventOfCodeSolution(2017, 13)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var layers = input.RowsSplittedAsInt(": ")
            .Select(r => new Layer(r[0], r[1]))
            .ToList();

        var caught = layers.Where(l => l.TimedForIntercept()).ToList();
        Output.Answer(caught.Sum(l => l.Severity));

        var time = 0;
        while (layers.Any(l => l.TimedForIntercept(time)))
        {
            time++;
        }

        Output.Answer(time);
    }
}