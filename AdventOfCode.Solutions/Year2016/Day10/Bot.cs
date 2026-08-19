namespace AdventOfCode.Solutions.Year2016.Day10;

public class Bot(int id, int lowId = -1, Target lowTarget = Target.Bot, int highId = -1, Target highTarget = Target.Bot)
{
    public int Id { get; } = id;
    public int LowId { get; } = lowId;
    public Target LowTarget { get; } = lowTarget;
    public int HighId { get; } = highId;
    public Target HighTarget { get; } = highTarget;

    public List<int> Chips { get; } = [];
    public bool CanAct => Chips.Count == 2;
    public bool IsPartOneBot => Chips.Contains(17) && Chips.Contains(61);
}

public enum Target
{
    Bot,
    Output
}