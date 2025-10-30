using Core.Components;
using Core.Util;

namespace Core;

public class Ship
{
    public bool Destroyed => Hull.Points <= 0;
    public required Hull Hull { get; set; }
    public required Armour Armour { get; set; }
    public required Sensors Sensors { get; set; }
    public required List<Weapon> Weapons { get; set; }

    public Crittable WeaponStatus { get; init; } = new();
    public CargoHold Cargo { get; init; } = new();
    public required FuelTank Fuel { get; init; }

    public required PowerPlant PowerPlant { get; init; }

    public MDrive? MDrive { get; init; }
    public JDrive? JumpDrive { get; init; }

    public required Computer Computer { get; init; }
    public LifeSupport LifeSupport { get; init; } = new();

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
                PowerPlantCrit(roller);
            }
            else if (roll == 4) // Fuel
            {
                FuelCrit(roller);
            }
            else if (roll == 5) // Weapon
            {
                WeaponCrit(roller);
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
                MDriveCrit(roller);
            }
            else if (roll == 9) // Cargo
            {
                CargoCrit(roller);
            }
            else if (roll == 10) // J-Drive
            {
                JumpDriveCrit(roller);
            }
            else if (roll == 11) // Crew
            {
                LifeSupportCrit(roller);
            }
            else if (roll == 12) // Computer
            {
                ComputerCrit();
            }
            else
            {
                throw new Exception($"You shouldn't get {roll} number on 2d6.");
            }
        }
    }


    private void WeaponCrit(IRoller roller)
    {
        var weapon = Weapons.Random(roller);

        WeaponStatus.IncreaseCritSeverity();

        weapon.Condition = WeaponStatus.CritSeverity switch
        {
            1 => WeaponCondition.Baned,
            2 => WeaponCondition.Disabled,
            _ => WeaponCondition.Destroyed,
        };

        if (WeaponStatus.CritSeverity >= 4)
        {
            Hull.SufferCrit(roller);
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

    private void CargoCrit(IRoller roller)
    {
        Cargo.IncreaseCritSeverity();

        Cargo.DestroyCargo(Cargo.CritSeverity switch
        {
            1 => 10,
            2 => roller.Roll(1) * 10,
            3 => roller.Roll(2) * 10,
            > 3 => 100,
            _ => throw new Exception("Should be unreachable"),
        });

        if (Cargo.CritSeverity == 5 || Cargo.CritSeverity == 6)
        {
            Hull.SufferCrit(roller);
        }
    }

    private void FuelCrit(IRoller roller)
    {
        Fuel.IncreaseCritSeverity();

        Fuel.Condition = Fuel.CritSeverity switch
        {
            1 => FuelTankCondition.LeakingHourly,
            2 => FuelTankCondition.LeakingHourly,
            4 => FuelTankCondition.LeakingHourly,
            _ => throw new Exception("Should be unreachable"),
        };

        if (Fuel.CritSeverity == 5)
        {
            Hull.SufferCrit(roller);
        }
        else if (Fuel.CritSeverity == 6)
        {
            var crits = roller.Roll(1);
            for (var i = 0; i < crits; i++)
            {
                Hull.SufferCrit(roller);
            }
        }
    }

    private void PowerPlantCrit(IRoller roller)
    {
        PowerPlant.IncreaseCritSeverity();

        if (MDrive is not null)
        {
            MDrive.PowerPlantCrits = PowerPlant.CritSeverity switch
            {
                1 => DriveEffectFromPowerPlantCrits.LostOneThrust,
                2 => DriveEffectFromPowerPlantCrits.LostTwoThrust,
                3 => DriveEffectFromPowerPlantCrits.LostThreeThrust,
                _ => DriveEffectFromPowerPlantCrits.LostTwoThrust,
            };
        }

        if (PowerPlant.CritSeverity == 5)
        {
            Hull.SufferCrit(roller);
        }
        else if (PowerPlant.CritSeverity == 6)
        {
            var crits = roller.Roll(1);
            for (var i = 0; i < crits; i++)
            {
                Hull.SufferCrit(roller);
            }
        }
    }

    private void JumpDriveCrit(IRoller roller)
    {
        if (JumpDrive is not null)
        {

            JumpDrive.IncreaseCritSeverity();

            JumpDrive.Condition = JumpDrive.CritSeverity switch
            {
                1 => JumpDriveCondition.Damaged,
                2 => JumpDriveCondition.Disabled,
                _ => JumpDriveCondition.Destroyed,
            };

        if (JumpDrive.CritSeverity >= 4)
            {
                Hull.SufferCrit(roller);
            }
        }
    }

    private void MDriveCrit(IRoller roller)
    {
        if (MDrive is not null)
        {

            MDrive.IncreaseCritSeverity();

            if (MDrive.CritSeverity == 6)
            {
                Hull.SufferCrit(roller);
            }
        }
    }

    private void LifeSupportCrit(IRoller roller)
    {
        LifeSupport.IncreaseCritSeverity();
        
        if (LifeSupport.CritSeverity == 1)
        {
            var damage = roller.Roll(1);
            LifeSupport.CrewMembers.Random(roller).LoseHealth(damage);
        }
        else if (LifeSupport.CritSeverity == 2)
        {
            var hours = roller.Roll(1);
            LifeSupport.RemainingRoundsOfLifeSupport = hours * 10;
        }
        else if (LifeSupport.CritSeverity == 3)
        {
            // 1D occupants take 2D damage
            throw new NotImplementedException();
        }
        else if (LifeSupport.CritSeverity == 4)
        {
            var rounds = roller.Roll(1);
            LifeSupport.RemainingRoundsOfLifeSupport = rounds;
        }
        else if (LifeSupport.CritSeverity == 5)
        {
            foreach (var person in LifeSupport.CrewMembers)
            {
                var damage = roller.Roll(3);
                person.LoseHealth(damage);
            }
        }
        else if (LifeSupport.CritSeverity == 6)
        {
            LifeSupport.RemainingRoundsOfLifeSupport = 0;
        }

    }

    private void ComputerCrit()
    {
        Computer.IncreaseCritSeverity();
    }
}
