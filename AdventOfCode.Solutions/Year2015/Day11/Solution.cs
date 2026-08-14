using System.Text.RegularExpressions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day11;

[AdventOfCodeSolution(2015, 11)]
public partial class Solution : IAdventOfCodeSolution
{
    private const char OverflowChar = (char)('z' + 1);
    private readonly Regex _pairRegex = PairRegex();

    public void Run(string input)
    {
        var password = GetNextValidPassword(input);
        Output.Answer(password);
        
        password = IncrementPassword(password);
        password = GetNextValidPassword(password);
        Output.Answer(password);
    }

    private string GetNextValidPassword(string password)
    {
        while (IsInvalidPassword(password))
        {
            password = IncrementPassword(password);
        }

        return password;
    }
    
    private bool IsInvalidPassword(string password) =>
        _pairRegex.Count(password) < 2 || !Straights.Any(password.Contains);

    private static string IncrementPassword(string password)
    {
        var offset = 0;
        var characters = password.ToCharArray();
        while (true)
        {
            var replaceAt = password.Length - 1 - offset;

            var currentChar = password[replaceAt];
            currentChar += currentChar is 'h' or 'k' or 'n' ? (char)2 : (char)1;

            var overflow = currentChar == OverflowChar;
            characters[replaceAt] = overflow ? 'a' : currentChar;

            if (!overflow)
                return new string(characters);

            offset++;
        }
    }

    private static readonly List<string> Straights =
    [
        "abc", "bcd", "cde", "def", "efg", "fgh", "ghi", "hij", "ijk", "jkl", "klm", "lmn", "mno", "nop", "opq", "pqr",
        "qrs", "rst", "stu", "tuv", "uvw", "vwx", "wxy", "xyz"
    ];
    
    [GeneratedRegex(@"(\w)\1{1}")]
    private static partial Regex PairRegex();
}