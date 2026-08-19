using AdventOfCode.Solutions.Extensions;

namespace AdventOfCode.Solutions.Year2016.Day04;

public record Room(string Name, int Id, string Checksum)
{
    public bool IsReal => Name.Replace("-", string.Empty)
        .ToCharArray()
        .Distinct()
        .OrderByDescending(c => Name.Count(c2 => c == c2))
        .ThenBy(c => c)
        .Take(5)
        .JoinToString() == Checksum;

    public Room WithDecryptedName()
    {
        var rotations = Id % 26;

        var chars = Name.ToCharArray()
            .Select(c => c == '-' ? c : (char)(c + rotations))
            .Select(c => c > 'z' ? (char)(c - 26) : c);

        return this with { Name = string.Join("", chars) };
    }
}