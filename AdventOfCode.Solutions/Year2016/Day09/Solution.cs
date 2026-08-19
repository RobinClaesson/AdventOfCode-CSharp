using System.Text.RegularExpressions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day09;

[AdventOfCodeSolution(2016, 9)]
public partial class Solution : IAdventOfCodeSolution
{
    private static Regex _markerRegex = MarkerRegex();

    public void Run(string input)
    {
        Output.Answer(DecompressedLength(input, false));
        Output.Answer(DecompressedLength(input));
    }

    private static long DecompressedLength(string compressed, bool recursiveDecompress = true)
    {
        long totalLength = 0;
        var index = 0;
        while (index < compressed.Length)
        {
            var match = _markerRegex.Match(compressed, index);
            if (!match.Success)
            {
                totalLength += compressed.Length - index;
                break;
            }

            var dataStart = match.Index + match.Length;
            var dataLength = int.Parse(match.Groups[1].Value);
            var repetitions = int.Parse(match.Groups[2].Value);

            var decompressedLength = recursiveDecompress
                ? DecompressedLength(compressed.Substring(dataStart, dataLength)) * repetitions
                : dataLength * repetitions;

            totalLength += match.Index - index + decompressedLength;
            index = match.Index + match.Length + dataLength;
        }

        return totalLength;
    }

    [GeneratedRegex(@"\((\d+)x(\d+)\)")]
    private static partial Regex MarkerRegex();
}