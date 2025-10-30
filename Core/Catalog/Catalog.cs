using Core.Components;

namespace Core.Catalog;

public static class WeaponCatalog
{
    public static Weapon Beam => new()
    {
        ID = Guid.NewGuid(),
        Name = "Beam Laser",
        DamageDice = 1,
        WeaponBonus = 4,
        Range = RangeBand.Medium
    };

    public static Weapon Pulse => new()
    {
        ID = Guid.NewGuid(),
        Name = "Pulse Laser",
        DamageDice = 2,
        WeaponBonus = 2,
        Range = RangeBand.Long
    };

    public static Weapon ParticleBarbette => new()
    {
        ID = Guid.NewGuid(),
        Name = "Particle Barbette",
        DamageDice = 4,
        Range = RangeBand.VeryLong
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
        TechLevel = 12,
        Hull = new(40),
        Armour = new(4),
        Sensors = SensorsCatalog.Military,
        Fuel = new(22),
        Weapons = [
            WeaponCatalog.Beam,
        ]
    };
    public static Ship FarTrader => new()
    {
        TechLevel = 12,
        Hull = new(80),
        Armour = new(2),
        Sensors = SensorsCatalog.Civilian,
        Fuel = new(41),
        Weapons = [
            WeaponCatalog.Pulse,
            WeaponCatalog.Pulse,
        ]
    };
    public static Ship Harrier => new()
    {
        TechLevel = 15,
        Hull = new(88)
        {
            StealhCoating = Stealh.Superior,
        },
        Armour = new(4),
        Sensors = SensorsCatalog.Advanced,
        Fuel = new(42),
        Weapons = [
            WeaponCatalog.Pulse,
            WeaponCatalog.Pulse,
        ]
    };
}
