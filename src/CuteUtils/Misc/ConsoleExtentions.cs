using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable S3998 // Threads should not lock on objects with weak identity

namespace CuteUtils.Misc;

/// <summary>
/// <see cref="Console"/> Extensions
/// </summary>
public static class ConsoleExt
{
    /// <summary>
    /// Writes the specified value to the console with the specified color.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="color">The color of the text.</param>
    public static void Write(object value, ConsoleColor color)
    {
        lock (Console.Out)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(value);
            Console.ForegroundColor = oldColor;
        }
    }

    /// <summary>
    /// Writes the specified value to the console with the specified color and appends a new line.
    /// </summary>
    /// <param name="value">The value to write.</param>
    /// <param name="color">The color of the text.</param>
    public static void WriteLine(object value, ConsoleColor color)
    {
        lock (Console.Out)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ForegroundColor = oldColor;
        }
    }

    /// <summary>
    /// Reads the next line of characters from the standard input stream and tries to convert it to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to convert the input string to.</typeparam>
    /// <returns>The input string converted to the specified type.</returns>
    /// <exception cref="NotSupportedException">Thrown if the conversion is not supported.</exception>
    public static T ReadLine<T>()
    {
        string attemptedValue = Console.ReadLine() ?? string.Empty;
        Type type = typeof(T);
        TypeConverter converter = TypeDescriptor.GetConverter(type);

        return (T)converter.ConvertFromString(attemptedValue)!;
    }

    /// <summary>
    /// Reads the next line of characters from the standard input stream and tries to convert it to the specified type.
    /// </summary>
    /// <typeparam name="T">The type to convert the input string to.</typeparam>
    /// <param name="input">The input string converted to the specified type.</param>
    /// <returns><see langword="true"/> if the conversion was successful. Otherwise <see langword="false"/>.</returns>
    public static bool TryReadLine<T>([NotNullWhen(true)] out T? input)
    {
        string attemptedValue = Console.ReadLine() ?? string.Empty;
        Type type = typeof(T);
        TypeConverter converter = TypeDescriptor.GetConverter(type);
        if (converter != null && converter.IsValid(attemptedValue))
        {
            input = (T)converter.ConvertFromString(attemptedValue)!;
            return true;
        }
        else
        {
            input = default;
            return false;
        }
    }

    /// <summary>
    /// Obtains the next character or function key pressed by the user and converts it to the specified type.
    /// The pressed key is displayed in the console window.
    /// </summary>
    /// <typeparam name="T">The type to convert the input character to.</typeparam>
    /// <returns>The input character converted to the specified type.</returns>
    /// <exception cref="NotSupportedException">Thrown if the conversion is not supported.</exception>
    public static T ReadKey<T>()
    {
        string attemptedValue = Console.ReadKey().KeyChar.ToString();
        Type type = typeof(T);
        TypeConverter converter = TypeDescriptor.GetConverter(type);

        return (T)converter.ConvertFromString(attemptedValue)!;
    }

    /// <summary>
    /// Obtains the next character or function key pressed by the user and tries to convert it to the specified type.
    /// The pressed key is displayed in the console window.
    /// </summary>
    /// <param name="input">The input character converted to the specified type.</param>
    /// <typeparam name="T">The type to convert the input character to.</typeparam>
    /// <returns><see langword="true"/> if the conversion was successful. Otherwise <see langword="false"/>.</returns>
    public static bool TryReadKey<T>([NotNullWhen(true)] out T? input)
    {
        string attemptedValue = Console.ReadKey().KeyChar.ToString();
        Type type = typeof(T);
        TypeConverter converter = TypeDescriptor.GetConverter(type);
        if (converter != null && converter.IsValid(attemptedValue))
        {
            input = (T)converter.ConvertFromString(attemptedValue)!;
            return true;
        }
        else
        {
            input = default;
            return false;
        }
    }

    /// <summary>
    /// Suspends execution of the current method until the user presses a key.
    /// </summary>
    /// <param name="key">The key that has to be pressed.</param>
    /// <param name="message">The message that will be displayed.</param>
    public static void Pause(ConsoleKey key, string? message = null)
    {
        Console.WriteLine(message ?? $"Press {key} to continue...");
        ConsoleKey? consoleKey = null;
        while (consoleKey != key)
        {
            consoleKey = Console.ReadKey(true).Key;
        }
    }

    /// <summary>
    /// Suspends execution of the current method until the user presses a key.
    /// </summary>
    /// <param name="message">The message that will be displayed.</param>
    public static void Pause(string message = "Press any key to continue...")
    {
        Console.WriteLine(message);
        _ = Console.ReadKey(true);
    }
}
