// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation;

namespace SquidEyes.Fundamentals;

public class CreditCard
{
    public class Validator : AbstractValidator<CreditCard>
    {
        private const string MUST_BE = "'{PropertyName}' must be ";

        public Validator()
        {
            RuleFor(x => x.Number)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(CreditCardValidator.IsNumber)
                .WithMessage(MUST_BE + "a valid credit-card number.");

            RuleFor(x => x.ExpirationMonth)
                .InclusiveBetween(1, 12);

            RuleFor(x => x.ExpirationYear)
                .InclusiveBetween(2024, 2040);

            RuleFor(x => x.Cvc)
                .NotEmpty();

            RuleFor(x => x)
                .Must(v => CreditCardValidator.IsCVC(v.Cvc, v.GetBrand()))
                .WithMessage(MUST_BE + "a valid credit-card CVC.")
                .When(v => CreditCardValidator.IsNumber(v.Number));
        }
    }

    public required string Number { get; init; }
    public required int ExpirationMonth { get; init; }
    public required int ExpirationYear { get; init; }
    public required int Cvc { get; init; }

    public CreditCardBrand GetBrand()
    {
        if (!CreditCardHelper.TryGetBrand(Number, out var brand))
        {
            throw new InvalidOperationException(
                "The \"Number\" property must be set before calling GetBrand().");
        }

        return brand!.Value;
    }
}