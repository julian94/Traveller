using Core.Components;

namespace Test.Core;

public class ShipTests
{
    [Test]
    public void CanCreateShip()
    {
        Ship ship = new() {
            TechLevel = 12,
            Hull = new(10),
            Armour = new(2),
            Sensors = new()
            {
                Capabilities = SensorCapability.Lidar & SensorCapability.Radar,
                InherentModifier = 0,
            },
            Fuel = new(10),
            PowerPlant = new(60),
            Weapons = [
                new(){
                    ID = new(),
                    Name = "Pulse Laser",
                    DamageDice = 2,
                    Destructive = false,
                    WeaponBonus = 2,
                    Range = RangeBand.Long
                }
            ]
        };

        Assert.That(ship.Hull.Points, Is.EqualTo(10));
    }

    [Test]
    public void CanAttack()
    {
        Ship attacker = ShipCatalog.Scout;
        Ship defender = ShipCatalog.FarTrader;
        Crew gunner = SpacePortBar.Jimbo;

        var defenderStartingHullPoints = defender.Hull.Points;

        defender.SufferAttack(new Attack(attacker.Weapons[0], gunner, 0, new FakeRoller([6, 6])), new FakeRoller([6, 6]));
        Assert.That(defender.Hull.Points, Is.Not.EqualTo(defenderStartingHullPoints));
    }

    [Test]
    public void ShipsGetDestroyedAfterTakingTooMuchDamage()
    {
        Ship defender = ShipCatalog.Scout;
        Assert.That(defender.Destroyed, Is.False);
        defender.SufferAttack(new FakeAttack()
        {
            Damage = 2000,
            PotentialCriticalHit = false,
            Success = true,
        }, new FakeRoller([6, 6]));
        Assert.That(defender.Destroyed, Is.True);
    }

    [Test]
    public void ShipsTakeDamageFromCriticalHitsToHull()
    {
        Ship defender = ShipCatalog.Scout;
        defender.SufferAttack(new FakeAttack()
        {
            Damage = 6,
            PotentialCriticalHit = true,
            Success = true,
        }, new FakeRoller([7, 1]));
        Assert.That(defender.Hull.Points, Is.LessThan(38));
    }

    [Test]
    public void ShipsTakeCriticalHitsFromTooMuchDamage()
    {
        Ship defender = ShipCatalog.Scout;
        var damage = 10;
        var expectedHealthWithoutCrit = defender.Hull.Points - (damage - defender.Armour.Points);

        defender.SufferAttack(new FakeAttack()
        {
            Damage = damage,
            PotentialCriticalHit = false,
            Success = true,
        }, new FakeRoller([7, 1]));

        Assert.That(defender.Hull.Points, Is.LessThan(expectedHealthWithoutCrit));
    }


    [Test]
    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(4, 1)]
    [TestCase(5, 1)]
    [TestCase(6, 1)]
    public void ArmourCritsLowerArmourCorrectly(int newSeverity, int armourLoss)
    {
        Ship defender = ShipCatalog.Scout;
        defender.Armour = new(4, newSeverity - 1);
        var startingArmour = defender.Armour.Points;

        defender.SufferAttack(new FakeAttack()
        {
            Damage = 5,
            PotentialCriticalHit = true,
            Success = true,
        }, new FakeRoller([6, armourLoss, 1]));

        using (Assert.EnterMultipleScope())
        {
            Assert.That(defender.Armour.Points, Is.EqualTo(startingArmour - armourLoss));
            Assert.That(defender.Armour.CritSeverity, Is.EqualTo(newSeverity));
        }
    }


    [Test]
    [TestCase(2, false)]
    [TestCase(7, false)]
    [TestCase(8, true)]
    [TestCase(12, true)]
    public void SensorsFindNormalShips(int roll, bool shouldSucceed)
    {
        Ship searcher = ShipCatalog.Scout;
        Ship defender = ShipCatalog.FarTrader;

        Crew sensorOperator = SpacePortBar.Jimbo;
        var activeRadar = false;

        FakeRoller roller = new([roll]);
        var foundShip = searcher.Sensors.TryDetect(
            RangeBand.Medium.ThrustRequired(), 
            activeRadar, 
            sensorOperator, 
            roller, 
            defender, 
            searcher.TechLevel);

        Assert.That(foundShip, Is.EqualTo(shouldSucceed));
    }
}
