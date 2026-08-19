using System.Text.RegularExpressions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day15;

[AdventOfCodeSolution(2016, 15)]
public partial class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var discRegex = DiscRegex();
        var discs = discRegex.Matches(input)
            .Select(m => new Disc(
                id: int.Parse(m.Groups["id"].Value),
                positions: int.Parse(m.Groups["positions"].Value),
                state: int.Parse(m.Groups["state"].Value)
            )).ToList();

        Output.Answer(FindCapsuleDropTime(discs));

        discs.ForEach(d => d.Reset());
        discs.Add(new Disc(7, 11, 0));
        Output.Answer(FindCapsuleDropTime(discs));
    }

    private static int FindCapsuleDropTime(List<Disc> discs)
    {
        var time = 0;
        while (discs.Any(d => !d.TimedForCapsule))
        {
            discs.ForEach(d => d.Tick());
            time++;
        }

        return time;
    }

    [GeneratedRegex(@"Disc #(?'id'\d+) has (?'positions'\d+) positions; at time=0, it is at position (?'state'\d+)")]
    private static partial Regex DiscRegex();
}