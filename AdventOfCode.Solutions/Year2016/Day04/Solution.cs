using System.Text.RegularExpressions;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day04;

[AdventOfCodeSolution(2016, 4)]
public partial class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var roomRegex = RoomRegex();

        var rooms = input.Rows()
            .Select(s => roomRegex.Match(s))
            .Select(m => new Room(
                Name: m.Groups[1].Value,
                Id: int.Parse(m.Groups[2].Value),
                Checksum: m.Groups[3].Value))
            .ToList();

        Output.Answer(rooms.Where(r => r.IsReal).Sum(r => r.Id));

        var decrypted = rooms.Select(room => room.WithDecryptedName()).ToList();
        Output.Answer(decrypted.First(r => r.Name.Contains("north")).Id);
    }

    [GeneratedRegex(@"([a-z]+(?:-[a-z]+)+)-(\d+)\[(\w+)\]")]
    private static partial Regex RoomRegex();
}