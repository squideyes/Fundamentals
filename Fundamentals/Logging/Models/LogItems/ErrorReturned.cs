// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using ErrorOr;

namespace SquidEyes.Fundamentals;

public class ErrorReturned(Tag activity, Error error) 
    : LogItemBase(Severity.Warn, activity)
{
    private readonly Error error = error.MayNotBe().Default();

    public override (Tag, object)[] GetTagValues()
    {
        var result = new List<(Tag, object)>()
        {
            (Tag.Create("Type"), error.Type),
            (Tag.Create("Code"), error.Code),
            (Tag.Create("Message"), error.Description)
        };

        if (error.Metadata is not null && error.Metadata.Count > 0)
            result.Add((Tag.Create("Metadata"), error.Metadata));

        return [.. result];
    }
}