//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    public static void LogMiscEvent(
        this ILogger logger,
        Tag activity,
        string code,
        string message,
        MiscEventKind kind = MiscEventKind.Info,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        logger.MiscEvent(
            kind == MiscEventKind.Info ? LogLevel.Information : LogLevel.Warning,
            nameof(MiscEvent),
            calledBy,
            activity.Value!,
            (correlationId.IsDefault() ? Guid.NewGuid() : correlationId).ToString("N"),
            code,
            message);
    }

    [LoggerMessage(
        EventId = EventIds.MiscEvent,
        EventName = nameof(MiscEvent),
        Message = "EventKind={EventKind},Caller={CalledBy};Activity={Activity};CorrelationId={CorrelationId};Code={Code};Message={Message}")]
    private static partial void MiscEvent(
        this ILogger logger,
        LogLevel logLevel,
        string eventKind,
        string calledBy,
        string activity,
        string correlationId,
        string code,
        string message);
}