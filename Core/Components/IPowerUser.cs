namespace Core.Components;

public interface IPowerUser
{
    public int PowerRequirementPerPowerLevel { get; init; }
    public int MaxPowerLevel { get; init; }

    public int CurrentPowerLevel { get; set; }
    public int MaxAvailablePowerLevel { get; set; }

    public int CurrentDraw => CurrentPowerLevel * PowerRequirementPerPowerLevel;
}
