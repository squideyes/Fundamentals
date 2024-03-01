// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class SoftEnum<T> : SoftArgBase
    where T : struct, Enum
{
    public SoftEnum(Tag tag, T? value)
        : base(tag)
    {
        Value = value;
    }

    public SoftEnum(Tag tag, string input, SoftArgStatus status)
        : base(tag, input, status)
    {
    }

    public T? Value { get; } = null!;
}