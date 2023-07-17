// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class MustBe<T> : VerbBase<T, MustBe<T>>
{
    public MustBe(T value, string argName, Func<T, bool> canEval)
        : base(value, argName, canEval, "Value must")
    {
    }
}