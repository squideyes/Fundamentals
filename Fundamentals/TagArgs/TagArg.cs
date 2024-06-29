using static SquidEyes.Fundamentals.TagArgState;

namespace SquidEyes.Fundamentals;

public class TagArg<T> : ITagArg
{
    internal TagArg(Tag tag, T arg)
    {
        Tag = tag;
        Arg = arg;
        State = Valid;
        Message = null!;
    }

    internal TagArg(Tag tag, string input, TagArgState state, int? length = null)
    {
        Tag = tag;
        State = state;
        Arg = default!;

        Message = state switch
        {
            NullOrWhitespace => $"The \"{tag}\" input may not be null, empty or whitespace,",
            ParseFailed => $"The \"{tag}\" input could not be parsed (Input: {input}, Type: {typeof(T)}).",
            Invalid => $"The \"{tag}\" arg failed validation.",
            TooShort => $"The \"{tag}\" arg is less than {length} characters long.",
            TooLong => $"The \"{tag}\" arg is greater than {length} characters long.",
            NotTrimmed => $"The \"{tag}\" arg has not been trimmed.",
            BadChars => $"The \"{tag}\" arg contains one or more invalid characters.",
            _ => throw new ArgumentOutOfRangeException(nameof(state))
        };
    }

    public Tag Tag { get; }
    public T Arg { get; }
    public string Message { get; }
    public TagArgState State { get; }

    public bool IsValid => State == Valid;

    internal static bool IsNullOrWhiteSpace(
        string input, Tag tag, bool isOptional, out TagArg<T> tagArg)
    {
        tagArg = default!;

        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                tagArg = new TagArg<T>(tag, default!);
            else
                tagArg = new TagArg<T>(tag, input!, NullOrWhitespace);
        }

        return !tagArg.IsDefault();
    }
}
