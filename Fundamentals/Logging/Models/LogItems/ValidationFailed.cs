// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation.Results;

namespace SquidEyes.Fundamentals;

public class ValidationFailed(Tag activity, ValidationFailure failure) 
    : LogItemBase(Severity.Warn, activity)
{
    private readonly ValidationFailure failure = failure.MayNotBe().Null();

    public override (Tag, object)[] GetTagValues()
    {
        return
        [
            (Tag.Create("PropertyName"), failure.PropertyName),
            (Tag.Create("ErrorCode"), failure.ErrorCode),
            (Tag.Create("ErrorMessage"), failure.ErrorMessage)
        ];
    }
}