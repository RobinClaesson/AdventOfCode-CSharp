using System.Drawing;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2019.Day17;

[AdventOfCodeSolution(2019, 17, stars: 1)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var computer = new IntCodeComputer(input.SplitAsLong(','));
        computer.Run();
        
        var outputs = computer.Outputs.Select(i => (char)i)
            .JoinToString()
            .Split('\n');

        var scaffoldPoints = Enumerable.Range(0, outputs.Length)
            .SelectMany(y => Enumerable.Range(0, outputs[y].Length).Select(x => new Point(x, y)))
            .Where(p => outputs[p.Y][p.X] == '#')
            .ToHashSet();

        var intersections = scaffoldPoints.Where(p => ScaffoldNeighborsCount(p) == 4).ToList();
        Output.Answer(intersections.Sum(p => p.X * p.Y));
        return;

        int ScaffoldNeighborsCount(Point point) => point.GetManhattanNeighbors()
            .Count(p => scaffoldPoints.Contains(p));
    }
}