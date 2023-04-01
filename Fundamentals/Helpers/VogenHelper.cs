// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Vogen;

namespace SquidEyes.Fundamentals;

public static class VogenHelper
{
    public static Validation GetValidation<T>(
        string value, Func<string, bool> isValue)
    {
        if (isValue(value))
            return Validation.Ok;

        return Validation.Invalid(
            $"\"{value}\" is an invalid \"{typeof(T)}\" value!");
    }
}