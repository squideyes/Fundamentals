// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public sealed partial class MultiTag : ValueObjectBase<MultiTag>
{
    public const int MaxTags = 10;

    public Tag[]? Tags { get; private set; }

    protected override void SetProperties(string? input) =>
        Tags = input!.Split(':').Select(Tag.Create).ToArray();

    public static bool IsInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return false;

        if (input.Any(char.IsWhiteSpace))
            return false;

        var fields = input.Split(':');

        if (fields.Length > MaxTags)
            return false;

        return fields.All(v => Tag.TryCreate(v, out Tag _));
    }

    public static MultiTag Create(string input) => DoCreate(input, IsInput);

    public static bool TryCreate(string input, out MultiTag result) =>
        DoTryCreate(input, IsInput, out result);

    public static implicit operator MultiTag(string input) => Create(input);
}