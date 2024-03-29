// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace SquidEyes.Fundamentals;

public static class StandardLoggerBuilder
{
    public static bool TrySetStandardLogger(
        ILogger logger,
        IConfiguration config, 
        TagValueSet enrichWiths = null!, 
        Action<LoggerConfiguration> configure = null!)
    {
        var ee = SerilogArgs.Create(config);

        if (ee.IsError)
        {
            foreach (var e in ee.Errors)
                logger.Warning($"TrySetStandardLogger Error: {e.Description}");

            return true;
        }

        var minLogEventLevel = ee.Value.MinSeverity.ToLogEventLevel();

        var loggerConfig = new LoggerConfiguration()
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

        if (enrichWiths is not null)
        {
            foreach (var tv in enrichWiths)
                loggerConfig.Enrich.WithProperty(tv.Key.ToString(), tv.Value);
        }

        loggerConfig.Enrich.FromLogContext();

        loggerConfig.WriteTo.Seq(ee.Value.SeqApiUri.AbsoluteUri,
            apiKey: ee.Value.SeqApiKey, restrictedToMinimumLevel: minLogEventLevel);

        loggerConfig.WriteTo.Console(theme: AnsiConsoleTheme.Code, outputTemplate:
            "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}");

        configure?.Invoke(loggerConfig);

        Log.Logger = loggerConfig.CreateLogger();

        return true;
    }

    public static ILogger GetBootstrapLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
    }
}