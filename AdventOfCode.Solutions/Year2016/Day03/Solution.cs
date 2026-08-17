using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day03;

[AdventOfCodeSolution(2016, 3)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var triangles = input.Rows()
            .Select(s => s.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse).ToArray())
            .ToList();

        var possible = triangles.Count(t => t.Sum() > 2 * t.Max());
        Output.Answer(possible);

        possible = 0;
        for (var x = 0; x < 3; x++)
        {
            for (var y = 0; y < triangles.Count; y += 3)
            {
                List<int> sides = [triangles[y][x], triangles[y + 1][x], triangles[y + 2][x]];
                if (sides.Sum() > 2 * sides.Max())
                    possible++;
            }
        }

        Output.Answer(possible);
    }
}