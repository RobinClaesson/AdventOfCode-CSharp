using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2025.Day01;

[AdventOfCodeSolution(2025, 1)]
public class Solution : IAdventOfCodeSolution
{
    private record Move(bool Right, int Steps);

    public void Run(string input)
    {
        var moves = input.Rows()
            .Select(row => new Move(row[0] == 'R', int.Parse(row[1..])))
            .ToList();
        Output.Answer(GetPassword(moves));

        var expandedMoves = moves.SelectMany(move =>
                Enumerable.Repeat(move with { Steps = 1 }, move.Steps))
            .ToList();
        Output.Answer(GetPassword(expandedMoves));
    }

    private static int PositiveMod(int x, int mod) => (x % mod + mod) % mod;

    private static int GetPassword(List<Move> moves)
    {
        var password = 0;
        var dial = 50;

        foreach (var move in moves)
        {
            dial += move.Right ? move.Steps : -move.Steps;

            dial = PositiveMod(dial, 100);
            if (dial == 0)
                password++;
        }

        return password;
    }
}