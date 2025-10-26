namespace Test.Core;
internal class FakeRoller(List<int> rolls) : IRoller
{
    private int _roll;
    public int D3() => rolls[_roll++];
    public int Roll(int dice, int bonus = 0) => rolls[_roll++] + bonus;
    public RollResult Roll(int dice, int bonus, int target) => new(Roll(dice, bonus), target);
}

file class FakeRollerTests
{
    [Test]
    public void FakeRollerIncrementsProperly()
    {
        List<int> rolls = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        FakeRoller roller = new(rolls);
        for (int i = 0; i < rolls.Count; i++)
        {
            Assert.That(roller.Roll(1), Is.EqualTo(rolls[i]));
        }
    }
}
