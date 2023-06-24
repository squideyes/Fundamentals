// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Diagnostics;

namespace SquidEyes.Fundamentals;

public static class MiscValidators
{
    //[DebuggerHidden]
    //public static ArgSet BeNullOrEmpty(this MayNot<ArgSet> m)
    //{
    //    return m.ThrowErrorIfNotIsValid(
    //        v => v is not null && !v.IsEmpty,
    //        v => $"be an empty ArgSet");
    //}

    [DebuggerHidden]
    public static List<V> BeEmpty<V>(this MayNot<List<V>> m)
    {
        return m.ThrowErrorIfNotIsValid(
            v => v.IsNotEmpty(),
            v => "be null, empty, or contain one or more default items.");
    }

    [DebuggerHidden]
    public static string BeNullOrEmpty(this MayNot<string> m)
    {
        return m.ThrowErrorIfNotIsValid(
            v => !string.IsNullOrEmpty(v),
            v => "be null or String.Empty");
    }

    [DebuggerHidden]
    public static string BeNullOrWhitespace(this MayNot<string> m)
    {
        return m.ThrowErrorIfNotIsValid(
            v => !string.IsNullOrWhiteSpace(v),
            v => "be null, String.Empty or be comprised of whitespace characters");
    }
}