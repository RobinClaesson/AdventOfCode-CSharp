namespace AdventOfCode.Solutions.Year2019;

public record IntCodeInstruction(int Instruction, Opcode Opcode, ParameterMode[] ParameterModes);

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
    Halt = 99
}

public enum ParameterMode
{
    Position = 0,
    Immediate = 1
}