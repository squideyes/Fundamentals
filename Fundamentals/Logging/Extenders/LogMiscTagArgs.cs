// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    private record MiscTagArgsDetails(MultiTag MultiTag, Dictionary<string, object> TagArgs);

    public static void LogMiscTagArgs(
        this ILogger logger,
        MultiTag multiTag,
        TagArgSet tagArgs,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        logger.MiscTagArgs(
            new MiscTagArgsDetails(multiTag.ToString(), tagArgs.ToSimplifiedDictionary()),
            new LogContext(calledBy, correlationId));
    }

    [LoggerMessage(
        EventId = EventIds.MiscTagArgs,
        EventName = nameof(MiscTagArgs),
        Level = LogLevel.Information,
        SkipEnabledCheck = true,
        Message = LogConsts.StandardMessage)]
    private static partial void MiscTagArgs(
        this ILogger logger,
        MiscTagArgsDetails details,
        LogContext context);
}