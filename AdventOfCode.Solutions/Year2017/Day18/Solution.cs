using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day18;

[AdventOfCodeSolution(2017, 18)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var instructions = input.RowsSplitted(' ')
            .Select(r => r.Select(s => s.Trim()).ToArray())
            .ToList();

        var soloProgram = new Program(instructions);

        while (soloProgram.ReceivedSound is null)
            soloProgram.Process();

        Output.Answer(soloProgram.ReceivedSound);

        var duoProgram0 = new Program(instructions, ProgramMode.Duo, 0);
        var duoProgram1 = new Program(instructions, ProgramMode.Duo, 1);
        duoProgram0.Partner = duoProgram1;
        duoProgram1.Partner = duoProgram0;

        while (duoProgram0.IsRunning || duoProgram1.IsRunning)
        {
            duoProgram0.Process();
            duoProgram1.Process();
        }

        Output.Answer(duoProgram1.NumOfOutputs);
    }
}