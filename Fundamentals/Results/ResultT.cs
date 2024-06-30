namespace SquidEyes.Fundamentals;

public class Result<T> : Result
{
    private readonly T? value;

    protected internal Result(T? value, bool isSuccess, Error error)
        : base(isSuccess, error) => this.value = value;

    protected internal Result(T? value, bool isSuccess, Error[] errors)
        : base(isSuccess, errors) => this.value = value;

    public T Value => IsSuccess ? value! : throw new InvalidOperationException(
        "The value of a Failure result can not be accessed.");

    public static implicit operator Result<T>(T? value) => Create(value);
}
