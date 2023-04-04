// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Numerics;

namespace SquidEyes.Fundamentals;

public static class GenericExtenders
{
    public static bool HasMaskBits(this byte value, byte mask) =>
        (value & mask) == mask;

    public static bool HasMaskBits(this int value, int mask) =>
        (value & mask) == mask;

    public static bool IsDefault<T>(this T value) =>
        Equals(value, default(T));

    public static List<T> ToListOf<T>(this T value) =>
        new(new List<T> { value });

    public static HashSet<T> ToHashSetOf<T>(this T value) =>
        new(new List<T> { value });

    public static bool In<T>(this T value, params T[] values)
        where T : IEquatable<T>
    {
        values.Must().Be(v => v.HasItems());

        return values.Contains(value);
    }

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

    public static string ToString<T>(
        this T value, Func<T, string> getString = null!)
    {
        if (getString is null)
            return value!.ToString()!;
        else
            return getString(value!);
    }

    public static void DoIfCanDo<T>(this T value, Func<T, bool> canDo, Action<T> @do)
    {
        if (canDo(value))
            @do(value);
    }

    public static bool IsTrue<T>(this T value)
          where T : INumber<T>
    {
        return value != T.Zero;
    }

    public static bool IsFalse<T>(this T value)
        where T : INumber<T>
    {
        return value == T.Zero;
    }

    public static R Get<T, R>(this T value, Func<T, R> func) => func(value);

    public static void Act<T>(this T value, Action<T> action) => action(value);
}