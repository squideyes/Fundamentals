// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    internal record InvalidTagArgDetails(
        string Tag, 
        TagArgState State, 
        string Message);

    public static void LogIfInvalidTagArg(
        this ILogger logger,
        ITagArg tagArg,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        if (tagArg.IsValid)
            return;

        logger.InvalidTagArg(
            new InvalidTagArgDetails(
                tagArg.Tag.Value!, tagArg.State!, tagArg.Message!),
            new BasicLogScope(calledBy, correlationId));
    }

    [LoggerMessage(
        EventId = EventIds.InvalidTagArg,
        EventName = nameof(InvalidTagArg),
        Level = LogLevel.Warning,
        SkipEnabledCheck = true,
        Message = $"{nameof(InvalidTagArg)}={{@Details}};Scope={{@Scope}}")]
    private static partial void InvalidTagArg(
        this ILogger logger,
        InvalidTagArgDetails details,
        BasicLogScope scope);
}