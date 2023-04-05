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
    public static T BeZero<T>(this Must<T> m)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v == T.Zero,
            v => "be zero");
    }

    [DebuggerHidden]
    public static T BeZero<T>(this MayNot<T> m)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v == T.Zero,
            v => "be zero");
    }

    [DebuggerHidden]
    public static T BeGreaterThan<T>(this Must<T> m, T value)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v > value,
            v => $"be greater than {value}");
    }

    [DebuggerHidden]
    public static T BeGreaterThanOrEqualTo<T>(this Must<T> m, T value)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v >= value,
            v => $"be greater than or equal to{value}");
    }

    [DebuggerHidden]
    public static T BeLessThan<T>(this Must<T> m, T value)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v < value,
            v => $"be less than {value}");
    }

    [DebuggerHidden]
    public static T BeLessThanOrEqualTo<T>(this Must<T> m, T value)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v <= value,
            v => $"be less than or equal to{value}");
    }

    [DebuggerHidden]
    public static T BePositive<T>(this Must<T> m)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v > T.Zero,
            v => "be greater than zero");
    }

    [DebuggerHidden]
    public static T BePositiveOrZero<T>(this Must<T> m)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v >= T.Zero,
            v => "be greater than or equal to zero");
    }

    [DebuggerHidden]
    public static T BeNegative<T>(this Must<T> m)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v < T.Zero,
            v => "be less than zero");
    }

    [DebuggerHidden]
    public static T BeNegativeOrZero<T>(this Must<T> m)
        where T : INumber<T>
    {
        return m.ThrowErrorIfNotIsValid(
            v => v <= T.Zero,
            v => "be less than or equal to zero");
    }
}