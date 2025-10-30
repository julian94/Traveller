namespace Core.Components;

public class Weapon
{
    public required string Name { get; set; }

    public required int DamageDice { get; set; }

    public bool Destructive { get; set; } = false;

    public int WeaponBonus { get; set; } = 0;

    public required RangeBand Range { get; set; }

    public required WeaponType WeaponType { get; init; }

    public WeaponCondition Condition { get; set; } = WeaponCondition.Working;

    // TODO: Missiles.
}

public enum WeaponCondition
{
    Working,
    Baned,
    Disabled,
    Destroyed,
}

public enum WeaponType
{
    FirmpointFixed,
    FirmpointTurretSingle,
    FirmpointBarbette,
    Fixed,
    TurretSingle,
    TurretDouble,
    TurretTriple,
    TurretQuad,
    Barbette,
    BaySmall,
    BayMedium,
    BayLarge,
    Spinal,
}
