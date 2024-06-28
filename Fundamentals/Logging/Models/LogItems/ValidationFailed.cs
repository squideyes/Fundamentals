// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation.Results;

namespace SquidEyes.Fundamentals;

public class ValidationFailed(Tag activity, ValidationFailure failure, TagValueSet metadata = null!)
    : LogItemBase(Severity.Warn, activity, metadata)
{
    private readonly ValidationFailure failure = failure.MayNotBe().Null();

    protected override TagValueSet GetCustomTagValues()
    {
        var tagValues = new TagValueSet();

        tagValues.Upsert("PropertyName", failure.PropertyName);
        tagValues.Upsert("ErrorCode", failure.ErrorCode);
        tagValues.Upsert("ErrorMessage", failure.ErrorMessage);

        return tagValues;
    }
}