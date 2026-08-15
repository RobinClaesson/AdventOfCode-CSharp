using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day14;

[AdventOfCodeSolution(2015, 14)]
public class Solution : IAdventOfCodeSolution
{
    private record Reindeer(string Name, int Speed, int Stamina, int Rest);
    public void Run(string input)
    {
        var reindeers = input.RowsSplitted(' ')
            .Select(r => new Reindeer(
                Name: r[0],
                Speed: int.Parse(r[3]),
                Stamina: int.Parse(r[6]),
                Rest: int.Parse(r[13])))
            .ToList();

        Output.Answer(reindeers.Max(r => CalculateDistanceAfterXSeconds(r, 2503)));

        // If it looks stupid but works
        var scores = reindeers.Select(_ => 0).ToList();
        for (var seconds = 1; seconds <= 2503; seconds++)
        {
            var states = reindeers.Select(r => CalculateDistanceAfterXSeconds(r, seconds)).ToList();
            var leaderDistance = states.Max();
            for (var i = 0; i < states.Count; i++)
            {
                if(states[i] == leaderDistance)
                    scores[i]++;
            }
        }
        
        Output.Answer(scores.Max());
    }

    private static int CalculateDistanceAfterXSeconds(Reindeer reindeer, int seconds)
    {
        var distance = 0;
        var remaining = seconds;
        
        while (remaining > 0)
        {
            var time = Math.Min(remaining, reindeer.Stamina);
            distance += time * reindeer.Speed;
            remaining -= time;
            time = Math.Min(remaining, reindeer.Rest);
            remaining -= time;
        }
        
        return distance;
    }
}