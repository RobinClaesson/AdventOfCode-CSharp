namespace AdventOfCode.Solutions.Year2019;

public record IntCodeInstruction(long Instruction, Opcode Opcode, ParameterMode[] ParameterModes)
{
    public static IntCodeInstruction Parse(long instruction)
    {
        var instructionString = instruction.ToString().PadLeft(5, '0');
        return new IntCodeInstruction(
            Instruction: instruction,
            Opcode: (Opcode)(instruction % 100),
            ParameterModes:
            [
                ParseParameterMode(instructionString[2]),
                ParseParameterMode(instructionString[1]),
                ParseParameterMode(instructionString[0])
            ]
        );
    }

    public static ParameterMode ParseParameterMode(char mode) => mode switch
    {
        '0' => ParameterMode.Position,
        '1' => ParameterMode.Immediate,
        '2' => ParameterMode.Relative,
        _ => throw new Exception($"Unknown parameter mode {mode}")
    };
};

public enum Opcode
{
    Add = 1,
    Multiply = 2,
    Input = 3,
    Output = 4,
    JumpIfTrue = 5,
    JumpIfFalse = 6,
    LessThan = 7,
    Equal = 8,
    AdjustRelativeBase = 9, 
    Halt = 99
}

public enum ParameterMode
{
    Position = 0,
    Immediate = 1,
    Relative = 2
}