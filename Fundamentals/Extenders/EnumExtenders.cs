// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.ComponentModel;

namespace SquidEyes.Fundamentals;

public static class EnumExtenders
{
    public static T ToEnumValue<T>(this string value) =>
        (T)Enum.Parse(typeof(T), value, true);

    public static bool TryParseEnumList<T>(
        this List<string> values, out List<T> items)
        where T : struct
    {
        items = new List<T>();

        if (values.Count == 0)
            return false;

        var joined = string.Join("", values)
            .Replace(" ", "").Split(',');

        foreach (var value in joined)
        {
            if (!Enum.TryParse(value, true, out T symbol))
                return false;

            items.Add(symbol);
        }

        return items.Count > 0;
    }

    public static string ToUpper(this Enum value) =>
        value.ToString().ToUpper();

    public static string ToLower(this Enum value) => 
        value.ToString().ToLower();

    public static string ToDescription(this Enum enumeration)
    {
        var fi = enumeration.GetType()
            .GetField(enumeration.ToString())!;

        var attributes = (DescriptionAttribute[])fi
            .GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes != null && attributes.Length > 0)
            return attributes[0].Description;
        else
            return enumeration.ToString();
    }
}