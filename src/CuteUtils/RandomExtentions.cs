namespace CuteUtils;

/// <summary>
/// <see cref="Random"/> Extensions
/// </summary>
public static class RandomExt
{
    public static T NextItem<T>(this Random random, IEnumerable<T> enumerable)
    {
        ArgumentNullException.ThrowIfNull(enumerable);

        return enumerable.ElementAt(random.Next(enumerable.Count()));
    }

    public static bool NextBool(this Random random)
    {
        return random.Next(2) == 0;
    }

    public static T NextEnum<T>(this Random random) where T : struct, Enum
    {
        T[] values = Enum.GetValues<T>();
        return values[random.Next(values.Length)];
    }

    public static T NextEnum<T>(this Random random, T[] values) where T : struct, Enum
    {
        return values[random.Next(values.Length)];
    }
}