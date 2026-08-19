using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day14;

[AdventOfCodeSolution(2016, 14)]
public partial class Solution : IAdventOfCodeSolution
{
    private static readonly MD5 Md5Hash = MD5.Create();
    private static readonly UTF8Encoding Utf8Encoding = new();

    public void Run(string input)
    {
        var threeNumbers = ThreeRepeatRegex();
        var fiveNumbers = FiveRepeatRegex();

        var part1 = FindIndexOfLastKey(1);
        var part2 = FindIndexOfLastKey(2017);

        Output.Answer(part1);
        Output.Answer(part2);
        return;

        string CreateMd5Hash(int integer, int rounds)
        {
            var hash = $"{input}{integer}";
            for (var i = 0; i < rounds; i++)
            {
                var bytes = Utf8Encoding.GetBytes(hash);
                hash = Md5Hash.ComputeHash(bytes).Select(b => b.ToString("x2")).JoinStrings();
            }

            return hash;
        }

        int FindIndexOfLastKey(int hashRounds)
        {
            var hashes = Enumerable.Range(0, 1000)
                .Select(i => CreateMd5Hash(i, hashRounds))
                .ToList();

            var keyCount = 0;
            var index = -1;
            while (keyCount < 64)
            {
                Output.Log($"Found {keyCount}/64 keys. Index: {index++}");
                hashes.Add(CreateMd5Hash(index + 1000, hashRounds));
                var current = threeNumbers.Match(hashes[index]);

                if (!current.Success)
                    continue;

                var expectedChar = current.Value[0];
                var upcoming = hashes[(index + 1)..(index + 1001)]
                    .SelectMany(h => fiveNumbers.Matches(h))
                    .Any(m => m.Value[0] == expectedChar);

                if (upcoming)
                    keyCount++;
            }

            return index;
        }
    }

    [GeneratedRegex(@"(\w)\1{2}")]
    private static partial Regex ThreeRepeatRegex();

    [GeneratedRegex(@"(\w)\1{4}")]
    private static partial Regex FiveRepeatRegex();
}