namespace AdventOfCode.Solutions;

/// <summary>
/// Specifies what Advent of code puzzle a class is a solution for
/// </summary>
/// <param name="year">The year of the puzzle</param>
/// <param name="day">The day of the puzzle</param>
[AttributeUsage(AttributeTargets.Class)]
public class AdventOfCodeSolutionAttribute(int year, int day) : Attribute
{
    /// <summary>
    /// The year of the puzzle
    /// </summary>
    public int Year { get; } = year;
    /// <summary>
    /// The day of the puzzle
    /// </summary>
    public int Day { get; } = day;
}