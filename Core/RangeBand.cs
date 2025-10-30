namespace Core;

/// <summary>
/// Used to determine bonus/malus for sensors and weapons.
/// </summary>
public enum RangeBand
{
    /// <summary>
    /// Literally touching
    /// </summary>
    None,

    /// <summary>
    /// 1 km or less
    /// </summary>
    Adjacent,

    /// <summary>
    /// 1 - 10 km
    /// </summary>
    Close,

    /// <summary>
    /// 11 - 1 250 km
    /// </summary>
    Short,

    /// <summary>
    /// 1 251 - 10 000 km
    /// </summary>
    Medium,

    /// <summary>
    /// 10 001 - 25 000 km
    /// </summary>
    Long,

    /// <summary>
    /// 25 001 - 50 000 km
    /// </summary>
    VeryLong,

    /// <summary>
    /// More than 50 000 km
    /// </summary>
    Distant,

    /// <summary>
    /// Unkown distance, a clear sign that something is wrong.
    /// </summary>
    Unknown,
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

    /// <summary>
    /// Get how many units of thrust you need to reach a certain distance.
    /// </summary>
    /// <param name="range">The distance you want to reach.</param>
    /// <returns>Units of thrust.</returns>
    /// <exception cref="NotImplementedException"></exception>
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

    /// <summary>
    /// Get which range band you are at given a certain distance.
    /// </summary>
    /// <param name="distance">Distance in thrust units.</param>
    /// <returns>Range band.</returns>
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
