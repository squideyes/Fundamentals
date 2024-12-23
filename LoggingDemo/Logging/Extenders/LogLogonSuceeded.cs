// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using SquidEyes.Fundamentals;
using System.Runtime.CompilerServices;

namespace LoggingDemo;

public static partial class ILoggerExtenders
{
    private record LogonSucceededDetails(Broker Broker, Gateway Gateway, string AccountId);

    public static void LogLogonSucceeded(
        this ILogger logger,
        Broker broker,
        Gateway gateway,
        string accountId,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        logger.LogonSucceeded(
            new LogonSucceededDetails(broker, gateway, accountId),
            new BasicLogScope(calledBy, correlationId));
    }

    [LoggerMessage(
        EventId = CustomEventIds.LogonSucceeded,
        EventName = nameof(LogonSucceeded),
        Level = LogLevel.Information,
        SkipEnabledCheck = true,
        Message = $"{nameof(LogonSucceeded)}={{@Details}};Scope={{@Scope}}")]
    private static partial void LogonSucceeded(
        this ILogger logger,
        LogonSucceededDetails details,
        BasicLogScope scope);
}