//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    public static void LogIfFailedResult(
        this ILogger logger,
        Tag activity,
        Result result,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        if (result.IsSuccess)
            return;

        if (correlationId.IsDefault())
            correlationId = Guid.NewGuid();

        foreach (var error in result.Errors)
        {
            logger.FailedResult(
                LogLevel.Warning,
                nameof(ErrorResult),
                calledBy,
                activity.Value!,
                correlationId.ToString("N"),
                error.Code.ToString(),
                error.Message);
        }
    }

    [LoggerMessage(
        EventId = EventIds.ErrorResult,
        EventName = nameof(ErrorResult),
        Message = "EventKind={EventKind},Caller={CalledBy};Activity={Activity};CorrelationId={CorrelationId};Code={Code};Message={Message}")]
    private static partial void FailedResult(
        this ILogger logger,
        LogLevel logLevel,
        string eventKind,
        string calledBy,
        string activity,
        string correlationId,
        string code,
        string message);
}