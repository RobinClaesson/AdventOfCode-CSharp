using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day09;

[AdventOfCodeSolution(2015, 9)]
public class Solution : IAdventOfCodeSolution
{
    private record Town(string Name, int Index);

    public void Run(string input)
    {
        var towns = input.RowsSplitted(' ')
            .SelectMany(r => new List<string> { r[0], r[2] })
            .Distinct()
            .Select((town, index) => new Town(town, index))
            .ToList();

        var distances = new int[towns.Count, towns.Count];
        towns.Aggregate(input, (acc, t) => acc.Replace(t.Name, t.Index.ToString()))
            .RowsSplitted(' ')
            .Select(r => new List<string> { r[0], r[2], r[4] }.Select(int.Parse).ToArray())
            .ToList()
            .ForEach(r =>
            {
                distances[r[0], r[1]] = r[2];
                distances[r[1], r[0]] = r[2];
            });

        var pathLengths = towns.Select(t => t.Index).ToList()
            .Permutations()
            .Select(CalculatePathDistance).ToList();

        Output.Answer(pathLengths.Min());
        Output.Answer(pathLengths.Max());

        return;

        int CalculatePathDistance(List<int> path)
        {
            var sum = 0;
            for (var i = 0; i < path.Count - 1; i++)
                sum += distances[path[i], path[i + 1]];
            return sum;
        }
    }
}