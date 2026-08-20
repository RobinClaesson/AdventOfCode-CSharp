using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day05;

[AdventOfCodeSolution(2017, 5)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        Output.Answer(CountJumps(_ => 1));
        Output.Answer(CountJumps(offset => offset >= 3 ? -1 : 1));
        return;

        int CountJumps(Func<int, int> increment)
        {
            var offsets = input.RowsAsInt();
            var jumps = 0;
            var index = 0;
            while (index >= 0 && index < offsets.Count)
            {
                var offset = offsets[index];
                offsets[index] += increment(offset);
                index += offset;
                jumps++;
            }

            return jumps;
        }
    }
}