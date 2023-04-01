// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public static class EnumList
{
    public static List<T> FromAll<T>() =>
        Enum.GetValues(typeof(T)).Cast<T>().ToList();
}