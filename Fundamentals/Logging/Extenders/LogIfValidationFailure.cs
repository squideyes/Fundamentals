using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    public static void LogIfValidationFailure(
        this ILogger logger,
        Tag activity,
        ValidationResult result,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        if (result.IsValid)
            return;

        if (correlationId.IsDefault())
            correlationId = Guid.NewGuid();

        foreach (var failure in result.Errors)
        {
            logger.ValidationFailure(
                LogLevel.Warning,
                nameof(ValidationFailure),
                calledBy,
                activity.Value!,
                correlationId.ToString("N"),
                failure.PropertyName,
                failure.ErrorCode,
                failure.ErrorMessage);
        }
    }

    [LoggerMessage(
        EventId = EventIds.ValidationFailure,
        EventName = nameof(ValidationFailure),
        Message = "EventKind={EventKind};Caller={CalledBy};Activity={Activity};CorrelationId={CorrelationId};Property={PropertyName};ErrorCode={ErrorCode};ErrorMessage={ErrorMessage}")]
    private static partial void ValidationFailure(
        this ILogger logger,
        LogLevel logLevel,
        string eventKind,
        string calledBy,
        string activity,
        string correlationId,
        string propertyName,
        string errorCode,
        string errorMessage);
}