using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day20;

[AdventOfCodeSolution(2015, 20)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var limit = int.Parse(input);

        var presents = 0;
        var house = 0;
        while (presents < limit)
        {
            house++;
            presents = GetFactors(house).Sum() * 10;
        }

        Output.Answer(house);

        presents = 0;
        house = 0;
        while (presents < limit)
        {
            house++;
            presents = GetFactors(house).Where(f => f * 50 >= house).Sum() * 11;
        }

        Output.Answer(house);
    }

    private static readonly Dictionary<int, List<int>> FactorsCache = [];

    public static List<int> GetFactors(int number)
    {
        if (FactorsCache.TryGetValue(number, out var cachedFactors))
            return cachedFactors;

        var factors = new List<int>();
        var max = (int)Math.Sqrt(number);

        for (var factor = 1; factor <= max; ++factor)
        {
            if (number % factor != 0)
                continue;

            factors.Add(factor);

            var divisor = number / factor;
            if (factor != divisor)
                factors.Add(divisor);
        }

        FactorsCache[number] = factors;
        return factors;
    }
}