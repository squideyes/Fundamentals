// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Vogen;

namespace SquidEyes.Fundamentals;

[ValueObject<string>]
public readonly partial struct Tag
{
    public static Validation Validate(string value) =>
        VogenHelper.GetValidation<Tag>(value, IsValue);

    public static bool IsValue(string value) =>
        value is not null && value.IsTagValue();
}