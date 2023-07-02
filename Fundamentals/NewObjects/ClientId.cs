namespace SquidEyes.Fundamentals.NewObjects;

public record ClientId
{
    public const int Length = 8;

    private static readonly char[] charSet =
        "ABCDEFGHJKLMNPQRSTUVWXYZ23456789".ToCharArray();

    private static readonly HashSet<char> hashSet = charSet.ToHashSet();

    private ClientId(string value) => Value = value;

    public string Value { get; init; }

    public static bool IsInput(string input)
    {
        return input is not null
            && input.Length == Length
            && input.All(hashSet.Contains);
    }

    public static ClientId? Create(string input)
    {
        if (string.IsNullOrEmpty(input))
            return null;

        if (input.Length != Length)
            return null;

        return new ClientId(input);
    }
}