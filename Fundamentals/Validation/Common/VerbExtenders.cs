// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static class VerbExtenders
{
    public static Must<T> Must<T>(this T value, Func<T, bool> canEval = null!,
        [CallerArgumentExpression(nameof(value))] string argName = null!)
    {
        return new Must<T>(value, argName, canEval ?? (v => true));
    }

    public static MayNot<T> MayNot<T>(this T value, Func<T, bool> canEval = null!,
        [CallerArgumentExpression(nameof(value))] string argName = null!)
    {
        return new MayNot<T>(value, argName, canEval ?? (v => true));
    }
}