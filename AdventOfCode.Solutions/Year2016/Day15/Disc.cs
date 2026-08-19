namespace AdventOfCode.Solutions.Year2016.Day15;

public class Disc(int id, int positions, int state)
{
    public int Id { get; } = id;
    public int Positions { get; } = positions;
    public int State { get; private set; } = state;
    public int InitialState { get; } = state;

    public bool TimedForCapsule => (State + Id) % Positions == 0;

    public void Tick()
    {
        State = (State + 1) % Positions;
    }

    public void Reset()
    {
        State = InitialState;
    }
}