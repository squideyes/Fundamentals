// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public static class EqualityHelper
{
    public static bool IsEqualTo<T>(T left, T right)
        where T : IEquatable<T>
    {
        if (!typeof(T).IsValueType)
        {
            if (left is null)
                return right is null;
        }

        return left.Equals(right);
    }

    public static bool IsClassEqualTo<T>(T left, T right)
        where T : class, IEquatable<T>
    {
        if (left is null)
            return right is null;

        return left.Equals(right);
    }

    public static bool IsStructEqualTo<T>(T left, T right)
        where T : struct, IEquatable<T>
    {
        return left.Equals(right);
    }
}