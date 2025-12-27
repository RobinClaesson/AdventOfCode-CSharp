using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day07;

[AdventOfCodeSolution(2015, 7)]
public class Solution : IAdventOfCodeSolution
{
    private readonly Dictionary<string, ushort> _wires = new();

    public void Run(string input)
    {
        var instructions = input
            .Rows()
            .Where(row => !string.IsNullOrWhiteSpace(row))
            .Select(r => new Instruction(r))
            .ToList();

        RunInstructions(instructions);
        var wireAValue = _wires["a"];
        Output.Answer(wireAValue);

        _wires.Clear();
        _wires["b"] = wireAValue;
        RunInstructions(instructions);

        Output.Answer(_wires["a"]);
    }

    private void RunInstructions(List<Instruction> instructions)
    {
        while (!_wires.ContainsKey("a"))
        {
            instructions
                .Where(i => !_wires.ContainsKey(i.TargetWire))
                .Where(i => i.SourcesWires.All(_wires.ContainsKey))
                .ToList()
                .ForEach(ApplyInstruction);
        }
    }

    private void ApplyInstruction(Instruction instruction)
    {
        _wires[instruction.TargetWire] = instruction.InstructionType switch
        {
            InstructionType.Set => instruction.Constant ?? _wires[instruction.SourcesWires[0]],
            InstructionType.Not => (ushort)~_wires[instruction.SourcesWires[0]],
            InstructionType.LShift => (ushort)(_wires[instruction.SourcesWires[0]] << instruction.Constant!),
            InstructionType.RShift => (ushort)(_wires[instruction.SourcesWires[0]] >> instruction.Constant!),
            InstructionType.And => (ushort)(_wires[instruction.SourcesWires[0]] &
                                            (instruction.Constant ?? _wires[instruction.SourcesWires[1]])),
            InstructionType.Or => (ushort)(_wires[instruction.SourcesWires[0]] |
                                           (instruction.Constant ?? _wires[instruction.SourcesWires[1]])),
            _ => 0
        };
    }
}