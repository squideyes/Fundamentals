// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Diagnostics;

namespace SquidEyes.Fundamentals;

public static class MiscValidators
{
    [DebuggerHidden]
    public static string NonNullAndTrimmed(
        this MustBe<string> m, bool mayBeEmpty = false)
    {
        string suffix;

        if (mayBeEmpty)
            suffix = "empty or non-empty and trimmed.";
        else
            suffix = "non-empty and trimmed.";

        return m.ThrowErrorIfNotIsValid(
            v => v.IsNonNullAndTrimmed(false),
            v => suffix);
    }

    [DebuggerHidden]
    public static List<V> Empty<V>(this MayNotBe<List<V>> m)
    {
        return m.ThrowErrorIfNotIsValid(
            v => v.IsNotEmpty(),
            v => "be null, empty, or contain one or more default items.");
    }

    [DebuggerHidden]
    public static string NullOrEmpty(this MayNotBe<string> m)
    {
        return m.ThrowErrorIfNotIsValid(
            v => !string.IsNullOrEmpty(v),
            v => "be null or String.Empty");
    }

    [DebuggerHidden]
    public static string NullOrWhitespace(this MayNotBe<string> m)
    {
        return m.ThrowErrorIfNotIsValid(
            v => !string.IsNullOrWhiteSpace(v),
            v => "be null, String.Empty or be comprised of whitespace characters");
    }
}