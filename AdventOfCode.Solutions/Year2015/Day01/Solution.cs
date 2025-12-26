using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day01;

[AdventOfCodeSolution(2015, 1)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var floor = 0;
        foreach (var c in input)
        {
            if (c == '(')
                floor++;
            else floor--;
        }

        Output.Answer(floor);

        floor = 0;
        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] == '(')
                floor++;
            else floor--;

            if (floor == -1)
            {
                Output.Answer(i + 1);
                break;
            }
        }
    }
}