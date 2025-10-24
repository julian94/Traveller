namespace Core;

public class Ship
{
    public bool Destroyed => Hull.Points <= 0;
    public required Hull Hull { get; set; }
    public required Armour Armour { get; set; }
    public required List<Weapon> Weapons { get; set; }

    public void SufferAttack(IAttack attack, IRoller roller)
    {
        var realdamage = Math.Max(attack.Damage - Armour.Points, 0);
        if (realdamage == 0) return;

        if (attack.PotentialCriticalHit)
        {
            ResolveCrit(roller, 1);
        }

        int hullCrits = Hull.LoseHealth(realdamage);

        if (hullCrits > 0)
        {
            ResolveCrit(roller, hullCrits);
        }
    }

    public void ResolveCrit(IRoller roller, int severity)
    {
        var roll = roller.Roll(2);

        for (int i = 0; i < severity; i++)
        {
            if (roll == 6)
            {
                ArmourCrit(roller);
            }
            else if (roll == 7)
            {
                HullCrit(roller);
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }

    private void HullCrit(IRoller roller)
    {
        Hull.CurrentSeverity++;
        Hull.CurrentSeverity = Math.Min(Hull.CurrentSeverity, 6);
        var damage = roller.Roll(Hull.CurrentSeverity);
        var result = Hull.LoseHealth(damage);
        
        if (result > 0)
        {
            ResolveCrit(roller, result);
        }
    }

    private void ArmourCrit(IRoller roller)
    {
        Armour.CurrentSeverity++;
        Armour.CurrentSeverity = Math.Min(Armour.CurrentSeverity, 6);

        if (Armour.CurrentSeverity == 1)
        {
            Armour.LosePoints(1);
        }
        else if (Armour.CurrentSeverity == 2)
        {
            Armour.LosePoints(roller.D3());
        }
        else if (Armour.CurrentSeverity == 3 || Armour.CurrentSeverity == 4)
        {
            Armour.LosePoints(roller.Roll(1));
        }
        else if (Armour.CurrentSeverity == 5 && Armour.CurrentSeverity == 6)
        {
            Armour.LosePoints(roller.Roll(2));
            Hull.SufferCrit(roller);
        }
    }
}

public class Weapon: ICrittable
{
    public required Guid ID { get; set; }

    public required string Name { get; set; }

    public required int DamageDice { get; set; }

    public required bool Destructive { get; set; }

    public required int WeaponBonus { get; set; }

    public int CurrentSeverity { get; set; }
}

public class Hull: ICrittable
{
    public Hull(int points)
    {
        Points = points;
        var tenPercentOfHullRoundedUp = Convert.ToInt32(Math.Ceiling(Points * 0.1));
        CritTresholds = [];
        for (int i = Points; i > 0; i -= tenPercentOfHullRoundedUp)
        {
            CritTresholds.Add(i);
        }
    }

    public int Points { get; set; }
    public int CurrentSeverity { get; set ; }

    public int LoseHealth(int damage)
    {
        var oldTreshold = 0;
        for (int i = 0; i > CritTresholds.Count; i++)
        {
            if (Points <= CritTresholds[i]) oldTreshold = i;
        }
        Points -= damage;
        var newTreshold = 0;
        for (int i = 0; i > CritTresholds.Count; i++)
        {
            if (Points <= CritTresholds[i]) newTreshold = i;
        }

        if (Points < 0) return 0;

        return (newTreshold - oldTreshold);
    }

    private List<int> CritTresholds { get; init; }

    public int SufferCrit(IRoller roller)
    {
        CurrentSeverity++;
        CurrentSeverity = Math.Min(CurrentSeverity, 6);
        var damage = roller.Roll(CurrentSeverity);
        return LoseHealth(damage);
    }
}

public class Armour : ICrittable
{
    public required int Points { get; set; }
    public int CurrentSeverity { get; set; }

    public void LosePoints(int points) => Points = Math.Min(Points - points, 0);
}

public class AttackResult
{
    public bool Success { get; init; }
}

public class Attack : IAttack
{
    private const int TargetNumber = 8;
    private const int CriticalHitTreshold = 6;
    public Attack(Weapon weapon, int externalBonus, IRoller roller)
    {
        var attackRoll = roller.Roll(2, weapon.WeaponBonus + externalBonus);
        Damage = roller.Roll(weapon.DamageDice, attackRoll - TargetNumber);
        Success = attackRoll >= TargetNumber;
        PotentialCriticalHit = attackRoll >= TargetNumber + CriticalHitTreshold;
    }

    public bool Success { get; init; }
    public bool PotentialCriticalHit { get; init; }
    public int Damage { get; init; }
}

public interface IAttack
{
    int Damage { get; init; }
    bool PotentialCriticalHit { get; init; }
    bool Success { get; init; }
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

    public int D3() => _random.Next(1, 3);

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
    public int D3();
    public int Roll(int dice, int bonus = 0);

    public RollResult Roll(int dice, int bonus, int target);
}

public interface ICrittable
{
    public int CurrentSeverity { get; set; }
}
