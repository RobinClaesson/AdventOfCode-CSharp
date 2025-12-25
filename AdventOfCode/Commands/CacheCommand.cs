using System.CommandLine;
using AdventOfCode.InputHandler;
using AdventOfCode.InputHandler.Cache;

namespace AdventOfCode.Commands;

public class CacheCommand : Command
{
    private readonly IInputCache _inputCache;

    private readonly DateCommand _containsCommand = new("contains", "Check if cache contains input for given puzzle");
    private readonly DateCommand _fetchCommand = new("fetch", "Force fetch input for give puzzle to cache");

    public CacheCommand(IInputCache inputCache) : base("cache", "Manage cached inputs")
    {
        _inputCache = inputCache;

        _containsCommand.SetAction(CacheContainsAction);
        _fetchCommand.SetAction(CacheFetchAction);

        Add(_containsCommand);
        Add(_fetchCommand);
    }

    private void CacheContainsAction(ParseResult parseResult)
    {
        var year = _containsCommand.GetParsedYear(parseResult);
        var day = _containsCommand.GetParsedDay(parseResult);
        
        var cacheContainsInput = _inputCache.HasInput(year, day);
        var message = cacheContainsInput
            ? $"Yes, cache contains input for puzzle {year} / {day}"
            : $"No, cache does not contains input for puzzle {year} / {day}";
        Console.WriteLine(message);
    }

    private async Task CacheFetchAction(ParseResult parseResult)
    {
        if (!AdventOfCodeClient.CanMakeRequest())
        {
            var lastRequest = AdventOfCodeClient.GetLastRequest();
            Console.WriteLine(
                $"Request throttled! Can not make Advent of Code request until UTC {lastRequest!.NextRequestAllowedAt}");
        }

        var year = _fetchCommand.GetParsedYear(parseResult);
        var day = _fetchCommand.GetParsedDay(parseResult);

        var settings = Settings.GetSettings();
        var client = new AdventOfCodeClient(_inputCache, settings.SessionToken, settings.Contact);
        await client.GetInputAsync(year, day, force: true);
        Console.WriteLine($"Input for puzzle {year} / {day} has been cached");
    }
}