// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class MustBe<T>(T value, string argName, Func<T, bool> canEval) 
    : VerbBase<T, MustBe<T>>(value, argName, canEval, "Value must")
{
}