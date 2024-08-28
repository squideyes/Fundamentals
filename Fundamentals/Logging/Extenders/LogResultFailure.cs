// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    private record ResultFailureDetails(
        string Activity,
        string Code,
        string Message,
        Dictionary<string, object>? Metadata);

    public static void LogResultFailure(
        this ILogger logger,
        MultiTag activity,
        Result result,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        if (!result.IsFailure)
            throw new InvalidOperationException();

        var context = new LogContext(calledBy, correlationId);

        foreach (var failure in result.Errors)
        {
            logger.ResultFailure(
                new ResultFailureDetails(activity.ToString(),
                    failure.Code!, failure.Message!, failure.Metadata),
                context);
        }
    }

    [LoggerMessage(
        EventId = EventIds.ResultFailure,
        EventName = nameof(ResultFailure),
        Level = LogLevel.Warning,
        SkipEnabledCheck = true,
        Message = LogConsts.StandardMessage)]
    private static partial void ResultFailure(
        this ILogger logger,
        ResultFailureDetails details,
        LogContext context);
}