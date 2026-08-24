using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2019.Day09;

[AdventOfCodeSolution(2019, 9)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var program = input.SplitAsLong(',');
        var computer = new IntCodeComputer(program)
        {
            Input = 1
        };
        
        computer.Run();
        Output.Answer(computer.Output);
        
        computer.Reset();
        computer.Input = 2;
        
        computer.Run();
        Output.Answer(computer.Output);
    }
}