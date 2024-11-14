// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using SquidEyes.Fundamentals;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace LoggingDemo;

public static partial class ILoggerExtenders
{
    private record RawTxReceivedDetails(
        object Payload);

    public static void LogRawTxReceived(
        this ILogger logger,
        JsonElement payload,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        logger.RawTxReceived(
            new RawTxReceivedDetails(payload.Expand(true)),
            new BasicLogScope(calledBy, correlationId));
    }

    [LoggerMessage(
        EventId = CustomEventIds.RawTxReceived,
        EventName = nameof(RawTxReceived),
        Level = LogLevel.Information,
        SkipEnabledCheck = true,
        Message = $"{nameof(RawTxReceived)}={{@Details}};Scope={{@Scope}}")]
    private static partial void RawTxReceived(
        this ILogger logger,
        RawTxReceivedDetails details,
        BasicLogScope scope);
}