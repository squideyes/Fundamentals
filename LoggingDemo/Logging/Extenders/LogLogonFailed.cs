// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using SquidEyes.Fundamentals;
using System.Net;
using System.Runtime.CompilerServices;

namespace LoggingDemo;

public static partial class ILoggerExtenders
{
    private record LogonFailedDetails(
        Broker Broker, 
        Gateway Gateway, 
        string AccountId,
        HttpStatusCode StatusCode,
        string Reason);

    public static void LogLogonFailed(
        this ILogger logger,
        Broker broker,
        Gateway gateway,
        string accountId,
        HttpStatusCode statusCode,
        string reason,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        logger.LogonFailed(
            new LogonFailedDetails(broker, gateway, accountId, statusCode, reason),
            new BasicLogScope(calledBy, correlationId));
    }

    [LoggerMessage(
        EventId = CustomEventIds.LogonFailed,
        EventName = nameof(LogonFailed),
        Level = LogLevel.Information,
        SkipEnabledCheck = true,
        Message = $"{nameof(LogonFailed)}={{@Details}};Scope={{@Scope}}")]
    private static partial void LogonFailed(
        this ILogger logger,
        LogonFailedDetails details,
        BasicLogScope scope);
}