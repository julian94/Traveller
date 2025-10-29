namespace Core;

public class Roller : IRoller
{
    private readonly Random _random = new();

    public int D3() => _random.Next(1, 3);

    private int Roll() => _random.Next(1, 6);

    public int Roll(int dice, int bonus = 0)
    {
        var result = 0;
        for (int i = 0; i < dice; i++)
        {
            result += Roll();
        }
        return result + bonus;
    }
    public RollResult Roll(int dice, int bonus, int target) => new(Roll(dice, bonus), target);
}

public interface IRoller
{
    public int D3();
    public int Roll(int dice, int bonus = 0);

    public RollResult Roll(int dice, int bonus, int target);
}

public class RollResult(int result, int target)
{
    public bool Success => Effect >= 0;
    public int Result { get; init; } = result;
    public int Effect { get; init; } = result - target;
}
