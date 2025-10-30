namespace Core.Components;

public class FuelTank(int initialFuelTonnage) : Crittable
{
    public int Tons { get; set; } = initialFuelTonnage;

    public FuelTankCondition Condition { get; set; } = FuelTankCondition.Working;

    public void LeakTons(int tons)
    {
        Tons = Math.Max(Tons - tons, 0);
    }

    public void LeakPercentage(int percentage)
    {
        var leak = (Tons * 100) / percentage;
        LeakTons(leak);
    }
}

public enum FuelTankCondition
{
    Working,
    LeakingHourly,
    LeakingRoundly,
    Destroyed,
}
