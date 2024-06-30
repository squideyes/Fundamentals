namespace SquidEyes.Fundamentals;

public interface IValidationFailed
{
    public static readonly Error ValidationError =
        new("ValidationFailure", "A validation problem occurred.");

    Error[] Errors { get; }
}
