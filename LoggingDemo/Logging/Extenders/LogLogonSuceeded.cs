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
    private record LogonSucceededDetails(Broker Broker, Gateway Gateway, String AccountId);

    public static void LogLogonSucceeded(
        this ILogger logger,
        MultiTag multiTag,
        Broker broker,
        Gateway gateway,
        string accountId,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        logger.LogonSucceeded(
            new LogonSucceededDetails(broker, gateway, accountId),
            new LogContext(multiTag, calledBy, correlationId));
    }

    [LoggerMessage(
        EventId = CustomEventIds.LogonSucceeded,
        EventName = nameof(LogonSucceeded),
        Level = LogLevel.Information,
        Message = LogConsts.StandardMessage)]
    private static partial void LogonSucceeded(
        this ILogger logger,
        LogonSucceededDetails details,
        LogContext context);
}