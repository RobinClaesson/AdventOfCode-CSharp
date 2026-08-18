using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2023.Day01;

[AdventOfCodeSolution(2023, 1)]
public partial class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var rows = input.Rows();

        var digitRegex = DigitRegex();
        var part1 = rows
            .Select(s => digitRegex.Matches(s))
            .Select(m => int.Parse($"{m.First()}{m.Last()}"))
            .Sum();
        Output.Answer(part1);

        var extendedDigitRegex = ExtendedDigitRegex();
        var part2 = rows
            .Select(s => extendedDigitRegex.Matches(s).Select(m => m.Groups[1].Value).ToList())
            .Select(m => 10 * TranslateDigit(m.First()) + TranslateDigit(m.Last()))
            .Sum();
        Output.Answer(part2);
    }

    private static int TranslateDigit(string digit) => digit switch
    {
        "one" => 1, "two" => 2, "three" => 3,
        "four" => 4, "five" => 5, "six" => 6,
        "seven" => 7, "eight" => 8, "nine" => 9,
        _ => int.Parse(digit)
    };

    [GeneratedRegex(@"\d")]
    private static partial Regex DigitRegex();

    [GeneratedRegex(@"(?=(\d|one|two|three|four|five|six|seven|eight|nine))")]
    private static partial Regex ExtendedDigitRegex();
}