using System.CommandLine;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode.Commands;

public partial class SettingsCommand : Command
{
    private static readonly List<PropertyInfo> SettingsProperties = typeof(Settings).GetProperties().ToList();

    public SettingsCommand() : base("settings", "Manage Advent of Code settings")
    {
        var listCommand = new Command("list", "Lists all settings");
        listCommand.SetAction(_ =>
        {
            var settings = Settings.LoadSettingsFile();
            Console.WriteLine("Settings:");
            ListSettings(settings);
        });

        var setCommand = new Command("set", "Sets the given settings");
        var setOptions = SettingsProperties.Select(property =>
                new Option<string>($"--{PascalToKebabCase(property.Name)}")
                {
                    HelpName = property.Name,
                })
            .ToList();
        setOptions.ForEach(setCommand.Add);

        setCommand.SetAction(parseResult =>
        {
            var optionValues = setOptions.ToDictionary(
                option => option.HelpName,
                parseResult.GetValue
            );

            if (optionValues.Values.All(string.IsNullOrWhiteSpace))
            {
                Console.WriteLine("No new settings provided. Too see options, run: ");
                Console.WriteLine($"{AppDomain.CurrentDomain.FriendlyName} {Name} {setCommand.Name} -h");
                return;
            }

            var currentSettings = Settings.LoadSettingsFile();
            SettingsProperties.ForEach(property =>
            {
                var value = optionValues[property.Name];
                if (!string.IsNullOrWhiteSpace(value))
                    property.SetValue(currentSettings, value);
            });
            currentSettings.Save();

            Console.WriteLine("Updated settings:");
            ListSettings(currentSettings);
        });


        Add(listCommand);
        Add(setCommand);
    }

    private static void ListSettings(Settings settings) => SettingsProperties.ForEach(property =>
        Console.WriteLine($"  {property.Name}: '{property.GetValue(settings)}'"));

    [GeneratedRegex(@"([a-z0-9])([A-Z])")]
    private static partial Regex PascalCaseRegex();

    private static string PascalToKebabCase(string pascalCase) =>
        PascalCaseRegex().Replace(pascalCase, "$1-$2").ToLower();
}