namespace AdventOfCode.Solutions.Year2017.Day18;

public class Program(List<string[]> instructions, ProgramMode mode = ProgramMode.Solo, int id = 0)
{
    private readonly Dictionary<string, long> _registers = mode switch
    {
        ProgramMode.Duo => new Dictionary<string, long> { ["p"] = id, },
        _ => []
    };

    private int _ptr;
    private readonly Queue<long> _queue = [];

    public long? PlayedSound { get; set; }
    public long? ReceivedSound { get; set; }
    public int NumOfOutputs { get; private set; }
    public bool Deadlocked { get; private set; }
    public bool IsRunning => !Deadlocked && _ptr < instructions.Count;
    public Program? Partner { get; set; }

    public void Process()
    {
        if (_ptr >= instructions.Count)
            return;

        var instruction = instructions[_ptr];
        var opp = instruction[0];
        var x = instruction[1];
        var y = instruction.Length > 2 ? instruction[2] : string.Empty;

        switch (opp)
        {
            case "snd":
                if (mode == ProgramMode.Solo)
                {
                    PlayedSound = GetValue(x);
                }
                else
                {
                    Partner?._queue.Enqueue(GetValue(x));
                    NumOfOutputs++;
                }

                _ptr++;
                break;

            case "set":
                _registers[x] = GetValue(y);
                _ptr++;
                break;

            case "add":
                _registers[x] = GetValue(x) + GetValue(y);
                _ptr++;
                break;

            case "mul":
                _registers[x] = GetValue(x) * GetValue(y);
                _ptr++;
                break;

            case "mod":
                _registers[x] = GetValue(x) % GetValue(y);
                _ptr++;
                break;

            case "rcv":
                if (mode == ProgramMode.Solo)
                {
                    if (GetValue(x) != 0)
                        ReceivedSound = PlayedSound;

                    _ptr++;
                }
                else
                {
                    if (_queue.Count > 0)
                    {
                        Deadlocked = false;
                        _registers[x] = _queue.Dequeue();
                        _ptr++;
                    }
                    else
                    {
                        Deadlocked = true;
                    }
                }

                break;

            case "jgz":
                _ptr += GetValue(x) > 0 ? (int)GetValue(y) : 1;
                break;

            default:
                throw new InvalidOperationException($"Unknown instruction: {opp}");
        }
    }

    private long GetValue(string value) => long.TryParse(value, out var number)
        ? number
        : _registers.GetValueOrDefault(value, 0);
}

public enum ProgramMode
{
    Solo,
    Duo
}