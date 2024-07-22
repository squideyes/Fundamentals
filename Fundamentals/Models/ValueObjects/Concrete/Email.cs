// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public sealed class Email : ValueObjectBase<Email>
{
    public const int MaxLength = 50;

    private static readonly EmailValidator validator = new();

    public string? Value { get; private set; }

    protected override void SetProperties(string? input)
    {
        Value = input;
    }

    public static bool IsInput(string? input) =>
        validator.IsValid(input, MaxLength);

    public static Email Create(string input) => 
        DoCreate(input, IsInput);

    public static bool TryCreate(string input, out Email result) =>
        DoTryCreate(input, IsInput, out result);
}