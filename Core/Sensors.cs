namespace Core;

public class Sensors : ICrittable
{
    public int CurrentSeverity { get; set; } = 0;

    public required SensorCapability Capabilities { get; init; }

    public int InherentModifier { get; init; }

    public int Modifier => InherentModifier - (CurrentSeverity > 0 ? -2 : 0);

    public RangeBand MaxRange => CurrentSeverity switch
    {
        2 => RangeBand.Medium,
        3 => RangeBand.Short,
        4 => RangeBand.Close,
        5 => RangeBand.Adjacent,
        6 => RangeBand.None,
        _ => RangeBand.Distant,
    };

    public bool TryDetect(int distance, bool activeRadar, Crew sensorOperator, IRoller roller, Ship target)
    {
        var targetIsTooFarAway = MaxRange < distance.GetBand();
        if (targetIsTooFarAway)
        {
            return false;
        }

        var radarModifier = activeRadar ? 2 : 0;
        var totalModifier = Modifier + sensorOperator.SensorSkill + radarModifier + target.SensorProfileModifier;

        var roll = roller.Roll(2, totalModifier, Difficulties.Average);
        return roll.Success;
    }
}

[Flags]
public enum SensorCapability
{
    Lidar,
    Radar,
    Jammers,
    Densitometer,
    NeuralActivitySensor,
}

public enum SensorDetail
{
    None,
    Minimal,
    Limited,
    Full,
}
