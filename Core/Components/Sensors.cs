using Core.Util;

namespace Core.Components;

public class Sensors : Crittable
{
    public required SensorCapability Capabilities { get; init; }

    public int InherentModifier { get; init; }

    public int Modifier => InherentModifier - (CritSeverity > 0 ? -2 : 0);

    public RangeBand MaxRange => CritSeverity switch
    {
        2 => RangeBand.Medium,
        3 => RangeBand.Short,
        4 => RangeBand.Close,
        5 => RangeBand.Adjacent,
        6 => RangeBand.None,
        _ => RangeBand.Distant,
    };

    public bool TryDetect(int distance, bool activeRadar, Crew sensorOperator, IRoller roller, Ship target, int techLevel)
    {
        var targetIsTooFarAway = MaxRange < distance.GetBand();
        if (targetIsTooFarAway)
        {
            return false;
        }

        var radarModifier = activeRadar ? 2 : 0;
        var totalModifier = Modifier + sensorOperator.SensorSkill + radarModifier + target.SensorProfileModifier(techLevel);

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
