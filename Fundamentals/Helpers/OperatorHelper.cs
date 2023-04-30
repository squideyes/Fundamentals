// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

internal class OperatorHelper
{
    public static bool CompareTo<T>(T left, T right, Func<T, T, bool> getResult)
        where T: IComparable<T>
    {
        if (left is null)
            return right is null;

        return getResult(left, right);
    }
}