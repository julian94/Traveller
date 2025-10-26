namespace Core;

public class Weapon : ICrittable
{
    public required Guid ID { get; set; }

    public required string Name { get; set; }

    public required int DamageDice { get; set; }

    public required bool Destructive { get; set; }

    public required int WeaponBonus { get; set; }

    public int CurrentSeverity { get; set; }
}
