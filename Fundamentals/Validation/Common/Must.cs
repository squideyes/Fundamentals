// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class Must<T> : VerbBase<T, Must<T>>
{
    public Must(T value, string argName, Func<T, bool> canEval)
        : base(value, argName, canEval, "Value must")
    {
    }
}