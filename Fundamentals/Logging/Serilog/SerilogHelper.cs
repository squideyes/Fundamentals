// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using static SquidEyes.Fundamentals.LogLevel;

namespace SquidEyes.Fundamentals;

public static class SerilogHelper
{
    public static ILogger GetBasicLogger(Uri seqUri, string seqApiKey = null!,
        LogLevel minLogLevel = Info, Dictionary<string, object> enrichWith = null!)
    {
        if (seqUri == null)
            throw new ArgumentNullException(nameof(seqUri));

        if (!seqUri.IsAbsoluteUri)
            throw new ArgumentOutOfRangeException(nameof(seqUri));

        var minLogEventLevel = minLogLevel.ToLogEventLevel();

        var config = new LoggerConfiguration();

        config.MinimumLevel.Debug();//.Is(minLogEventLevel);

        config.Destructure.ByTransforming<Enum>(v => v.ToString());

        config.Destructure.ByTransforming<Identifier>(v => v.ToString());

        if (enrichWith != null)
        {
            foreach (var kv in enrichWith)
                config.Enrich.WithProperty(kv.Key, kv.Value);
        }

        config.WriteTo.Seq(seqUri.AbsoluteUri,
            apiKey: seqApiKey.Convert(v => string.IsNullOrWhiteSpace(v) ? null : v),
            restrictedToMinimumLevel: LogEventLevel.Debug);

        config.WriteTo.Console(
            theme: AnsiConsoleTheme.Code,
            outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}");

        return config.CreateLogger();
    }
}