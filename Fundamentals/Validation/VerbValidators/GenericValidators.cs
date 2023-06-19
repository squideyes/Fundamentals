//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

//using System.Diagnostics;

//namespace SquidEyes.Fundamentals;

//public static class GenericValidators
//{
//    [DebuggerHidden]
//    public static T BeDefault<T>(this Must<T> m)
//    {
//        return m.ThrowErrorIfNotIsValid(
//            v => v.IsDefault(),
//            v => $"be a default({typeof(T).FullName})");
//    }

//    [DebuggerHidden]
//    public static T BeDefault<T>(this MayNot<T> m)
//    {
//        return m.ThrowErrorIfNotIsValid(
//            v => !v.IsDefault(),
//            v => $"be a default({typeof(T).FullName})");
//    }

//    [DebuggerHidden]
//    public static T BeEnumValue<T>(this Must<T> m)
//        where T : struct, Enum
//    {
//        return m.ThrowErrorIfNotIsValid(
//            v => v.IsEnumValue(),
//            v => $"be a defined {typeof(T)} value");
//    }

//    [DebuggerHidden]
//    public static T BeBetween<T>(this Must<T> m, T min, T max)
//        where T : IComparable<T>
//    {
//        if (max.CompareTo(min) < 0)
//            throw new InvalidOperationException($"{max} < {min}");

//        return m.ThrowErrorIfNotIsValid(
//            v => v.CompareTo(min) >= 0 && v.CompareTo(max) <= 0,
//            v => "be >= {min} and <= {max}");
//    }

//    [DebuggerHidden]
//    public static T BeNonDefaultAndValid<T>(this Must<T> m)
//        where T : IValidatable
//    {
//        m.Value.MayNot().BeDefault();
//        m.Value.Validate();

//        return m.Value;
//    }

//    [DebuggerHidden]
//    public static T BeNull<T>(this Must<T> m)
//        where T : class
//    {
//        return m.ThrowErrorIfNotIsValid(
//            v => v is null,
//            v => "be null");
//    }

//    [DebuggerHidden]
//    public static T BeNull<T>(this MayNot<T> m)
//        where T : class
//    {
//        return m.ThrowErrorIfNotIsValid(
//            v => v is not null,
//            v => "be null");
//    }

//    [DebuggerHidden]
//    public static T Be<T>(this Must<T> m, T value)
//        where T : IEquatable<T>
//    {
//        return m.ThrowErrorIfNotIsValid(
//            v => m.Value.Equals(value),
//            v => "be equal to");
//    }

//    [DebuggerHidden]
//    public static T Be<T>(this Must<T> m, Func<T, bool> isValid)
//    {
//        if (isValid is null)
//        {
//            throw new InvalidOperationException(
//                "An \"isValid\" expression must be supplied.");
//        }

//        return m.ThrowErrorIfNotIsValid(v => isValid(v),
//            "Value does not fall within the expected range.");
//    }
//}