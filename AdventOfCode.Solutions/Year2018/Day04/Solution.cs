using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2018.Day04;

[AdventOfCodeSolution(2018, 4)]
public partial class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var x = DateTime.Parse("1518-09-17 23:48");
        var guardIdRegex = GuardIdRegex();

        var events = EventRegex().Matches(input)
            .Select(m => new Event
            (
                DateTime.Parse(m.Groups["time"].Value),
                m.Groups["description"].Value
            ))
            .OrderBy(e => e.Time)
            .ToList();

        var sleepingMinutes = guardIdRegex.Matches(input)
            .Select(m => int.Parse(m.Groups["id"].Value))
            .Distinct()
            .ToDictionary(id => id, id => new List<int>());

        var currentGuard = 0;
        for (var i = 0; i < events.Count; i++)
        {
            var currentEvent = events[i];

            var guardIdMatch = guardIdRegex.Match(currentEvent.Description);
            if (guardIdMatch.Success)
            {
                currentGuard = int.Parse(guardIdMatch.Groups["id"].Value);
            }
            else
            {
                var nextEvent = events[++i];
                var length = (int)(nextEvent.Time - currentEvent.Time).TotalMinutes;
                var minutes = Enumerable.Range(currentEvent.Time.Minute, length);
                sleepingMinutes[currentGuard].AddRange(minutes);
            }
        }

        var mostTotalSleep = sleepingMinutes.MaxBy(m => m.Value.Count);
        Output.Answer(mostTotalSleep.Key * mostTotalSleep.Value.MostCommon());

        var mostSleepingOnAMinute = sleepingMinutes
            .Where(kv => kv.Value.Count > 0)
            .Select(kv =>
            (
                id: kv.Key,
                minute: kv.Value.GroupBy(m => m).MaxBy(g => g.Count())!
            )).MaxBy(g => g.minute.Count());
        Output.Answer(mostSleepingOnAMinute.id * mostSleepingOnAMinute.minute.Key);
    }

    private record Event(DateTime Time, string Description);

    [GeneratedRegex(@"\[(?'time'[^\]]+)\] (?'description'.*)")]
    private static partial Regex EventRegex();

    [GeneratedRegex(@"#(?'id'\d+)")]
    private static partial Regex GuardIdRegex();
}