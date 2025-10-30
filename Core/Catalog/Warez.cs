using Core.Components;

namespace Core.Catalog;

public static class Warez
{
    public static List<ISoftware> StandardSoftware => [Manoeuvre, Library];

    public static SimpleSoftware Manoeuvre => new()
    {
        Name = "Manoeuvre",
        Bandwidth = 0,
    };

    public static SimpleSoftware Intellect => new()
    {
        Name = "Intellect",
        Bandwidth = 10,
    };

    public static SimpleSoftware Library => new()
    {
        Name = "Library",
        Bandwidth = 10,
    };

    public static TieredSoftware Evade1 => new()
    {
        Name = "Evade",
        BandwidthTiers = [0, 10],
        CurrentTier = 0,
    };

    public static TieredSoftware FireControl2 => new()
    {
        Name = "Fire Control",
        BandwidthTiers = [0, 10],
        CurrentTier = 0,
    };

    public static TieredSoftware JumpControl1 => new()
    {
        Name = "Jump Control",
        BandwidthTiers = [0, 5],
        CurrentTier = 0,
    };

    public static TieredSoftware JumpControl2 => new()
    {
        Name = "Jump Control",
        BandwidthTiers = [0, 5, 10],
        CurrentTier = 0,
    };

    public static TieredSoftware JumpControl3 => new()
    {
        Name = "Jump Control",
        BandwidthTiers = [0, 5, 10, 15],
        CurrentTier = 0,
    };

    public static TieredSoftware JumpControl4 => new()
    {
        Name = "Jump Control",
        BandwidthTiers = [0, 5, 10, 15, 20],
        CurrentTier = 0,
    };

    public static TieredSoftware JumpControl5 => new()
    {
        Name = "Jump Control",
        BandwidthTiers = [0, 5, 10, 15, 20, 25],
        CurrentTier = 0,
    };

    public static TieredSoftware JumpControl6 => new()
    {
        Name = "Jump Control",
        BandwidthTiers = [0, 5, 10, 15, 20, 25, 30],
        CurrentTier = 0,
    };

    public static TieredSoftware VirtualCrew0 => new()
    {
        Name = "Virtual Crew",
        BandwidthTiers = [0, 5],
        CurrentTier = 0,
    };

    public static TieredSoftware VirtualCrew1 => new()
    {
        Name = "Virtual Crew",
        BandwidthTiers = [0, 5, 10],
        CurrentTier = 0,
    };

    public static TieredSoftware VirtualCrew2 => new()
    {
        Name = "Virtual Crew",
        BandwidthTiers = [0, 5, 10, 15],
        CurrentTier = 0,
    };
}
