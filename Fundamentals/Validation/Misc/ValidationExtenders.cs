//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

//using System.Text.Json;
//using System.Text.RegularExpressions;
//using static System.DayOfWeek;

//namespace SquidEyes.Fundamentals;

//public static partial class ValidationExtenders
//{
//    private static readonly PhoneNumbers.PhoneNumberUtil pnu =
//        PhoneNumbers.PhoneNumberUtil.GetInstance();

//    private static readonly Regex dnsNameValidator = GetDnsNameValidator();

//    private static readonly Regex cosmosNameValidator = 
//        GetCosmosNameValidator();

//    private static readonly Dictionary<string, Regex> regexes = new();

//    public static bool HasItems<T>(
//        this IEnumerable<T> items, bool nonDefault = true)
//    {
//        return items.HasItems(1, int.MaxValue,
//            v => !nonDefault || !Equals(v, default(T)));
//    }

//    public static bool HasItems<T>(
//        this IEnumerable<T> items, Func<T, bool>? isValid)
//    {
//        return items.HasItems(1, int.MaxValue, isValid);
//    }

//    public static bool HasItems<T>(this IEnumerable<T> items,
//        int minItems, int maxItems, bool nonDefault = true)
//    {
//        return items.HasItems(minItems, maxItems,
//            v => !nonDefault || !Equals(v, default(T)));
//    }

//    public static bool HasItems<T>(this IEnumerable<T> items,
//        int minItems, int maxItems, Func<T, bool>? isValid)
//    {
//        if (minItems < 1)
//            throw new ArgumentOutOfRangeException(nameof(minItems));

//        if (maxItems < minItems)
//            throw new ArgumentOutOfRangeException(nameof(maxItems));

//        int count = 0;

//        foreach (var item in items)
//        {
//            if (isValid != null && !isValid(item))
//                return false;

//            if (++count > maxItems)
//                return false;
//        }

//        return count >= minItems;
//    }

//    public static bool IsFlagsValue(this Enum value)
//    {
//        switch (Type.GetTypeCode(
//            Enum.GetUnderlyingType(value.GetType())))
//        {
//            case TypeCode.Int16:
//            case TypeCode.Int32:
//            case TypeCode.Int64:
//            case TypeCode.SByte:
//            case TypeCode.Single:
//                if (Convert.ToInt64(value) < 0)
//                    throw new ArgumentOutOfRangeException(nameof(value));
//                break;
//            default:
//                break;
//        }

//        ulong flags = Convert.ToUInt64(value);

//        var values = Enum.GetValues(value.GetType());

//        int count = values.Length - 1;

//        ulong initialFlags = flags;

//        ulong flag = 0;

//        while (count >= 0)
//        {
//            flag = Convert.ToUInt64(values.GetValue(count));

//            if ((count == 0) && (flag == 0))
//                break;

//            if ((flags & flag) == flag)
//            {
//                flags -= flag;

//                if (flags == 0)
//                    return true;
//            }

//            count--;
//        }

//        if (flags != 0)
//            return false;

//        if (initialFlags != 0 || flag == 0)
//            return true;

//        return false;
//    }

//    public static bool IsMultiTagValue(this string value)
//    {
//        if (string.IsNullOrWhiteSpace(value))
//            return false;

//        var fields = value.Split(':');

//        if (!fields.Length.IsBetween(1, MultiTag.MaxTags))
//            return false;

//        return fields.All(Tag.IsValue);
//    }

//    public static bool IsEmptyOrTrimmed(this string value) =>
//        value is not null && (value == "" || value.IsNonEmptyAndTrimmed());

//    public static bool IsNullOrTrimmed(this string value) =>
//        value is null || value!.IsNonEmptyAndTrimmed();

//    public static bool IsTrimmed(this string value, bool mayBeEmpty = false)
//    {
//        if (value is null)
//            return false;

//        if (string.IsNullOrEmpty(value))
//            return mayBeEmpty;

//        return value.IsNonEmptyAndTrimmed();
//    }

//    private static bool IsNonEmptyAndTrimmed(this string value)
//    {
//        return value is not null
//            && value.Length >= 1
//            && !char.IsWhiteSpace(value[0])
//            && !char.IsWhiteSpace(value[^1]);
//    }

//    public static bool IsDate(this DateTime value) =>
//        value.TimeOfDay == TimeSpan.Zero;

//    public static bool IsWeekday(this DateTime date) =>
//        date.DayOfWeek.IsWeekday();

//    public static bool IsWeekend(this DateTime date) =>
//        date.DayOfWeek.IsWeekend();

//    public static bool IsWeekday(this DateOnly date) =>
//        date.DayOfWeek.IsWeekday();

//    public static bool IsWeekend(this DateOnly date) =>
//        date.DayOfWeek.IsWeekend();

//    public static bool IsWeekday(this DayOfWeek value) =>
//        value >= Monday && value <= Friday;

//    public static bool IsWeekend(this DayOfWeek value) =>
//        value == Saturday || value == Sunday;

//    public static bool IsDefault<T>(this T value) =>
//        Equals(value, default(T));

//    public static bool IsNotEmpty<K, V>(this IDictionary<K, V> value)
//    {
//        return value is not null && value.Any() && !value
//            .Any(v => v.Key.IsDefault() || v.Value.IsDefault());
//    }

//    public static bool IsNotEmpty<T>(this List<T> value)
//    {
//        return value is not null && value.Any() && !value
//            .Any(v => v.IsDefault() || v.IsDefault());
//    }

//    public static bool IsUnique<T>(this IEnumerable<T> values) =>
//        values.All(new HashSet<T>().Add);

//    public static bool IsBetween<T>(this T value, T minValue, T maxValue, bool inclusive = true)
//        where T : IComparable
//    {
//        if (maxValue.CompareTo(minValue) < 0)
//            throw new ArgumentOutOfRangeException(nameof(maxValue));

//        if (inclusive)
//            return (value.CompareTo(minValue) >= 0) && (value.CompareTo(maxValue) <= 0);
//        else
//            return (value.CompareTo(minValue) > 0) && (value.CompareTo(maxValue) < 0);
//    }

//    public static bool IsEnumValue<T>(this T value)
//        where T : struct, Enum
//    {
//        return Enum.IsDefined(value);
//    }

//    public static bool HasFlags(this Enum value) => value.GetType()
//        .GetCustomAttributes(typeof(FlagsAttribute), false).Any();

//    public static bool IsPhoneNumber(this string value)
//    {
//        if (string.IsNullOrWhiteSpace(value))
//            return false;

//        if (!value.StartsWith("+") && value.Length > 2)
//            return false;

//        try
//        {
//            return pnu.IsValidNumber(pnu.Parse(value, "US"));
//        }
//        catch
//        {
//            return false;
//        }
//    }

//    public static bool IsEmptyOrWhitespace(this string? value)
//    {
//        if (value == null)
//            return false;

//        if (value.Length == 0)
//            return true;

//        return value.Any(char.IsWhiteSpace);
//    }

//    public static bool IsRegexMatch(
//        this string value, string pattern, RegexOptions options)
//    {
//        if (regexes.TryGetValue(pattern!, out Regex? regex))
//            return regex.IsMatch(value);

//        if (!pattern.IsRegexPattern())
//            throw new ArgumentOutOfRangeException(nameof(pattern));

//        options |= RegexOptions.Compiled;
//        options |= RegexOptions.NonBacktracking;

//        regex = new Regex(pattern, options);

//        regexes.Add(pattern, regex);

//        return regex.IsMatch(value);
//    }

//    public static bool IsRegexPattern(this string value)
//    {
//        if (string.IsNullOrEmpty(value))
//            return false;

//        var regex = new Regex(value, RegexOptions.None,
//            TimeSpan.FromMilliseconds(100));

//        try
//        {
//            regex.IsMatch("");

//            return true;
//        }
//        catch
//        {
//            return false;
//        }
//    }

//    public static bool IsJson(this string json)
//    {
//        if (string.IsNullOrWhiteSpace(json))
//            return false;

//        try
//        {
//            using var jsonDoc = JsonDocument.Parse(json);

//            return true;
//        }
//        catch (JsonException)
//        {
//            return false;
//        }
//    }

//    public static bool IsGuid(this string value) =>
//        Guid.TryParse(value, out _);

//    public static bool IsDnsName(this string value) =>
//        dnsNameValidator.IsMatch(value);

//    public static bool IsComosName(this string value) =>
//        cosmosNameValidator.IsMatch(value);

//    public static bool IsBlobName(this string value)
//    {
//        if (string.IsNullOrWhiteSpace(value))
//            return false;

//        if (value.Length < 1 || value.Length > 255)
//            return false;

//        if (value.StartsWith('/') || value.EndsWith('/'))
//            return false;

//        foreach (var part in value.Split('/'))
//        {
//            if (part.Length == 0)
//                return false;

//            if (!part.All(char.IsAsciiLetterOrDigit))
//                return false;
//        }

//        return true;
//    }

//    [GeneratedRegex("^[a-z0-9](?!.*--)[a-z0-9-]{1,61}[a-z0-9]$")]
//    private static partial Regex GetDnsNameValidator();

//    [GeneratedRegex("^[A-Za-z0-9](?!.*--)[A-Za-z0-9-]{1,41}[A-Za-z0-9]$")]
//    private static partial Regex GetCosmosNameValidator();
//}