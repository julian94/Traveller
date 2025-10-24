namespace Core;

public class Ship
{
    public required Hull Hull { get; set; }
    public required Armour Armour { get; set; }
    public required List<Weapon> Weapons { get; set; }

    public void TakeDamage(int damage, IRoller roller)
    {
        var realdamage = Math.Max(damage - Armour.Points, 0);
        Hull.Points -= realdamage;
        return;
    }
}

public class Weapon
{
    public required Guid ID { get; set; }

    public required string Name { get; set; }

    public required int DamageDice { get; set; }

    public required bool Destructive { get; set; }

    public required int WeaponBonus { get; set; }
    
    public AttackResult Attack(Ship target, int bonus, IRoller roller)
    {
        var attackRoll = roller.Roll(DamageDice, WeaponBonus + bonus, 8);

        if (attackRoll.Success)
        {
            var damage = roller.Roll(DamageDice, attackRoll.Effect);
            target.TakeDamage(damage, roller);

            return new()
            {
                Success = true,
            };
        }
        else
        {
            return new()
            {
                Success = false,
            };
        }
    }
}

public class Hull
{
    public required int Points { get; set; }
}

public class Armour
{
    public required int Points { get; set; }
}

public class AttackResult
{
    public bool Success { get; init; }
}

public class RollResult(int result, int target)
{
    public bool Success => Effect >= 0;
    public int Result { get; init; } = result;
    public int Effect { get; init; } = target - result;
}

public class Roller : IRoller
{
    private readonly Random _random = new();

    private int Roll() => _random.Next(1, 6);

    public int Roll(int dice, int bonus = 0)
    {
        var result = 0;
        for (int i = 0; i < dice; i++)
        {
            result += Roll();
        }
        return result + bonus;
    }
    public RollResult Roll(int dice, int bonus, int target) => new(Roll(dice, bonus), target);
}

public interface IRoller
{
    public int Roll(int dice, int bonus);

    public RollResult Roll(int dice, int bonus, int target);
}
