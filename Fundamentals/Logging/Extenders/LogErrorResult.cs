//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

using Microsoft.Extensions.Logging;
using SquidEyes.Fundamentals.Results;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    public static void LogErrorResult(
        this ILogger logger,
        Tag activity,
        Error error,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        logger.ErrorResult(
            LogLevel.Warning,
            nameof(ErrorResult),
            calledBy,
            activity.Value!,
            (correlationId.IsDefault() ? Guid.NewGuid() : correlationId).ToString("N"),
            error.Code.ToString(),
            error.Message);
    }

    [LoggerMessage(
        EventId = EventIds.ErrorResult,
        EventName = nameof(ErrorResult),
        Message = "EventKind={EventKind},Caller={CalledBy};Activity={Activity};CorrelationId={CorrelationId};Code={Code};Message={Message}")]
    private static partial void ErrorResult(
        this ILogger logger,
        LogLevel logLevel,
        string eventKind,
        string calledBy,
        string activity,
        string correlationId,
        string code,
        string message);
}