using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day15;

[AdventOfCodeSolution(2017, 15)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var generatorStartingPoints = input.RowsSplitted(' ')
            .Select(r => int.Parse(r.Last()))
            .ToList();

        var generatorA = new Generator(16807, generatorStartingPoints[0]);
        var generatorB = new Generator(48271, generatorStartingPoints[1]);

        Output.Answer(FindMatches(40000000));

        generatorA.Reset();
        generatorA.Multiple = 4;
        generatorB.Reset();
        generatorB.Multiple = 8;
        
        Output.Answer(FindMatches(5000000));
        return;

        int FindMatches(int rounds) => Enumerable.Range(0, rounds)
            .Count(_ => generatorA.Next() == generatorB.Next());
    }

    private class Generator(long factor, int start)
    {
        private const long Mod = 2147483647;
        private const int BitMask = 0xFFFF;

        private long _current = start;

        public int? Multiple { get; set; }

        public long Next()
        {
            do
            {
                _current = _current * factor % Mod;
            } while (Multiple.HasValue && _current % Multiple != 0);

            return _current & BitMask;
        }

        public void Reset()
        {
            _current = start;
        }
    }
}