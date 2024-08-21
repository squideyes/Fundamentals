using SquidEyes.Fundamentals;
using FluentAssertions;

namespace SquidEyes.UnitTests.Results;

public class ResultExtendersTests
{
    [Fact]
    public void Ensure_WhenResultIsFailure_ShouldReturnOriginalResult()
    {
        var result = Result.Failure<int>(new Error("OriginalError", "ERR001"));
        var ensuredResult = result.Ensure(v => v > 0, new Error("ValueMustBePositive", "VAL001"));

        ensuredResult.Should().Be(result);
    }

    [Fact]
    public void Ensure_WhenResultIsSuccessAndPredicateIsTrue_ShouldReturnOriginalResult()
    {
        var result = Result.Success(5);
        var ensuredResult = result.Ensure(v => v > 0, new Error("ValueMustBePositive", "VAL001"));

        ensuredResult.Should().Be(result);
    }

    [Fact]
    public void Ensure_WhenResultIsSuccessAndPredicateIsFalse_ShouldReturnFailureResult()
    {
        var result = Result.Success(0);
        var error = new Error("ValueMustBePositive", "VAL001");
        var ensuredResult = result.Ensure(v => v > 0, error);

        ensuredResult.IsFailure.Should().BeTrue();
        ensuredResult.Errors.Should().ContainSingle().Which.Should().Be(error);
    }

    [Fact]
    public void Map_WhenResultIsSuccess_ShouldApplyMappingFunction()
    {
        var result = Result.Success(5);
        var mappedResult = result.Map(v => v * 2);

        mappedResult.IsSuccess.Should().BeTrue();
        mappedResult.Value.Should().Be(10);
    }

    [Fact]
    public void Map_WhenResultIsFailure_ShouldReturnFailureResult()
    {
        var error = new Error("OriginalError", "ERR001");
        var result = Result.Failure<int>(error);
        var mappedResult = result.Map(v => v.ToString());

        mappedResult.IsFailure.Should().BeTrue();
        mappedResult.Errors.Should().ContainSingle().Which.Should().Be(error);
    }

    [Fact]
    public void Bind_WhenResultIsSuccess_ShouldApplyBindFunction()
    {
        var result = Result.Success(5);
        var bindResult = result.Bind(v => Result.Success(v * 2));

        bindResult.IsSuccess.Should().BeTrue();
        bindResult.Value.Should().Be(10);
    }

    [Fact]
    public void Bind_WhenResultIsFailure_ShouldReturnFailureResult()
    {
        var error = new Error("OriginalError", "ERR001");
        var result = Result.Failure<int>(error);
        var bindResult = result.Bind(v => Result.Success(v * 2));

        bindResult.IsFailure.Should().BeTrue();
        bindResult.Errors.Should().ContainSingle().Which.Should().Be(error);
    }

    [Fact]
    public async Task Bind_WithTask_WhenResultIsSuccess_ShouldApplyBindFunction()
    {
        var result = Result.Success(5);
        var bindResult = await result.Bind(v => Task.FromResult(Result.Success(v * 2)));

        bindResult.IsSuccess.Should().BeTrue();
        bindResult.Value.Should().Be(10);
    }

    [Fact]
    public async Task Bind_WithTask_WhenResultIsFailure_ShouldReturnFailureResult()
    {
        var error = new Error("OriginalError", "ERR001");
        var result = Result.Failure<int>(error);
        var bindResult = await result.Bind(v => Task.FromResult(Result.Success(v * 2)));

        bindResult.IsFailure.Should().BeTrue();
        bindResult.Errors.Should().ContainSingle().Which.Should().Be(error);
    }

    [Fact]
    public void Tap_WhenResultIsSuccess_ShouldExecuteAction()
    {
        var result = Result.Success(5);
        var wasTapExecuted = false;
        var tappedResult = result.Tap(_ => wasTapExecuted = true);

        tappedResult.Should().Be(result);
        wasTapExecuted.Should().BeTrue();
    }

    [Fact]
    public void Tap_WhenResultIsFailure_ShouldNotExecuteAction()
    {
        var result = Result.Failure<int>(new Error("Error", "ERR001"));
        var wasTapExecuted = false;
        var tappedResult = result.Tap(_ => wasTapExecuted = true);

        tappedResult.Should().Be(result);
        wasTapExecuted.Should().BeFalse();
    }

    [Fact]
    public async Task Tap_WithTask_WhenResultIsSuccess_ShouldExecuteAction()
    {
        var result = Result.Success(5);
        var wasTapExecuted = false;
        var tappedResult = await result.Tap(() => Task.Run(() => wasTapExecuted = true));

        tappedResult.Should().Be(result);
        wasTapExecuted.Should().BeTrue();
    }

    [Fact]
    public async Task Tap_WithTask_WhenResultIsFailure_ShouldNotExecuteAction()
    {
        var result = Result.Failure<int>(new Error("Error", "ERR001"));
        var wasTapExecuted = false;
        var tappedResult = await result.Tap(() => Task.Run(() => wasTapExecuted = true));

        tappedResult.Should().Be(result);
        wasTapExecuted.Should().BeFalse();
    }

    [Fact]
    public async Task Tap_WithTaskResult_WhenResultIsSuccess_ShouldExecuteAction()
    {
        var resultTask = Task.FromResult(Result.Success(5));
        var wasTapExecuted = false;
        var tappedResult = await resultTask.Tap(_ => Task.Run(() => wasTapExecuted = true));

        tappedResult.IsSuccess.Should().BeTrue();
        tappedResult.Value.Should().Be(5);
        wasTapExecuted.Should().BeTrue();
    }

    [Fact]
    public async Task Tap_WithTaskResult_WhenResultIsFailure_ShouldNotExecuteAction()
    {
        var resultTask = Task.FromResult(Result.Failure<int>(new Error("Error", "ERR001")));
        var wasTapExecuted = false;
        var tappedResult = await resultTask.Tap(_ => Task.Run(() => wasTapExecuted = true));

        tappedResult.IsFailure.Should().BeTrue();
        wasTapExecuted.Should().BeFalse();
    }
}