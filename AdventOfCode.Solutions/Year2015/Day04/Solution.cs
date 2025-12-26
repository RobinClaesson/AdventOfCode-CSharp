using System.Security.Cryptography;
using System.Text;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day04;

[AdventOfCodeSolution(2015, 4)]
public class Solution : IAdventOfCodeSolution
{
    private readonly MD5 _md5Hash = MD5.Create();

    public void Run(string input)
    {
        var i = 0;
        var hash = "";
        do
        {
            i++;
            hash = Md5Hash(input + i);
        } while (hash[..5] != "00000");

        Output.Answer(i);

        do
        {
            i++;
            hash = Md5Hash(input + i);
        } while (hash[..6] != "000000");

        Output.Answer(i);
    }

    private string Md5Hash(string input)
    {
        var hashBytes = _md5Hash.ComputeHash(new UTF8Encoding().GetBytes(input));
        return string.Join(string.Empty, hashBytes.Select(b => b.ToString("x2")));
    }
}