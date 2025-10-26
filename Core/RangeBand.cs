namespace Core;

public enum RangeBand
{
    Unknown,
    None,
    Adjacent,
    Close,
    Short,
    Medium,
    Long,
    VeryLong,
    Distant,
}

public static class RangeBandConstants
{
    public const int None       = 0;
    public const int Adjacent   = 1;
    public const int Close      = 1;
    public const int Short      = 2;
    public const int Medium     = 5;
    public const int Long       = 10;
    public const int VeryLong   = 25;
    public const int Distant    = 50;

    public static int ThrustRequired(this RangeBand range) => range switch
    {
        RangeBand.None => None,
        RangeBand.Adjacent => Adjacent,
        RangeBand.Close => Close,
        RangeBand.Short => Short,
        RangeBand.Medium => Medium,
        RangeBand.Long => Long,
        RangeBand.VeryLong => VeryLong,
        RangeBand.Distant => Distant,
        _ => throw new NotImplementedException(),
    };

    public static RangeBand GetBand(this int distance) => Math.Abs(distance) switch
    {
        0 => RangeBand.None,
        <= Adjacent => RangeBand.Adjacent,
        <= Adjacent + Close => RangeBand.Close,
        <= Adjacent + Close + Short => RangeBand.Short,
        <= Adjacent + Close + Short + Medium => RangeBand.Medium,
        <= Adjacent + Close + Short + Medium + Long => RangeBand.Long,
        <= Adjacent + Close + Short + Medium + Long + VeryLong => RangeBand.VeryLong,
        _ => RangeBand.Distant,
    };
}
