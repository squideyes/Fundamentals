// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Diagnostics;
using System.Numerics;

namespace SquidEyes.Fundamentals;

public static class INumberValidators
{
    [DebuggerHidden]
    public static T Zero<T>(this MustBe<T> m)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v == T.Zero,
            v => "be zero");
    }

    [DebuggerHidden]
    public static T Zero<T>(this MayNotBe<T> m)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v == T.Zero,
            v => "be zero");
    }

    [DebuggerHidden]
    public static T GreaterThan<T>(this MustBe<T> m, T value)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v > value,
            v => $"be greater than {value}");
    }

    [DebuggerHidden]
    public static T GreaterThanOrEqualTo<T>(this MustBe<T> m, T value)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v >= value,
            v => $"be greater than or equal to{value}");
    }

    [DebuggerHidden]
    public static T LessThan<T>(this MustBe<T> m, T value)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v < value,
            v => $"be less than {value}");
    }

    [DebuggerHidden]
    public static T LessThanOrEqualTo<T>(this MustBe<T> m, T value)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v <= value,
            v => $"be less than or equal to{value}");
    }

    [DebuggerHidden]
    public static T Positive<T>(this MustBe<T> m)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v > T.Zero,
            v => "be greater than zero");
    }

    [DebuggerHidden]
    public static T PositiveOrZero<T>(this MustBe<T> m)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v >= T.Zero,
            v => "be greater than or equal to zero");
    }

    [DebuggerHidden]
    public static T Negative<T>(this MustBe<T> m)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v < T.Zero,
            v => "be less than zero");
    }

    [DebuggerHidden]
    public static T NegativeOrZero<T>(this MustBe<T> m)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v <= T.Zero,
            v => "be less than or equal to zero");
    }
}