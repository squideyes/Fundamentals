namespace SquidEyes.Fundamentals;

public readonly record struct Error(string Code, string Message)
{
    internal static readonly Error Empty = new(string.Empty, string.Empty);

    internal static readonly Error NullValue =
        new("Error.NullValue", "The specified Result.Value is NULL.");

    public string Code { get; } = Code ?? throw new ArgumentNullException();
    public string Message { get; } = Message ?? throw new ArgumentNullException();

    public override readonly string ToString() => Code;
}
