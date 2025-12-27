namespace AdventOfCode.Solutions.Extensions;

/// <summary>
/// string extensions for parsing common inputs
/// </summary>
public static class InputExtensions
{
    extension(string input)
    {
        /// <summary>
        /// Get all individual numbers in an input
        /// <code>"1234" => [1, 2, 3, 4]</code>
        /// </summary>
        /// <returns>List of all numbers in the input</returns>
        public List<int> AllCharsAsInts() => input
            .Select(c => c - '0')
            .ToList();

        /// <summary>
        /// Split input by <see cref="Environment.NewLine"/>
        /// <code>"qwer\nasdf\nzxcv" => ["qwer", "asdf", "zxcv"]</code>
        /// </summary>
        /// <returns>List of all rows in the input</returns>
        public List<string> Rows() => input
            .Split(Environment.NewLine)
            .ToList();

        /// <summary>
        /// Split input by <see cref="Environment.NewLine"/> then parse each row as int
        /// <code>"123\n456\n789" => [123, 456, 789]</code>
        /// </summary>
        /// <returns>List of all rows in the input as int</returns>
        public List<int> RowsAsInt() => input
            .Rows()
            .Select(int.Parse)
            .ToList();

        /// <summary>
        /// Split input by <see cref="Environment.NewLine"/> then get all individual numbers in each row
        /// <code>"123\n456\n789" => [[1, 2, 3], [4, 5, 6], [7, 8, 9]]</code>
        /// </summary>
        /// <returns>List of lists of all numbers in each row of the input</returns>
        public List<List<int>> IntGrid() => input
            .Rows()
            .Select(r => r.AllCharsAsInts())
            .ToList();

        /// <summary>
        /// Split input by <see cref="Environment.NewLine"/> then split each row by the given separator
        /// </summary>
        /// <param name="separator">character to split the rows by</param>
        /// <returns>List of string arrays from splitting the rows of the input</returns>
        public List<string[]> RowsSplitted(char separator) => input
            .Trim()
            .Rows()
            .Select(s => s.Split(separator))
            .ToList();

        /// <summary>
        /// Split input by <see cref="Environment.NewLine"/> then split each row by the given separator
        /// </summary>
        /// <param name="separator">string to split the rows by</param>
        /// <returns>List of string arrays from splitting the rows of the input</returns>
        public List<string[]> RowsSplitted(string separator) => input
            .Trim()
            .Rows()
            .Select(s => s.Split(separator))
            .ToList();

        /// <summary>
        /// Split input by <see cref="Environment.NewLine"/> then split each row by the given separator and parses each entry as int
        /// </summary>
        /// <param name="separator">character to split the rows by</param>
        /// <returns>List of int arrays from splitting the rows of the input then parsing them as int</returns>
        public List<int[]> RowsSplittedAsInt(char separator) => input
            .RowsSplitted(separator)
            .Select(s => s.Select(int.Parse).ToArray())
            .ToList();

        /// <summary>
        /// Split input by <see cref="Environment.NewLine"/> then split each row by the given separator and parses each entry as int
        /// </summary>
        /// <param name="separator">string to split the rows by</param>
        /// <returns>List of int arrays from splitting the rows of the input then parsing them as int</returns>
        public List<int[]> RowsSplittedAsInt(string separator) => input
            .RowsSplitted(separator)
            .Select(s => s.Select(int.Parse).ToArray())
            .ToList();

        /// <summary>
        /// Split input by <see cref="Environment.NewLine"/> then split each row by the given separator and parses each entry as long
        /// </summary>
        /// <param name="separator">character to split the rows by</param>
        /// <returns>List of int arrays from splitting the rows of the input then parsing them as long</returns>
        public List<long[]> RowsSplittedAsLong(char separator) => input
            .RowsSplitted(separator)
            .Select(s => s.Select(long.Parse).ToArray())
            .ToList();

        /// <summary>
        /// Split input by <see cref="Environment.NewLine"/> then split each row by the given separator and parses each entry as long
        /// </summary>
        /// <param name="separator">string to split the rows by</param>
        /// <returns>List of int arrays from splitting the rows of the input then parsing them as long</returns>
        public List<long[]> RowsSplittedAsLong(string separator) => input
            .RowsSplitted(separator)
            .Select(s => s.Select(long.Parse).ToArray())
            .ToList();

        /// <summary>
        /// Split input by the given separator and parses each entry as int
        /// </summary>
        /// <param name="separator">character to split the rows by</param>
        /// <returns>List of ints from splitting the input then parsing them as int</returns>
        public List<int> SplitAsInt(char separator) => input
            .Split(separator, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
    }
}