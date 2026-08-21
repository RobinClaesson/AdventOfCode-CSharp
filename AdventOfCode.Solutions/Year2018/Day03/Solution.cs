using System.Drawing;
using System.Text.RegularExpressions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2018.Day03;

[AdventOfCodeSolution(2018, 3)]
public partial class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var claims = ClaimRegex().Matches(input).Select(m => new Claim
        (
            id: int.Parse(m.Groups["id"].Value),
            start: new Point(int.Parse(m.Groups["x"].Value), int.Parse(m.Groups["y"].Value)),
            size: new Size(int.Parse(m.Groups["w"].Value), int.Parse(m.Groups["h"].Value))
        )).ToList();

        var allPoints = claims.SelectMany(c => c.Points).ToList();
        var groupedPoints = allPoints.GroupBy(c => c).Select(g => g.ToList()).ToList();

        Output.Answer(groupedPoints.Count(g => g.Count > 1));

        var noOverlap = groupedPoints.Where(g => g.Count == 1)
            .SelectMany(g => g).ToHashSet();

        var intactClaim = claims.First(c => c.Points.All(p => noOverlap.Contains(p)));
        Output.Answer(intactClaim.Id);
    }

    private class Claim(int id, Point start, Size size)
    {
        public List<Point> Points { get; } = Enumerable.Range(start.X, size.Width)
            .SelectMany(x => Enumerable.Range(start.Y, size.Height)
                .Select(y => new Point(x, y)))
            .ToList();

        public int Id { get; } = id;
    }

    [GeneratedRegex(@"#(?'id'\d+) @ (?'x'\d+),(?'y'\d+): (?'w'\d+)x(?'h'\d+)")]
    private static partial Regex ClaimRegex();
}