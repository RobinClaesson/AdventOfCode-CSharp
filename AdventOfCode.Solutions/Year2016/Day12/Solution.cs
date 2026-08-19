using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day12;

[AdventOfCodeSolution(2016, 12)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var operations = input.RowsSplitted(' ')
            .Select(row => new Operation(row))
            .ToList();

        var registers = new Dictionary<string, int>
        {
            { "a", 0 }, { "b", 0 }, { "c", 0 }, { "d", 0 }
        };

        RunProgram();
        Output.Answer(registers["a"]);

        registers["a"] = 0;
        registers["b"] = 0;
        registers["c"] = 1;
        registers["d"] = 0;
        
        RunProgram();
        Output.Answer(registers["a"]);
        return;

        int GetValue(string operationValue) => int.TryParse(operationValue, out var value)
            ? value
            : registers.GetValueOrDefault(operationValue);

        void RunProgram()
        {
            var ptr = 0;
            while (ptr < operations.Count)
            {
                var operation = operations[ptr];
                switch (operation.Instruction)
                {
                    default:
                    case Instruction.Cpy:
                        registers[operation.Y] = GetValue(operation.X);
                        ptr++;
                        break;
                    case Instruction.Inc:
                        registers[operation.X]++;
                        ptr++;
                        break;
                    case Instruction.Dec:
                        registers[operation.X]--;
                        ptr++;
                        break;
                    case Instruction.Jnz:
                        if (GetValue(operation.X) != 0)
                            ptr += GetValue(operation.Y);
                        else
                            ptr++;
                        break;
                }
            }
        }
    }

    private record Operation(string[] Input)
    {
        public string[] Input { get; init; } = Input;
        public Instruction Instruction { get; } = Enum.Parse<Instruction>(Input[0], true);
        public string X => Input[1];
        public string Y => Input.Length > 2 ? Input[2] : string.Empty;
    }

    private enum Instruction
    {
        Cpy,
        Inc,
        Dec,
        Jnz
    }
}