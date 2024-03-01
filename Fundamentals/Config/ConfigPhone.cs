// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class ConfigPhone : ConfigBase
{
    public ConfigPhone(Tag tag, Phone value)
        : base(tag)
    {
        Value = value;
    }

    public ConfigPhone(Tag tag, string input, ConfigStatus status)
        : base(tag, input, status)
    {
    }

    public Phone Value { get; } = null!;
}