// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

internal class OperatorHelper
{
    public static bool CompareTo<T>(T lhs, T rhs, Func<T, T, bool> getResult)
        where T: IComparable<T>
    {
        if (lhs is null)
            return rhs is null;

        return getResult(lhs, rhs);
    }
}