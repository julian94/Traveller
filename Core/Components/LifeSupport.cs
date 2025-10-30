namespace Core.Components;

public class LifeSupport : Crittable
{
    public List<Crew> CrewMembers { get; init; } = [];
    public int? RemainingRoundsOfLifeSupport { get; set; }
}
