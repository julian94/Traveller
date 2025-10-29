namespace Core;

public class Hull : ICrittable
{
    public Hull(int points)
    {
        Points = points;
        var tenPercentOfHullRoundedUp = Convert.ToInt32(Math.Ceiling(Points * 0.1));
        CritTresholds = [];
        for (int i = Points; i > 0; i -= tenPercentOfHullRoundedUp)
        {
            CritTresholds.Add(i);
        }
    }

    public int Points { get; set; }
    public int CurrentSeverity { get; set; }

    public bool HeatShielding { get; init; } = false;
    public Stealh StealhCoating { get; init; } = Stealh.None;

    public int SensorProfile(int techLevelOfThisShip, int techLevelOfOtherShip) =>
        (HeatShielding ? -2 : 0) +
        (StealhCoating switch
        {
            Stealh.None => 0,
            Stealh.Standard => -4 - Math.Max(techLevelOfThisShip - techLevelOfOtherShip, 0),
            Stealh.Superior => -6 - Math.Max(techLevelOfThisShip - techLevelOfOtherShip, 0),
            _ => throw new NotImplementedException(),
        });

    public int LoseHealth(int damage)
    {
        var oldTreshold = 0;
        for (int i = 0; i < CritTresholds.Count; i++)
        {
            if (Points <= CritTresholds[i]) oldTreshold = i;
        }
        Points -= damage;
        var newTreshold = 0;
        for (int i = 0; i < CritTresholds.Count; i++)
        {
            if (Points <= CritTresholds[i]) newTreshold = i;
        }

        if (Points < 0) return 0;

        return (newTreshold - oldTreshold);
    }

    private List<int> CritTresholds { get; init; }

    public int SufferCrit(IRoller roller)
    {
        CurrentSeverity++;
        CurrentSeverity = Math.Min(CurrentSeverity, 6);
        var damage = roller.Roll(CurrentSeverity);
        return LoseHealth(damage);
    }
}

public enum Stealh
{
    None,
    Standard,
    Superior,
}
