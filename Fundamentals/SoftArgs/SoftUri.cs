// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class SoftUri : SoftBase
{
    public SoftUri(Tag tag, Uri value)
        : base(tag)
    {
        Value = value;
    }

    public SoftUri(Tag tag, string input, SoftStatus status)
        : base(tag, input, status)
    {
    }

    public Uri Value { get; } = null!;
}