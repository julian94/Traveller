namespace Core;

public class Crew
{
    public int LeadershipSkill { get; set; } = 0;
    public int PilotSkill { get; set; } = 0;
    public int SensorSkill { get; set; } = 0;
    public int WeaponSkill { get; set; } = 0;

    public void LoseHealth(int health)
    {
        // todo.
    }
}
