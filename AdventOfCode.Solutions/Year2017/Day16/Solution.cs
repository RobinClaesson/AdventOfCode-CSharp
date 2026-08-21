using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2017.Day16;

[AdventOfCodeSolution(2017, 16)]
public class Solution : IAdventOfCodeSolution
{
    private const string ProgramString = "abcdefghijklmnop";
    private const int Rounds = 1000000000;

    public void Run(string input)
    {
        var programs = ProgramString.ToCharArray();
        var seen = new List<string> { ProgramString };

        var moves = input.Split(',').ToList();

        moves.ForEach(DanceMove);
        var state = new string(programs);
        seen.Add(state);
        
        Output.Answer(state);
        
        for (var round = 1; round < Rounds; round++)
        {
            moves.ForEach(DanceMove);

            state = new string(programs);
            if (seen.Contains(state))
            {
                var jump = seen.Count - seen.IndexOf(state);
                round += ((Rounds - 1 - round) / jump) * jump;
            }
            else
            {
                seen.Add(state);
            }
        }

        Output.Answer(new string(programs));
        return;

        void DanceMove(string move)
        {
            switch (move[0])
            {
                default:
                case 's':
                    var x = int.Parse(move[1..]);
                    programs = programs[^x..].Concat(programs[..^x]).ToArray();
                    break;

                case 'x':
                    var exchange = move[1..].Split('/').Select(int.Parse).ToArray();
                    Swap(exchange[0], exchange[1]);
                    break;

                case 'p':
                    var partners = move[1..].Split('/');
                    Swap(programs.IndexOf(partners[0]), programs.IndexOf(partners[1]));
                    break;
            }
        }

        void Swap(int a, int b) => (programs[a], programs[b]) = (programs[b], programs[a]);
    }
}