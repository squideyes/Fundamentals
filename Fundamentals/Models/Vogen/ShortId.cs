// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Vogen;

namespace SquidEyes.Fundamentals;

[ValueObject<string>]
public readonly partial struct ShortId 
{
    private const int SIZE = 22;

    private static readonly char[] charSet =
        "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz23456789".ToCharArray();

    private static readonly HashSet<char> hashSet = new(charSet);

    public static Validation Validate(string value) =>
        VogenHelper.GetValidation<ShortId>(value, IsValue);

    public static bool IsValue(string value) =>
        value?.Length == SIZE && value.All(hashSet.Contains);

    public static ShortId Next() =>
        new(IdHelper.GetRandomId(charSet, SIZE));
}