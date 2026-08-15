using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day13;

[AdventOfCodeSolution(2015, 13)]
public partial class Solution : IAdventOfCodeSolution
{
    private record Guest(string Name, int Index);

    public void Run(string input)
    {
        var guests = input.RowsSplitted(' ')
            .Select(r => r[0])
            .Distinct()
            .Select((name, index) => new Guest(name, index))
            .ToList();

        var affections = new int[guests.Count + 1, guests.Count + 1];

        guests.Aggregate(input.Replace(".", string.Empty),
                (acc, t) => acc.Replace(t.Name, t.Index.ToString()))
            .RowsSplitted(' ')
            .ForEach(row =>
            {
                var guest = int.Parse(row.First());
                var target = int.Parse(row.Last());
                var multiplier = row[2] == "gain" ? 1 : -1;
                var affection = int.Parse(row[3]);

                affections[guest, target] = affection * multiplier;
            });

        // Can be optimized since [1, 2, 3] = [2, 3, 1]
        var seatingsPart1 = guests.Select(g => g.Index).Permutations();
        Output.Answer(seatingsPart1.Max(CalculateSeatingHappiness));

        var seatingsPart2 = guests.Select(g => g.Index).Append(guests.Count).Permutations();
        Output.Answer(seatingsPart2.Max(CalculateSeatingHappiness));

        return;

        int CalculateSeatingHappiness(List<int> seating)
        {
            var happiness = 0;
            for (var i = 0; i < seating.Count - 1; i++)
            {
                happiness += affections[seating[i], seating[i + 1]];
                happiness += affections[seating[i + 1], seating[i]];
            }

            happiness += affections[seating.First(), seating.Last()];
            happiness += affections[seating.Last(), seating.First()];

            return happiness;
        }
    }
}