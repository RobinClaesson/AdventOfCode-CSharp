using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day10;

[AdventOfCodeSolution(2017, 10)]
public class Solution : IAdventOfCodeSolution
{
    private const int Size = 256;
    private static readonly int[] ExtraLengths = [17, 31, 73, 47, 23];

    public void Run(string input)
    {
        var lengths = input.SplitAsInt(',');
        var knot = HashKnot(lengths, 1);
        Output.Answer(knot[0] * knot[1]);

        lengths = input.Select(c => (int)c).Concat(ExtraLengths).ToList();
        var hash = HashKnot(lengths, 64)
            .Chunk(16)
            .Select(Xor)
            .Select(x => x.ToString("x2"))
            .JoinToString();
        Output.Answer(hash);
    }

    private static List<int> HashKnot(List<int> lengths, int rounds)
    {
        var numbers = Enumerable.Range(0, Size).ToList();
        var skipSize = 0;
        var position = 0;

        for (var i = 0; i < rounds; i++)
            lengths.ForEach(Twist);

        return numbers;

        void Twist(int length)
        {
            var end = position + length;
            var endIndex = Math.Min(end, Size);
            var concatCount = Math.Max(0, end - endIndex);

            var items = numbers[position..endIndex]
                .Concat(numbers[..concatCount])
                .Reverse()
                .ToList();

            for (var i = 0; i < length; i++)
            {
                numbers[position++] = items[i];
                position %= Size;
            }

            position += skipSize++;
            position %= Size;
        }
    }

    private static int Xor(IEnumerable<int> source) => source.Aggregate(0, (acc, x) => acc ^ x);
}