using ErrorOr;
using FluentValidation.Results;

namespace SquidEyes.Fundamentals;

public abstract class SoftBase
{
    public Tag Tag { get; }
    public SoftStatus Status { get; }
    public string? Message { get; }

    public bool IsValid => Status == SoftStatus.IsValid;

    protected SoftBase(Tag tag)
    {
        Tag = tag;
        Status = SoftStatus.IsValid;
        Message = null;
    }

    protected SoftBase(Tag tag, string input, SoftStatus status)
    {
        Tag = tag;

        Status = status;

        Message = status switch
        {
            SoftStatus.NullOrEmpty =>
                $"The '{tag}' input may not be null or empty",
            SoftStatus.ParseError =>
                $"The '{tag}' input could not be parsed (Input: {input})",
            SoftStatus.NotValid =>
                $"The '{tag}' value is an invalid string.",
            _ =>
                throw new ArgumentOutOfRangeException(nameof(status))
        };
    }

    public Error ToError(string code = null!)
    {
        if (Status == SoftStatus.IsValid)
            throw new InvalidOperationException("IsValid == false!");

        return Error.Validation(code, Message!);
    }

    public ValidationFailure ToValidationFailure() =>
        new(Tag.ToString(), Message);

    protected void ThrowWhenNotIsValid()
    {
        if (Status != SoftStatus.IsValid)
        {
            throw new InvalidOperationException(
                "A non-valid value may not be returned!");
        }
    }
}
