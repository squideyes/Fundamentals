// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.Json;

namespace SquidEyes.Fundamentals;

public static class Safe
{
    public static bool TryGetValue(Func<bool> func)
    {
        try
        {
            return func();
        }
        catch
        {
            return false;
        }
    }

    public static bool TryGetValue<T>(Func<T> func, out T value)
    {
        try
        {
            value = func();

            return true;
        }
        catch
        {
            value = default!;

            return false;
        }
    }

    public static bool TryParseJson<T>(
        string json, JsonSerializerOptions options, out T? value)
    {
        try
        {
            value = JsonSerializer.Deserialize<T>(json, options);

            return true;
        }
        catch
        {
            value = default!;

            return false;
        }
    }
}