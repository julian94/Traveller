namespace Core.Catalog;

public static class WeaponCatalog
{
    public static Weapon Beam => new()
    {
        ID = Guid.NewGuid(),
        Name = "Beam Laser",
        DamageDice = 1,
        Destructive = false,
        WeaponBonus = 4,
    };

    public static Weapon Pulse => new()
    {
        ID = Guid.NewGuid(),
        Name = "Pulse Laser",
        DamageDice = 2,
        Destructive = false,
        WeaponBonus = 2,
    };
}

public static class SensorsCatalog
{
    public static Sensors Basic => new()
    {
        InherentModifier = -4,
    };
    public static Sensors Civilian => new()
    {
        InherentModifier = -2,
    };
    public static Sensors Military => new()
    {
        InherentModifier = 0,
    };
    public static Sensors Improved => new()
    {
        InherentModifier = 1,
    };
    public static Sensors Advanced => new()
    {
        InherentModifier = 2,
    };
}

public static class ShipCatalog
{
    public static Ship Scout => new()
    {
        Hull = new(40),
        Armour = new()
        {
            Points = 4,
        },
        Sensors = SensorsCatalog.Military,
        Weapons = [
            WeaponCatalog.Beam,
        ]
    };
    public static Ship FarTrader => new()
    {
        Hull = new(80),
        Armour = new()
        {
            Points = 2,
        },
        Sensors = SensorsCatalog.Civilian,
        Weapons = [
            WeaponCatalog.Pulse,
            WeaponCatalog.Pulse,
        ]
    };
}
