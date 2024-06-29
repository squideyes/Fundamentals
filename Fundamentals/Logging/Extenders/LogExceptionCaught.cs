// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    public static void LogExceptionCaught(
        this ILogger logger,
        Tag activity,
        Exception exception,
        bool withFileInfo = false,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        if (correlationId.IsDefault())
            correlationId = Guid.NewGuid();

        logger.ExceptionCaughtWithLevel(activity, exception,
            withFileInfo, 0, correlationId.ToString("N"), calledBy);
    }

    private static void ExceptionCaughtWithLevel(
        this ILogger logger,
        Tag activity,
        Exception exception,
        bool withFileInfo,
        int level,
        string correlationId,
        string calledBy)
    {
        logger.ExceptionCaught(
            LogLevel.Error,
            nameof(LogExceptionCaught),
            calledBy,
            activity.Value!,
            correlationId,
            level,
            exception.GetType().FullName!,
            exception.TargetSite!.ToString()!,
            exception.Message,
            string.Join(";", exception.GetStackTraceLines(withFileInfo)));

        if (exception.InnerException is not null)
        {
            logger.ExceptionCaughtWithLevel(activity, exception.InnerException,
                withFileInfo, ++level, correlationId, calledBy);
        }
    }

    [LoggerMessage(
        EventId = EventIds.ExceptionCaught,
        EventName = nameof(ExceptionCaught),
        Message = "EventKind={EventKind},Caller={CalledBy};Activity={Activity};CorrelationId={CorrelationId};Level={Level};ErrorType={ErrorType};TargetSite={TargetSite};Message={ErrorMessage};StackTrace={StackTrace}")]
    private static partial void ExceptionCaught(
        this ILogger logger,
        LogLevel logLevel,
        string eventKind,
        string calledBy,
        string activity,
        string correlationId,
        int level,
        string errorType,
        string targetSite,
        string errorMessage,
        string stackTrace);
}