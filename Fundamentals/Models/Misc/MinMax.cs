// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public readonly struct MinMax<T> 
    where T : struct, IComparable<T>
{
    public MinMax(T minValue, T maxValue)
    {
        if (minValue.CompareTo(maxValue) > 0)
            throw new ArgumentOutOfRangeException(nameof(maxValue));

        MinValue = minValue;
        MaxValue = maxValue;
    }

    public T MinValue { get; }
    public T MaxValue { get; }

    public override string ToString() => $"{MinValue} => {MaxValue}";
}