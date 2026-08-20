using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day08;

[AdventOfCodeSolution(2017, 8)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var registers = new Dictionary<string, int>();
        var maxValue = int.MinValue;

        input.RowsSplitted(' ').ForEach(ApplyInstruction);
        Output.Answer(registers.Values.Max());
        Output.Answer(maxValue);
        return;

        void ApplyInstruction(string[] instruction)
        {
            var conditionRegisterValue = registers.GetValueOrDefault(instruction[4], 0);
            var conditionOperation = instruction[5];
            var conditionValue = int.Parse(instruction[6]);

            if (!CheckCondition(conditionRegisterValue, conditionOperation, conditionValue))
                return;

            var register = instruction[0];
            var value = int.Parse(instruction[2]) * (instruction[1] == "dec" ? -1 : 1);
            registers[register] = registers.GetValueOrDefault(register, 0) + value;

            maxValue = Math.Max(maxValue, registers[register]);
        }

        bool CheckCondition(int registerValue, string operation, int value) => operation switch
        {
            ">" => registerValue > value,
            ">=" => registerValue >= value,
            "<" => registerValue < value,
            "<=" => registerValue <= value,
            "==" => registerValue == value,
            "!=" => registerValue != value,
            _ => throw new Exception($"Invalid operation {operation}")
        };
    }
}