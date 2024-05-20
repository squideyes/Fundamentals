// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation;
using System.Text;
using static SquidEyes.Fundamentals.PostalCodeValidator;

namespace SquidEyes.Fundamentals;

public class Address
{
    public class Validator : AbstractValidator<Address>
    {
        public static class MaxLengths
        {
            public const int Country = 2;
            public const int Address = 50;
            public const int Locality = 25;
            public const int Region = 25;
        }

        public Validator()
        {
            RuleFor(x => x.Country)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(MaxLengths.Country)
                .Must(CountryValidator.IsCountryCode)
                .WithMessage("'{PropertyName}' must be an ISO 3166 country code.");

            RuleFor(x => x.Address1)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(MaxLengths.Address)
                .Must(v => v!.IsNonNullAndTrimmed())
                .WithMessage("'{PropertyName}' must be non-empty and trimmed.");

            RuleFor(x => x.Address2)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(MaxLengths.Address)
                .Must(v => v!.IsNonNullAndTrimmed())
                .WithMessage("'{PropertyName}' must be null or non-empty and trimmed.")
                .When(v => v.Address2 is not null, ApplyConditionTo.AllValidators);

            RuleFor(x => x.Locality)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(MaxLengths.Locality)
                .Must(v => v!.IsNonNullAndTrimmed())
                .WithMessage("'{PropertyName}' must be non-empty and trimmed.");

            RuleFor(x => x.Region)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MaximumLength(MaxLengths.Region)
                .Must(v => v!.IsNonNullAndTrimmed())
                .WithMessage("'{PropertyName}' must be non-empty and trimmed.");

            RuleFor(x => x)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(v => IsPostalCode(v.PostalCode, v.Country))
                .WithMessage(v => $"'{{PropertyName}}' must be valid for {v.Country}.");
        }
    }

    public required string Country { get; init; }
    public required string Address1 { get; init; }
    public string? Address2 { get; init; }
    public required string Locality { get; init; }
    public required string Region { get; init; }
    public required string PostalCode { get; init; }

    public string GetOneLineAddress()
    {
        var sb = new StringBuilder();

        sb.Append(Address1);

        if (!string.IsNullOrEmpty(Address2))
            sb.AppendDelimited(Address2, ", ");

        sb.AppendDelimited(Locality!, ", ");
        sb.AppendDelimited(Region!, ", ");
        sb.AppendDelimited(PostalCode!, ", ");
        sb.AppendDelimited(Country!, ", ");

        return sb.ToString();
    }
}