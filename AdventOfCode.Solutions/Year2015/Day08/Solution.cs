using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day08;

[AdventOfCodeSolution(2015, 8)]
public partial class Solution : IAdventOfCodeSolution
{
    private readonly Regex _escapeCharactersRegex = EscapeCharactersRegex();

    public void Run(string input)
    {
        var rows = input.Rows();

        var codeLength = rows.Sum(s => s.Length);
        var memoryLength = rows
            .Select(s => s[1..^1])
            .Select(s => _escapeCharactersRegex.Replace(s, "X"))
            .Sum(s => s.Length);
        var encodedLength = rows.Sum(s => s.Length + s.Count('"') + s.Count('\\') + 2);

        Output.Answer(codeLength - memoryLength);
        Output.Answer(encodedLength - codeLength);
    }

    [GeneratedRegex(@"\\\""|\\\\|\\x\w{2}")]
    private static partial Regex EscapeCharactersRegex();
}