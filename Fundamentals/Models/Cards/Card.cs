// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation;
using static SquidEyes.Fundamentals.MessageVerb;

namespace SquidEyes.Fundamentals;

public class Card
{
    public class Validator : AbstractValidator<Card>
    {
        public Validator()
        {
            RuleFor(x => x.Number)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(CardValidator.IsNumber)
                .WithMessage(MustBe, "a valid card number.");
            
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(v => v.IsNonNullAndTrimmed()) // Improve this!!!!!!!!!!!
                .WithMessage(MustBe, "a valid credit-card name.");

            RuleFor(x => x.ExpirationMonth)
                .InclusiveBetween(1, 12);

            RuleFor(x => x.ExpirationYear)
                .InclusiveBetween(2024, 2040);

            RuleFor(x => x.Cvc)
                .NotEmpty();

            RuleFor(x => x)
                .Must(v => CardValidator.IsCVC(v.Cvc, v.GetBrand()))
                .WithMessage(MustBe, "a valid card CVC.")
                .When(v => CardValidator.IsNumber(v.Number));
        }
    }

    public required string Number { get; init; }
    public required string Name { get; init; }
    public required int ExpirationMonth { get; init; }
    public required int ExpirationYear { get; init; }
    public required int Cvc { get; init; }

    public CardBrand GetBrand()
    {
        if (!CardHelper.TryGetBrand(Number, out var brand))
        {
            throw new InvalidOperationException(
                "The \"Number\" property must be set before calling GetBrand().");
        }

        return brand!.Value;
    }
}