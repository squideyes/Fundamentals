namespace SquidEyes.Fundamentals;

public sealed class Email : ValueObjectBase<Email>
{
    public const int MaxLength = 50;

    public string? Value { get; private set; }

    protected override void SetProperties(string input)
    {
        Value = input;
    }

    public static bool IsInput(string input)
    {
        if (input is null)
            return false;

        if (input.Length > MaxLength)
            return false;

        // TODO: Improve this!!!!!!!!!!!!!!!!!!!!!
        return true;
    }

    public static Email Create(string input) =>
        DoCreate(input, IsInput);

    public static bool TryCreate(string input, out Email result) =>
        DoTryCreate(input, IsInput, out result);
}
