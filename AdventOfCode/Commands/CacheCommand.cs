using System.CommandLine;
using AdventOfCode.InputHandler.Cache;

namespace AdventOfCode.Commands;

public class CacheCommand : Command
{
    private readonly IInputCache _inputCache;
    public CacheCommand(IInputCache inputCache) : base("cache", "Manage cached inputs")
    {
        _inputCache = inputCache;
        
        var containsCommand = new DateCommand("contains", "Check if cache contains input for given puzzle");
        containsCommand.SetAction(parseResult => ContainsAction(parseResult, containsCommand));

        Add(containsCommand);
    }

    private void ContainsAction(ParseResult parseResult, DateCommand containsCommand)
    {
        
        var year = parseResult.GetValue(containsCommand.YearArgument);
        var day = parseResult.GetValue(containsCommand.DayArgument);
        var cacheContainsInput = _inputCache.HasInput(year, day);
        var message = cacheContainsInput
            ? $"Yes, cache contains input for puzzle {year} / {day}"
            : $"No, cache does not contains input for puzzle {year} / {day}";
        Console.WriteLine(message);
    }
}