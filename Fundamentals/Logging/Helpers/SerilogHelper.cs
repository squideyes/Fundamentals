// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using static SquidEyes.Fundamentals.Severity;

namespace SquidEyes.Fundamentals;

public static class SerilogHelper
{
    public static ILogger GetStandardLogger(Uri seqApiUri, string seqApiKey = null!,
        Severity minServerity = Info, Dictionary<Tag, object> enrichWith = null!)
    {
        seqApiUri.Must().Be(v => v.IsAbsoluteUri);
        seqApiKey?.MayNot().BeNullOrWhitespace();

        var minLogEventLevel = minServerity.ToLogEventLevel();

        var config = new LoggerConfiguration()
            .MinimumLevel.Is(minLogEventLevel)
            .Destructure.ByTransforming<DateOnly>(
                v => v.ToString("yyyy-MM-dd"))
            .Destructure.ByTransforming<Enum>(
                v => v.ToString())
            .Destructure.ByTransforming<Guid>(
                v => v.ToString("D"))
            .Destructure.ByTransforming<Tag>(
                v => v.ToString())
            .Destructure.ByTransforming<TimeOnly>(
                v => v.ToString("HH:mm:ss.fff"))
            .Destructure.ByTransforming<TimeSpan>(
                v => v.ToString(@"d\.hh\:mm\:ss\.fff"));

        if (enrichWith != null)
        {
            foreach (var kv in enrichWith)
                config.Enrich.WithProperty(kv.Key.ToString(), kv.Value);
        }

        config.WriteTo.Seq(seqApiUri.AbsoluteUri,
            apiKey: seqApiKey.Convert(v => string.IsNullOrWhiteSpace(v) ? null : v),
            restrictedToMinimumLevel: minLogEventLevel);

        config.WriteTo.Console(
            theme: AnsiConsoleTheme.Code,
            outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}");

        return config.CreateLogger();
    }
}