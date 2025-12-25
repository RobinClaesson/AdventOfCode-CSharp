namespace AdventOfCode.Solutions;

[AttributeUsage(AttributeTargets.Class)]
public class AdventOfCodeSolutionAttribute(int year, int day) : Attribute
{
    public int Year { get; } = year;
    public int Day { get; } = day;
}