namespace Core.Components;

public class JDrive(int maxJump) : Crittable, IPowerUser
{
    // TODO: Add astrogation and jump checks.

    public int MaxJump { get; init; } = maxJump;

    public JumpDriveCondition Condition { get; set; } = JumpDriveCondition.Normal;
    public required int PowerRequirementPerPowerLevel { get; init; }
    public required int MaxPowerLevel { get; init; }
    public int CurrentPowerLevel { get; set; }
    public int MaxAvailablePowerLevel { get; set; }
}

public enum JumpDriveCondition
{
    Normal,
    Damaged,
    Disabled,
    Destroyed,
}
