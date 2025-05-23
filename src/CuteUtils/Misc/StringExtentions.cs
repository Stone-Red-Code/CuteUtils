﻿using System.Globalization;
using System.Text;

namespace CuteUtils.Misc;

/// <summary>
/// <see cref="string"/> Extensions
/// </summary>
public static class StringExt
{
    /// <summary>
    /// Removes all invalid chars from the specified <see cref="string"/>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="allowSpaces"></param>
    /// <returns></returns>
    public static string ToFileName(this string str, bool allowSpaces = false)
    {
        char[] invalidChars = Path.GetInvalidFileNameChars();

        if (!allowSpaces)
        {
            str = str.Replace(" ", string.Empty);
        }

        foreach (char item in invalidChars)
        {
            str = str.Replace(item.ToString(), string.Empty);
        }

        string normalizedString = str.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();

        foreach (char c in normalizedString)
        {
            UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                _ = stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    /// <summary>
    /// Removes all invalid chars from the specified <see cref="string"/>
    /// </summary>
    /// <param name="str"></param>
    /// <param name="allowSpaces"></param>
    /// <returns></returns>
    public static string ToPath(this string str, bool allowSpaces = false)
    {
        char[] invalidChars = Path.GetInvalidPathChars();

        if (!allowSpaces)
        {
            str = str.Replace(" ", string.Empty);
        }

        foreach (char item in invalidChars)
        {
            str = str.Replace(item.ToString(), string.Empty);
        }

        string normalizedString = str.Normalize(NormalizationForm.FormD);
        StringBuilder stringBuilder = new StringBuilder();

        foreach (char c in normalizedString)
        {
            UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != UnicodeCategory.NonSpacingMark)
            {
                _ = stringBuilder.Append(c);
            }
        }

        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    /// <summary>
    /// Truncates a <see cref="string"/> to the specified length.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    public static string Truncate(this string str, int length)
    {
        if (str.Length > length && length > 0)
        {
            return str[..length];
        }

        return str;
    }

    /// <summary>
    /// Truncates a <see cref="string"/> to the specified length.
    /// </summary>
    /// <param name="str"></param>
    /// <param name="length"></param>
    /// <param name="ellipsis"></param>
    /// <returns></returns>
    public static string Truncate(this string str, int length, bool ellipsis)
    {
        if (str.Length > length && length > 0)
        {
            if (ellipsis && length > 3)
            {
                return $"{str[..(length - 3)]}...";
            }
            else
            {
                return str[..length];
            }
        }

        return str;
    }

    /// <summary>
    /// Uses the correct newline <see cref="string"/> defined for this environment.
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string CorrectNewLine(this string str)
    {
        if (Environment.OSVersion.Platform == PlatformID.Unix)
        {
            str = str.Replace("\r\n", "\n");
        }
        else
        {
            str = str.Replace("\n", "\r\n"); //Ik that this can produce wrong results
        }

        return str;
    }

    /// <summary>
    /// Removes all white spaces from the specified <see cref="string"/>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string RemoveWhitespaces(this string str)
    {
        StringBuilder result = new StringBuilder();
        foreach (char c in str)
        {
            if (!char.IsWhiteSpace(c))
            {
                _ = result.Append(c);
            }
        }
        return result.ToString();
    }

    /// <summary>
    /// Reverses the specified <see cref="string"/>
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string Reverse(this string str)
    {
        char[] array = str.ToCharArray();
        Array.Reverse(array);
        return new string(array);
    }
}