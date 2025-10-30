namespace Core.Components;

public class LifeSupport : Crittable
{
    // TODO: How assign Crew to tasks?
    public List<Crew> CrewMembers { get; init; } = [];
    public int? RemainingRoundsOfLifeSupport { get; set; }
}
