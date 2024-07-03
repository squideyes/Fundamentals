using System.Globalization;
using static SquidEyes.Fundamentals.TagArgState;

namespace SquidEyes.Fundamentals;

public class TagArg<T> : ITagArg
{
    internal TagArg(Tag tag, T arg, TagArgArgKind kind)
    {
        Kind = kind;
        Tag = tag;
        Arg = arg;
        State = Valid;
        TypeName = typeof(T).FullName!;
        Message = null!;
    }

    internal TagArg(Tag tag, string input, TagArgState state, int? length = null)
    {
        Kind = default;
        Tag = tag;
        Arg = default!;
        State = state;
        TypeName = null!;

        Message = state switch
        {
            BadChars => $"The \"{tag}\" arg contains one or more invalid characters.",
            Invalid => $"The \"{tag}\" arg failed validation.",
            NotTrimmed => $"The \"{tag}\" arg has not been trimmed.",
            NullOrWhitespace => $"The \"{tag}\" input may not be null, empty or whitespace,",
            ParseFailed => $"The \"{tag}\" input could not be parsed (Input: \"{input}\", Type: {typeof(T)}).",
            TooShort => $"The \"{tag}\" arg is less than {length} characters long.",
            TooLong => $"The \"{tag}\" arg is greater than {length} characters long.",
            _ => throw new ArgumentOutOfRangeException(nameof(state))
        };
    }

    public Tag Tag { get; }
    public T Arg { get; }
    public TagArgArgKind Kind { get; }
    public string TypeName { get; }
    public string Message { get; }
    public TagArgState State { get; }

    public bool IsValid => State == Valid;

    public V GetArgAs<V>() => (V)Convert.ChangeType(Arg, typeof(V))!;

    public Result ToResult(string code)
    {
        if (IsValid)
            return Result.Success(Arg);
        else
            return Result.Failure<T>(new Error(code, Message));
    }

    public Error ToError(string code) => ToResult(code).Errors.First();

    public static bool TryGetNonAsciiArgKind(out TagArgArgKind kind)
    {
        kind = typeof(T) switch
        {
            Type t when t == typeof(bool) => TagArgArgKind.Bool,
            Type t when t == typeof(byte) => TagArgArgKind.Byte,
            Type t when t == typeof(char) => TagArgArgKind.Char,
            Type t when t == typeof(DateOnly) => TagArgArgKind.DateOnly,
            Type t when t == typeof(DateTime) => TagArgArgKind.DateTime,
            Type t when t == typeof(double) => TagArgArgKind.Double,
            Type t when t == typeof(Email) => TagArgArgKind.Email,
            Type t when t == typeof(Enum) => TagArgArgKind.Enum,
            Type t when t == typeof(float) => TagArgArgKind.Float,
            Type t when t == typeof(Guid) => TagArgArgKind.Guid,
            Type t when t == typeof(int) => TagArgArgKind.Int32,
            Type t when t == typeof(long) => TagArgArgKind.Int64,
            Type t when t == typeof(MultiTag) => TagArgArgKind.MultiTag,
            Type t when t == typeof(Phone) => TagArgArgKind.Phone,
            Type t when t == typeof(short) => TagArgArgKind.Int16,
            Type t when t == typeof(Tag) => TagArgArgKind.Tag,
            Type t when t == typeof(TimeOnly) => TagArgArgKind.TimeOnly,
            Type t when t == typeof(TimeSpan) => TagArgArgKind.TimeSpan,
            Type t when t == typeof(Uri) => TagArgArgKind.Uri,
            _ => default
        };

        return !kind.IsDefault();
    }

    public static TagArg<string> Create(Tag tag, string input)
    {
        tag.MayNotBe().Null();

        if (TagArg<string>.IsNullOrWhiteSpace(input, tag, out var tagArg))
            return tagArg;
        else if (!input.IsBase64String())
            return new TagArg<string>(tag, input, NotBase64);
        else
            return new TagArg<string>(tag, input, TagArgArgKind.Base64);
    }

    public static TagArg<string> Create(Tag tag, string arg, bool isRequired = true,
        bool mustBeTrimmed = true, int minLength = 1, int maxLength = 100,
            AsciiFilter filter = AsciiFilter.AllChars, Func<string, bool> isValid = null!)
    {
        tag.MayNotBe().Null();
        minLength.MustBe().Positive();
        maxLength.MustBe().GreaterThanOrEqualTo(minLength);
        filter.MustBe().EnumValue();

        if (!isRequired && string.IsNullOrEmpty(arg))
            return new TagArg<string>(tag, null!, TagArgArgKind.TextLine);
        else if (TagArg<string>.IsNullOrWhiteSpace(arg, tag, out var tagArg))
            return tagArg;
        else if (arg.Length < minLength)
            return new TagArg<string>(tag, arg, TooShort, minLength);
        else if (arg.Length > maxLength)
            return new TagArg<string>(tag, arg, TooShort, maxLength);
        else if (mustBeTrimmed && !arg.IsTrimmed())
            return new TagArg<string>(tag, arg, NotTrimmed);
        else if (!arg.IsNonEmptyAndAscii(filter))
            return new TagArg<string>(tag, arg, BadChars);
        else if (isValid is not null && !isValid(arg))
            return new TagArg<string>(tag, arg, Invalid);
        else
            return new TagArg<string>(tag, arg, TagArgArgKind.TextLine);
    }

    public static TagArg<T> Create(Tag tag, T arg, Func<T, bool> isValid = null!)
    {
        tag.MayNotBe().Null();

        if (!TryGetNonAsciiArgKind(out TagArgArgKind kind))
            throw new ArgumentOutOfRangeException(nameof(arg));

        if (isValid is not null && !isValid(arg))
            return new TagArg<T>(tag, arg, TagArgArgKind.TextLine);

        return new TagArg<T>(tag, arg, kind);
    }

    internal static bool IsNullOrWhiteSpace(string input, Tag tag, out TagArg<T> tagArg)
    {
        tagArg = default!;

        if (string.IsNullOrWhiteSpace(input))
            tagArg = new TagArg<T>(tag, input!, NullOrWhitespace);

        return !tagArg.IsDefault();
    }
}
