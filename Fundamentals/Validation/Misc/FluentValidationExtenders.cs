// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation;

namespace SquidEyes.Fundamentals;

public static class FluentValidationExtenders
{
    private const string MUST_BE = "'{PropertyName}' must be ";

    public static IRuleBuilderOptions<T, string?> IsTrimmed<T>(
        this IRuleBuilder<T, string?> rule, bool mayBeEmpty = false)
    {
        string message;

        if (mayBeEmpty)
            message = MUST_BE + "empty or non-empty and trimmed.";
        else
            message = MUST_BE + "non-empty and trimmed.";

        return rule.Must(v => v!.IsTrimmed(mayBeEmpty))
            .WithMessage(message);
    }

    public static IRuleBuilderOptions<T, string?> IsNullOrTrimmed<T>(
        this IRuleBuilder<T, string?> rule)
    {
        return rule.Must(v => v!.IsNullOrTrimmed())
            .WithMessage(MUST_BE + "null or non-empty and trimmed.");
    }

    public static IRuleBuilderOptions<T, string?> NonNullAndHasItems<T>(
        this IRuleBuilder<T, string?> rule)
    {
        return rule.Must(v => v is not null && v.HasItems())
            .WithMessage(MUST_BE + "non-null and have one or more non-default items.");
    }
}