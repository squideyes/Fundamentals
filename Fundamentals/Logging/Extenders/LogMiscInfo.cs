// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    internal record MiscInfoDetails(
        Tag Tag, 
        string Message);

    public static void LogMiscInfo(
        this ILogger logger,
        Tag code,
        string message,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        logger.MiscInfo(
            new MiscInfoDetails(code.Value!, message),
            new BasicLogScope(calledBy, correlationId));
    }

    [LoggerMessage(
        EventId = EventIds.MiscInfo,
        EventName = nameof(MiscInfo),
        Level = LogLevel.Information,
        SkipEnabledCheck = true,
        Message = $"{nameof(MiscInfo)}={{@Details}};Scope={{@Scope}}")]
    private static partial void MiscInfo(
        this ILogger logger,
        MiscInfoDetails details,
        BasicLogScope scope);
}