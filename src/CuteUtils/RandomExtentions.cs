namespace CuteUtils;

/// <summary>
/// <see cref="Random"/> Extensions
/// </summary>
public static class RandomExt
{
    /// <summary>
    /// Returns a random item from the specified enumerable.
    /// </summary>
    /// <typeparam name="T">The type of the items in the enumerable.</typeparam>
    /// <param name="random">The random number generator.</param>
    /// <param name="enumerable">The enumerable to select a random item from.</param>
    /// <returns>A random item from the enumerable.</returns>
    public static T NextItem<T>(this Random random, IEnumerable<T> enumerable)
    {
        ArgumentNullException.ThrowIfNull(enumerable);

        return enumerable.ElementAt(random.Next(enumerable.Count()));
    }

    /// <summary>
    /// Returns a random boolean value.
    /// </summary>
    /// <param name="random">The random number generator.</param>
    /// <returns>A random boolean value.</returns>
    public static bool NextBool(this Random random)
    {
        return random.Next(2) == 0;
    }

    /// <summary>
    /// Returns a random value from the specified enum type.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <param name="random">The random number generator.</param>
    /// <returns>A random value from the enum type.</returns>
    public static T NextEnum<T>(this Random random) where T : struct, Enum
    {
        T[] values = Enum.GetValues<T>();
        return values[random.Next(values.Length)];
    }

    /// <summary>
    /// Returns a random value from the specified array of enum values.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <param name="random">The random number generator.</param>
    /// <param name="values">The array of enum values.</param>
    /// <returns>A random value from the array of enum values.</returns>
    public static T NextEnum<T>(this Random random, T[] values) where T : struct, Enum
    {
        return values[random.Next(values.Length)];
    }
}
