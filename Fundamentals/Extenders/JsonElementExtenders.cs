// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.Json;

namespace SquidEyes.Fundamentals;

public static class JsonElementExtenders
{
    public static bool GetBoolean(this JsonElement element, string propertyName) =>
        element.GetProperty(propertyName).GetBoolean();

    public static string GetString(this JsonElement element, string propertyName) =>
        element.GetProperty(propertyName).GetString()!;

    public static float GetFloat(this JsonElement element, string propertyName) =>
        element.GetProperty(propertyName).GetSingle();

    public static double GetDouble(this JsonElement element, string propertyName) =>
        element.GetProperty(propertyName).GetDouble();

    public static int GetInt32(this JsonElement element, string propertyName) =>
        element.GetProperty(propertyName).GetInt32();

    //public static Uri GetUri(this JsonElement element, string propertyName)
    //{
    //    return element.GetProperty(propertyName).GetString()
    //        .Get(v => v == null ? null : new Uri(v))!;
    //}

    //public static T GetEnumValue<T>(this JsonElement element, string propertyName)
    //{
    //    return element.GetProperty(propertyName).GetString()
    //        .Get(v => v == null ? default : v.ToEnumValue<T>())!;
    //}

    public static DateTime GetDateTime(this JsonElement element, string propertyName) =>
        element.GetProperty(propertyName).GetDateTime();

    //public static TimeSpan GetTimeSpan(this JsonElement element, string propertyName)
    //{
    //    return element.GetProperty(propertyName).GetString()
    //       .Get(v => v == null ? default : TimeSpan.Parse(v));
    //}
}