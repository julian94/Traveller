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
}
