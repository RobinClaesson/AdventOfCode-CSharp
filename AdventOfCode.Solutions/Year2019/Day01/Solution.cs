using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2019.Day01;

[AdventOfCodeSolution(2019, 1)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var rows = input.RowsAsInt();
        
        Output.Answer(rows.Select(CalcFuel).Sum()); 
        Output.Answer(rows.Select(CalcRocketEquation).Sum()); 
    }

    private static int CalcFuel(int mass) => (mass / 3) - 2;

    private static int CalcRocketEquation(int mass)
    {
        var sum = 0;
        while (mass > 0)
        {
            var fuel = Math.Max(0, CalcFuel(mass));
            sum += fuel;
            mass = fuel;
        }
        return sum;
    }
}