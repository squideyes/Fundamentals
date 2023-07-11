// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public sealed class ActorId : ValueObjectBase<ActorId>
{
    public const int Length = 8;

    private static readonly char[] charSet =
        "ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();

    private static readonly HashSet<char> hashSet = charSet.ToHashSet();

    public string? Value { get; private set; }

    protected override void SetProperties(string input)
    {
        Value = input;
    }

    public static bool IsInput(string input)
    {
        return input is not null
            && input.Length == Length
            && input.All(hashSet.Contains);
    }

    public static ActorId Create(string input) =>
        DoCreate(input, IsInput);

    public static bool TryCreate(string input, out ActorId result) =>
        DoTryCreate(input, IsInput, out result);

    public static ActorId Next() =>
        Create(IdHelper.GetRandomId(charSet, Length));
}