// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class MayNotBe<T>(T value, string argName, Func<T, bool> canEval) 
    : VerbBase<T, MayNotBe<T>>(value, argName, canEval, "Value may not")
{
}