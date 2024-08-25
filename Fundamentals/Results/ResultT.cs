// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class Result<T> : Result
{
    private readonly T? value;

    protected internal Result(T? value, ResultKind kind, Error error)
        : base(kind, error) => this.value = value;

    protected internal Result()
        : base() => value = default;

    protected internal Result(T? value, ResultKind kind, Error[] errors)
        : base(kind, errors) => this.value = value;

    public T Value => IsSuccess ? value! : throw new InvalidOperationException(
        "The Value of a \"Failure\" Result may not be accessed.");

    public static implicit operator Result<T>(T? value) => Create(value);
}