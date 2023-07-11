// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.RegularExpressions;

namespace SquidEyes.Fundamentals;

public sealed partial class Tag : ValueObjectBase<Tag>
{
    public static readonly int MaxLength = 24;

    private static readonly Regex validator = GetValidator();

    public string? Value { get; private set; }

    protected override void SetProperties(string input)
    {
        Value = input;
    }

    public static bool IsInput(string input) =>
        input is not null && validator.IsMatch(input);

    public static Tag Create(string input) =>
        DoCreate(input, IsInput);

    public static bool TryCreate(string input, out Tag result) =>
        DoTryCreate(input, IsInput, out result);

    public static implicit operator Tag(string input) => Create(input);

    [GeneratedRegex(@"^[A-Z][A-Za-z0-9]{0,23}$")]
    private static partial Regex GetValidator();
}