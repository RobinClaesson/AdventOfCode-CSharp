using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day16;

[AdventOfCodeSolution(2015, 16)]
public class Solution : IAdventOfCodeSolution
{
    private static readonly Dictionary<string, int> Expected = new()
    {
        { "children", 3 }, { "cats", 7 }, { "samoyeds", 2 },
        { "pomeranians", 3 }, { "akitas", 0 }, { "vizslas", 0 },
        { "goldfish", 5 }, { "trees", 3 }, { "cars", 2 },
        { "perfumes", 1 },
    };

    public void Run(string input)
    {
        var rows = input.Replace(",", string.Empty)
            .Replace(":", string.Empty)
            .RowsSplitted(' ');

        string part1 = "", part2 = "";
        foreach (var row in rows)
        {
            string key1 = row[2], key2 = row[4], key3 = row[6];
            int value1 = int.Parse(row[3]), value2 = int.Parse(row[5]), value3 = int.Parse(row[7]);

            if (Expected[key1] == value1 && Expected[key2] == value2 && Expected[key3] == value3)
            {
                part1 = row[1];
            }

            if (CheckPart2(key1, value1) && CheckPart2(key2, value2) && CheckPart2(key3, value3))
            {
                part2 = row[1];
            }
        }

        Output.Answer(part1);
        Output.Answer(part2);
    }

    private static bool CheckPart2(string key, int value) => key switch
    {
        "cats" or "trees" => value > Expected[key],
        "pomeranians" or "goldfish" => value < Expected[key],
        _ => Expected[key] == value
    };
}