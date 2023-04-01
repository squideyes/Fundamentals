// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public static class IListExtenders
{
    public static bool IsNotEmpty<T>(this List<T> value)
    {
        return value is not null && value.Any() && !value
            .Any(v => v.IsDefault() || v.IsDefault());
    }
}