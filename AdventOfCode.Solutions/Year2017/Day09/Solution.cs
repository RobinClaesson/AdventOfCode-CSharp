using System.Text.RegularExpressions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day09;

[AdventOfCodeSolution(2017, 9)]
public partial class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var ignored = IgnoreRegex().Replace(input, string.Empty);
        var cleaned = GarbageRegex().Replace(ignored, string.Empty);

        var stack = 0;
        var score = 0;
        foreach (var c in cleaned)
        {
            switch (c)
            {
                case '{':
                    score += ++stack;
                    break;
                case '}':
                    stack--;
                    break;
            }
        }

        Output.Answer(score);
        Output.Answer(GarbageRegex().Matches(ignored).Sum(m => m.Groups[1].Length));
    }

    [GeneratedRegex(@"!.")]
    private static partial Regex IgnoreRegex();

    [GeneratedRegex(@"<([^>]*)>")]
    private static partial Regex GarbageRegex();

}