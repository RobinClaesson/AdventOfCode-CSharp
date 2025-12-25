using System.CommandLine;
using AdventOfCode;
using AdventOfCode.Commands;
using AdventOfCode.InputHandler.Cache;

var settings = Settings.GetSettings();
var inputCache = new FileInputCache(settings.InputCachePath);

var rootCommand = new RootCommand("Advent of Code CLI")
{
    new CacheCommand(inputCache),
    new SettingsCommand(),
};

var parseResult = rootCommand.Parse(args);
return await parseResult.InvokeAsync();