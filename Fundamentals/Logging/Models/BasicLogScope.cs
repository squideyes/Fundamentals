// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public record BasicLogScope(string CalledBy, string CorrelationId)
{
    public BasicLogScope(string calledBy, Guid correlationId)
        : this(calledBy, ToCorrelationId(correlationId))
    {
    }

    private static string ToCorrelationId(Guid value) =>
        (value.IsDefault() ? Guid.NewGuid() : value).ToString("N");
};