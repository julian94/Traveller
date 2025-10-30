namespace Core.Components;

public class Computer(int bandwidth, List<ISoftware> software, bool fib = false, bool bis = false) : Crittable
{
    public int MaxBandwidth { get; init; } = bandwidth;

    public int Bandwidth => MaxBandwidth - CritSeverity switch
    {
        0 => 0,
        1 => 0,
        2 => 1 * 5,
        3 => 2 * 5,
        4 => 3 * 5,
        _ => MaxBandwidth,
    };

    public List<ISoftware> Software { get; init; } = software;
}

public interface ISoftware
{
    public string Name { get; }
    public int Bandwidth { get; }
    public bool Active { get; set; }
}

// TODO: probably refactor

public class SimpleSoftware : ISoftware
{
    public required string Name { get; init; }
    public required int Bandwidth { get; init; }
    public bool Active { get; set; } = false;
}

public class TieredSoftware : ISoftware
{
    public required string Name { get; init; }
    public bool Active { get; set; } = false;
    public int Bandwidth => BandwidthTiers[CurrentTier];
    public required List<int> BandwidthTiers { get; init; }
    public int CurrentTier { get; set; }
}

public class TieredSoftwareWithModifier : ISoftware
{
    public required string Name { get; init; }
    public bool Active { get; set; } = false;
    public int Bandwidth => BandwidthTiers[CurrentTier] + ExtraBandwidth;
    public required List<int> BandwidthTiers { get; init; }
    public int CurrentTier { get; set; }
    public int ExtraBandwidth { get; set; }
}

