// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using SquidEyes.Fundamentals.Results;

namespace SquidEyes.Fundamentals;

public class ErrorReturned(Tag activity, Error error, TagValueSet metadata = null!)
    : LogItemBase(Severity.Warn, activity, metadata!)
{
    private readonly Error error = error.MayNotBe().Null();

    protected override TagValueSet GetCustomTagValues()
    {
        var tagValues = new TagValueSet();

        tagValues.Upsert("Code", error.Code);
        tagValues.Upsert("Message", error.Message);

        return tagValues;
    }
}