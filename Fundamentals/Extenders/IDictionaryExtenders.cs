// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public static class IDictionaryExtenders
{
    public static bool IsNotEmpty<K, V>(this IDictionary<K, V> value)
    {
        return value is not null && value.Any() && !value
            .Any(v => v.Key.IsDefault() || v.Value.IsDefault());
    }
}