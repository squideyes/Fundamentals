// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

internal static partial class MiscExtenders
{
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
}