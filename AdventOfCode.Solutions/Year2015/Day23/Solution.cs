using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day23;

[AdventOfCodeSolution(2015, 23)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var program = input.Replace(",", string.Empty).RowsSplitted(' ');

        var ptr = 0;
        var registers = new Dictionary<string, uint>()
        {
            ["a"] = 0,
            ["b"] = 0
        };

        while (ptr < program.Count)
            ExecuteInstruction();
        
        Output.Answer(registers["b"]);

        ptr = 0;
        registers["a"] = 1;
        registers["b"] = 0;
        
        while (ptr < program.Count)
            ExecuteInstruction();
        
        Output.Answer(registers["b"]);
        return;

        void ExecuteInstruction()
        {
            var instruction = program[ptr];
            var r = instruction[1];
            
            switch (instruction[0])
            {
                case "hlf":
                    registers[r] /= 2;
                    ptr++;
                    break;
                case "tpl":
                    registers[r] *= 3;
                    ptr++;
                    break;
                case "inc":
                    registers[r]++;
                    ptr++;
                    break;
                case "jmp":
                    ptr += int.Parse(r);
                    break;
                case "jie":
                    ptr += registers[r] % 2 == 0 ? int.Parse(instruction[2]) : 1;
                    break;
                case "jio":
                    ptr += registers[r] == 1 ? int.Parse(instruction[2]) : 1;
                    break;
            }
        }
    }
}