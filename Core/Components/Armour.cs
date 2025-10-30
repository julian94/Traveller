namespace Core.Components;

public class Armour(int initialPoints, int initialCritSeverity = 0) : Crittable(initialCritSeverity)
{
    public int Points { get; private set; } = initialPoints;

    public void LosePoints(int points) => Points = Math.Max(Points - points, 0);
}
