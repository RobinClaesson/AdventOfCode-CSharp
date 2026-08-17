namespace AdventOfCode.Solutions.Year2015.Day22;

internal record GameState(int BossHitPoints, int BossDamage)
{
    public int PlayerHitPoints { get; init; } = 50;
    public int Mana { get; init; } = 500;
    public Attacker Attacker { get; init; } = Attacker.Player;
    public int SpentMana { get; init; }
    public int ShieldTimer { get; init; }
    public int PoisonTimer { get; init; }
    public int RechargeTimer { get; init; }

    public bool PlayerIsDead => PlayerHitPoints <= 0;
    public bool BossIsDead => BossHitPoints <= 0;

    public GameState ApplyEffects(int playerTurnDamage)
    {
        var playerHitPoints = Attacker == Attacker.Player ? PlayerHitPoints - playerTurnDamage : PlayerHitPoints;

        if (playerHitPoints <= 0)
        {
            return this with
            {
                PlayerHitPoints = playerHitPoints
            };
        }

        return this with
        {
            PlayerHitPoints = playerHitPoints,
            BossHitPoints = PoisonTimer > 0 ? BossHitPoints - 3 : BossHitPoints,
            Mana = RechargeTimer > 0 ? Mana + 101 : Mana,
            ShieldTimer = ShieldTimer > 0 ? ShieldTimer - 1 : ShieldTimer,
            PoisonTimer = PoisonTimer > 0 ? PoisonTimer - 1 : PoisonTimer,
            RechargeTimer = RechargeTimer > 0 ? RechargeTimer - 1 : RechargeTimer,
        };
    }

    public GameState CastMagicMissile() => this with
    {
        BossHitPoints = BossHitPoints - 4,
        Mana = Mana - 53,
        SpentMana = SpentMana + 53,
        Attacker = Attacker.Boss
    };

    public GameState CastDrain() => this with
    {
        BossHitPoints = BossHitPoints - 2,
        PlayerHitPoints = PlayerHitPoints + 2,
        Mana = Mana - 73,
        SpentMana = SpentMana + 73,
        Attacker = Attacker.Boss
    };

    public GameState CastShield() => this with
    {
        ShieldTimer = 6,
        Mana = Mana - 113,
        SpentMana = SpentMana + 113,
        Attacker = Attacker.Boss
    };

    public GameState CastPoison() => this with
    {
        PoisonTimer = 6,
        Mana = Mana - 173,
        SpentMana = SpentMana + 173,
        Attacker = Attacker.Boss
    };

    public GameState CastRecharge() => this with
    {
        RechargeTimer = 5,
        Mana = Mana - 229,
        SpentMana = SpentMana + 229,
        Attacker = Attacker.Boss
    };

    public IEnumerable<GameState> PossiblePlayerActions()
    {
        if (Mana > 53)
            yield return CastMagicMissile();
        if (Mana > 73)
            yield return CastDrain();
        if (Mana > 113)
            yield return CastShield();
        if (Mana > 173)
            yield return CastPoison();
        if (Mana > 229)
            yield return CastRecharge();
    }

    public GameState BossAttacks()
    {
        var playerArmor = ShieldTimer > 0 ? 7 : 0;
        var damage = BossDamage - playerArmor;
        var nextState = this with
        {
            PlayerHitPoints = PlayerHitPoints - damage,
            Attacker = Attacker.Player
        };
        return nextState;
    }
}

internal enum Attacker
{
    Player,
    Boss
}