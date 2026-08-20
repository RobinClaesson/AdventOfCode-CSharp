using System.Drawing;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;
using AdventOfCode.Solutions.Navigation;

namespace AdventOfCode.Solutions.Year2017.Day11;

[AdventOfCodeSolution(2017, 11)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var positions = input.Split(',').Select(d => d switch
        {
            "n" => FlatTopHexagonDirection.North,
            "ne" => FlatTopHexagonDirection.NorthEast,
            "se" => FlatTopHexagonDirection.SouthEast,
            "s" => FlatTopHexagonDirection.South,
            "sw" => FlatTopHexagonDirection.SouthWest,
            _ => FlatTopHexagonDirection.NorthWest
        }).Aggregate(new List<HexagonPoint> { HexagonPoint.Zero }, (acc, step) =>
            acc.Append(acc.Last().Step(step)).ToList());

        Output.Answer(positions.Last().Length);
        Output.Answer(positions.Max(p => p.Length));
    }
}