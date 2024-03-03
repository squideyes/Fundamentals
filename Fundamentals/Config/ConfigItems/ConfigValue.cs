// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class ConfigValue<T> : ConfigItemBase
    where T : struct, IParsable<T>
{
    public ConfigValue(Tag tag, T? value)
        : base(tag)
    {
        Value = value;
    }

    public ConfigValue(Tag tag, string input, ConfigStatus status)
        : base(tag, input, status)
    {
    }

    public T? Value { get; } = null!;
}