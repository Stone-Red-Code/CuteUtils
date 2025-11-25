using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace CuteUtils.Misc;

/// <summary>
/// Provides extension methods for working with <see cref="string"/> values.
/// </summary>
public static class StringExt
{
    internal static string Sanitize(string str, char[] invalidChars, bool allowSpaces)
    {
        if (!allowSpaces)
        {
            str = str.Replace(" ", string.Empty);
        }

        foreach (char c in invalidChars)
        {
            str = str.Replace(c.ToString(), string.Empty);
        }

        return str.RemoveDiacritics();
    }

    /// <summary>
    /// Converts the string into a valid file name by removing invalid characters
    /// and optionally removing spaces. Diacritics (accents) are also removed.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="allowSpaces">Whether spaces should be preserved.</param>
    /// <returns>A sanitized string that is safe to use as a file name.</returns>
    public static string ToFileName(this string str, bool allowSpaces = false)
    {
        string path = Sanitize(str, Path.GetInvalidFileNameChars(), allowSpaces);

        if (path.Length > 255)
        {
            path = path[..255];
        }

        return path;
    }

    /// <summary>
    /// Converts the string into a valid file system path segment by removing invalid characters
    /// and optionally removing spaces. Diacritics (accents) are also removed.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="allowSpaces">Whether spaces should be preserved.</param>
    /// <returns>A sanitized string that is safe to use as a path segment.</returns>
    public static string ToPath(this string str, bool allowSpaces = false)
    {
        return Sanitize(str, Path.GetInvalidPathChars(), allowSpaces);
    }

    /// <summary>
    /// Returns a truncated version of the string with a maximum length.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="length">The maximum allowed length.</param>
    /// <returns>
    /// The truncated string if the input exceeds <paramref name="length"/>;
    /// otherwise, the original string.
    /// </returns>
    public static string Truncate(this string str, int length)
    {
        return (length > 0 && str.Length > length) ? str[..length] : str;
    }

    /// <summary>
    /// Returns a truncated version of the string with a maximum length,
    /// optionally appending an ellipsis ("...") if the string is shortened.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <param name="length">The maximum allowed length.</param>
    /// <param name="ellipsis">Whether to append "..." when truncated.</param>
    /// <returns>The truncated string, with optional ellipsis.</returns>
    public static string Truncate(this string str, int length, bool ellipsis)
    {
        if (length <= 0 || str.Length <= length)
        {
            return str;
        }

        if (ellipsis && length > 3)
        {
            return $"{str[..(length - 3)]}...";
        }

        return str[..length];
    }

    /// <summary>
    /// Normalizes newline characters in the string to the current environment's newline format.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <returns>
    /// A string where all newline sequences are converted to
    /// <see cref="Environment.NewLine"/>.
    /// </returns>
    /// <remarks>
    /// This method safely normalizes mixed newline styles (CR, LF, CRLF).
    /// </remarks>
    public static string CorrectNewLine(this string str)
    {
        return str
            .Replace("\r\n", "\n")
            .Replace("\r", "\n")
            .Replace("\n", Environment.NewLine);
    }

    /// <summary>
    /// Removes all whitespace characters from the string.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <returns>The string with all whitespace removed.</returns>
    public static string RemoveWhitespaces(this string str)
    {
        return new string(str.Where(c => !char.IsWhiteSpace(c)).ToArray());
    }

    /// <summary>
    /// Returns a new string with the characters reversed.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <returns>The reversed string.</returns>
    public static string Reverse(this string str)
    {
        char[] arr = str.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }

    /// <summary>
    /// Replaces all occurrences of a substring with another string,
    /// using a case-insensitive comparison.
    /// </summary>
    /// <param name="input">The input string.</param>
    /// <param name="search">The substring to search for.</param>
    /// <param name="replacement">The replacement text.</param>
    /// <returns>The modified string.</returns>
    public static string ReplaceCaseInsensitive(this string input, string search, string replacement)
    {
        return Regex.Replace(input, Regex.Escape(search), replacement.Replace("$", "$$"), RegexOptions.IgnoreCase);
    }

    /// <summary>
    /// Removes diacritic marks (accents) from characters in the string.
    /// </summary>
    /// <param name="str">The input string.</param>
    /// <returns>The string with diacritics removed.</returns>
    /// <remarks>
    /// This is useful for normalization and for generating file-safe names.
    /// </remarks>
    public static string RemoveDiacritics(this string str)
    {
        string normalized = str.Normalize(NormalizationForm.FormD);
        StringBuilder sb = new StringBuilder(normalized.Length);

        foreach (char c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                _ = sb.Append(c);
            }
        }

        return sb.ToString().Normalize(NormalizationForm.FormC);
    }
}
