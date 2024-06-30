namespace SquidEyes.Fundamentals;

public sealed class ValidationFailed<T> : Result<T>, IValidationFailed
{
    private ValidationFailed(Error[] errors)
        : base(default, false, IValidationFailed.ValidationError)
    {
        Errors = errors;
    }

    public new Error[] Errors { get; }

    public static ValidationFailed<T> WithErrors(Error[] errors) => new(errors);
}
