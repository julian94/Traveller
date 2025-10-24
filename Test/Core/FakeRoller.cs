namespace Test.Core;
internal class FakeRoller(IEnumerable<int> rolls) : IRoller
{
    public int D3() => rolls.Take(1).First();
    public int Roll(int dice, int bonus = 0) => rolls.Take(1).First();
    public RollResult Roll(int dice, int bonus, int target) => new(Roll(dice, bonus), target);
}
