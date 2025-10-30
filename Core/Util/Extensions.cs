using Core.Components;
namespace Core.Util;

internal static class Extensions
{
    public static Weapon Random(this List<Weapon> list, IRoller roller)
    {
        return list.First();
    }
    public static Crew Random(this List<Crew> list, IRoller roller)
    {
        return list.First();
    }
}
