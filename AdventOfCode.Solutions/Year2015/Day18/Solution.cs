using System.Drawing;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day18;

[AdventOfCodeSolution(2015, 18)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var lights = input.Rows()
            .Select(r => r.Select(c => c == '#' ? 1 : 0).ToArray())
            .ToArray();
        
        var size = lights.Length;

        var points = Enumerable.Range(0, size)
            .SelectMany(y => Enumerable.Range(0, size)
                .Select(x => new Point(x, y))
            ).ToList();

        var corners = new List<Point>
        {
            new(0, 0), new(size - 1, 0), new(0, size - 1), new(size - 1, size - 1)
        };

        IterateLights(100);
        Output.Answer(lights.SelectMany(r => r).Sum());

        lights = input.Rows()
            .Select(r => r.Select(c => c == '#' ? 1 : 0).ToArray())
            .ToArray();

        corners.ForEach(p => lights[p.Y][p.X] = 1);
        points = points.Except(corners).ToList();

        IterateLights(100);
        Output.Answer(lights.SelectMany(r => r).Sum());
        return;

        void IterateLights(int iterations)
        {
            for (var i = 0; i < iterations; i++)
            {
                var buffer = Enumerable.Range(0, size)
                    .Select(y => new int[size])
                    .ToArray();
                corners.ForEach(p => buffer[p.Y][p.X] = 1);

                foreach (var point in points)
                {
                    var aliveNeighbors = point.GetNeighbors()
                        .Where(p => p.X >= 0 && p.X < size && p.Y >= 0 && p.Y < size)
                        .Sum(p => lights[p.Y][p.X]);

                    buffer[point.Y][point.X] = lights[point.Y][point.X] == 1 && aliveNeighbors is 2 or 3 ||
                                               lights[point.Y][point.X] == 0 && aliveNeighbors is 3
                        ? 1
                        : 0;
                }

                lights = buffer;
            }
        }
    }
}