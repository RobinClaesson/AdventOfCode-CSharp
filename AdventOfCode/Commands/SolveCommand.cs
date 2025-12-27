using System.CommandLine;
using AdventOfCode.InputHandler;
using AdventOfCode.InputHandler.Cache;
using AdventOfCode.Solutions;

namespace AdventOfCode.Commands;

public class SolveCommand : DateCommand
{
    private readonly IInputCache _inputCache;

    private readonly Option<string> _inputFileOption = new("--input")
    {
        Description = "Specify input file"
    };

    public SolveCommand(IInputCache inputCache) : base("solve", "Solve an Advent of Code puzzle")
    {
        _inputCache = inputCache;
        Add(_inputFileOption);
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

        var input = await GetInput(parseResult, year, day);
        var solution = SolutionProvider.GetSolution(year, day);
        solution?.Run(input);
    }

    private async Task<string> GetInput(ParseResult parseResult, int year, int day)
    {
        if (parseResult.GetValue(_inputFileOption) is { } inputFilePath)
        {
            return await File.ReadAllTextAsync(inputFilePath);
        }

        var settings = Settings.GetSettings();
        var aocClient = new AdventOfCodeClient(_inputCache, settings.SessionToken, settings.Contact);
        return await aocClient.GetInputAsync(year, day);
    }
}