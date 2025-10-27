namespace Core;

public interface ICrittable
{
    public int CurrentSeverity { get; set; }
    public const int MinSeverity = 0;
    public const int MaxSeverity = 6;
}
