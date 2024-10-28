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

        var scope = new BasicLogScope(calledBy, correlationId);

        foreach (var failure in result.Errors)
        {
            logger.ValidationFailure(
                new ValidationFailureDetails(failure.PropertyName, 
                    failure.ErrorCode, failure.ErrorMessage),
                scope);
        }
    }

    [LoggerMessage(
        EventId = EventIds.ValidationFailure,
        EventName = nameof(ValidationFailure),
        Level = LogLevel.Warning,
        SkipEnabledCheck = true,
        Message = $"{nameof(ValidationFailure)}={{@Details}};Scope={{@Scope}}")]
    private static partial void ValidationFailure(
        this ILogger logger,
        ValidationFailureDetails details,
        BasicLogScope scope);
}