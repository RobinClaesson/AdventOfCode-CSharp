using System.Drawing;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day14;

[AdventOfCodeSolution(2017, 14)]
public class Solution : IAdventOfCodeSolution
{
    private const int Size = 128;

    public void Run(string input)
    {
        var rows = Enumerable.Range(0, Size)
            .Select(i => HashKnot.GetHash($"{input}-{i}", HashOutputType.Binary))
            .ToList();

        Output.Answer(rows.Sum(r => r.Count(c => c == '1')));

        var used = Enumerable.Range(0, Size).SelectMany(y =>
                Enumerable.Range(0, Size).Select(x => new Point(x, y)))
            .Where(p => rows[p.Y][p.X] == '1')
            .ToList();

        var groups = 0;
        while (used.Count > 0)
        {
            groups++;
            var group = FindGroup(used[0]);
            used = used.Except(group).ToList();
        }
        
        Output.Answer(groups);
        return;

        HashSet<Point> FindGroup(Point startPoint)
        {
            var group = new HashSet<Point> { startPoint };

            var queue = new Queue<Point>();
            queue.Enqueue(startPoint);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                current.GetManhattanNeighbors()
                    .Where(p => p is { X: >= 0 and < Size, Y: >= 0 and < Size } && rows[p.Y][p.X] == '1')
                    .Where(group.Add)
                    .ToList()
                    .ForEach(p => queue.Enqueue(p));
            }

            return group;
        }
    }
}