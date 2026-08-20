namespace AdventOfCode.Solutions.Year2016.Day15;

public record Disc(int Id, int Positions, int State)
{
    public bool TimedForCapsule(int delay) => (State + delay + Id) % Positions == 0;
}