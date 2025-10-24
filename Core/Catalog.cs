namespace Core.Catalog;

public static class Weapons
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

public static class Ships
{
    public static Ship Scout => new()
    {
        Hull = new()
        {
            Points = 40,
        },
        Armour = new()
        {
            Points = 4,
        },
        Weapons = [
            Weapons.Beam,
        ]
    };
    public static Ship FarTrader => new()
    {
        Hull = new()
        {
            Points = 80,
        },
        Armour = new()
        {
            Points = 2,
        },
        Weapons = [
            Weapons.Pulse,
            Weapons.Pulse,
        ]
    };
}