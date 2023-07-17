// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static class VerbExtenders
{
    public static MustBe<T> MustBe<T>(this T value, Func<T, bool> canEval = null!,
        [CallerArgumentExpression(nameof(value))] string argName = null!)
    {
        return new MustBe<T>(value, argName, canEval ?? (v => true));
    }

    public static MayNotBe<T> MayNotBe<T>(this T value, Func<T, bool> canEval = null!,
        [CallerArgumentExpression(nameof(value))] string argName = null!)
    {
        return new MayNotBe<T>(value, argName, canEval ?? (v => true));
    }
}