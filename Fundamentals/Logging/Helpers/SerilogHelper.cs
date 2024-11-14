// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System.Diagnostics;
using System.Text.Json;
using static SquidEyes.Fundamentals.ILoggerExtenders;

namespace SquidEyes.Fundamentals;

public static class SerilogHelper
{
    public static void InitLogDotLogger(LogLevel minLogLevel = LogLevel.Information,
        Action<LoggerConfiguration> configure = null!)
    {
        Serilog.Debugging.SelfLog.Enable(output => Debug.WriteLine(output));

        Serilog.Debugging.SelfLog.Enable(Console.Error);

        var config = new LoggerConfiguration()
            .MinimumLevel.Is(minLogLevel.ToLogEventLevel())
            .OmitTypeField<BasicLogScope>()
            .OmitTypeField<ExceptionCaughtDetails>()
            .OmitTypeField<InvalidTagArgDetails>()
            .OmitTypeField<ValidationFailureDetails>()
            .OmitTypeField<MiscInfoDetails>()
            .OmitTypeField<MiscTagArgsDetails>()
            .OmitTypeField<ResultFailureDetails>()
            .OmitTypeField<BasicLogScope>()
            .Destructure.ByTransforming<DateOnly>(v => v.ToString("yyyy-MM-dd"))
            .Destructure.ByTransforming<DateTime>(v => v.ToString("MM/dd/yyyy HH:mm:ss.fff"))
            .Destructure.ByTransforming<Email>(v => v.ToString())
            .Destructure.ByTransforming<Enum>(v => v.ToString())
            .Destructure.ByTransforming<Guid>(v => v.ToString("D"))
            .Destructure.ByTransforming<MultiTag>(v => v.ToString())
            .Destructure.ByTransforming<Phone>(v => v.ToString())
            .Destructure.ByTransforming<SemVer>(v => v.ToString())
            .Destructure.ByTransforming<Tag>(v => v.ToString())
            .Destructure.ByTransforming<TimeOnly>(v => v.ToString("HH:mm:ss.fff"))
            .Destructure.ByTransforming<TimeSpan>(v => v.ToString(@"d\.hh\:mm\:ss\.fff"));

        config.Enrich.FromLogContext();

        config.WriteTo.Console(theme: AnsiConsoleTheme.Code, outputTemplate:
            "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}");

        configure?.Invoke(config);

        Log.Logger = config.CreateLogger();
    }

    public static LogEventLevel ToLogEventLevel(this LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Debug => LogEventLevel.Debug,
            LogLevel.Information => LogEventLevel.Information,
            LogLevel.Warning => LogEventLevel.Warning,
            LogLevel.Error => LogEventLevel.Error,
            _ => throw new LoggingException("A valid \"logLevel\" must be supplied.")
        };
    }

    public static Serilog.ILogger GetInitLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();
    }

    public static bool IsValid(this Serilog.ILogger logger, IEnumerable<ITagArg> tagArgs)
    {
        var isValid = true;

        foreach (var tagArg in tagArgs)
        {
            if (tagArg.IsValid)
                continue;

            isValid = false;

            logger.Warning("InitError: {Message}", tagArg.Message);
        }

        return isValid;
    }

    public static object Expand(this JsonElement element, bool forcePascalCase = false)
    {
        static string ToPascalCase(string value) => char.ToUpper(value[0]) + value[1..];

        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                var dict = new Dictionary<string, object>();
                foreach (var property in element.EnumerateObject())
                {
                    var key = forcePascalCase ? ToPascalCase(property.Name) : property.Name;
                    dict[key] = Expand(property.Value);
                }
                return dict;
            case JsonValueKind.Array:
                return element.EnumerateArray()
                    .Select(v => Expand(v, forcePascalCase))
                    .ToList();
            case JsonValueKind.String:
                return element.GetString()!;
            case JsonValueKind.Number:
                if (element.TryGetInt64(out long int64))
                    return int64;
                return element.GetDouble();
            case JsonValueKind.True:
                return true;
            case JsonValueKind.False:
                return false;
            case JsonValueKind.Null:
                return null!;
            default:
                return element.ToString();
        }
    }
}