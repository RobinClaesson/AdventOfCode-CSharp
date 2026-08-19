using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day10;

[AdventOfCodeSolution(2016, 10)]
public partial class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var bots = GivesRegex().Matches(input)
            .Select(m => new Bot(
                id: int.Parse(m.Groups[1].Value),
                lowId: int.Parse(m.Groups[3].Value),
                highId: int.Parse(m.Groups[5].Value),
                lowTarget: Enum.Parse<Target>(m.Groups[2].Value, true),
                highTarget: Enum.Parse<Target>(m.Groups[4].Value, true)
            ))
            .ToDictionary(b => b.Id, b => b);

        ValueRegex().Matches(input)
            .Select(m => new
            {
                Value = int.Parse(m.Groups[1].Value),
                Bot = int.Parse(m.Groups[2].Value)
            }).ToList()
            .ForEach(v => bots.GetValueOrDefault(v.Bot)?.Chips.Add(v.Value));

        var output = new Dictionary<int, int>();
        var part1 = -1;
        while (bots.Values.Any(b => b.CanAct))
        {
            var active = bots.Values.First(b => b.CanAct);
            GiveBotChip(active.LowTarget, active.LowId, active.Chips.Min());
            GiveBotChip(active.HighTarget, active.HighId, active.Chips.Max());
            active.Chips.Clear();

            if (bots.Values.FirstOrDefault(b => b.IsPartOneBot) is { } part1Bot)
                part1 = part1Bot.Id;
        }

        Output.Answer(part1);
        Output.Answer(output[0] * output[1] * output[2]);
        return;

        void GiveBotChip(Target target, int id, int value)
        {
            switch (target)
            {
                default:
                case Target.Bot:
                    bots.GetValueOrDefault(id)?.Chips.Add(value);
                    break;
                case Target.Output:
                    output[id] = value;
                    break;
            }
        }
    }

    [GeneratedRegex(@"value (\d+) goes to bot (\d+)")]
    private static partial Regex ValueRegex();

    [GeneratedRegex(@"bot (\d+) gives low to (bot|output) (\d+) and high to (bot|output) (\d+)")]
    private static partial Regex GivesRegex();
}