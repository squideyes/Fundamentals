// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class ConfigEmail : ConfigItemBase
{
    public ConfigEmail(Tag tag, Email value)
        : base(tag)
    {
        Value = value;
    }

    public ConfigEmail(Tag tag, string input, ConfigStatus status)
        : base(tag, input, status)
    {
    }

    public Email Value { get; } = null!;
}