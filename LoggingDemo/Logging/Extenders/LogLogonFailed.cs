// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using LoggingDemo;
using SquidEyes.Fundamentals.LoggingDemo;
using System.Net;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    public static void LogLogonFailed(
        this ILogger logger,
        Tag activity,
        Broker broker,
        Gateway gateway,
        HttpStatusCode statusCode,
        string reason,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        logger.LogonFailed(
            LogLevel.Information,
            nameof(LogonFailed),
            calledBy,
            activity.Value!,
            (correlationId.IsDefault() ? Guid.NewGuid() : correlationId).ToString("N"),
            broker,
            gateway,
            statusCode,
            reason);
    }

    [LoggerMessage(
        EventId = CustomEventIds.LogonFailed,
        EventName = nameof(LogonFailed),
        Message = "EventKind={EventKind},Caller={CalledBy};Activity={Activity};CorrelationId={CorrelationId};Broker={Broker};Gateway={Gateway};StatusCode={StatusCode};Reason={Reason}")]
    private static partial void LogonFailed(
        this ILogger logger,
        LogLevel logLevel,
        string eventKind,
        string calledBy,
        string activity,
        string correlationId,
        Broker broker,
        Gateway gateway,
        HttpStatusCode statusCode,
        string reason);
}