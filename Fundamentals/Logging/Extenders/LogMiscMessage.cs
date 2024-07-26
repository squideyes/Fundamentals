// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    public static void LogMiscMessage(
        this ILogger logger,
        Tag activity,
        Tag code,
        string message,
        MiscLogLevel level = MiscLogLevel.Info,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        logger.MiscInfo(
            level == MiscLogLevel.Info ? LogLevel.Information : LogLevel.Warning,
            nameof(MiscInfo),
            calledBy,
            activity.Value!,
            (correlationId.IsDefault() ? Guid.NewGuid() : correlationId).ToString("N"),
            code.Value!,
            message);
    }

    [LoggerMessage(
        EventId = EventIds.MiscInfo,
        EventName = nameof(MiscInfo),
        Message = LoggingConsts.Prefix + "Code={Code};Message={Message}")]
    private static partial void MiscInfo(
        this ILogger logger,
        LogLevel logLevel,
        string eventKind,
        string calledBy,
        string activity,
        string correlationId,
        string code,
        string message);
}