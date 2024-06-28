using Microsoft.Extensions.Logging;
using SquidEyes.Fundamentals.Results;
using static SquidEyes.Fundamentals.TagArgState;

namespace SquidEyes.Fundamentals;

public class TagArg<T> : ITagArg
{
    internal TagArg(Tag tag, T value)
    {
        Tag = tag;
        Value = value;
        State = TagArgState.IsValid;
        Message = null!;
    }

    internal TagArg(Tag tag, string input, TagArgState state)
    {
        Tag = tag;
        State = state;
        Value = default!;

        Message = state switch
        {
            NullOrEmpty => $"The \"{tag}\" input may not be null or empty",
            ParseError => $"The \"{tag}\" input could not be parsed (Input: {input})",
            NotValid => $"The \"{tag}\" value is an invalid string.",
            _ => throw new ArgumentOutOfRangeException(nameof(state))
        };
    }

    public Tag Tag { get; }
    public T Value { get; }
    public string Message { get; }
    public TagArgState State { get; }

    public bool IsValid => State == TagArgState.IsValid;

    public Result<R> ToFailureResult<R>(string code)
    {
        if (State != TagArgState.IsValid)
        {
            throw new InvalidOperationException(
                "The \"ToFailureResult\" method may only be called on invalid TagArgs!");
        }

        return Result.Failure<R>(new Error(code, Message));
    }

    public void LogIfFailure(ILogger logger, string code)
    {
        if (State != TagArgState.IsValid)
            logger.LogWarning(new Error(code, Message));
    }

    internal static bool IsNullOrEmpty(
        string input, Tag tag, bool isOptional, out TagArg<T> arg)
    {
        arg = default!;

        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                arg = new TagArg<T>(tag, default!);
            else
                arg = new TagArg<T>(tag, input!, NullOrEmpty);
        }

        return !arg.IsDefault();
    }
}
