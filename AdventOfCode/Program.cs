using System.CommandLine;
using AdventOfCode;
using AdventOfCode.Commands;
using AdventOfCode.InputHandler.Cache;

var settings = Settings.GetSettings();
var inputCache = new FileInputCache(settings.InputCachePath);

var rootCommand = new RootCommand("Advent of Code CLI")
{
    new SettingsCommand(),
    new CacheCommand(inputCache),
};

var parseResult = rootCommand.Parse(args);
return await parseResult.InvokeAsync();