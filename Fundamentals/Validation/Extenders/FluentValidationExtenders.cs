// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation;

namespace SquidEyes.Fundamentals;

public static class FluentValidationExtenders
{
    private const string MUST_BE = "'{PropertyName}' must be ";

    private static readonly HashSet<char> punctuation =
        new(@"^[!#$%&()*+,\-./:;<=>?@[\\\]^_{|}~]$".ToCharArray());

    public static IRuleBuilderOptions<T, string?> IsCountryCode<T>(this IRuleBuilder<T, string?> rule) =>
        rule.Must(CountryValidator.IsCountryCode).WithMessage(MUST_BE + "a valid ISO country code.");

    public static IRuleBuilderOptions<T, string?> IsEmail<T>(this IRuleBuilder<T, string?> rule) =>
        rule.Must(Email.IsInput).WithMessage(MUST_BE + "a valid email address.");

    public static IRuleBuilderOptions<T, string?> IsPhone<T>(this IRuleBuilder<T, string?> rule) =>
        rule.Must(Phone.IsInput).WithMessage(MUST_BE + "a valid E164 phone number.");

    public static IRuleBuilderOptions<T, string?> IsNonNullTextLine<T>(
        this IRuleBuilder<T, string?> rule, int maxLength, Func<string, bool> isValid = null!)
    {
        var valid = isValid is null ? " " : "valid ";

        bool IsValid(string? value)
        {
            if (!value!.IsNonNullAndTrimmed())
                return false;

            if (value!.Length > maxLength)
                return false;

            if (!value.All(c => char.IsAsciiLetterOrDigit(c) || punctuation.Contains(c)))
                return false;

            if (isValid is not null && !isValid(value))
                return false;

            return true;
        }

        return rule.Must(IsValid)
            .WithMessage(MUST_BE + $"a {valid}non-empty text-line ({maxLength} characters).");
    }

    public static IRuleBuilderOptions<T, char?> IsNullOrInitial<T>(
        this IRuleBuilder<T, char?> rule)
    {
        return rule.Must(v => v is null || char.IsAsciiLetterUpper(v.Value))
            .WithMessage(MUST_BE + "null or an uppercase initial.");
    }

    public static IRuleBuilderOptions<T, string?> IsPersonName<T>(
        this IRuleBuilder<T, string?> rule, int maxLength = int.MaxValue)
    {
        return rule.Must(v => v!.IsPersonName(maxLength))
            .WithMessage(MUST_BE + $"a valid person name ({maxLength} letters, spaces and/or dashes, max).");
    }

    public static IRuleBuilderOptions<T, IEnumerable<TElement>> IsNonNullAndHasItems<T, TElement>(
        this IRuleBuilder<T, IEnumerable<TElement>> ruleBuilder)
    {
        return ruleBuilder.Must(v => v is not null && v.HasItems())
            .WithMessage(MUST_BE + "non-null a contain one or more non-default items.");
    }
}