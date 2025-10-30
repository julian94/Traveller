namespace Core.Components;

public class Weapon : Crittable
{
    public required Guid ID { get; set; }

    public required string Name { get; set; }

    public required int DamageDice { get; set; }

    public bool Destructive { get; set; } = false;

    public int WeaponBonus { get; set; } = 0;

    public required RangeBand Range { get; set; }
}
