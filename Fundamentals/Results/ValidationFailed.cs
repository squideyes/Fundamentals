namespace SquidEyes.Fundamentals;

public sealed class ValidationFailed : Result, IValidationFailed
{
    private ValidationFailed(Error[] errors)
        : base(false, IValidationFailed.ValidationError) => Errors = errors;

    public new Error[] Errors { get; }

    public static ValidationFailed WithErrors(Error[] errors) => new(errors);
}
