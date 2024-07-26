// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using LoggingDemo;
using SquidEyes.Fundamentals.LoggingDemo;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    public static void LogLogonSucceeded(
        this ILogger logger,
        Tag activity,
        Broker broker,
        Gateway gateway,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        logger.LogonSucceeded(
            LogLevel.Information,
            nameof(LogonSucceeded),
            calledBy,
            activity.Value!,
            (correlationId.IsDefault() ? Guid.NewGuid() : correlationId).ToString("N"),
            broker,
            gateway);
    }

    [LoggerMessage(
        EventId = CustomEventIds.LogonSucceeded, 
        EventName = nameof(LogonSucceeded),
        Message = "EventKind={EventKind};Caller={CalledBy};Activity={Activity};CorrelationId={CorrelationId};Broker={Broker};Gateway={Gateway}")]
    private static partial void LogonSucceeded(
        this ILogger logger,
        LogLevel logLevel,
        string eventKind,
        string calledBy,
        string activity,
        string correlationId,
        Broker broker,
        Gateway gateway);
}