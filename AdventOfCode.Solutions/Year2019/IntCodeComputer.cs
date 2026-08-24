namespace AdventOfCode.Solutions.Year2019;

public class IntCodeComputer(List<long> program)
{
    public IntCodeComputer(List<int> program) : this(program.Select(i => (long)i).ToList())
    {
    }

    private Dictionary<long, long> _program = ResetProgram(program);

    public long Ptr { get; private set; }
    public long RelativeBase { get; set; }
    public bool Halted { get; private set; }
    public long Input { get; set; }
    public long Output { get; private set; }
    public List<long> Outputs { get; private set; } = [];

    public long Noun
    {
        get => _program[1];
        set => _program[1] = value;
    }

    public long Verb
    {
        get => _program[2];
        set => _program[2] = value;
    }

    public long this[int i]
    {
        get => _program[i];
        set => _program[i] = value;
    }

    public long Run()
    {
        while (!Halted)
            Process();

        return _program[0];
    }

    public void Reset()
    {
        Ptr = 0;
        RelativeBase = 0;
        Output = 0;
        Outputs = [];
        Halted = false;
        _program = ResetProgram(program);
    }

    private static Dictionary<long, long> ResetProgram(List<long> program) => program
        .Select((instruction, position) => (instruction, position))
        .ToDictionary(x => (long)x.position, x => (long)x.instruction);

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
            case Opcode.AdjustRelativeBase:
                AdjustRelativeBase(instruction);
                break;
            case Opcode.Halt:
                Halted = true;
                break;

            default:
                throw new Exception($"Unknown opcode {instruction.Opcode}");
        }
    }

    private void Arithmetic(IntCodeInstruction instruction, Func<long, long, long> operation)
    {
        var a = GetParameterValue(instruction, 1);
        var b = GetParameterValue(instruction, 2);
        var destination = GetParameterAddress(instruction, 3);

        _program[destination] = operation(a, b);
        Ptr += 4;
    }

    private void LoadInput(IntCodeInstruction instruction)
    {
        var destination = GetParameterAddress(instruction, 1);

        _program[destination] = Input;
        Ptr += 2;
    }

    private void OutputValue(IntCodeInstruction instruction)
    {
        Output = GetParameterValue(instruction, 1);
        Outputs.Add(Output);
        Ptr += 2;
    }

    private void JumpIf(IntCodeInstruction instruction, Func<long, bool> condition)
    {
        var value = GetParameterValue(instruction, 1);
        if (condition(value))
            Ptr = GetParameterValue(instruction, 2);
        else
            Ptr += 3;
    }

    private void Compare(IntCodeInstruction instruction, Func<long, long, bool> condition)
    {
        var a = GetParameterValue(instruction, 1);
        var b = GetParameterValue(instruction, 2);
        var destination = GetParameterAddress(instruction, 3);

        _program[destination] = condition(a, b) ? 1 : 0;
        Ptr += 4;
    }

    private void AdjustRelativeBase(IntCodeInstruction instruction)
    {
        RelativeBase += GetParameterValue(instruction, 1);
        Ptr += 2;
    }

    public IntCodeInstruction GetCurrentInstruction() => IntCodeInstruction.Parse(_program[Ptr]);


    public long GetParameterValue(IntCodeInstruction instruction, int parameter) =>
        instruction.ParameterModes[parameter - 1] switch
        {
            ParameterMode.Position or ParameterMode.Relative =>
                GetProgramValue(GetParameterAddress(instruction, parameter)),
            ParameterMode.Immediate => GetProgramValue(Ptr + parameter),

            _ => throw new Exception($"Unknown parameter mode {instruction.ParameterModes[parameter - 1]}")
        };

    public long GetParameterAddress(IntCodeInstruction instruction, int parameter) =>
        instruction.ParameterModes[parameter - 1] switch
        {
            ParameterMode.Position => GetParameter(parameter),
            ParameterMode.Relative => RelativeBase + GetParameter(parameter),

            _ => throw new Exception(
                $"Invalid write parameter mode: {instruction.ParameterModes[parameter - 1]}")
        };

    public long GetParameter(int parameter) => _program.GetValueOrDefault(Ptr + parameter, 0);

    public long GetProgramValue(long position) => _program.GetValueOrDefault(position, 0);
}