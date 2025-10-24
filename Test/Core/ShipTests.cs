namespace Test.Core;

public class ShipTests
{
    [Test]
    public void CanCreateShip()
    {
        Ship ship = new(){
            Hull = new(10),
            Armour = new()
            {
                Points = 2,
            },
            Weapons = [
                new(){
                    ID = new(),
                    Name = "Pulse Laser",
                    DamageDice = 2,
                    Destructive = false,
                    WeaponBonus = 2,
                }
            ]
        };

        Assert.That(ship.Hull.Points, Is.EqualTo(10));
    }

    [Test]
    public void CanAttack()
    {
        Ship attacker = Ships.Scout;
        Ship defender = Ships.FarTrader;

        var defenderStartingHullPoints = defender.Hull.Points;

        defender.SufferAttack(new Attack(attacker.Weapons[0], 0, new FakeRoller([6, 6])), new FakeRoller([6, 6]));
        Assert.That(defender.Hull.Points, Is.Not.EqualTo(defenderStartingHullPoints));
    }

    [Test]
    public void ShipsGetDestroyedAfterTakingTooMuchDamage()
    {
        Ship defender = Ships.Scout;
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
        Ship defender = Ships.Scout;
        defender.SufferAttack(new FakeAttack()
        {
            Damage = 6,
            PotentialCriticalHit = true,
            Success = true,
        }, new FakeRoller([7, 1]));
        Assert.That(defender.Hull.Points, Is.LessThan(38));
    }
}
