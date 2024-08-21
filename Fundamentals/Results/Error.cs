// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class Error : IEquatable<Error>
{
    private Error()
    {
    }

    public Error(MultiTag code, string message, TagArgSet metadata = null!)
    {
        Code = code.MayNotBe().Null().ToString();

        Message = message.MustBe().NonNullAndTrimmed();

        if (metadata is not null)
        {
            metadata.MustBe().True(v => v.HasItems());

            Metadata = [];

            foreach (var tagArg in metadata)
                Metadata.Add(tagArg.Tag.ToString(), tagArg.GetArgAsObject());
        }
    }

    public string? Code { get; }
    public string? Message { get; }
    public Dictionary<string, object>? Metadata { get; }

    public override string ToString() => Code!.ToString();

    public bool Equals(Error? other) =>
        other is not null && Code == other.Code && Message == other.Message;

    internal static readonly Error Empty = new();

    internal static readonly Error NullValue =
        new("Error:NullValue", "The specified Result.Value is NULL.");

    public override bool Equals(object? other) =>
        other is Error error && Equals(error);

    public override int GetHashCode() => HashCode.Combine(Code, Message);
}