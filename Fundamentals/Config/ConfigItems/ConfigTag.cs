// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class ConfigTag : ConfigItemBase
{
    public ConfigTag(Tag tag, Tag value)
        : base(tag)
    {
        Value = value;
    }

    public ConfigTag(Tag tag, string input, ConfigStatus status)
        : base(tag, input, status)
    {
    }

    public Tag Value { get; } = null!;
}