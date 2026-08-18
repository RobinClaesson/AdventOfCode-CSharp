using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2016.Day07;

public partial record Ip(string Address)
{
    public string Address { get; init; } = Address;
    
    public List<string> Supernets { get; } = _hypernetSplitRegex.Split(Address).ToList();

    public List<string> Hypernets { get; } = _hypernetRegex.Matches(Address)
        .Select(m => m.Groups[1].Value).ToList();
        
    public List<string> SupernetAbas => Supernets.SelectMany(s =>
        _abaRegex.Matches(s).Select(m => m.Groups[1].Value)).ToList();

    public List<string> HypernetAbas => Hypernets.SelectMany(s =>
        _abaRegex.Matches(s).Select(m => m.Groups[1].Value)).ToList();

    public bool SupportsTls => Supernets.Any(s => _abbaRegex.IsMatch(s)) &&
                               !Hypernets.Any(s => _abbaRegex.IsMatch(s));

    public bool SupportsSsl => SupernetAbas.Any(aba => HypernetAbas.Any(bab =>
        aba[0] == bab[1] && aba[1] == bab[0]));

    private static Regex _abbaRegex = AbbaRegex();
    private static Regex _abaRegex = AbaRegex();
    private static Regex _hypernetRegex = HypernetRegex();
    private static Regex _hypernetSplitRegex = HypernetSplitRegex();
    
    [GeneratedRegex(@"([A-Za-z])(?!\1)([A-Za-z])\2\1")]
    private static partial Regex AbbaRegex();

    [GeneratedRegex(@"(?=(([A-Za-z])(?!\2)([A-Za-z])\2))")]
    private static partial Regex AbaRegex();

    [GeneratedRegex(@"\[(\w+)\]")]
    private static partial Regex HypernetRegex();

    [GeneratedRegex(@"\[\w+\]")]
    private static partial Regex HypernetSplitRegex();
}