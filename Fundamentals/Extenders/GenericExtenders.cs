// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public static class GenericExtenders
{
    public static bool SetOutThenReturn<T>(
        this bool state, out T instance, T value)
    {
        instance = value;

        return state;
    }

    public static T As<T>(this object value) => (T)value;

    public static bool HasMaskBits(this byte value, byte mask) =>
        (value & mask) == mask;

    public static bool HasMaskBits(this int value, int mask) =>
        (value & mask) == mask;

    public static List<T> ToListOf<T>(this T value) =>
        new([value]);

    public static HashSet<T> ToHashSetOf<T>(this T value) =>
        new(new List<T> { value });

    public static bool In<T>(this T value, params T[] values)
        where T : IEquatable<T>
    {
        values.MustBe().True(v => v.HasItems());

        return values.Contains(value);
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

    public static R Convert<T, R>(this T value, Func<T, R> getValue) => getValue(value);

    public static void Do<T>(this T value, Action<T> action) => action(value);

    public static T DoThenReturn<T>(this T value, Action action)
    {
        action();

        return value;
    }
}