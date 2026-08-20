using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day06;

[AdventOfCodeSolution(2017, 6)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var banks = input.SplitAsInt('\t');
        var seen = new HashSet<string> { GetBanksString(banks) };
        string? loopPoint = null;

        while (loopPoint is null)
        {
            var index = banks.IndexOf(banks.Max());
            var blocks = banks[index];
            banks[index++] = 0;
            for (var i = 0; i < blocks; i++)
            {
                index %= banks.Count;
                banks[index++]++;
            }

            var banksString = GetBanksString(banks);
            if (!seen.Add(banksString))
            {
                loopPoint = banksString;
            }
        }

        Output.Answer(seen.Count);
        Output.Answer(seen.Count - seen.Index().First(x => x.Item == loopPoint).Index);
    }

    private static string GetBanksString(List<int> banks) => string.Join(",", banks);
}