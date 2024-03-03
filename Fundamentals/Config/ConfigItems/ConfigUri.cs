// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class ConfigUri : ConfigItemBase
{
    public ConfigUri(Tag tag, Uri value)
        : base(tag)
    {
        Value = value;
    }

    public ConfigUri(Tag tag, string input, ConfigStatus status)
        : base(tag, input, status)
    {
    }

    public Uri Value { get; } = null!;
}