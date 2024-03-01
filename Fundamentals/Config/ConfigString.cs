// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class ConfigString : ConfigBase
{
    public ConfigString(Tag tag, string value)
        : base(tag)
    {
        Value = value;
    }

    public ConfigString(Tag tag, string input, ConfigStatus status)
        : base(tag, input, status)
    {
    }

    public string Value { get; } = null!;
}