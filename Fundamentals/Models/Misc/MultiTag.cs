// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Vogen;

namespace SquidEyes.Fundamentals;

[ValueObject<string>]
public readonly partial struct MultiTag
{
    public const int MaxTags = 10;

    public int Count => Value.Count(c => c == ':') + 1;

    public static MultiTag From(Tag[] tags) =>
        From(string.Join(":", tags.Select(t => t.ToString())));

    public static Validation Validate(string value) =>
        VogenHelper.GetValidation<MultiTag>(value, IsValue);

    public static bool IsValue(string value) =>
        value is not null && value.IsMultiTagValue();
}