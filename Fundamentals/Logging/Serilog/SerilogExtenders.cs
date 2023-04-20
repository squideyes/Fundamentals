// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Serilog.Events;

namespace SquidEyes.Fundamentals;

public static class SerilogExtenders
{
    public static LogEventLevel ToLogEventLevel(this LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Debug => LogEventLevel.Debug,
            LogLevel.Info => LogEventLevel.Information,
            LogLevel.Warn => LogEventLevel.Warning,
            LogLevel.Error => LogEventLevel.Error,
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel))
        };
    }
}