using Core.Components;
namespace Core.Util;

internal static class Extensions
{
    public static Weapon Random(this IEnumerable<Weapon> enumerable)
    {
        return enumerable.First();
    } 
}
