// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    private record ExceptionCaughtDetails(
        string ErrorType,
        string Target,
        string Message,
        string StackTrace);

    public static void LogExceptionCaught(
        this ILogger logger,
        Exception exception,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        var context = new LogScope(calledBy, correlationId);

        logger.ExceptionCaughtWithLevel(context, 0, exception);
    }

    private static void ExceptionCaughtWithLevel(
        this ILogger logger,
        LogScope context,
        int level,
        Exception exception)
    {
        var details = new ExceptionCaughtDetails(
            exception.GetType().FullName!,
            exception.TargetSite!.ToString()!,
            exception.Message,
            string.Join(";", exception.GetStackTraceLines(false)));

        logger.ExceptionCaught(details, context);

        if (exception.InnerException is not null)
        {
            logger.ExceptionCaughtWithLevel(
                context, ++level, exception.InnerException);
        }
    }

    [LoggerMessage(
        EventId = EventIds.ExceptionCaught,
        EventName = nameof(ExceptionCaught),
        Level = LogLevel.Error,
        SkipEnabledCheck = true,
        Message = LogConsts.StandardMessage)]
    private static partial void ExceptionCaught(
        this ILogger logger,
        ExceptionCaughtDetails details,
        LogScope scope);
}