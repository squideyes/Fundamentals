using FluentValidation;

namespace SquidEyes.Fundamentals;

public static class FluentValidationExtenders
{
    public static IRuleBuilderOptions<T, string?> IsTrimmed<T>(
        this IRuleBuilder<T, string?> rule, bool mayBeEmpty = false)
    {
        const string PREFIX = "'{PropertyName}' must be ";

        string message;

        if (mayBeEmpty)
            message = PREFIX + "NULL or non-empty and trimmed.";
        else
            message = PREFIX + "non-empty and trimmed.";

        return rule.Must(v => v!.IsTrimmed(mayBeEmpty))
            .WithMessage(message);
    }
}
