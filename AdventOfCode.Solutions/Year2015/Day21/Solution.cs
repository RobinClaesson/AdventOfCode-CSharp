using AdventOfCode.Solutions.Extensions;
using AdventOfCode.Solutions.IO;

namespace AdventOfCode.Solutions.Year2015.Day21;

[AdventOfCodeSolution(2015, 21)]
public class Solution : IAdventOfCodeSolution
{
    private record Stats(int Damage = 0, int Armor = 0, int HitPoints = 100, int ItemCost = 0)
    {
        public static Stats Sum(params List<Stats> stats) => new(
            Damage: stats.Sum(s => s.Damage),
            Armor: stats.Sum(s => s.Armor),
            ItemCost: stats.Sum(s => s.ItemCost)
        );
    };

    private enum Attacker
    {
        Player,
        Boss
    }

    public void Run(string input)
    {
        var rows = input.RowsSplitted(' ');
        var bossStats = new Stats
        (
            Damage: int.Parse(rows[1].Last()),
            Armor: int.Parse(rows[2].Last()),
            HitPoints: int.Parse(rows[0].Last())
        );

        var possiblePlayerStats = WeaponStats.SelectMany(weapon =>
            ArmorStats.SelectMany(armor =>
                RingStats.SelectMany(ring1 =>
                    RingStats.Where(ring2 => ring1 != ring2)
                        .Select(ring2 => Stats.Sum(weapon, armor, ring1, ring2))))).ToList();

        Output.Answer(possiblePlayerStats
            .OrderBy(s => s.ItemCost)
            .First(s => PlayerWinsFight(bossStats, s))
            .ItemCost);
        Output.Answer(possiblePlayerStats
            .OrderByDescending(s => s.ItemCost)
            .First(s => !PlayerWinsFight(bossStats, s))
            .ItemCost);
    }

    private static bool PlayerWinsFight(Stats bossStats, Stats playerStats)
    {
        var bossHp = bossStats.HitPoints;
        var playerHp = playerStats.HitPoints;
        var attacker = Attacker.Player;

        while (bossHp > 0 && playerHp > 0)
        {
            if (attacker == Attacker.Player)
            {
                bossHp -= Math.Max(1, playerStats.Damage - bossStats.Armor);
                attacker = Attacker.Boss;
            }
            else
            {
                playerHp -= Math.Max(1, bossStats.Damage - playerStats.Armor);
                attacker = Attacker.Player;
            }
        }

        return playerHp > 0;
    }

    private static readonly List<Stats> WeaponStats =
    [
        new(Damage: 4, ItemCost: 8),
        new(Damage: 5, ItemCost: 10),
        new(Damage: 6, ItemCost: 25),
        new(Damage: 7, ItemCost: 40),
        new(Damage: 8, ItemCost: 74),
    ];

    private static readonly List<Stats> ArmorStats =
    [
        new(),
        new(Armor: 1, ItemCost: 13),
        new(Armor: 2, ItemCost: 31),
        new(Armor: 3, ItemCost: 53),
        new(Armor: 4, ItemCost: 75),
        new(Armor: 5, ItemCost: 102),
    ];

    private static readonly List<Stats> RingStats =
    [
        new(),
        new(),
        new(Damage: 1, Armor: 0, ItemCost: 25),
        new(Damage: 2, Armor: 0, ItemCost: 50),
        new(Damage: 3, Armor: 0, ItemCost: 100),
        new(Damage: 0, Armor: 1, ItemCost: 20),
        new(Damage: 0, Armor: 2, ItemCost: 40),
        new(Damage: 0, Armor: 3, ItemCost: 80),
    ];
}