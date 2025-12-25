using System.CommandLine;
using AdventOfCode.InputHandler;
using AdventOfCode.InputHandler.Cache;
using AdventOfCode.Solutions;

namespace AdventOfCode.Commands;

public class SolveCommand : DateCommand
{
    private readonly IInputCache _inputCache;

    public SolveCommand(IInputCache inputCache) : base("solve", "Solve an Advent of Code puzzle")
    {
        _inputCache = inputCache;
        SetAction(SolveAction);
    }

    private async Task SolveAction(ParseResult parseResult)
    {
        var year = GetParsedYear(parseResult);
        var day = GetParsedDay(parseResult);

        if (!SolutionProvider.HasSolution(year, day))
        {
            Console.WriteLine($"No solution implemented for puzzle {year} / {day}");
            return;
        }

        var settings = Settings.GetSettings();
        var aocClient = new AdventOfCodeClient(_inputCache, settings.SessionToken, settings.Contact);
        var input = await aocClient.GetInputAsync(year, day);
        
        var solution = SolutionProvider.GetSolution(year, day);
        solution?.Run(input);
    }
}