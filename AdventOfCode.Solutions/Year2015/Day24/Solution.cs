using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day24;

[AdventOfCodeSolution(2015, 24)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        Output.MinLogInterval = TimeSpan.Zero;

        var packages = input.RowsAsLong();
        var totalWeight = packages.Sum();

        FindMinimalQuantumEntanglement(3);
        FindMinimalQuantumEntanglement(4);
        return;

        void FindMinimalQuantumEntanglement(int numOfGroups)
        {
            var groupWeight = totalWeight / numOfGroups;
            var groupSize = packages.Count / numOfGroups;
            
            Output.Log($"Finding subsets of max size {groupSize} with weight {groupWeight}.");
            var possibleGroups = packages.Subsets(groupSize)
                .Where(s => s.Sum() == groupWeight &&
                            packages.Except(s).Sum() == (numOfGroups - 1) * groupWeight)
                .ToList();

            Output.Log($"Found {possibleGroups.Count} possible groups. Finding minimal quantum entanglement.");
            var sortedGroups = possibleGroups.OrderBy(g => g.Count)
                .ThenBy(g => g.Product())
                .ToList();

            Output.Answer(sortedGroups.First().Product());
        }
    }
}