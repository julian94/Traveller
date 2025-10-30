namespace Core;

public class Crew
{
    // TODO: Handle virtual crew.

    // TODO: Replace with Characteristics instead of simple default.
    public bool Alive => Health > 0;

    // TODO: Unconscious is when END is 0 and either STR or DEX is 0.
    public bool Conscious => Health > 7;
    public int Health { get; set; } = 21;
    public int LeadershipSkill { get; set; } = 0;
    public int PilotSkill { get; set; } = 0;
    public int SensorSkill { get; set; } = 0;
    public int WeaponSkill { get; set; } = 0;

    public void LoseHealth(int health)
    {
        Health -= health;
    }
}
