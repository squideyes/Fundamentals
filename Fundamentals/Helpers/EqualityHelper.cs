// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public static class EqualityHelper
{
    public static bool IsEqualTo<T>(T lhs, T rhs)
        where T : IEquatable<T>
    {
        if (!typeof(T).IsValueType)
        {
            if (lhs is null)
                return rhs is null;
        }

        return lhs.Equals(rhs);
    }

    public static bool IsClassEqualTo<T>(T lhs, T rhs)
        where T : class, IEquatable<T>
    {
        if (lhs is null)
            return rhs is null;

        return lhs.Equals(rhs);
    }

    public static bool IsStructEqualTo<T>(T lhs, T rhs)
        where T : struct, IEquatable<T>
    {
        return lhs.Equals(rhs);
    }
}