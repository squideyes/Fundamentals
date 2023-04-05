// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Globalization;
using System.Runtime.InteropServices;

namespace SquidEyes.Fundamentals;

public static class DateTimeExtenders
{
    private static readonly TimeZoneInfo eastern =
        GetTimeZoneInfo("Eastern Standard Time", "America/New_York");

    private static readonly TimeZoneInfo central =
        GetTimeZoneInfo("Central Standard Time", "America/Chicago");

    private static readonly TimeZoneInfo mountain =
        GetTimeZoneInfo("Mountain Standard Time", "America/Denver");

    private static readonly TimeZoneInfo pacific =
        GetTimeZoneInfo("Pacific Standard Time", "America/Los_Angeles");

    public static DateTime ToEasternFromUtc(this DateTime value) =>
        value.ToZoneFromUtc(eastern);

    public static DateTime ToCentralFromUtc(this DateTime value) =>
        value.ToZoneFromUtc(central);

    public static DateTime ToMountainFromUtc(this DateTime value) =>
        value.ToZoneFromUtc(mountain);

    public static DateTime ToPacificFromUtc(this DateTime value) =>
        value.ToZoneFromUtc(pacific);

    public static DateTime ToUtcFromEastern(this DateTime value) =>
        value.ToUtcFromZone(eastern);

    public static DateTime ToUtcFromCentral(this DateTime value) =>
        value.ToUtcFromZone(central);

    public static DateTime ToUtcFromMountain(this DateTime value) =>
        value.ToUtcFromZone(mountain);

    public static DateTime ToUtcFromPacific(this DateTime value) =>
        value.ToUtcFromZone(pacific);

    private static DateTime ToZoneFromUtc(this DateTime value, TimeZoneInfo tzi)
    {
        value.Kind.Must().Be(v => v == DateTimeKind.Utc);

        return TimeZoneInfo.ConvertTimeFromUtc(value, tzi);
    }

    public static string ToDayName(this DateTime date)
    {
        return date.Day switch
        {
            1 or 21 or 31 => date.Day + "st",
            2 or 22 => date.Day + "nd",
            3 or 23 => date.Day + "rd",
            _ => date.Day + "th"
        };
    }

    public static string ToMonthName(this DateTime date) =>
        CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(date.Month);

    private static DateTime ToUtcFromZone(this DateTime value, TimeZoneInfo tzi)
    {
        value.Kind.Must().Be(v => v == DateTimeKind.Unspecified);

        return TimeZoneInfo.ConvertTimeToUtc(value, tzi);
    }

    public static bool OnInterval(this DateTime value, int seconds)
    {
        seconds.Must().BePositive();

        return value.Ticks % TimeSpan.FromSeconds(seconds).Ticks == 0L;
    }

    private static TimeZoneInfo GetTimeZoneInfo(string windowsId, string linuxId)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return TimeZoneInfo.FindSystemTimeZoneById(windowsId);
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return TimeZoneInfo.FindSystemTimeZoneById(linuxId);
        else
            throw new InvalidOperationException("Only works on Linux and Windows");
    }
}