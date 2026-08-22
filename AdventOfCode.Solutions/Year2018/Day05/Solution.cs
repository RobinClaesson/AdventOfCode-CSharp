using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2018.Day05;

[AdventOfCodeSolution(2018, 5)]
public class Solution : IAdventOfCodeSolution
{
    private const int CaseDiff = 'a' - 'A';

    public void Run(string input)
    {
        var reacted = React(input);
        Output.Answer(reacted.Length);

        var shortestImproved = Enumerable.Range('A', 26)
            .Select(i => ((char)i).ToString())
            .Select(c => input.Replace(c, string.Empty).Replace(c.ToLower(), string.Empty))
            .Select(React)
            .Min(s => s.Length);    

        Output.Answer(shortestImproved);
    }

    private static string React(string input)
    {
        var stack = new Stack<char>();

        foreach (var c in input)
        {
            if (stack.Count > 0 && Math.Abs(stack.Peek() - c) == CaseDiff)
                stack.Pop();
            else
                stack.Push(c);
        }

        return stack.Reverse().JoinToString();
    }
}