// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation;

namespace LoggingDemo;

public class Person(string firstName, char? initial, string lastName)
{
    public class Validator : AbstractValidator<Person>
    {
        public Validator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.Initial)
                .Must(v => v is null || char.IsAsciiLetterUpper(v.Value))
                .WithMessage("'{PropertyName}' must be null or an uppercase ASCII letter.");

            RuleFor(x => x.LastName)
                .NotEmpty();
        }
    }

    public string? FirstName { get; } = firstName;
    public char? Initial { get; } = initial;
    public string? LastName { get; } = lastName;
}