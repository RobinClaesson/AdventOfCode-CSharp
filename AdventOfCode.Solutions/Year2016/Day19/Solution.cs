using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day19;

[AdventOfCodeSolution(2016, 19)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var numOfElves = int.Parse(input);

        var current = CreateElves(numOfElves)[0];
        while (current.Next != current)
        {
            current.Next.Remove();
            current = current.Next;
        }
        
        Output.Answer(current.Number);

        var elves = CreateElves(numOfElves);
        current = elves[0];
        var remainingElfCount = elves.Count;
        var opposite = elves[remainingElfCount / 2];

        //We know from the example that when we have 5 left the elf after the starting point wins
        //This removes handling edge cases for small circles  
        while (remainingElfCount > 5)
        {
            var nextOpposite = remainingElfCount % 2 == 0
                ? opposite.Next
                : opposite.Next.Next;

            opposite.Remove();
            opposite = nextOpposite;
            remainingElfCount--;

            current = current.Next;
        }

        Output.Answer(current.Next.Number);
    }

    private static List<Elf> CreateElves(int count)
    {
        var elves = Enumerable.Range(1, count)
            .Select(i => new Elf(i))
            .ToList();

        elves.ForEach(e =>
        {
            var index = e.Number - 1;
            e.Next = elves[(index + 1) % count];
            e.Previous = elves[(index - 1 + count) % count];
        });

        return elves;
    }
}