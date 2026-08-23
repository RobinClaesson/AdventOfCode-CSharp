using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2019.Day02;

[AdventOfCodeSolution(2019, 2)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var program = input.SplitAsInt(',');
        var computer = new IntCodeComputer(program)
        {
            Noun = 12,
            Verb = 2
        };

        Output.Answer(computer.Run());

        var pair = Enumerable.Range(0, 100)
            .SelectMany(noun => Enumerable.Range(0, 100).Select(verb => (noun, verb)))
            .FirstOrDefault(p =>
            {
                computer.Reset();
                computer.Noun = p.noun;
                computer.Verb = p.verb;
                return computer.Run() == 19690720;
            });
        
        Output.Answer(100 * pair.noun + pair.verb);
    }
}