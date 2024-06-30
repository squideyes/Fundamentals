namespace SquidEyes.Fundamentals;

public class Error : IEquatable<Error>
{
    public Error(Tag code, string message, TagArgSet? metadata = null!)
    {
        Code = code.MayNotBe().Null();
        Message = message.MustBe().NonNullAndTrimmed();

        if (metadata is not null)
            Metadata = metadata.MustBe().True(v => v.HasItems());
    }

    public Tag Code { get; }
    public string Message { get; }
    public TagArgSet? Metadata { get; }

    public static readonly Error None = new(string.Empty, string.Empty);

    public static readonly Error NullValue =
        new("Error.NullValue", "The specified result value is NULL.");

    public virtual bool Equals(Error? other)
    {
        if (other is null)
            return false;

        return Code == other.Code && Message == other.Message
            && Equals(Metadata, other.Metadata);
    }

    public override string ToString() => Code.ToString();

    public override bool Equals(object? other) =>
        other is Error error && Equals(error);

    public override int GetHashCode() =>
        HashCode.Combine(Code, Message, Metadata);


    public static implicit operator string(Error error) =>
        error.ToString();

    public static bool operator ==(Error? left, Error? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Error? left, Error? right) => !(left == right);
}
