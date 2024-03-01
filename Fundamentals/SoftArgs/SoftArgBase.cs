using ErrorOr;
using FluentValidation.Results;

namespace SquidEyes.Fundamentals;

public abstract class SoftArgBase : ISoftArg
{
    public Tag Tag { get; }
    public SoftArgStatus Status { get; }
    public string? Message { get; }

    public bool IsValid => Status == SoftArgStatus.IsValid;

    protected SoftArgBase(Tag tag)
    {
        Tag = tag;
        Status = SoftArgStatus.IsValid;
        Message = null;
    }

    protected SoftArgBase(Tag tag, string input, SoftArgStatus status)
    {
        Tag = tag;

        Status = status;

        Message = status switch
        {
            SoftArgStatus.NullOrEmpty =>
                $"The '{tag}' input may not be null or empty",
            SoftArgStatus.ParseError =>
                $"The '{tag}' input could not be parsed (Input: {input})",
            SoftArgStatus.NotValid =>
                $"The '{tag}' value is an invalid string.",
            _ =>
                throw new ArgumentOutOfRangeException(nameof(status))
        };
    }

    public Error ToError(string code = null!)
    {
        if (Status == SoftArgStatus.IsValid)
            throw new InvalidOperationException("IsValid == false!");

        return Error.Validation(code, Message!);
    }

    public ValidationFailure ToValidationFailure() =>
        new(Tag.ToString(), Message);

    protected void ThrowWhenNotIsValid()
    {
        if (Status != SoftArgStatus.IsValid)
        {
            throw new InvalidOperationException(
                "A non-valid value may not be returned!");
        }
    }
}
