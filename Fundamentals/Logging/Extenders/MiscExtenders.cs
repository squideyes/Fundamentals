// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Microsoft.Extensions.Logging;
using Serilog.Events;

namespace SquidEyes.Fundamentals;

internal static class MiscExtenders
{
    public static LogEventLevel ToLogEventLevel(this Severity logLevel)
    {
        return logLevel switch
        {
            Severity.Debug => LogEventLevel.Debug,
            Severity.Info => LogEventLevel.Information,
            Severity.Warn => LogEventLevel.Warning,
            Severity.Error => LogEventLevel.Error,
            _ => throw new LogSmartException(
                "A defined \"severity\" must be supplied.")
        };
    }

    public static LogLevel ToLogLevel(this Severity logLevel)
    {
        return logLevel switch
        {
            Severity.Debug => LogLevel.Debug,
            Severity.Info => LogLevel.Information,
            Severity.Warn => LogLevel.Warning,
            Severity.Error => LogLevel.Error,
            _ => throw new LogSmartException(
                "A defined \"severity\" must be supplied.")
        };
    }

    public static bool IsTagValue(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!char.IsAsciiLetterUpper(value[0]))
            return false;

        if (value.Length == 1)
            return true;

        if (value.Length > 24)
            return false;

        return value.Skip(1).All(char.IsAsciiLetterOrDigit);
    }
}