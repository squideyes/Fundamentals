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

        if (value.Length == 1)
            return char.IsAsciiLetterUpper(value[0]);

        if (value.Length > 16)
            return false;

        if (!char.IsAsciiLetterUpper(value[0]))
            return false;

        static bool IsTokenChar(char c)
        {
            return c switch
            {
                '1' or 'I' or 'i' => false,
                '0' or 'O' or 'i' => false,
                _ => char.IsAsciiLetterOrDigit(c)
            };
        };

        return value.Skip(1).All(IsTokenChar);
    }

    public static bool IsConfigKeyValue(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        var fields = value.Split(':');

        if (!fields.Length.Between(1, 4))
            return false;

        return !fields.Any(v => !v.IsTokenValue());
    }

    public static string ToString<T>(
        this T value, Func<T, string> getString = null!)
    {
        if (getString is null)
            return value!.ToString()!;
        else
            return getString(value!);
    }
}