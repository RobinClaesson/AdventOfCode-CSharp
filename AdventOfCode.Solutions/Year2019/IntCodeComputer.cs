using System.Reflection.Emit;

namespace AdventOfCode.Solutions.Year2019;

public class IntCodeComputer(List<int> program)
{
    private List<int> _program = program.ToList();

    public int Ptr { get; private set; }
    public bool Halted { get; private set; }
    public int Input { get; set; }
    public int Output { get; private set; }

    public int Noun
    {
        get => _program[1];
        set => _program[1] = value;
    }

    public int Verb
    {
        get => _program[2];
        set => _program[2] = value;
    }

    public int this[int i]
    {
        get => _program[i];
        set => _program[i] = value;
    }

    public int Run()
    {
        while (!Halted)
            Process();

        return _program[0];
    }

    public void Reset()
    {
        Ptr = 0;
        Output = 0;
        Halted = false;
        _program = program.ToList();
    }

    public void Process()
    {
        var instruction = GetCurrentInstruction();
        switch (instruction.Opcode)
        {
            case Opcode.Add:
                Arithmetic(instruction, (a, b) => a + b);
                break;
            case Opcode.Multiply:
                Arithmetic(instruction, (a, b) => a * b);
                break;
            case Opcode.Input:
                LoadInput(instruction);
                break;
            case Opcode.Output:
                OutputValue(instruction);
                break;
            case Opcode.JumpIfTrue:
                JumpIf(instruction, v => v != 0);
                break;
            case Opcode.JumpIfFalse:
                JumpIf(instruction, v => v == 0);
                break;
            case Opcode.LessThan:
                Compare(instruction, (a, b) => a < b);
                break;
            case Opcode.Equal:
                Compare(instruction, (a, b) => a == b);
                break;
            case Opcode.Halt:
                Halted = true;
                break;

            default:
                throw new Exception($"Unknown opcode {instruction.Opcode}");
        }

        if (Ptr >= _program.Count)
        {
            Halted = true;
        }
    }

    private void Arithmetic(IntCodeInstruction instruction, Func<int, int, int> operation)
    {
        var a = GetParameterValue(instruction, 1);
        var b = GetParameterValue(instruction, 2);
        var destination = GetParameter(3);
        _program[destination] = operation(a, b);
        Ptr += 4;
    }

    private void LoadInput(IntCodeInstruction instruction)
    {
        var destination = GetParameter(1);
        _program[destination] = Input;
        Ptr += 2;
    }

    private void OutputValue(IntCodeInstruction instruction)
    {
        Output = GetParameterValue(instruction, 1);
        Ptr += 2;
    }

    private void JumpIf(IntCodeInstruction instruction, Func<int, bool> condition)
    {
        var value = GetParameterValue(instruction, 1);
        if (condition(value))
            Ptr = GetParameterValue(instruction, 2);
        else
            Ptr += 3;
    }

    private void Compare(IntCodeInstruction instruction, Func<int, int, bool> condition)
    {
        var a = GetParameterValue(instruction, 1);
        var b = GetParameterValue(instruction, 2);
        var destination = GetParameter(3);
        _program[destination] = condition(a, b) ? 1 : 0;
        Ptr += 4;
    }

    public IntCodeInstruction GetCurrentInstruction()
    {
        var instruction = _program[Ptr];

        return new IntCodeInstruction(
            Instruction: instruction,
            Opcode: (Opcode)(instruction % 100),
            ParameterModes:
            [
                (ParameterMode)((instruction / 100) & 1),
                (ParameterMode)((instruction / 1000) & 1),
                (ParameterMode)((instruction / 10000) & 1)
            ]
        );
    }

    public int GetParameterValue(IntCodeInstruction instruction, int parameter) =>
        instruction.ParameterModes[parameter - 1] switch
        {
            ParameterMode.Position => _program[_program[Ptr + parameter]],
            ParameterMode.Immediate => _program[Ptr + parameter],
            _ => throw new Exception($"Unknown parameter mode {instruction.ParameterModes[parameter]}")
        };

    public int GetParameter(int parameter) => _program[Ptr + parameter];
}