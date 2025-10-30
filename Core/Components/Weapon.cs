namespace Core.Components;

public class Weapon
{
    public required Guid ID { get; set; }

    public required string Name { get; set; }

    public required int DamageDice { get; set; }

    public bool Destructive { get; set; } = false;

    public int WeaponBonus { get; set; } = 0;

    public required RangeBand Range { get; set; }

    public WeaponCondition Condition { get; set; } = WeaponCondition.Working;
}

public enum WeaponCondition
{
    Working,
    Baned,
    Disabled,
    Destroyed,
}
