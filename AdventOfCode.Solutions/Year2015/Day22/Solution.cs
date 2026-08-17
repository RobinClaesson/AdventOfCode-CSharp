using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day22;

[AdventOfCodeSolution(2015, 22)]
public class Solution : IAdventOfCodeSolution
{
    public void Run(string input)
    {
        var rows = input.RowsSplitted(' ');
        var initialGameState = new GameState(
            BossHitPoints: int.Parse(rows[0].Last()),
            BossDamage: int.Parse(rows[1].Last())
        );

        Output.Answer(FindSmallestManaUseToWin(initialGameState));
        Output.Answer(FindSmallestManaUseToWin(initialGameState, 1));
    }

    private static int FindSmallestManaUseToWin(GameState gameState, int playerTurnDamage = 0)
    {
        var queue = new PriorityQueue<GameState, int>();
        queue.Enqueue(gameState, gameState.SpentMana);
        while (queue.Count > 0)
        {
            var currentState = queue.Dequeue().ApplyEffects(playerTurnDamage);

            if (currentState.PlayerIsDead)
                continue;

            if (currentState.BossIsDead)
                return currentState.SpentMana;

            switch (currentState.Attacker)
            {
                default:
                case Attacker.Player:
                    currentState.PossiblePlayerActions().ToList()
                        .ForEach(s => queue.Enqueue(s, s.SpentMana));
                    break;

                case Attacker.Boss:
                    var nextState = currentState.BossAttacks();
                    queue.Enqueue(nextState, nextState.SpentMana);
                    break;
            }
        }

        return int.MaxValue;
    }
}