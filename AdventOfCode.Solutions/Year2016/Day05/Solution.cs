using System.Security.Cryptography;
using System.Text;
using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2016.Day05;

[AdventOfCodeSolution(2016, 5)]
public class Solution : IAdventOfCodeSolution
{
    public const int PasswordLength = 8;
    private const string LeadingZeroes = "00000";
    private static readonly MD5 Md5Hash = MD5.Create();
    private static readonly UTF8Encoding Utf8Encoding = new UTF8Encoding();

    public void Run(string input)
    {
        var password1 = string.Empty;
        var password2 = new char[PasswordLength];
        for (var salt = 0; password2.Contains('\0'); salt++)
        {
            var hash = CreateMd5Hash(salt);
            if (hash[..5] != LeadingZeroes)
                continue;

            if (password1.Length < PasswordLength)
                password1 += hash[5];

            if(hash[5] is < '0' or > '7')
                continue;
            
            var index = int.Parse(hash[5].ToString());
            if (password2[index] != '\0')
                continue;

            password2[index] = hash[6];
            Output.Log($"P1: {password1.Length}/{PasswordLength} | P2: {password2.Count(c => c != '\0')}/{PasswordLength}");
        }

        Output.Answer(password1);
        Output.Answer(password2.JoinChars());
        return;

        string CreateMd5Hash(int salt)
        {
            var bytes = Utf8Encoding.GetBytes($"{input}{salt}");
            var hash = Md5Hash.ComputeHash(bytes)
                .Select(b => b.ToString("x2"));

            return string.Join(string.Empty, hash);
        }
    }
}