// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.ComponentModel;

namespace SquidEyes.Fundamentals;

internal static class MiscInternalExtenders
{
    public static bool IsPlural(this char value) => "AEIOU".Contains(value);

    public static bool TryCast<T>(this object instance, out T result)
    {
        result = default!;

        if (instance is T t)
        {
            result = t;

            return true;
        }

        if (instance != null)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));

            if (converter.CanConvertFrom(instance.GetType()))
            {
                result = (T)converter.ConvertFrom(instance)!;

                return true;
            }

            return false;
        }

        return !typeof(T).IsValueType;
    }
}