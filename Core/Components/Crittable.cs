namespace Core.Components;

public class Crittable(int initialValue = 0)
{
    public const int MinSeverity = 0;
    public const int MaxSeverity = 6;
    public int CritSeverity { get; private set; } = initialValue;

    public void IncreaseCritSeverity()
    {
        CritSeverity = Math.Min(CritSeverity + 1, MaxSeverity);
    }

    public void DecreaseCritSeverity()
    {
        CritSeverity = Math.Max(CritSeverity - 1, MinSeverity);
    }
}
