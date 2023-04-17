// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.RegularExpressions;
using static System.DayOfWeek;

namespace SquidEyes.Fundamentals;

public static partial class ValidationExtenders
{
    private static readonly PhoneNumbers.PhoneNumberUtil pnu =
        PhoneNumbers.PhoneNumberUtil.GetInstance();

    private static readonly Regex dnsNameValidator = GetDnsNameValidator();

    private static readonly Dictionary<string, Regex> regexes = new();

    public static bool IsTokenValue(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!char.IsAsciiLetterUpper(value[0]))
            return false;

        if (value.Length == 1)
            return true;

        if (value.Length > 16)
            return false;

        return value.Skip(1).All(char.IsAsciiLetterOrDigit);
    }

    public static bool IsConfigKeyValue(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        var fields = value.Split(':');

        if (!fields.Length.IsBetween(1, 4))
            return false;

        return !fields.Any(v => !v.IsTokenValue());
    }

    public static bool IsEmptyOrTrimmed(this string value) =>
        value is not null && (value == "" || value.IsTrimmed());

    public static bool IsNonEmptyAndTrimmed(this string value) =>
        !string.IsNullOrEmpty(value) && value.IsTrimmed();

    private static bool IsTrimmed(this string value) =>
        !char.IsWhiteSpace(value[0]) && !char.IsWhiteSpace(value[^1]);
    public static bool IsDate(this DateTime value) =>
        value.TimeOfDay == TimeSpan.Zero;

    public static bool IsWeekday(this DateOnly date) =>
        date.DayOfWeek.IsWeekday();

    public static bool IsWeekend(this DateOnly date) =>
        date.DayOfWeek.IsWeekend();

    public static bool IsWeekday(this DayOfWeek value) =>
        value >= Monday && value <= Friday;

    public static bool IsWeekend(this DayOfWeek value) =>
        value == Saturday || value == Sunday;

    public static bool IsDefault<T>(this T value) =>
        Equals(value, default(T));

    public static bool IsNotEmpty<K, V>(this IDictionary<K, V> value)
    {
        return value is not null && value.Any() && !value
            .Any(v => v.Key.IsDefault() || v.Value.IsDefault());
    }

    public static bool IsNotEmpty<T>(this List<T> value)
    {
        return value is not null && value.Any() && !value
            .Any(v => v.IsDefault() || v.IsDefault());
    }

    public static bool IsUnique<T>(this IEnumerable<T> values) =>
        values.All(new HashSet<T>().Add);

    public static bool IsBetween<T>(this T value, T minValue, T maxValue, bool inclusive = true)
        where T : IComparable
    {
        if (maxValue.CompareTo(minValue) < 0)
            throw new ArgumentOutOfRangeException(nameof(maxValue));

        if (inclusive)
            return (value.CompareTo(minValue) >= 0) && (value.CompareTo(maxValue) <= 0);
        else
            return (value.CompareTo(minValue) > 0) && (value.CompareTo(maxValue) < 0);
    }

    public static bool IsEnumValue<T>(this T value)
        where T : struct, Enum
    {
        return Enum.IsDefined(value);
    }

    public static bool HasFlags(this Enum value) => value.GetType()
        .GetCustomAttributes(typeof(FlagsAttribute), false).Any();

    public static bool IsPhoneNumber(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!value.StartsWith("+") && value.Length > 2)
            return false;

        try
        {
            return pnu.IsValidNumber(pnu.Parse(value, "US"));
        }
        catch
        {
            return false;
        }
    }

    public static bool IsEmptyOrWhitespace(this string? value)
    {
        if (value == null)
            return false;

        if (value.Length == 0)
            return true;

        return value.Any(char.IsWhiteSpace);
    }

    public static bool IsRegexMatch(
        this string value, string pattern, RegexOptions options)
    {
        if (regexes.TryGetValue(pattern!, out Regex? regex))
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

    public static bool IsGuid(this string value) =>
        Guid.TryParse(value, out _);

    public static bool IsDnsName(this string value) =>
        dnsNameValidator.IsMatch(value);

    public static bool IsBlobName(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (value.Length < 1 || value.Length > 255)
            return false;

        if (value.StartsWith('/') || value.EndsWith('/'))
            return false;

        foreach (var part in value.Split('/'))
        {
            if (part.Length == 0)
                return false;

            if (!part.All(char.IsAsciiLetterOrDigit))
                return false;
        }

        return true;
    }

    [GeneratedRegex("^[a-z0-9](?!.*--)[a-z0-9-]{1,61}[a-z0-9]$", RegexOptions.Compiled)]
    private static partial Regex GetDnsNameValidator();
}