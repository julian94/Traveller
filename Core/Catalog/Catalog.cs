using Core.Components;

namespace Core.Catalog;

public static class WeaponCatalog
{
    public static Weapon Beam => new()
    {
        Name = "Beam Laser",
        DamageDice = 1,
        WeaponBonus = 4,
        Range = RangeBand.Medium,
        WeaponType = WeaponType.TurretSingle,
    };

    public static Weapon Pulse => new()
    {
        Name = "Pulse Laser",
        DamageDice = 2,
        WeaponBonus = 2,
        Range = RangeBand.Long,
        WeaponType = WeaponType.TurretSingle,
    };

    public static Weapon ParticleBarbette => new()
    {
        Name = "Particle Barbette",
        DamageDice = 4,
        Range = RangeBand.VeryLong,
        WeaponType = WeaponType.Barbette,
    };
}

public static class SensorsCatalog
{
    public static Sensors Basic => new()
    {
        Capabilities =
            SensorCapability.Lidar &
            SensorCapability.Radar,
        InherentModifier = -4,
    };
    public static Sensors Civilian => new()
    {
        Capabilities =
            SensorCapability.Lidar &
            SensorCapability.Radar,
        InherentModifier = -2,
    };
    public static Sensors Military => new()
    {
        Capabilities =
            SensorCapability.Lidar &
            SensorCapability.Radar &
            SensorCapability.Jammers,
        InherentModifier = 0,
    };
    public static Sensors Improved => new()
    {
        Capabilities =
            SensorCapability.Lidar &
            SensorCapability.Radar &
            SensorCapability.Jammers &
            SensorCapability.Densitometer,
        InherentModifier = 1,
    };
    public static Sensors Advanced => new()
    {
        Capabilities =
            SensorCapability.Lidar &
            SensorCapability.Radar &
            SensorCapability.Jammers &
            SensorCapability.Densitometer &
            SensorCapability.NeuralActivitySensor,
        InherentModifier = 2,
    };
}

public static class ShipCatalog
{
    public static Ship Scout => new()
    {
        Computer = new(5, [.. Warez.StandardSoftware, Warez.JumpControl2], bis: true, fib: false),
        TechLevel = 12,
        Hull = new(40),
        Armour = new(4),
        Sensors = SensorsCatalog.Military,
        Fuel = new(22),
        PowerPlant = new(60),
        Weapons = [
            WeaponCatalog.Beam,
        ]
    };
    public static Ship FarTrader => new()
    {
        Computer = new(5, [.. Warez.StandardSoftware, Warez.JumpControl2], bis: true, fib: false),
        TechLevel = 12,
        Hull = new(80),
        Armour = new(2),
        Sensors = SensorsCatalog.Civilian,
        Fuel = new(41),
        PowerPlant = new(75),
        Weapons = [
            WeaponCatalog.Pulse,
            WeaponCatalog.Pulse,
        ]
    };
    public static Ship Harrier => new()
    {
        Computer = new(20, [.. Warez.StandardSoftware, Warez.JumpControl2, Warez.Evade1, Warez.FireControl2], bis: false, fib: false),
        TechLevel = 15,
        Hull = new(88)
        {
            StealhCoating = Stealh.Superior,
        },
        Armour = new(4),
        Sensors = SensorsCatalog.Advanced,
        Fuel = new(42),
        PowerPlant = new(260),
        Weapons = [
            WeaponCatalog.ParticleBarbette,
            WeaponCatalog.Pulse,
        ]
    };
}
