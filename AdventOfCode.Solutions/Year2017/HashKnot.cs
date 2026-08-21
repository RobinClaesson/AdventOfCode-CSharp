using AdventOfCode.Solutions.Extensions;

namespace AdventOfCode.Solutions.Year2017;

public static class HashKnot
{
    public static int Size { get; set; } = 256;
    private static readonly int[] ExtraLengths = [17, 31, 73, 47, 23];

    public static string GetHash(string input, HashOutputType outputType = HashOutputType.Hexadecimal) =>
        GetHash(ConvertString(input), outputType);

    public static List<int> GetKnot(string input, int rounds = 64) => GetKnot(ConvertString(input), rounds);

    public static string GetHash(List<int> lengths, HashOutputType outputType = HashOutputType.Hexadecimal) =>
        GetKnot(lengths)
            .Chunk(16)
            .Select(Xor)
            .Select(x => x.ToString(HashFormat(outputType)))
            .JoinToString();

    public static List<int> GetKnot(List<int> lengths, int rounds = 64)
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

    public static List<int> ConvertString(string input) => input
        .Select(c => (int)c)
        .Concat(ExtraLengths)
        .ToList();

    private static int Xor(IEnumerable<int> source) => source.Aggregate(0, (acc, x) => acc ^ x);

    private static string HashFormat(HashOutputType type) => type switch
    {
        HashOutputType.Hexadecimal => "x2",
        HashOutputType.Binary => "b8",
        _ => string.Empty
    };
}

public enum HashOutputType
{
    Hexadecimal,
    Binary
}