using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day07;

[AdventOfCodeSolution(2016, 7)]
public partial class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var ips = input.Rows().Select(s => new Ip(s)).ToList();

        Output.Answer(ips.Count(ip => ip.SupportsTls));
        Output.Answer(ips.Count(ip => ip.SupportsSsl));
    }
}