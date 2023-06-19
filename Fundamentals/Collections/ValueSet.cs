//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

//using System.Numerics;

//namespace SquidEyes.Fundamentals;

//public class ValueSet
//{
//    private readonly Type type;
//    private readonly List<object> values;

//    private ValueSet(Type type, List<object> values)
//    {
//        this.type = type;
//        this.values = values;
//    }

//    public List<T> Get<T>()
//    {
//        typeof(T).Must().Be(v => v == type);

//        return values.Cast<T>().ToList();
//    }

//    public static ValueSet From<T>(params T[] values)
//        where T : Enum
//    {
//        return From(values.ToList());
//    }

//    public static ValueSet From<T>(IEnumerable<T> values)
//        where T : Enum
//    {
//        values.Any().Must().Be(true);
//        values.Must().Be(v => v.IsUnique());

//        return new(typeof(T), values.Cast<object>().ToList());
//    }

//    public static ValueSet From(params int[] values) =>
//        From((IEnumerable<int>)values);

//    public static ValueSet From(IEnumerable<int> values)
//    {
//        values.Any().Must().Be(true);
//        values.Must().Be(v => v.IsUnique());

//        return new(typeof(int), values.Cast<object>().ToList());
//    }

//    public static ValueSet Generate(int min, int max, int step)
//    {
//        ValidateMinMaxStep(min, max, step);

//        var values = new List<object>();

//        for (int value = min; value < max; value += step)
//            values.Add(value);

//        return new ValueSet(typeof(int), values);
//    }

//    public static ValueSet From(params double[] values) =>
//        From((IEnumerable<double>)values);

//    public static ValueSet From(IEnumerable<double> values)
//    {
//        values.Any().Must().Be(true);
//        values.Must().Be(v => v.IsUnique());

//        return new(typeof(double), values.Cast<object>().ToList());
//    }

//    public static ValueSet Generate(
//        double min, double max, double step, int? digits = null)
//    {
//        ValidateMinMaxStep(min, max, step);

//        var values = new List<object>();

//        for (double value = min; value < max; value += step)
//        {
//            if (digits.HasValue)
//                values.Add(double.Round(value, digits.Value));
//            else
//                values.Add(value);
//        }

//        return new ValueSet(typeof(double), values);
//    }

//    private static void ValidateMinMaxStep<T>(T min, T max, T step)
//        where T : INumber<T>
//    {
//        max.Must().BeGreaterThan(min);
//        step.Must().Be(v => v > T.Zero && step <= (max - min));
//    }
//}