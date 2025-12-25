namespace AdventOfCode.Solutions.Extensions;

public static class InputExtensions
{
    extension(string input)
    {
        public List<int> AllCharsAsInts() => input
            .Select(c => c - '0')
            .ToList();

        public List<string> Rows() => input
            .Split(Environment.NewLine)
            .ToList();

        public List<int> RowsAsInt() => input
            .Rows()
            .Select(int.Parse)
            .ToList();

        public List<List<int>> IntGrid() => input
            .Rows()
            .Select(r => r.Select(c => int.Parse($"{c}")).ToList())
            .ToList();

        public List<string[]> RowsSplitted(char separator) => input
            .Trim()
            .Rows()
            .Select(s => s.Split(separator))
            .ToList();

        public List<string[]> RowsSplitted(string separator) => input
            .Trim()
            .Rows()
            .Select(s => s.Split(separator))
            .ToList();

        public List<int[]> RowsSplittedAsInt(char separator) => input
            .RowsSplitted(separator)
            .Select(s => s.Select(int.Parse).ToArray())
            .ToList();

        public List<int[]> RowsSplittedAsInt(string separator) => input
            .RowsSplitted(separator)
            .Select(s => s.Select(int.Parse).ToArray())
            .ToList();

        public List<long[]> RowsSplittedAsLong(char separator) => input
            .RowsSplitted(separator)
            .Select(s => s.Select(long.Parse).ToArray())
            .ToList();

        public List<long[]> RowsSplittedAsLong(string separator) => input
            .RowsSplitted(separator)
            .Select(s => s.Select(long.Parse).ToArray())
            .ToList();

        public List<int> SplitAsInt(char separator) => input
            .Split(separator, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
    }
}