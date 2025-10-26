namespace Core;

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
