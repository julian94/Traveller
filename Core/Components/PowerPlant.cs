namespace Core.Components;

public class PowerPlant(int powerOutput) : Crittable
{
    public int MaxPowerOutput { get; init; } = powerOutput;

    public int PowerOutput => (MaxPowerOutput * 100) / CritSeverity switch
    {
        0 => 100,
        1 => 90,
        2 => 80,
        3 => 50,
        _ => 0,
    };
}
