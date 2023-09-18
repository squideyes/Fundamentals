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
    public static bool TrySetStandardLogger(ILogger logger, IConfiguration config,  
        TagValueSet enrichWiths, Action<LoggerConfiguration> configure = null!)
    {
        var oneOf = SerilogArgs.Create(config);

        if (oneOf.TryPickT1(out var result, out var _))
        {
            foreach (var e in result.Errors)
                logger.Warning($"{e.PropertyName}: {e.ErrorMessage}");

            return false;
        }

        var serilogArgs = oneOf.AsT0;

        var minLogEventLevel = serilogArgs.MinSeverity.ToLogEventLevel();

        var loggerConfiguration = new LoggerConfiguration()
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

        foreach (var tv in enrichWiths)
            loggerConfiguration.Enrich.WithProperty(tv.Key.ToString(), tv.Value);

        loggerConfiguration.Enrich.FromLogContext();

        loggerConfiguration.WriteTo.Seq(serilogArgs.SeqApiUri.AbsoluteUri,
            apiKey: serilogArgs.SeqApiKey, restrictedToMinimumLevel: minLogEventLevel);

        loggerConfiguration.WriteTo.Console(theme: AnsiConsoleTheme.Code, outputTemplate:
            "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}");

        configure?.Invoke(loggerConfiguration);

        Log.Logger = loggerConfiguration.CreateLogger();

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