namespace Core;

public class Ship
{
    public bool Destroyed => Hull.Points <= 0;
    public required Hull Hull { get; set; }
    public required Armour Armour { get; set; }
    public required Sensors Sensors { get; set; }
    public required List<Weapon> Weapons { get; set; }

    public required int TechLevel { get; init; }

    public int SensorProfileModifier(int techLevelOfShipTryingToFindThisShip) => Hull.SensorProfile(TechLevel, techLevelOfShipTryingToFindThisShip);

    public void SufferAttack(IAttack attack, IRoller roller)
    {
        var realdamage = Math.Max(attack.Damage - Armour.Points, 0);
        if (realdamage == 0) return;

        if (attack.PotentialCriticalHit)
        {
            ResolveCrit(roller, 1);
        }

        int hullCrits = Hull.LoseHealth(realdamage);

        if (hullCrits > 0)
        {
            ResolveCrit(roller, hullCrits);
        }
    }

    public void ResolveCrit(IRoller roller, int severity)
    {
        var roll = roller.Roll(2);

        for (int i = 0; i < severity; i++)
        {
            if (roll == 2) // Sensors
            {
                Sensors.IncreaseCritSeverity();
            }
            else if (roll == 3) // Power Plant
            {
                throw new NotImplementedException();
            }
            else if (roll == 4) // Fuel
            {
                throw new NotImplementedException();
            }
            else if (roll == 5) // Weapon
            {
                throw new NotImplementedException();
            }
            else if (roll == 6) // Armour
            {
                ArmourCrit(roller);
            }
            else if (roll == 7) // Hull
            {
                HullCrit(roller);
            }
            else if (roll == 8) // M-Drive
            {
                throw new NotImplementedException();
            }
            else if (roll == 9) // Cargo
            {
                throw new NotImplementedException();
            }
            else if (roll == 10) // J-Drive
            {
                throw new NotImplementedException();
            }
            else if (roll == 11) // Crew
            {
                throw new NotImplementedException();
            }
            else if (roll == 12) // Computer
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new Exception($"You shouldn't get {roll} number on 2d6.");
            }
        }
    }

    private void HullCrit(IRoller roller)
    {
        Hull.IncreaseCritSeverity();
        var damage = roller.Roll(Hull.CritSeverity);
        var result = Hull.LoseHealth(damage);
        
        if (result > 0)
        {
            ResolveCrit(roller, result);
        }
    }

    private void ArmourCrit(IRoller roller)
    {
        Armour.IncreaseCritSeverity();

        var armourLoss = Armour.CritSeverity switch
        {
            1 => 1,
            2 => roller.D3(),
            <= 4 => roller.Roll(1),
            <= 6 => roller.Roll(2),
            _ => throw new Exception("Should be unreachable"),

        };
        Armour.LosePoints(armourLoss);
        
        if (Armour.CritSeverity == 5 || Armour.CritSeverity == 6)
        {
            Hull.SufferCrit(roller);
        }
    }
}
