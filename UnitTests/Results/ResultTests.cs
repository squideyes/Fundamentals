using SquidEyes.Fundamentals;
using FluentAssertions;

namespace SquidEyes.UnitTests.Results;

public class ResultTests
{
    [Fact]
    public void Success_ShouldCreateSuccessResult()
    {
        var result = Result.Success();

        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.IsCancel.Should().BeFalse();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Cancel_ShouldCreateCancelResult()
    {
        var result = Result.Cancel();

        result.IsCancel.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.IsFailure.Should().BeFalse();
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Failure_ShouldCreateFailureResult()
    {
        var error = new Error("TestError", "TEST001");
        var result = Result.Failure(error);

        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
        result.IsCancel.Should().BeFalse();
        result.Errors.Should().ContainSingle().Which.Should().Be(error);
    }

    [Fact]
    public void SuccessT_ShouldCreateSuccessResultWithValue()
    {
        var value = 42;
        var result = Result.Success(value);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(value);
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void FailureT_ShouldCreateFailureResultWithError()
    {
        var error = new Error("TestError", "TEST001");
        var result = Result.Failure<int>(error);

        result.IsFailure.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Should().Be(error);
        result.Invoking(r => r.Value).Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Create_WithNonNullValue_ShouldReturnSuccessResult()
    {
        var value = "test";
        var result = Result.Create(value);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(value);
    }

    [Fact]
    public void Create_WithNullValue_ShouldReturnFailureResult()
    {
        var result = Result.Create<string>(null);

        result.IsFailure.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Should().Be(Error.NullValue);
    }

    [Fact]
    public void Ensure_WhenPredicateIsTrue_ShouldReturnSuccessResult()
    {
        var value = 10;
        var result = Result.Ensure(value, v => v > 0, new Error("ValueMustBePositive", "VAL001"));

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(value);
    }

    [Fact]
    public void Ensure_WhenPredicateIsFalse_ShouldReturnFailureResult()
    {
        var value = -5;
        var error = new Error("ValueMustBePositive", "VAL001");
        var result = Result.Ensure(value, v => v > 0, error);

        result.IsFailure.Should().BeTrue();
        result.Errors.Should().ContainSingle().Which.Should().Be(error);
    }

    [Fact]
    public void Combine_WhenAllResultsAreSuccess_ShouldReturnSuccessResult()
    {
        var result1 = Result.Success(1);
        var result2 = Result.Success(2);
        var result3 = Result.Success(3);

        var combinedResult = Result.Combine(result1, result2, result3);

        combinedResult.IsSuccess.Should().BeTrue();
        combinedResult.Value.Should().Be(1);
    }

    [Fact]
    public void Combine_WhenAnyResultIsFailure_ShouldReturnFailureResult()
    {
        var result1 = Result.Success(1);
        var result2 = Result.Failure<int>(new Error("Error2", "ERR002"));
        var result3 = Result.Failure<int>(new Error("Error3", "ERR003"));

        var combinedResult = Result.Combine(result1, result2, result3);

        combinedResult.IsFailure.Should().BeTrue();
        combinedResult.Errors.Should().HaveCount(2);
        combinedResult.Errors.Should().Contain(e => e.Code == "Error2" && e.Message == "ERR002");
        combinedResult.Errors.Should().Contain(e => e.Code == "Error3" && e.Message == "ERR003");
    }

    [Fact]
    public void Combine_TwoResults_WhenBothAreSuccess_ShouldReturnSuccessResult()
    {
        var result1 = Result.Success(1);
        var result2 = Result.Success("test");

        var combinedResult = Result.Combine(result1, result2);

        combinedResult.IsSuccess.Should().BeTrue();
        combinedResult.Value.Should().Be((1, "test"));
    }

    [Fact]
    public void Combine_TwoResults_WhenAnyIsFailure_ShouldReturnFailureResult()
    {
        var result1 = Result.Success(1);
        var result2 = Result.Failure<string>(new Error("Error", "ERR001"));

        var combinedResult = Result.Combine(result1, result2);

        combinedResult.IsFailure.Should().BeTrue();
        combinedResult.Errors.Should().ContainSingle().Which.Should().Be(new Error("Error", "ERR001"));
    }
}