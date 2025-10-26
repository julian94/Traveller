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
