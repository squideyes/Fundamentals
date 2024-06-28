namespace SquidEyes.Fundamentals;

public class TagValue
{
    private TagValue(Tag tag, object value, TagValueKind kind)
    {
        Tag = tag.MayNotBe().Null();
        Value = value.MayNotBe().Default();
        Kind = kind.MustBe().EnumValue();
    }

    public Tag Tag { get; }
    public TagValueKind Kind { get; }

    internal object Value { get; }

    public override string? ToString()
    {
        return Kind switch
        {
            TagValueKind.DateOnly => "MM/dd/yyyy",
            TagValueKind.DateTime => "MM/dd/yyyy HH:mm:ss.fff",
            TagValueKind.TimeOnly => "HH:mm:ss.fff",
            TagValueKind.TimeSpan => @"d\.hh\:mm\:ss\.fff",
            _ => Value.ToString()
        };
    }

    public T GetAs<T>() => (T)Convert.ChangeType(Value, typeof(T));

    public static TagValue Create(Tag tag, bool value) =>
        new(tag, value, TagValueKind.Bool);

    public static TagValue Create(Tag tag, DateOnly value) =>
        new(tag, value, TagValueKind.DateOnly);

    public static TagValue Create(Tag tag, DateTime value) =>
        new(tag, value, TagValueKind.DateTime);

    public static TagValue Create(Tag tag, double value) =>
        new(tag, value, TagValueKind.Double);

    public static TagValue Create(Tag tag, Email value) =>
        new(tag, value, TagValueKind.Email);

    public static TagValue Create<T>(Tag tag, T value)
        where T : struct, Enum
    {
        return new(tag, value, TagValueKind.Enum);
    }

    public static TagValue Create(Tag tag, Guid value) =>
        new(tag, value.MayNotBe().Default(), TagValueKind.Guid);

    public static TagValue Create(Tag tag, int value) =>
        new(tag, value, TagValueKind.Int32);

    public static TagValue Create(Tag tag, long value) =>
        new(tag, value, TagValueKind.Int64);

    public static TagValue Create(Tag tag, MultiTag value) =>
        new(tag, value, TagValueKind.MultiTag);

    public static TagValue Create(Tag tag, Phone value) =>
        new(tag, value, TagValueKind.Phone);

    public static TagValue Create(Tag tag, string value) =>
        new(tag, value.MustBe().NonNullAndTrimmed(), TagValueKind.String);

    public static TagValue Create(Tag tag, Tag value) =>
        new(tag, value, TagValueKind.Tag);

    public static TagValue Create(Tag tag, TimeOnly value) =>
        new(tag, value, TagValueKind.TimeOnly);

    public static TagValue Create(Tag tag, TimeSpan value) =>
        new(tag, value, TagValueKind.TimeSpan);

    public static TagValue Create(Tag tag, Uri value) =>
        new(tag, value, TagValueKind.Uri);
}
