// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class SoftValue<T> : SoftBase
    where T : struct, IParsable<T>
{
    public SoftValue(Tag tag, T? value)
        : base(tag)
    {
        Value = value;
    }

    public SoftValue(Tag tag, string input, SoftStatus status)
        : base(tag, input, status)
    {
    }

    public T? Value { get; } = null!;
}