using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day12;

[AdventOfCodeSolution(2017, 12)]
public partial class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var numbersRegex = NumbersRegex();

        var pipes = input.Rows()
            .Select(r => numbersRegex.Matches(r)
                .Skip(1)
                .Select(m => int.Parse(m.Value)).ToList())
            .ToList();

        var groupZero = FindGroup(0);
        Output.Answer(groupZero.Count);

        var groupCount = 1;
        var ungrouped = Enumerable.Range(0, pipes.Count).Except(groupZero).ToList();
        while (ungrouped.Count > 0)
        {
            groupCount++;
            var group = FindGroup(ungrouped[0]);
            ungrouped = ungrouped.Except(group).ToList();
        }

        Output.Answer(groupCount);

        return;

        HashSet<int> FindGroup(int startPoint)
        {
            var group = new HashSet<int> { startPoint };

            var queue = new Queue<int>();
            queue.Enqueue(startPoint);
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                pipes[current].Where(group.Add).ToList().ForEach(p => queue.Enqueue(p));
            }

            return group;
        }
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex NumbersRegex();
}