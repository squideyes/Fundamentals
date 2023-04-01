// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text;
using System.Text.RegularExpressions;

namespace SquidEyes.Fundamentals;

public static partial class StringExtenders
{
    private static readonly Dictionary<string, Regex> regexes = new();

    public static bool IsEmptyOrWhitespace(this string? value)
    {
        if (value == null)
            return false;

        if (value.Length == 0)
            return true;

        return value.Any(char.IsWhiteSpace);
    }

    public static bool IsMatch(
        this string value, string pattern, RegexOptions options)
    {
        if (regexes.TryGetValue(pattern!, out Regex regex))
            return regex.IsMatch(value);

        if (!pattern.IsRegexPattern())
            throw new ArgumentOutOfRangeException(nameof(pattern));

        options |= RegexOptions.Compiled;
        options |= RegexOptions.NonBacktracking;

        regex = new Regex(pattern, options);

        regexes.Add(pattern, regex);

        return regex.IsMatch(value);
    }

    public static bool IsRegexPattern(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        var regex = new Regex(value, RegexOptions.None,
            TimeSpan.FromMilliseconds(100));

        try
        {
            regex.IsMatch("");

            return true;
        }
        catch
        {
            return false;
        }
    }

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

    public static bool IsGuid(this string value) =>
        Guid.TryParse(value, out _);

    public static bool IsNonEmptyAndTrimmed(this string value)
    {
        return !string.IsNullOrWhiteSpace(value)
            && !char.IsWhiteSpace(value[0])
            && !char.IsWhiteSpace(value[^1]);
    }

    public static string WithTrailingSlash(this string value)
    {
        return value.EndsWith(Path.DirectorySeparatorChar.ToString()) ?
            value : value + Path.DirectorySeparatorChar;
    }

    public static bool EnsurePathExists(this string value, bool valueIsPath)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentOutOfRangeException(nameof(value));

            var path = value;

            if (!valueIsPath)
                path = Path.GetDirectoryName(path)!;

            path = Path.GetFullPath(path.WithTrailingSlash());

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool IsFolderName(this string value, bool mustBeRooted = true)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        try
        {
            _ = new DirectoryInfo(value);

            if (!mustBeRooted)
                return true;
            else
                return Path.IsPathRooted(value);
        }
        catch (ArgumentException)
        {
            return false;
        }
        catch (PathTooLongException)
        {
            return false;
        }
        catch (NotSupportedException)
        {
            return false;
        }
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

    public static bool IsFileName(this string value, bool mustBeRooted = true)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;

            var dummy = new FileInfo(value);

            return !mustBeRooted || Path.IsPathRooted(value);
        }
        catch
        {
            return false;
        }
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