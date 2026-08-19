using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day20;

[AdventOfCodeSolution(2016, 20)]
public class Solution : IAdventOfCodeSolution
{
    private const long MaxIp = 4294967295L;

    public void Run(string input)
    {
        var ranges = input.RowsSplittedAsLong('-')
            .Select(r => new Range(r[0], r[1]))
            .ToList();

        var merged = MergeRanges(ranges);
        Output.Answer(merged[0].End + 1);

        var bannedIps = merged.Sum(r => r.Length);
        Output.Answer(MaxIp - bannedIps + 1);
    }

    internal class Range(long start, long end)
    {
        public long Start { get; set; } = start;
        public long End { get; set; } = end;
        public long Length => End - Start + 1;
    }

    private static List<Range> MergeRanges(IEnumerable<Range> ranges)
    {
        var mergedRanges = new LinkedList<Range>(ranges
            .OrderBy(r => r.Start)
            .ThenBy(r => r.End));

        var currentNode = mergedRanges.First;
        while (currentNode is not null)
        {
            var current = currentNode.Value;

            //If the next banned ip range starts lower than 2 away from current end they can be merged
            while (currentNode?.Next is not null && current.End >= currentNode.Next.Value.Start - 1)
            {
                if (current.End < currentNode.Next.Value.End)
                    current.End = currentNode.Next.Value.End;

                mergedRanges.Remove(currentNode.Next);
            }

            currentNode = currentNode!.Next;
        }

        return mergedRanges.ToList();
    }
}