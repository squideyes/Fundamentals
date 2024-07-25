using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    public static void LogMiscTagArgs(
        this ILogger logger,
        Tag activity,
        Tag code,
        TagArgSet tagArgs,
        MiscLogLevel level = MiscLogLevel.Info,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        logger.Log(level == MiscLogLevel.Info ? LogLevel.Information : LogLevel.Warning,
            LoggingConsts.Prefix + "Code={Code};TagArgs={@TagArgs}",
            "MiscTagArgs"!,
            calledBy,
            activity.Value!,
            (correlationId.IsDefault() ? Guid.NewGuid() : correlationId).ToString("N"),
            code.Value,
            tagArgs.ToSimplifiedDictionary());
    }
}