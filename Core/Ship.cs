namespace Core;

public class Ship
{
    public bool Destroyed => Hull.Points <= 0;
    public required Hull Hull { get; set; }
    public required Armour Armour { get; set; }
    public required List<Weapon> Weapons { get; set; }

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
                throw new NotImplementedException();
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
                throw new ArgumentOutOfRangeException("roll", roll, "You shouldn't get this number on 2d6.");
            }
        }
    }

    private void HullCrit(IRoller roller)
    {
        Hull.CurrentSeverity++;
        Hull.CurrentSeverity = Math.Min(Hull.CurrentSeverity, 6);
        var damage = roller.Roll(Hull.CurrentSeverity);
        var result = Hull.LoseHealth(damage);
        
        if (result > 0)
        {
            ResolveCrit(roller, result);
        }
    }

    private void ArmourCrit(IRoller roller)
    {
        Armour.CurrentSeverity++;
        Armour.CurrentSeverity = Math.Min(Armour.CurrentSeverity, 6);

        if (Armour.CurrentSeverity == 1)
        {
            Armour.LosePoints(1);
        }
        else if (Armour.CurrentSeverity == 2)
        {
            Armour.LosePoints(roller.D3());
        }
        else if (Armour.CurrentSeverity == 3 || Armour.CurrentSeverity == 4)
        {
            Armour.LosePoints(roller.Roll(1));
        }
        else if (Armour.CurrentSeverity == 5 || Armour.CurrentSeverity == 6)
        {
            Armour.LosePoints(roller.Roll(2));
            Hull.SufferCrit(roller);
        }
    }
}
