using System.Text.RegularExpressions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day25;

[AdventOfCodeSolution(2015, 25)]
public partial class Solution : IAdventOfCodeSolution
{
    private const long Multiplier = 252533L;
    private const long Divisor = 33554393L;

    public void Run(string input)
    {
        var numbers = NumbersRegex().Matches(input)
            .Select(m => int.Parse(m.Value))
            .ToList();
        var targetRow = numbers[0];
        var targetColumn = numbers[1];

        var adder = targetRow + 1;
        var targetNumber = 1 + Enumerable.Range(1, targetRow - 1).Sum();
        for (var column = 2; column <= targetColumn; column++)
        {
            targetNumber += adder;
            adder++;
        }

        var result = 20151125L;
        for (var i = 2; i <= targetNumber; i++)
        {
            result *= Multiplier;
            result %= Divisor;
        }

        Output.Answer(result);
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex NumbersRegex();
}