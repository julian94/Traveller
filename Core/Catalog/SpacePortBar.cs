namespace Core.Catalog;

public static class SpacePortBar
{
    public static Crew JohnTraveller => new()
    {
        LeadershipSkill = 5,
        PilotSkill = 5,
        SensorSkill = 5,
        WeaponSkill = 5,
    };

    public static Crew Jimbo => new();

    public static Crew BillyBob => new()
    {
        LeadershipSkill = -3,
        PilotSkill = -3,
        SensorSkill = -3,
        WeaponSkill = -3,
    };


    public static Crew VirtualCrew0 => new()
    {
        LeadershipSkill = 0,
        PilotSkill = 0,
        SensorSkill = 0,
        WeaponSkill = 0,
    };

    public static Crew VirtualCrew1 => new()
    {
        LeadershipSkill = 1,
        PilotSkill = 1,
        SensorSkill = 1,
        WeaponSkill = 1,
    };

    public static Crew VirtualCrew2 => new()
    {
        LeadershipSkill = 2,
        PilotSkill = 2,
        SensorSkill = 2,
        WeaponSkill = 2,
    };

    public static Crew VirtualGunner0 => new()
    {
        WeaponSkill = 0,
    };

    public static Crew VirtualGunner1 => new()
    {
        WeaponSkill = 1,
    };

    public static Crew VirtualGunner2 => new()
    {
        WeaponSkill = 2,
    };
}
