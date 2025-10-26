namespace Core;

public class Armour : ICrittable
{
    public required int Points { get; set; }
    public int CurrentSeverity { get; set; }

    public void LosePoints(int points) => Points = Math.Max(Points - points, 0);
}
