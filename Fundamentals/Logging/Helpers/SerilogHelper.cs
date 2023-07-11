// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace SquidEyes.Fundamentals;

public static class SerilogHelper
{
    public static ILogger GetStandardLogger(
        StandardLoggerArgs args, Action<LoggerConfiguration> configure = null!)
    {
        var minLogEventLevel = args.MinSeverity.ToLogEventLevel();

        var config = new LoggerConfiguration()
            .MinimumLevel.Is(minLogEventLevel)
            .Destructure.ByTransforming<AccountId>(v => v.ToString())
            .Destructure.ByTransforming<ActorId>(v => v.ToString())
            .Destructure.ByTransforming<DateOnly>(v => v.ToString("yyyy-MM-dd"))
            .Destructure.ByTransforming<Delta>(v => v.ToString())
            .Destructure.ByTransforming<Email>(v => v.ToString())
            .Destructure.ByTransforming<Enum>(v => v.ToString())
            .Destructure.ByTransforming<Guid>(v => v.ToString("D"))
            .Destructure.ByTransforming<MultiTag>(v => v.ToString())
            .Destructure.ByTransforming<Phone>(v => v.ToString())
            .Destructure.ByTransforming<SemVer>(v => v.ToString())
            .Destructure.ByTransforming<Tag>(v => v.ToString())
            .Destructure.ByTransforming<TimeOnly>(v => v.ToString("HH:mm:ss.fff"))
            .Destructure.ByTransforming<TimeSpan>(v => v.ToString(@"d\.hh\:mm\:ss\.fff"));

        if (args.EnrichWith != null)
        {
            foreach (var kv in args.EnrichWith)
                config.Enrich.WithProperty(kv.Key.ToString(), kv.Value);
        }

        config.WriteTo.Seq(args.SeqApiUri.AbsoluteUri,
            apiKey: args.SeqApiKey.Convert(
                v => string.IsNullOrWhiteSpace(v) ? null : v),
            restrictedToMinimumLevel: minLogEventLevel);

        config.WriteTo.Console(
            theme: AnsiConsoleTheme.Code,
            outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}");

        configure?.Invoke(config);

        return config.CreateLogger();
    }
}