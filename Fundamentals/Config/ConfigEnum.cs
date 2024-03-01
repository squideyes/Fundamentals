// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class ConfigEnum<T> : ConfigBase
    where T : struct, Enum
{
    public ConfigEnum(Tag tag, T? value)
        : base(tag)
    {
        Value = value;
    }

    public ConfigEnum(Tag tag, string input, ConfigStatus status)
        : base(tag, input, status)
    {
    }

    public T? Value { get; } = null!;
}