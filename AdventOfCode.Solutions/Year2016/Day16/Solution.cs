using System.Text;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day16;

[AdventOfCodeSolution(2016, 16)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        Output.Answer(Checksum(BuildData(272)));
        Output.Answer(Checksum(BuildData(35651584)));
        return;

        string BuildData(int diskSize)
        {
            var data = input;
            while (data.Length < diskSize)
            {
                data = DragonCurve(data);
            }

            return data[..diskSize];
        }
    }

    private static string DragonCurve(string a)
    {
        var sb = new StringBuilder(a).Append('0');
        for (var i = a.Length - 1; i >= 0; i--)
            sb.Append(a[i] == '0' ? '1' : '0');
        return sb.ToString();
    }

    private static string Checksum(string data)
    {
        var checksum = new string(data);
        while (checksum.Length % 2 == 0)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < checksum.Length; i += 2)
                sb.Append(checksum[i] == checksum[i + 1] ? '1' : '0');

            checksum = sb.ToString();
        }

        return checksum;
    }
}