namespace AdventOfCode.Solutions.Year2019;

public class IntCodeComputer(List<int> program)
{
    private List<int> _program = program.ToList();

    public int Ptr { get; private set; }
    public bool Halted { get; private set; }

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
        Halted = false;
        _program = program.ToList();
    }

    public void Process()
    {
        switch (_program[Ptr])
        {
            case 1:
                Arithmetic((a, b) => a + b);
                break;
            case 2:
                Arithmetic((a, b) => a * b);
                break;
            case 99:
                Halted = true;
                break;

            default:
                throw new Exception($"Unknown opcode {_program[Ptr]}");
        }

        if (Ptr >= _program.Count)
        {
            Halted = true;
        }
    }

    private void Arithmetic(Func<int, int, int> operation)
    {
        var info = GetOperationInfo();
        _program[info.Destination] = operation(info.ValueA, info.ValueB);
        Ptr += 4;
    }

    private OperationInfo GetOperationInfo() => new(
        ValueA: _program[_program[Ptr + 1]],
        ValueB: _program[_program[Ptr + 2]],
        Destination: _program[Ptr + 3]
    );

    private record OperationInfo(int ValueA, int ValueB, int Destination);
}