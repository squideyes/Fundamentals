// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace SquidEyes.Fundamentals;

public static partial class ILoggerExtenders
{
    private record ValidationFailureDetails(
        string Property,
        string Code,
        string Message);

    public static void LogIfValidationFailure(
        this ILogger logger,
        ValidationResult result,
        Guid correlationId = default,
        [CallerMemberName] string calledBy = "")
    {
        if (result.IsValid)
            return;

        var context = new LogScope(calledBy, correlationId);

        foreach (var failure in result.Errors)
        {
            logger.ValidationFailure(
                new ValidationFailureDetails(failure.PropertyName, 
                    failure.ErrorCode, failure.ErrorMessage),
                context);
        }
    }

    [LoggerMessage(
        EventId = EventIds.ValidationFailure,
        EventName = nameof(ValidationFailure),
        Level = LogLevel.Warning,
        SkipEnabledCheck = true,
        Message = LogConsts.StandardMessage)]
    private static partial void ValidationFailure(
        this ILogger logger,
        ValidationFailureDetails details,
        LogScope scope);
}