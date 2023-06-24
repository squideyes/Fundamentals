// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation.Results;

namespace SquidEyes.Fundamentals;

public class ValidationFailed : LogItemBase
{
    private readonly ValidationFailure failure;

    public ValidationFailed(Tag activity, ValidationFailure failure)
        : base(Severity.Warn, activity)
    {
        this.failure = failure.MayNot().BeNull();
    }

    public override (Tag, object)[] GetTagValues()
    {
        return new (Tag, object)[]
        {
            (Tag.Create("PropertyName"), failure.PropertyName),
            (Tag.Create("ErrorCode"), failure.ErrorCode),
            (Tag.Create("ErrorMessage"), failure.ErrorMessage)
        };
    }
}