namespace SquidEyes.Fundamentals;

public class LogContext(MultiTag multiTag, string calledBy, Guid correlationId)
{
    public string MultiTag { get; } = multiTag.ToString();
    public string CalledBy { get; } = calledBy;
    public string CorrelationId { get; } = 
        (correlationId.IsDefault() ? Guid.NewGuid() : correlationId).ToString("N");
}
