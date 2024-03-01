// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class SoftString : SoftBase
{
    public SoftString(Tag tag, string value)
        : base(tag)
    {
        Value = value;
    }

    public SoftString(Tag tag, string input, SoftStatus status)
        : base(tag, input, status)
    {
    }

    public string Value { get; } = null!;
}