namespace SquidEyes.Fundamentals;

internal static class LogHelper
{
    public static string GetCorrelationId(this Guid value) =>
        (value.IsDefault() ? Guid.NewGuid() : value).ToString("N");

}
