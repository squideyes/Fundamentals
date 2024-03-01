// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class SoftUri : SoftArgBase
{
    public SoftUri(Tag tag, Uri value)
        : base(tag)
    {
        Value = value;
    }

    public SoftUri(Tag tag, string input, SoftArgStatus status)
        : base(tag, input, status)
    {
    }

    public Uri Value { get; } = null!;
}