using System.Text;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day18;

[AdventOfCodeSolution(2016, 18)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var rows = new List<string> { $".{input}." };
        
        while (rows.Count < 40)
            rows.Add(GetNextRow(rows.Last()));
        Output.Answer(CountSafe(rows));
        
        while (rows.Count < 400000)
            rows.Add(GetNextRow(rows.Last()));
        Output.Answer(CountSafe(rows));
    }

    private static string GetNextRow(string row)
    {
        var sb = new StringBuilder(".");
        for (var i = 1; i < row.Length - 1; i++)
            sb.Append(IsTrap(i) ? '^' : '.');
        return sb.Append('.').ToString();

        bool IsTrap(int index) => (row[index - 1] == '^' && row[index + 1] == '.') ||
                                  (row[index - 1] == '.' && row[index + 1] == '^');
    }

    private static int CountSafe(List<string> rows) => rows.Sum(r => r.AsSpan()[1..^1].Count('.'));
}