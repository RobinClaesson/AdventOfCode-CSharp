using System.CommandLine;
using AdventOfCode;
using AdventOfCode.Commands;

var settings = Settings.LoadSettingsFile();

var rootCommand = new RootCommand("Advent of Code CLI")
{
    new SettingsCommand()
};

var parseResult = rootCommand.Parse(args);
return await parseResult.InvokeAsync();