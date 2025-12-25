using System.CommandLine;

namespace AdventOfCode.Commands;

public abstract class DateCommand : Command
{
    protected readonly Argument<int> YearArgument = new("year")
    {
        Description = "The year of the puzzle"
    };

    protected readonly Argument<int> DayArgument = new("day")
    {
        Description = "The day of the puzzle"
    };

    protected DateCommand(string name, string description) : base(name, description)
    {
        Add(YearArgument);
        Add(DayArgument);
    }
}