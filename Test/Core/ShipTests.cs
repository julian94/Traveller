using Core;

namespace Test.Core;

public class ShipTests
{
    [Test]
    public void CanCreateShip()
    {
        Ship ship = new(){
            Hull = new(){
                Points = 10,
                Armour = new(){
                    Points = 2,
                }
            },
            Weapons = [
                new(){
                    ID = new(),
                    DamageDice = 2,
                    Destructive = false,
                }
            ]
        };

        Assert.That(ship.Hull.Points == 10);
    }

    [Test]
    public void CanAttack()
    {
        Ship attacker = new()
        {
            Hull = new()
            {
                Points = 10,
                Armour = new()
                {
                    Points = 2,
                }
            },
            Weapons = [
                new(){
                    ID = new(),
                    DamageDice = 2,
                    Destructive = false,
                }
            ]
        };
        Ship defender = new()
        {
            Hull = new()
            {
                Points = 10,
                Armour = new()
                {
                    Points = 2,
                }
            },
            Weapons = [
                new(){
                    ID = new(),
                    DamageDice = 2,
                    Destructive = false,
                }
            ]
        };

        Assert.That(defender.Hull.Points, Is.EqualTo(10));


        var weapon = attacker.Weapons[0];

        FakeRoller roller = new([6, 6, 6, 6, 6, 6]);
        weapon.Attack(defender, 4, roller);

        Assert.That(defender.Hull.Points, Is.Not.EqualTo(10));
    }
}
