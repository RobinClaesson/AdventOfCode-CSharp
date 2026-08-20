namespace AdventOfCode.Solutions.Year2017.Day13;

public record Layer(int Depth, int Range)
{
    public int Range { get; } = Range;
    public int Severity { get; } = Depth * Range;

    private readonly int _positions = 2 * Range - 2;

    public bool TimedForIntercept(int delay = 0) => (Depth + delay) % _positions == 0;
}