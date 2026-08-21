using System.Numerics;
using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day20;

[AdventOfCodeSolution(2017, 20)]
public partial class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var vectorRegex = VectorRegex();
        var particles = input.Rows()
            .Select(r => vectorRegex.Matches(r).Select(ParseVector).ToArray())
            .Select((v, i) => new Particle(i, v[0], v[1], v[2]))
            .ToList();

        Output.Answer(particles.MinBy(p => p.Acceleration.Length())!.Index);

        for (var ticksSinceCollision = 0; ticksSinceCollision < 10; ticksSinceCollision++)
        {
            var alive = particles.Where(p => p.Alive).ToList();

            alive.ForEach(p => p.Update());
            var collisions = alive.GroupBy(p => p.Position)
                .Where(g => g.Count() > 1)
                .SelectMany(g => g)
                .ToList();

            if (collisions.Count > 0)
            {
                ticksSinceCollision = 0;
                collisions.ForEach(p => p.Alive = false);
            }
        }

        Output.Answer(particles.Count(p => p.Alive));
    }

    private static Vector3 ParseVector(Match match) => new(
        int.Parse(match.Groups["x"].Value),
        int.Parse(match.Groups["y"].Value),
        int.Parse(match.Groups["z"].Value)
    );

    [GeneratedRegex(@"<(?'x'-*\d+),(?'y'-*\d+),(?'z'-*\d+)>")]
    private static partial Regex VectorRegex();
}