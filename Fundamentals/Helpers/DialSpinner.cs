//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

//using Loyc.Math;
//using System.Collections;
//using System.Numerics;

//namespace SquidEyes.Fundamentals;

//public class DialSpinner : IEnumerable<IConvertible>
//{
//    private readonly List<IConvertible> values;
//    private readonly int defaultIndex;

//    private int index = 0;

//    public DialSpinner(IEnumerable<IConvertible> values, IConvertible @default)
//    {
//        if (!values.HasItems(1, int.MaxValue, v => !v.IsDefault()))
//            throw new ArgumentOutOfRangeException(nameof(values));

//        var type = values.First().GetType();

//        if (values.Any(v => v.GetType() != type))
//            throw new ArgumentOutOfRangeException(nameof(values));

//        if (@default.GetType() != type)
//            throw new ArgumentOutOfRangeException(nameof(type));

//        this.values = values.OrderBy(v => v).ToList();

//        defaultIndex = index = values.ToList().IndexOf(@default);

//        if (index == -1)
//            throw new ArgumentOutOfRangeException(nameof(@default));
//    }

//    public IConvertible Value => values[index];

//    public int Count => values.Count;

//    public void Reset() => index = defaultIndex;

//    public int CompareTo(IConvertible other)
//    {
//        return Comparer.Default.Compare(
//            Value, Convert.ChangeType(other, Value.GetType()));
//    }

//    public void First() => index = 0;

//    public void Last() => index = values.Count - 1;

//    public void Default() => index = defaultIndex;

//    public bool Next(bool canWrapAround = true)
//    {
//        if (index == values.Count - 1)
//        {
//            if (canWrapAround)
//                First();

//            return canWrapAround;
//        }
//        else
//        {
//            index++;

//            return true;
//        }
//    }

//    public bool Previous(bool canWrapAround = true)
//    {
//        if (index == 0)
//        {
//            if (canWrapAround)
//                Last();

//            return canWrapAround;
//        }
//        else
//        {
//            index--;

//            return true;
//        }
//    }

//    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

//    public IEnumerator<IConvertible> GetEnumerator() => values.GetEnumerator();

//    public static DialSpinner Create<T>(T minValue, T maxValue, T step, T @default)
//        where T : IConvertible, IComparable<T>, INumber<T>
//    {
//        return new DialSpinner(GetValues(minValue, maxValue, step, @default), @default);
//    }

//    private static List<IConvertible> GetValues<T>(T minValue, T maxValue, T step, T @default)
//        where T : IConvertible, IComparable<T>, INumber<T>
//    {
//        static T Round(T value)
//        {
//            var asDouble = (double)Convert.ChangeType(value, typeof(double));

//            var rounded = Math.Round(asDouble, 5);

//            return (T)Convert.ChangeType(rounded, typeof(T));
//        }

//        if (minValue.CompareTo(default) < 0)
//            throw new ArgumentOutOfRangeException(nameof(minValue));

//        if (maxValue.CompareTo(minValue) <= 0)
//            throw new ArgumentOutOfRangeException(nameof(maxValue));

//        if (step.CompareTo(default) <= 0.0)
//            throw new ArgumentOutOfRangeException(nameof(step));

//        var values = new List<IConvertible>();

//        var value = minValue;

//        var math = Maths<T>.Math;

//        do
//        {
//            values.Add(value);

//            value = Round(math.Add(value, step));
//        }
//        while (value.CompareTo(maxValue) <= 0);

//        if (!values.Contains(@default))
//            throw new ArgumentOutOfRangeException(nameof(@default));

//        return values;
//    }
//}