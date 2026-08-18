using System.CommandLine;
using System.Text;
using AdventOfCode.Solutions;

namespace AdventOfCode.Commands;

public class PrintCommand : Command
{
    public PrintCommand() : base("print", "Prints information about the solutions")
    {
        var starsCommand = new Command("table", "Print markdown table of collected stars");
        starsCommand.SetAction(_ => PrintTableOfCollectedStars());
        Add(starsCommand);
    }

    private static void PrintTableOfCollectedStars()
    {
        var collectedStars = SolutionProvider.GetCollectedStars();

        var years = collectedStars.Keys
            .Select(k => k.Year)
            .Distinct()
            .Order()
            .ToList();

        var sb = new StringBuilder("|  Day | ")
            .Append(string.Join(" | ", years))
            .AppendLine(" |  All |").Append('|');

        Enumerable.Repeat("------|", years.Count + 2).ToList()
            .ForEach(s => sb.Append(s));
        sb.AppendLine();

        for (var day = 1; day <= 25; day++)
        {
            var starsOnDay = years
                .Select(year => collectedStars.GetValueOrDefault((year, day), 0))
                .ToList();
            var starStrings = starsOnDay
                .Select(n => $"{new string('*', n),4}")
                .ToList();

            sb.Append($"|   {day,2} | ")
                .Append(string.Join(" | ", starStrings))
                .AppendLine($" | {starsOnDay.Sum(),4} |");
        }

        var totals = collectedStars.GroupBy(kv => kv.Key.Year)
            .OrderBy(g => g.Key)
            .Select(g => g.Sum(kv => kv.Value))
            .ToList();

        sb.Append($"|  tot | ")
            .Append(string.Join(" | ", totals.Select(s => $"{s,4}")))
            .AppendLine($" | {totals.Sum(),4} |");

        Console.WriteLine(sb.ToString());
    }
}