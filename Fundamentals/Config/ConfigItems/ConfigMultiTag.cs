// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class ConfigMultiTag : ConfigItemBase
{
    public ConfigMultiTag(Tag tag, MultiTag value)
        : base(tag)
    {
        Value = value;
    }

    public ConfigMultiTag(Tag tag, string input, ConfigStatus status)
        : base(tag, input, status)
    {
    }

    public MultiTag Value { get; } = null!;
}