// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation;

namespace SquidEyes.Fundamentals;

public class StandardLoggerArgs
{
    public class Validator : AbstractValidator<StandardLoggerArgs>
    {
        public Validator()
        {
            RuleFor(x => x.SeqApiUri)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(v => v.IsAbsoluteUri)
                .WithMessage("'{PropertyName}' must be an absolute URI.");

            RuleFor(x => x.SeqApiKey)
                .IsTrimmed(true)
                .When(v => v is not null);

            RuleFor(x => x.MinSeverity)
                .IsInEnum();

            RuleFor(x => x.EnrichWith)
                .Must(v => !v!.IsEmpty)
                .WithMessage("'{PropertyName}' must be null or non-empty.")
                .When(v => v is not null);
        }
    }

    public required Uri SeqApiUri { get; init; }
    public string? SeqApiKey { get; init; }
    public Severity MinSeverity { get; init; } = Severity.Info;
    public TagValueSet? EnrichWith { get; init; } = null;
}