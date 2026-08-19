using System.Drawing;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day13;

[AdventOfCodeSolution(2016, 13)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var designerNumber = int.Parse(input);
        var target = new Point(31, 39);
        var start = new Point(1, 1);

        var distances = new Dictionary<Point, int> { { start, 0 } };
        var queue = new Queue<Point>();
        queue.Enqueue(start);
        while (!distances.ContainsKey(target))
        {
            var current = queue.Dequeue();

            current.GetManhattanNeighbors()
                .Where(ShouldVisit)
                .ToList()
                .ForEach(p =>
                {
                    queue.Enqueue(p);
                    distances[p] = distances[current] + 1;
                });
        }

        Output.Answer(distances[target]);
        Output.Answer(distances.Values.Count(d => d <= 50));
        return;

        bool ShouldVisit(Point point) => point is { X: >= 0, Y: >= 0 }
                                     && !distances.ContainsKey(point)
                                     && IsOpen(point);

        bool IsOpen(Point point)
        {
            var number = point.X * point.X
                         + 3 * point.X + 2 * point.X * point.Y
                         + point.Y
                         + point.Y * point.Y
                         + designerNumber;

            return Convert.ToString(number, 2).Count(c => c == '1') % 2 == 0;
        }
    }
}