using Core;
using Core.Catalog;

namespace Test.Core;

public class ShipTests
{
    [Test]
    public void CanCreateShip()
    {
        Ship ship = new(){
            Hull = new(){
                Points = 10,
            },
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

        Assert.That(ship.Hull.Points == 10);
    }

    [Test]
    public void CanAttack()
    {
        Ship attacker = Ships.Scout;
        Ship defender = Ships.FarTrader;

        var defenderStartingHullPoints = defender.Hull.Points;

        attacker.Weapons[0].Attack(defender, 4, new FakeRoller([6, 6]));

        Assert.That(defender.Hull.Points, Is.Not.EqualTo(defenderStartingHullPoints));
    }
}
