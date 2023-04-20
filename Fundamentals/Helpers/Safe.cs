// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public static class Safe
{
    public static bool Do(Action action)
    {
        try
        {
            action();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static T GetValueOrDefault<T>(Func<T> getValue, T @default = default!)
    {
        if (TryGetValue(() => getValue(), out T value))
            return value;
        else
            return @default;
    }

    public static bool TryGetValue<T>(Func<T> getValue, out T value)
    {
        try
        {
            value = getValue();

            return true;
        }
        catch
        {
            value = default!;

            return false;
        }
    }
}