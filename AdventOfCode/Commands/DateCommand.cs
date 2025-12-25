using System.CommandLine;

namespace AdventOfCode.Commands;

public class DateCommand : Command
{
    public Argument<int> YearArgument { get; } = new("year")
    {
        Description = "The year of the puzzle"
    };

    public Argument<int> DayArgument { get; } = new("day")
    {
        Description = "The day of the puzzle"
    };

    public DateCommand(string name, string description) : base(name, description)
    {
        Add(YearArgument);
        Add(DayArgument);
    }
}