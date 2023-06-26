// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Globalization;

namespace SquidEyes.Fundamentals;

public static class DateOnlyExtenders
{
    private static readonly DateTime epoch =
        new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static int ToUnixTime(this DateTime value) =>
        (int)value.Subtract(epoch).TotalSeconds;

    public static string ToDayName(this DateOnly date)
    {
        return date.Day switch
        {
            1 or 21 or 31 => date.Day + "st",
            2 or 22 => date.Day + "nd",
            3 or 23 => date.Day + "rd",
            _ => date.Day + "th"
        };
    }

    public static string ToMonthName(this DateOnly date) =>
        CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(date.Month);
}