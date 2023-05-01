namespace SquidEyes.Fundamentals;

public readonly struct TagValue<T> : IEquatable<TagValue<T>>, IComparable<TagValue<T>>
    where T : IEquatable<T>, IComparable<T>
{
    private TagValue(Tag tag, T value)
    {
        Tag = tag;
        Value = value;
    }

    public Tag Tag { get; }
    public T Value { get; }

    public override string ToString() => $"{Tag}={Value}";

    public static TagValue<T> From(Tag tag, T value)
    {
        tag.MayNot().BeDefault();
        value.MayNot().BeDefault();

        return new TagValue<T>(tag, value);
    }

    public bool Equals(TagValue<T> other) =>
        Tag.Equals(other.Tag) && Value.Equals(other.Value);

    public override bool Equals(object? other) =>
        other is TagValue<T> tagValue && Equals(tagValue);

    public override int GetHashCode() => HashCode.Combine(Tag, Value);

    public int CompareTo(TagValue<T> other)
    {
        if (Tag == other.Tag)
            return Value.CompareTo(other.Value);
        else
            return Tag.CompareTo(other.Tag);
    }

    public static bool operator ==(TagValue<T> left, TagValue<T> right) =>
        left.Equals(right);

    public static bool operator !=(TagValue<T> left, TagValue<T> right) =>
        !(left == right);

    public static bool operator <(TagValue<T> left, TagValue<T> right) =>
        left.CompareTo(right) < 0;

    public static bool operator <=(TagValue<T> left, TagValue<T> right) =>
        left.CompareTo(right) <= 0;

    public static bool operator >(TagValue<T> left, TagValue<T> right) =>
        left.CompareTo(right) > 0;

    public static bool operator >=(TagValue<T> left, TagValue<T> right) =>
        left.CompareTo(right) >= 0;
}
