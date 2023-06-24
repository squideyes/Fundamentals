namespace SquidEyes.Fundamentals;

public sealed class ClientId : ValueObjectBase<ClientId>
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

    public static ClientId Create(string input) =>
        DoCreate(input, IsInput);

    public static bool TryCreate(string input, out ClientId result) =>
        DoTryCreate(input, IsInput, out result);

    public static ClientId Next() =>
        Create(IdHelper.GetRandomId(charSet, Length));
}