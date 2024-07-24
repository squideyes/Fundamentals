using System.Text.Json;
using static SquidEyes.Fundamentals.TagArgState;

namespace SquidEyes.Fundamentals;

public class TagArg<T> : ITagArg
{
    private const int MAX_TEXTLINE_LENGTH = 100;

    private readonly string formatted;

    internal TagArg(Tag tag, T arg, TagArgArgKind kind, AsciiFilter filter = default!)
    {
        string GetFormatedString()
        {
            return typeof(T) switch
            {
                Type t when t == typeof(DateOnly) => GetArgAs<DateOnly>().Formatted(),
                Type t when t == typeof(DateTime) => GetArgAs<DateTime>().Formatted(),
                Type t when t == typeof(TimeOnly) => GetArgAs<TimeOnly>().Formatted(),
                Type t when t == typeof(TimeSpan) => GetArgAs<TimeSpan>().Formatted(),
                _ => arg?.ToString()!
            };
        }

        Arg = arg;
        Filter = filter;
        Kind = kind;
        Message = null!;
        State = Valid;
        Tag = tag;
        TypeName = typeof(T).FullName!;

        formatted = GetFormatedString();
    }

    internal TagArg(Tag tag, TagArgState state)
    {
        Arg = default!;
        Filter = default;
        Kind = default;
        Message = $"The \"{tag}\" arg failed validation.";
        State = state;
        Tag = tag;
        TypeName = null!;

        formatted = "{Invalid}";
    }

    internal TagArg(Tag tag, string input, TagArgState state, int? length = null)
    {
        string GetMessage()
        {
            return state switch
            {
                BadChars => $"The \"{tag}\" arg contains one or more invalid characters.",
                Invalid => $"The \"{tag}\" arg failed validation.",
                NotTrimmed => $"The \"{tag}\" arg has not been trimmed.",
                NullOrWhitespace => $"The \"{tag}\" input may not be null, empty or whitespace,",
                ParseFailed => $"The \"{tag}\" input could not be parsed (Input: \"{input}\", Type: {typeof(T)}).",
                TooLong => $"The \"{tag}\" arg is greater than {length} characters long.",
                _ => throw new ArgumentOutOfRangeException(nameof(state))
            };
        };

        Arg = default!;
        Filter = default;
        Kind = default;
        Message = GetMessage();
        State = state;
        Tag = tag;
        TypeName = null!;

        formatted = "{Invalid}";
    }

    public Tag Tag { get; }
    public T Arg { get; }
    public TagArgArgKind Kind { get; }
    public string TypeName { get; }
    public string Message { get; }
    public TagArgState State { get; }
    public AsciiFilter Filter { get; }

    public bool IsValid => State == Valid;

    public V GetArgAs<V>() => (V)Convert.ChangeType(Arg, typeof(V))!;

    public object GetArgAsObject() => Arg!;

    public Result<V> ToFailure<V>(string code)
    {
        if (IsValid)
        {
            throw new InvalidOperationException(
                "A Failure<R> may only be returned when IsValid is false.");
        }

        return Result.Failure<V>(new Error(code, Message));
    }

    public Result ToResult(string code)
    {
        if (IsValid)
            return Result.Success(Arg);
        else
            return Result.Failure<T>(new Error(code, Message));
    }

    public Error ToError(string code) => ToResult(code).Errors.First();

    public static bool TryGetNonTexLineKind(out TagArgArgKind kind)
    {
        var type = typeof(T);

        if (type.IsEnum)
        {
            kind = TagArgArgKind.Enum;

            return true;
        }

        kind = typeof(T) switch
        {
            Type t when t == typeof(bool) => TagArgArgKind.Bool,
            Type t when t == typeof(byte) => TagArgArgKind.Byte,
            Type t when t == typeof(char) => TagArgArgKind.Char,
            Type t when t == typeof(DateOnly) => TagArgArgKind.DateOnly,
            Type t when t == typeof(DateTime) => TagArgArgKind.DateTime,
            Type t when t == typeof(double) => TagArgArgKind.Double,
            Type t when t == typeof(Email) => TagArgArgKind.Email,
            Type t when t == typeof(float) => TagArgArgKind.Float,
            Type t when t == typeof(Guid) => TagArgArgKind.Guid,
            Type t when t == typeof(short) => TagArgArgKind.Int16,
            Type t when t == typeof(int) => TagArgArgKind.Int32,
            Type t when t == typeof(long) => TagArgArgKind.Int64,
            Type t when t == typeof(MultiTag) => TagArgArgKind.MultiTag,
            Type t when t == typeof(Phone) => TagArgArgKind.Phone,
            Type t when t == typeof(Tag) => TagArgArgKind.Tag,
            Type t when t == typeof(TimeOnly) => TagArgArgKind.TimeOnly,
            Type t when t == typeof(TimeSpan) => TagArgArgKind.TimeSpan,
            Type t when t == typeof(Uri) => TagArgArgKind.Uri,
            _ => default
        };

        return !kind.IsDefault();
    }

    public override string ToString() => formatted;

    public static TagArg<JsonElement> Create(
        Tag tag, JsonElement element, Func<JsonElement, bool> isValid = null!)
    {
        tag.MayNotBe().Null();

        if (isValid is not null && !isValid(element))
            return new TagArg<JsonElement>(tag, Invalid);

        return new TagArg<JsonElement>(tag, element, TagArgArgKind.Json);
    }

    public static TagArg<string> Create(Tag tag, string input, bool isRequired,
        AsciiFilter filter = AsciiFilter.AllChars, Func<string, bool> isValid = null!)
    {
        tag.MayNotBe().Null();
        filter.MayNotBe().Default();

        if (!isRequired && string.IsNullOrEmpty(input))
            return new TagArg<string>(tag, null!, TagArgArgKind.TextLine);

        if (TagArg<string>.IsNullOrWhiteSpace(input, tag, out var tagArg))
            return tagArg;

        if (!input.IsNonEmptyAndAscii(filter))
            return new TagArg<string>(tag, input, BadChars);

        if (isValid is null)
        {
            if (input.Length > MAX_TEXTLINE_LENGTH)
                return new TagArg<string>(tag, input, TooLong, MAX_TEXTLINE_LENGTH);

            if (!input.IsTrimmed())
                return new TagArg<string>(tag, input, NotTrimmed);
        }
        else
        {
            if (!isValid(input))
                return new TagArg<string>(tag, input, Invalid);
        }

        return new TagArg<string>(tag, input, TagArgArgKind.TextLine, filter);
    }

    public static TagArg<T> Create(Tag tag, T arg, Func<T, bool> isValid = null!)
    {
        tag.MayNotBe().Null();

        if (!TryGetNonTexLineKind(out TagArgArgKind kind))
            throw new ArgumentOutOfRangeException(nameof(arg));

        if (isValid is not null && !isValid(arg))
            return new TagArg<T>(tag, Invalid);

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
