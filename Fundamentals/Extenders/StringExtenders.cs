// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text;
using System.Text.RegularExpressions;

namespace SquidEyes.Fundamentals;

public static partial class StringExtenders
{
    public static string WhitespaceToNull(this string? value) =>
        string.IsNullOrWhiteSpace(value) ? null! : value;

    public static string ToPlusAndDigits(this string value) =>
        "+" + string.Join("", value.Skip(1).Where(char.IsDigit));

    public static double ToDoubleOrNaN(this string value) =>
        string.IsNullOrWhiteSpace(value) ? double.NaN : double.Parse(value);

    public static string ToSingleLine(
        this string value, string separator = "; ", bool trimmed = true)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value?.Trim()!;

        return string.Join(separator, value.ToLines()
            .Select(v => trimmed ? v.Trim() : v));
    }

    public static string ToDelimitedString<T>(this List<T> items,
        Func<T, string>? getValue = null,
        string delimiter = ", ", string finalDelimiter = " or ")
    {
        if (!items.HasItems())
            throw new ArgumentOutOfRangeException(nameof(items));

        ArgumentNullException.ThrowIfNull(delimiter, nameof(delimiter));

        ArgumentNullException.ThrowIfNull(finalDelimiter, nameof(finalDelimiter));

        var sb = new StringBuilder();

        for (int i = 0; i < items.Count - 1; i++)
        {
            if (i > 0)
                sb.Append(delimiter);

            if (getValue == null)
                sb.Append(items[i]);
            else
                sb.Append(getValue(items[i]));
        }

        if (sb.Length > 0)
            sb.Append(finalDelimiter);

        sb.Append(items.Last());

        return sb.ToString();
    }

    public static List<string> ToLines(this string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        var reader = new StringReader(value);

        var lines = new List<string>();

        string? line;

        while ((line = reader.ReadLine()) != null)
            lines.Add(line.Trim());

        return lines;
    }

    public static string WithTrailingSlash(this string value)
    {
        return value.EndsWith(Path.DirectorySeparatorChar.ToString()) ?
            value : value + Path.DirectorySeparatorChar;
    }

    public static Stream ToStream(this string value)
    {
        var stream = new MemoryStream();

        var writer = new StreamWriter(stream, Encoding.UTF8, -1, true);

        writer.Write(value);

        writer.Flush();

        stream.Position = 0;

        return stream;
    }

    public static Stream ToStream(this byte[] value)
    {
        var stream = new MemoryStream();

        var writer = new BinaryWriter(stream);

        writer.Write(value);

        writer.Flush();

        stream.Position = 0;

        return stream;
    }

    public static List<string> Wrap(this string text, int margin)
    {
        int start = 0;

        int end;

        var lines = new List<string>();

        text = text.Trim();

        while ((end = start + margin) < text.Length)
        {
            while (text[end] != ' ' && end > start)
                end -= 1;

            if (end == start)
                end = start + margin;

            lines.Add(text[start..end]);

            start = end + 1;
        }

        if (start < text.Length)
            lines.Add(text[start..]);

        return lines;
    }

    public static HashSet<T> ToHashSetOf<T>(
        this string value, Func<string, T> getValue)
    {
        return new HashSet<T>(value.ToListOf(getValue));
    }

    public static List<T> ToListOf<T>(
        this string value, Func<string, T> getValue)
    {
        var items = new List<T>();

        if (!value.IsEmptyOrWhitespace())
        {
            foreach (var item in value.Split(','))
            {
                if (!item.IsEmptyOrWhitespace())
                    items.Add(getValue(item));
            }
        }

        return items;
    }

    public static bool InChars(this string value, string chars) =>
        value.All(chars.Contains);

    public static List<string> CamelCaseToWords(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        return CamelCaseParser().Matches(value)
            .OfType<Match>()
            .Select(m => m.Value)
            .ToList();
    }

    public static string ToBase64(this string value) =>
        Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

    public static string FromBase64(this string value) =>
        Encoding.UTF8.GetString(Convert.FromBase64String(value));

    [GeneratedRegex("(^[a-z]+|[A-Z]+(?![a-z])|[A-Z][a-z]+)")]
    private static partial Regex CamelCaseParser();
}