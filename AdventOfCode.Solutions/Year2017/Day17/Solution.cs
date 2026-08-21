using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day17;

[AdventOfCodeSolution(2017, 17)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var steps = int.Parse(input);
        var numbers = new List<int> { 0 };
        var position = 0;

        for (var i = 1; i <= 2017; i++)
        {
            position += steps;
            position %= numbers.Count;
            numbers.Insert(++position, i);
        }

        Output.Answer(numbers[position + 1]);

        var valueAtZero = 0;
        position = 0;
        for (var i = 1; i <= 50000000; i++)
        {
            position = ((steps + position + i) % i) + 1;

            if (position == 1)
                valueAtZero = i;
        }

        Output.Answer(valueAtZero);
    }
}