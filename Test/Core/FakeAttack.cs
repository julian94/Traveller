namespace Test.Core;
internal class FakeAttack : IAttack
{
    public bool Success { get; init; }
    public bool PotentialCriticalHit { get; init; }
    public int Damage { get; init; }
}
