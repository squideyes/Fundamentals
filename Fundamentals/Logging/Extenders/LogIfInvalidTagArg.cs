//// ********************************************************
//// The use of this source code is licensed under the terms
//// of the MIT License (https://opensource.org/licenses/MIT)
//// ********************************************************

using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    public static void LogIfInvalidTagArg(
        this ILogger logger,
        Tag activity,
        ITagArg tagArg,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        if (tagArg.IsValid)
            return;

        logger.InvalidTagArg(
            LogLevel.Warning,
            nameof(InvalidTagArg),
            calledBy,
            activity.Value!,
            (correlationId.IsDefault() ? Guid.NewGuid() : correlationId).ToString("N"),
            tagArg.Tag.Value!,
            tagArg.State!,
            tagArg.Message!);
    }

    [LoggerMessage(
        EventId = EventIds.InvalidTagArg,
        EventName = nameof(InvalidTagArg),
        Message = "EventKind={EventKind};Caller={CalledBy};Activity={Activity};CorrelationId={CorrelationId};Code={Tag};State={State};Message={Message}")]
    private static partial void InvalidTagArg(
        this ILogger logger,
        LogLevel logLevel,
        string eventKind,
        string calledBy,
        string activity,
        string correlationId,
        string tag,
        TagArgState state,
        string message);
}