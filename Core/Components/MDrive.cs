namespace Core.Components;

public class MDrive(int maxThrust) : Crittable, IPowerUser
{
    // TODO: Add thing that use thrust.

    public int MaxThrust { get; init; } = maxThrust;

    public DriveEffectFromPowerPlantCrits PowerPlantCrits { get; set; }

    public int AvailableThrust => Math.Max(0, AvailableThrustFromPowerLevel - ThrustLostFromDriveDamage - ThrustLostFromPowerPlantDamage);

    private int AvailableThrustFromPowerLevel => CurrentPowerLevel;

    private int ThrustLostFromPowerPlantDamage => PowerPlantCrits switch
    {
        DriveEffectFromPowerPlantCrits.Normal => 0,
        DriveEffectFromPowerPlantCrits.LostOneThrust => 1,
        DriveEffectFromPowerPlantCrits.LostTwoThrust => 2,
        DriveEffectFromPowerPlantCrits.LostThreeThrust => 3,
        DriveEffectFromPowerPlantCrits.LostAllThrust => MaxThrust,
    };

    private int ThrustLostFromDriveDamage => CritSeverity switch
    {
        <= 1 => 0,
        2 => 1,
        3 => 2,
        4 => 3,
        >= 5 => MaxThrust,
    };

    public required int PowerRequirementPerPowerLevel { get; init; }
    public required int MaxPowerLevel { get; init; }
    public int CurrentPowerLevel { get; set; }
    public int MaxAvailablePowerLevel { get; set; }
}

public enum DriveEffectFromPowerPlantCrits
{
    Normal,
    LostOneThrust,
    LostTwoThrust,
    LostThreeThrust,
    LostAllThrust,
}
