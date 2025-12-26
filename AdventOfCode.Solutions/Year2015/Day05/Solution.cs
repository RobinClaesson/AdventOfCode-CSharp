using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day05;

[AdventOfCodeSolution(2015, 5)]
public partial class Solution : IAdventOfCodeSolution
{
    private readonly Regex _vowelRegex = VowelRegex();
    private readonly Regex _doubleCharsRegex = DoubleCharsRegex();
    private readonly Regex _doubleCharsWithBetweenRegex = DoubleCharsWithBetweenRegex();
    private readonly Regex _twoCharsAppearTwiceRegex = TwoCharsAppearTwiceRegex();
    private readonly string[] _part1Forbidden = ["ab", "cd", "pq", "xy"];

    public void Run(string input)
    {
        var inputRows = input.Rows();

        Output.Answer(inputRows.Count(IsNicePart1));
        Output.Answer(inputRows.Count(IsNicePart2));
    }

    private bool IsNicePart1(string s) =>
        !_part1Forbidden.Any(s.Contains)
        && _vowelRegex.Count(s) > 2
        && _doubleCharsRegex.IsMatch(s);

    private bool IsNicePart2(string s) =>
        _doubleCharsWithBetweenRegex.IsMatch(s)
        && _twoCharsAppearTwiceRegex.IsMatch(s);

    [GeneratedRegex(@"[aeiou]", RegexOptions.Compiled)]
    private static partial Regex VowelRegex();

    [GeneratedRegex(@"(.)\1", RegexOptions.Compiled)]
    private static partial Regex DoubleCharsRegex();

    [GeneratedRegex(@"(.).\1", RegexOptions.Compiled)]
    private static partial Regex DoubleCharsWithBetweenRegex();

    [GeneratedRegex(@"(.)(.).*\1\2", RegexOptions.Compiled)]
    private static partial Regex TwoCharsAppearTwiceRegex();
}