using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2024.Day01;

[AdventOfCodeSolution(2024, 1)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var left = new List<int>();
        var right = new List<int>();

        input.RowsSplitted(' ').ForEach(row =>
        {
            left.Add(int.Parse(row.First()));
            right.Add(int.Parse(row.Last()));
        });

        left = left.Order().ToList();
        right = right.Order().ToList();

        var diffSum = Enumerable.Range(0, left.Count)
            .Select(i => Math.Abs(left[i] - right[i]))
            .Sum();

        Output.Answer(diffSum);
        Output.Answer(left.Sum(l => l * right.Count(r => l == r)));
    }
}