// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class LogScope(string calledBy, Guid correlationId)
{
    public string CalledBy { get; } = calledBy;
    public string CorrelationId { get; } = 
        (correlationId.IsDefault() ? Guid.NewGuid() : correlationId).ToString("N");
}