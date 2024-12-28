// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation;
using System.Text;
using static SquidEyes.Fundamentals.MessageVerb;

namespace SquidEyes.Fundamentals;

public static class FluentValidationExtenders
{
    private static readonly HashSet<char> punctuation =
        new(@"^[!#$%&()*+,\-./:;<=>?@[\\\]^_{|}~]$".ToCharArray());

    public static IRuleBuilderOptions<T, string?> IsCountryCode<T>(this IRuleBuilder<T, string?> rule) =>
        rule.Must(CountryValidator.IsCountryCode).WithMessage(MustBe, "a valid ISO country code.");

    public static IRuleBuilderOptions<T, string?> IsEmail<T>(this IRuleBuilder<T, string?> rule) =>
        rule.Must(Email.IsInput).WithMessage(MustBe, "a valid email address.");

    public static IRuleBuilderOptions<T, string?> IsPhone<T>(this IRuleBuilder<T, string?> rule) =>
        rule.Must(Phone.IsInput).WithMessage(MustBe, "a valid E164 phone number.");

    public static IRuleBuilderOptions<T, string?> IsNullOrTextLine<T>(
        this IRuleBuilder<T, string?> rule, int maxLength = int.MaxValue, Func<string, bool> isValid = null!)
    {
        return rule.Must(v => v is null || (IsTextLine(v, isValid!) && v.Length <= maxLength))
            .WithMessage(GetIsTextLineMessage(maxLength, isValid is not null));
    }

    public static IRuleBuilderOptions<T, string?> IsNonNullTextLine<T>(
        this IRuleBuilder<T, string?> rule, int maxLength, Func<string, bool> isValid = null!)
    {
        return rule.Must(v => v is not null && IsTextLine(v, isValid!))
            .WithMessage(GetIsTextLineMessage(maxLength, isValid is not null));
    }

    public static IRuleBuilderOptions<T, char?> IsNullOrInitial<T>(
        this IRuleBuilder<T, char?> rule)
    {
        return rule.Must(v => v is null || char.IsAsciiLetterUpper(v.Value))
            .WithMessage(MustBe, "null or an uppercase initial.");
    }

    public static IRuleBuilderOptions<T, string?> IsPersonName<T>(
        this IRuleBuilder<T, string?> rule, int maxLength = int.MaxValue)
    {
        return rule.Must(v => v!.IsPersonName(maxLength))
            .WithMessage(MustBe, $"a valid person name ({maxLength} letters, spaces and/or dashes, max).");
    }

    public static IRuleBuilderOptions<T, IEnumerable<TElement>> IsNonNullAndHasItems<T, TElement>(
        this IRuleBuilder<T, IEnumerable<TElement>> ruleBuilder)
    {
        return ruleBuilder.Must(v => v is not null && v.HasItems())
            .WithMessage(MustBe, "non-null a contain one or more non-default items.");
    }

    public static IRuleBuilderOptions<T, P> WithMessage<T, P>(
        this IRuleBuilderOptions<T, P> builder, MessageVerb verb, string suffix)
    {
        var prefix = verb switch
        {
            MayNot => "may not",
            MayNotBe => "may not be",
            Must => "must",
            MustBe => "must be",
            _ => throw new ArgumentOutOfRangeException(nameof(verb))
        };

        return builder.WithMessage($"'{{PropertyName}}' {prefix} {suffix}");
    }

    private static string GetIsTextLineMessage(int maxLength, bool hasIsValid)
    {
        var sb = new StringBuilder();

        sb.Append("'{PropertyName}' must be a ");

        if (hasIsValid)
            sb.Append(" valid");

        sb.Append(" non-empty text-line");

        if (maxLength != int.MaxValue)
        {
            sb.Append(" (");
            sb.Append(maxLength);
            sb.Append(" characters)");
        }

        sb.Append('.');

        return sb.ToString();
    }

    private static bool IsTextLine(string? value, Func<string?, bool> isValid)
    {
        if (!value!.IsTrimmed())
            return false;

        if (!value!.All(c => char.IsAsciiLetterOrDigit(c) || punctuation.Contains(c)))
            return false;

        if (isValid is not null && !isValid(value))
            return false;

        return true;
    }
}